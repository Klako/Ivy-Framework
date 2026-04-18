using Ivy.Core;
using Ivy.Core.Auth;
using Ivy.Core.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

// ReSharper disable once CheckNamespace
namespace Ivy;

public class ConnectedAccountsService : IConnectedAccountsService
{
    private readonly IAuthSession _authSession;
    private readonly IServiceProvider _serviceProvider;
    private readonly IClientProvider _client;
    private readonly AppSessionStore _sessionStore;
    private readonly ILogger<ConnectedAccountsService> _logger;

    public ConnectedAccountsService(
        IAuthSession authSession,
        IServiceProvider serviceProvider,
        IClientProvider client,
        AppSessionStore sessionStore,
        ILogger<ConnectedAccountsService>? logger = null)
    {
        _authSession = authSession;
        _serviceProvider = serviceProvider;
        _client = client;
        _sessionStore = sessionStore;
        _logger = logger ?? NullLogger<ConnectedAccountsService>.Instance;
    }

    public string[] GetAvailableProviders()
    {
        return _authSession.ConnectedAccounts.Keys.ToArray();
    }

    public async Task<Uri> ConnectAccountAsync(string provider, WebhookEndpoint callback, CancellationToken cancellationToken = default)
    {
        var authProvider = _serviceProvider.GetKeyedService<IAuthProvider>(provider);
        if (authProvider == null)
        {
            throw new InvalidOperationException($"Connected account provider '{provider}' is not registered.");
        }

        var connectedSession = _authSession.ConnectedAccounts.TryGetValue(provider, out var existing)
            ? existing
            : new AuthSession(authToken: null);

        var authOptions = authProvider.GetAuthOptions();
        var option = authOptions.FirstOrDefault() ?? new AuthOption(AuthFlow.OAuth, Name: provider);

        var uri = await authProvider.GetOAuthUriAsync(connectedSession, option, callback, cancellationToken);

        if (connectedSession.AuthSessionData != null)
        {
            _authSession.AddConnectedAccount(provider, connectedSession);
            SetConnectedAccountCookies();
        }

        return uri;
    }

    public async Task<AuthToken?> HandleConnectCallbackAsync(string provider, HttpRequest request, CancellationToken cancellationToken = default)
    {
        var authProvider = _serviceProvider.GetKeyedService<IAuthProvider>(provider);
        if (authProvider == null)
        {
            throw new InvalidOperationException($"Connected account provider '{provider}' is not registered.");
        }

        var connectedSession = _authSession.ConnectedAccounts.TryGetValue(provider, out var existing)
            ? existing
            : new AuthSession(authToken: null);

        var token = await authProvider.HandleOAuthCallbackAsync(connectedSession, request, cancellationToken);
        connectedSession.AuthToken = token;

        if (token != null)
        {
            _authSession.AddConnectedAccount(provider, connectedSession);
            SetConnectedAccountCookies(triggerMachineAuthSync: true);
        }

        return token;
    }

    public async Task DisconnectAccountAsync(string provider, CancellationToken cancellationToken = default)
    {
        if (_authSession.ConnectedAccounts.TryGetValue(provider, out var session))
        {
            var authProvider = _serviceProvider.GetKeyedService<IAuthProvider>(provider);
            if (authProvider != null)
            {
                try
                {
                    await authProvider.LogoutAsync(session, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error logging out from connected account provider '{Provider}'", provider);
                }
            }

            _authSession.RemoveConnectedAccount(provider);
            SetConnectedAccountCookies(connectedProvidersToDelete: [provider], triggerMachineAuthSync: true);
        }
    }

    public IAuthSession? GetAccountSession(string provider)
    {
        return _authSession.ConnectedAccounts.TryGetValue(provider, out var session) ? session : null;
    }

    public async Task RefreshAllAsync(CancellationToken cancellationToken = default)
    {
        var providers = _authSession.ConnectedAccounts.Keys.ToList();
        foreach (var provider in providers)
        {
            var authProvider = _serviceProvider.GetKeyedService<IAuthProvider>(provider);
            if (authProvider == null)
            {
                continue;
            }

            var session = _authSession.ConnectedAccounts[provider];
            try
            {
                var newToken = await authProvider.RefreshAccessTokenAsync(session, cancellationToken);
                if (newToken != null)
                {
                    session.AuthToken = newToken;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error refreshing connected account '{Provider}'", provider);
            }
        }

        SetConnectedAccountCookies();
    }

    private void SetConnectedAccountCookies(IEnumerable<string>? connectedProvidersToDelete = null, bool triggerMachineAuthSync = false)
    {
        var cookieJarId = _sessionStore.RegisterAuthSessionCookies(_authSession, connectedProvidersToDelete: connectedProvidersToDelete);
        _client.SetAuthCookies(cookieJarId, reloadPage: false, triggerMachineReload: null, triggerMachineAuthSync: triggerMachineAuthSync);
    }
}
