using Ivy.Core.Apps;
using Ivy.Core.Helpers;
using Ivy.Core.HttpTunneling;
using Ivy.Core.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ivy.Core.Auth;

public record SetAuthCookiesRequest(string CookieJarId, string? ConnectionId, bool TriggerMachineReload);

public class AuthController() : Controller
{
    internal static string SanitizeForLog(string? input) => input?.Replace("\n", "").Replace("\r", "") ?? string.Empty;

    private static bool IsValidRedirectUri(Uri uri, ILogger logger)
    {
        // Allow relative URLs
        if (!uri.IsAbsoluteUri)
        {
            return true;
        }

        // For absolute URLs: must use HTTPS
        if (uri.Scheme != Uri.UriSchemeHttps)
        {
            logger.LogWarning("OAuth redirect rejected: non-HTTPS URL {Url}", SanitizeForLog(uri.ToString()));
            return false;
        }

        return true;
    }

    [Route("ivy/auth/oauth-login")]
    [HttpGet]
    public async Task<IActionResult> OAuthLogin(
        [FromQuery] string loginId,
        [FromServices] IOAuthLoginRegistry loginRegistry,
        [FromServices] IOAuthCallbackRegistry callbackRegistry,
        [FromServices] AppSessionStore sessionStore,
        [FromServices] ServerArgs serverArgs,
        [FromServices] ILogger<AuthController> logger)
    {
        if (string.IsNullOrWhiteSpace(loginId))
        {
            logger.LogWarning("OAuth login failed: Missing loginId");
            return BadRequest("Authentication error");
        }

        var pending = loginRegistry.GetAndRemove(loginId);
        if (pending == null)
        {
            logger.LogWarning("OAuth login failed: Invalid or expired loginId");
            return BadRequest("Invalid or expired login request. Please try again.");
        }

        var connectionId = pending.ConnectionId;
        var optionId = pending.OptionId;
        var provider = pending.Provider;

        if (!sessionStore.Sessions.TryGetValue(connectionId, out var appSession))
        {
            logger.LogWarning("OAuth login failed: Session not found for connection {ConnectionId}", SanitizeForLog(connectionId));
            return BadRequest("Authentication error");
        }

        // Register callback server-side
        var callbackId = callbackRegistry.RegisterPending(connectionId, optionId, provider);

        // Construct the OAuth callback endpoint
        var scheme = HttpContext.Request.Scheme;
        if (HttpContext.Request.Headers.TryGetValue("X-Forwarded-Proto", out var forwardedProto))
        {
            scheme = forwardedProto.ToString();
        }
        var host = HttpContext.Request.Host.Value ?? throw new InvalidOperationException("Host not found in request");
        var callback = WebhookEndpoint.CreateAuthCallback(callbackId, scheme, host, serverArgs.BasePath);

        if (string.IsNullOrWhiteSpace(provider))
        {
            // Main auth flow
            var authService = appSession.AppServices.GetService<IAuthService>();
            if (authService == null)
            {
                logger.LogWarning("OAuth login failed: Auth service not configured for connection {ConnectionId}", SanitizeForLog(connectionId));
                return BadRequest("Authentication error");
            }

            // Find the auth option by ID
            var options = authService.GetAuthOptions();
            var option = options.FirstOrDefault(o => o.Id == optionId);
            if (option == null)
            {
                logger.LogWarning("OAuth login failed: Auth option '{OptionId}' not found for connection {ConnectionId}", SanitizeForLog(optionId), SanitizeForLog(connectionId));
                return BadRequest("Authentication error");
            }

            try
            {
                var uri = await authService.GetOAuthUriAsync(option, callback, HttpContext.RequestAborted);

                if (!IsValidRedirectUri(uri, logger))
                {
                    return BadRequest("Invalid OAuth redirect URL");
                }

                return Redirect(uri.ToString());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "OAuth login failed: Error initiating OAuth for option '{OptionId}' on connection {ConnectionId}", SanitizeForLog(optionId), SanitizeForLog(connectionId));
                return BadRequest("Authentication error");
            }
        }
        else
        {
            // Connected account flow
            var connectedAccountsService = appSession.AppServices.GetService<IConnectedAccountsService>();
            if (connectedAccountsService == null)
            {
                logger.LogWarning("OAuth login failed: Connected accounts service not configured");
                return BadRequest("Connected accounts not configured");
            }

            try
            {
                var uri = await connectedAccountsService.ConnectAccountAsync(provider, callback, HttpContext.RequestAborted);

                if (!IsValidRedirectUri(uri, logger))
                {
                    return BadRequest("Invalid OAuth redirect URL");
                }

                return Redirect(uri.ToString());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "OAuth login failed: Error initiating connected account OAuth for provider '{Provider}' on connection {ConnectionId}", SanitizeForLog(provider), SanitizeForLog(connectionId));
                return BadRequest("Authentication error");
            }
        }
    }

    [Route("ivy/auth/callback")]
    [Route("ivy/auth/callback/{callbackId}")]
    [HttpGet]
    public async Task<IActionResult> OAuthCallback(
        string? callbackId,
        [FromQuery] string? code,
        [FromQuery] string? state,
        [FromQuery] string? error,
        [FromQuery(Name = "error_description")] string? errorDescription,
        [FromServices] IOAuthCallbackRegistry registry,
        [FromServices] IAuthProvider authProvider,
        [FromServices] AppSessionStore sessionStore,
        [FromServices] ServerArgs serverArgs,
        [FromServices] ILogger<AuthController> logger)
    {
        var effectiveId = callbackId ?? state;

        if (!string.IsNullOrEmpty(error))
        {
            logger.LogWarning("OAuth callback error: {Error} - {Description}", SanitizeForLog(error), SanitizeForLog(errorDescription));
            return BadRequest($"OAuth error: {error}");
        }

        if (string.IsNullOrEmpty(effectiveId))
        {
            logger.LogWarning("OAuth callback failed: Missing callback identifier (neither callbackId path nor state query)");
            return BadRequest("Invalid OAuth callback: missing callback identifier");
        }

        var pending = registry.GetAndRemove(effectiveId);
        if (pending == null)
        {
            logger.LogWarning("OAuth callback failed: Invalid or expired callback id '{CallbackId}'", SanitizeForLog(effectiveId));
            return BadRequest("Invalid or expired OAuth state. Please try logging in again.");
        }

        var path = (serverArgs.BasePath ?? "").Trim().Replace('\\', '/').TrimStart('/').TrimEnd('/');
        var isConnectedAccount = !string.IsNullOrWhiteSpace(pending.Provider);
        var queryParam = isConnectedAccount ? "connectedAccountLogin=1" : "oauthLogin=1";
        var redirectUrl = string.IsNullOrEmpty(path) ? $"/?{queryParam}" : $"/{path}/?{queryParam}";

        if (string.IsNullOrWhiteSpace(pending.Provider))
        {
            // Main auth flow
            try
            {
                TunneledHttpMessageHandler? httpMessageHandler;
                // Get the session and its HttpMessageHandler using the connectionId from the pending callback
                if (sessionStore.Sessions.TryGetValue(pending.ConnectionId, out var appSession))
                {
                    httpMessageHandler = appSession.AppServices.GetService<TunneledHttpMessageHandler>();
                }
                else
                {
                    logger.LogDebug("OAuth callback: session not found for connection {ConnectionId} (expected during redirect flow). Unable to retrieve frontend-tunneled HttpMessageHandler; Clerk auth provider may be affected.", SanitizeForLog(pending.ConnectionId));
                    httpMessageHandler = null;
                }

                var tempSession = AuthHelper.GetAuthSession(HttpContext, httpMessageHandler);

                var token = await authProvider.HandleOAuthCallbackAsync(tempSession, HttpContext.Request);

                if (token == null)
                {
                    logger.LogWarning("OAuth callback failed: No token returned");
                    return BadRequest("Authentication failed: no token received");
                }

                logger.LogInformation("OAuth callback successful, setting auth cookies");

                var cookies = new CookieJar();
                cookies.AddCookiesForAuthToken(token);
                cookies.AddCookiesForAuthSessionData(tempSession.AuthSessionData);
                cookies.AddCookiesForBrokeredSessions(tempSession.BrokeredSessions);
                cookies.WriteToResponse(Response);

                return LocalRedirect(redirectUrl);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "OAuth callback failed: Error processing callback");
                return BadRequest($"Authentication error: {ex.Message}");
            }
        }
        else
        {
            // Connected account flow
            if (!sessionStore.Sessions.TryGetValue(pending.ConnectionId, out var appSession))
            {
                logger.LogWarning("OAuth callback failed: Session not found for connection {ConnectionId}",
                    SanitizeForLog(pending.ConnectionId));
                return BadRequest("Session not found");
            }

            var connectedAccountsService = appSession.AppServices.GetService<IConnectedAccountsService>();
            if (connectedAccountsService == null)
            {
                logger.LogWarning("OAuth callback failed: Connected accounts service not configured");
                return BadRequest("Connected accounts not configured");
            }

            try
            {
                var token = await connectedAccountsService.HandleConnectCallbackAsync(
                    pending.Provider,
                    HttpContext.Request,
                    HttpContext.RequestAborted);

                if (token == null)
                {
                    logger.LogWarning("OAuth callback failed: No token returned for provider {Provider}",
                        SanitizeForLog(pending.Provider));
                    return BadRequest("Authentication failed: no token received");
                }

                logger.LogInformation("Connected account callback successful for provider {Provider}",
                    SanitizeForLog(pending.Provider));

                return LocalRedirect(redirectUrl);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "OAuth callback failed: Error processing connected account callback for provider {Provider}", SanitizeForLog(pending.Provider));
                return BadRequest($"Authentication error: {ex.Message}");
            }
        }
    }

    [Route("ivy/auth/set-auth-cookies")]
    [HttpPatch]
    public async Task<IActionResult> SetAuthCookies(
        [FromBody] SetAuthCookiesRequest request,
        [FromServices] AppSessionStore sessionStore,
        [FromServices] IContentBuilder contentBuilder,
        [FromServices] ILogger<AuthController> logger)
    {
        if (this.WriteCookiesToResponse(
            sessionStore,
            new CookieJarId(request.CookieJarId),
            CookieJarIntents.SetAuthCookies,
            out var cookies,
            out var triggerMachineAuthSync) is { } errorResponse)
        {
            return errorResponse;
        }

        if (request.TriggerMachineReload)
        {
            if (cookies.TryGet(CookieRegistryExtensions.PrefixCookieName("access_token"), out var authTokenValue) && !string.IsNullOrEmpty(authTokenValue))
            {
                // Trigger reload for all sessions with the same machineId on login
                if (HttpContext.Request.Headers.TryGetValue("X-Machine-Id", out var loginHeaderValue))
                {
                    var machineId = loginHeaderValue.ToString();
                    TriggerMachineReload(sessionStore, SanitizeForLog(machineId), SanitizeForLog(request.ConnectionId));
                }
            }
            else
            {
                // Trigger logout for all sessions with the same machineId on logout
                if (HttpContext.Request.Headers.TryGetValue("X-Machine-Id", out var headerValue))
                {
                    var machineId = headerValue.ToString();
                    await TriggerMachineLogout(sessionStore, SanitizeForLog(machineId), SanitizeForLog(request.ConnectionId), contentBuilder, logger);
                }
            }
        }

        if (triggerMachineAuthSync)
        {
            if (HttpContext.Request.Headers.TryGetValue("X-Machine-Id", out var headerValue))
            {
                var machineId = headerValue.ToString();
                TriggerMachineAuthSync(sessionStore, SanitizeForLog(machineId), SanitizeForLog(request.ConnectionId), logger);
            }
        }

        return Ok();
    }

    internal static string FindRootAncestor(AppSessionStore sessionStore, string connectionId)
    {
        var current = connectionId;
        while (sessionStore.Sessions.TryGetValue(current, out var session) && session.ParentId != null)
        {
            current = session.ParentId;
        }
        return current;
    }

    internal static IEnumerable<AppSession> GetMachineSessions(
        AppSessionStore sessionStore,
        string machineId,
        string? excludeConnectionId)
    {
        var processedRoots = new HashSet<string>();
        if (!string.IsNullOrEmpty(excludeConnectionId))
        {
            var excludedRoot = FindRootAncestor(sessionStore, excludeConnectionId);
            processedRoots.Add(excludedRoot);
        }

        // Find all sessions with this machineId
        var allSessions = sessionStore.Sessions.Values
            .Where(s => !s.IsDisposed() && s.MachineId == machineId)
            .ToList();

        foreach (var session in allSessions)
        {
            // Find root for this session
            var sessionRoot = FindRootAncestor(sessionStore, session.ConnectionId);

            // Skip if we've already processed this root (includes the excluded root)
            if (!processedRoots.Add(sessionRoot))
            {
                continue;
            }

            yield return session;
        }
    }

    internal static void TriggerMachineReload(
        AppSessionStore sessionStore,
        string machineId,
        string? excludeConnectionId)
    {
        foreach (var session in GetMachineSessions(sessionStore, machineId, excludeConnectionId))
        {
            // Just trigger page reload to pick up new auth cookies
            var clientProvider = session.AppServices.GetRequiredService<IClientProvider>();
            clientProvider.ReloadPage();
        }
    }

    private static async Task TriggerMachineLogout(
        AppSessionStore sessionStore,
        string machineId,
        string? excludeConnectionId,
        IContentBuilder contentBuilder,
        ILogger logger)
    {
        foreach (var session in GetMachineSessions(sessionStore, machineId, excludeConnectionId))
        {
            await SessionHelpers.AbandonSessionAsync(session, contentBuilder, resetTokenAndReload: true, triggerMachineReload: false, logger, "TriggerMachineLogout");
        }
    }

    private static void TriggerMachineAuthSync(
        AppSessionStore sessionStore,
        string machineId,
        string? excludeConnectionId,
        ILogger logger)
    {
        // Notify ALL sessions with same machineId except the originator
        var sessions = sessionStore.Sessions.Values
            .Where(s => !s.IsDisposed()
                        && s.MachineId == machineId
                        && s.ConnectionId != excludeConnectionId);

        foreach (var session in sessions)
        {
            logger.LogInformation("Triggering auth refresh from cookies for session {ConnectionId}", SanitizeForLog(session.ConnectionId));
            var clientProvider = session.AppServices.GetRequiredService<IClientProvider>();
            clientProvider.SyncAuthFromCookies();
        }
    }

    [Route("ivy/auth/sync-session")]
    [HttpPost]
    public IActionResult SyncSessionFromCookies(
        [FromHeader(Name = "X-Connection-Id")] string connectionId,
        [FromHeader(Name = "X-Machine-Id")] string machineId,
        [FromServices] AppSessionStore sessionStore,
        [FromServices] ILogger<AuthController> logger)
    {
        // Validate session exists
        if (!sessionStore.Sessions.TryGetValue(connectionId, out var session))
        {
            logger.LogWarning("SyncSessionFromCookies: Session not found for {ConnectionId}", SanitizeForLog(connectionId));
            return NotFound("Session not found");
        }

        // Verify machine ID matches as a security check
        if (session.MachineId != machineId)
        {
            logger.LogWarning("SyncSessionFromCookies: Machine ID mismatch for {ConnectionId}. Expected {Expected}, got {Actual}",
                SanitizeForLog(connectionId), SanitizeForLog(session.MachineId), SanitizeForLog(machineId));
            return BadRequest("Machine ID mismatch");
        }

        // Get existing AuthSession
        var authService = session.AppServices.GetService<IAuthService>();
        if (authService == null)
        {
            logger.LogWarning("SyncSessionFromCookies: Auth not configured for {ConnectionId}", SanitizeForLog(connectionId));
            return BadRequest("Auth not configured for this session");
        }

        // Parse cookies into fresh auth state
        var httpMessageHandler = session.AppServices.GetService<TunneledHttpMessageHandler>();
        var freshAuthState = AuthHelper.GetAuthSession(HttpContext, httpMessageHandler);
        var existingSession = authService.GetAuthSession();

        // Update main auth token in place
        existingSession.AuthToken = freshAuthState.AuthToken;
        existingSession.AuthSessionData = freshAuthState.AuthSessionData;

        // Sync brokered sessions (fires Add/Remove events → starts/stops refresh loops)
        SyncBrokeredSessions(existingSession, freshAuthState.BrokeredSessions, logger);

        // Sync connected accounts (fires AccountConnected/AccountDisconnected events → UI rebuilds)
        SyncConnectedAccounts(existingSession, freshAuthState.ConnectedAccounts, logger);

        logger.LogInformation("Refreshed auth session from cookies for {ConnectionId}", SanitizeForLog(connectionId));
        return Ok();
    }

    private static void SyncConnectedAccounts(
        IAuthSession existing,
        IReadOnlyDictionary<string, IAuthSession> newAccounts,
        ILogger logger)
    {
        var existingProviders = existing.ConnectedAccounts.Keys.ToHashSet();
        var newProviders = newAccounts.Keys.ToHashSet();

        // Remove providers no longer present
        foreach (var provider in existingProviders.Except(newProviders))
        {
            logger.LogInformation("SyncConnectedAccounts: Removing provider {Provider}", SanitizeForLog(provider));
            existing.RemoveConnectedAccount(provider);
        }

        // Add or update providers
        foreach (var (provider, newAccount) in newAccounts)
        {
            if (existing.ConnectedAccounts.TryGetValue(provider, out var existingAccount))
            {
                // Update existing in place
                existingAccount.AuthToken = newAccount.AuthToken;
                existingAccount.AuthSessionData = newAccount.AuthSessionData;
            }
            else
            {
                logger.LogInformation("SyncConnectedAccounts: Adding provider {Provider}", SanitizeForLog(provider));
                existing.AddConnectedAccount(provider, newAccount);
            }
        }
    }

    private static void SyncBrokeredSessions(
        IAuthSession existing,
        IReadOnlyDictionary<string, IAuthTokenHandlerSession> newSessions,
        ILogger logger)
    {
        var existingProviders = existing.BrokeredSessions.Keys.ToHashSet();
        var newProviders = newSessions.Keys.ToHashSet();

        // Remove providers no longer present
        foreach (var provider in existingProviders.Except(newProviders))
        {
            logger.LogInformation("SyncBrokeredSessions: Removing provider {Provider}", SanitizeForLog(provider));
            existing.RemoveBrokeredSession(provider);
        }

        // Add or update providers
        foreach (var (provider, newSession) in newSessions)
        {
            if (existing.BrokeredSessions.TryGetValue(provider, out var existingBrokered))
            {
                // Update existing in place
                existingBrokered.AuthToken = newSession.AuthToken;
                existingBrokered.AuthSessionData = newSession.AuthSessionData;
            }
            else
            {
                // Add new provider → fires BrokeredSessionAdded → starts refresh loop
                logger.LogInformation("SyncBrokeredSessions: Adding provider {Provider}", SanitizeForLog(provider));
                existing.AddBrokeredSession(provider, newSession);
            }
        }
    }
}
