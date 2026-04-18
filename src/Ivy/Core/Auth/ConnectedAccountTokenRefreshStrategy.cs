using Ivy.Core.Server;
using Microsoft.Extensions.Logging;

namespace Ivy.Core.Auth;

public class ConnectedAccountTokenRefreshStrategy : RefreshStrategy
{
    private readonly string _connectionId;
    private readonly string _provider;
    private readonly IAuthSession _parentSession;
    private readonly IConnectedAccountsService _connectedAccountsService;
    private readonly AppSessionStore _sessionStore;
    private readonly IClientProvider _client;
    private readonly ILogger _logger;

    private int _consecutiveRefreshFailures;
    private const int MaxConsecutiveRefreshFailures = 3;

    public override string LoggingName { get; }

    public ConnectedAccountTokenRefreshStrategy(
        string connectionId,
        string provider,
        IAuthTokenHandlerService authService,
        IAuthSession parentSession,
        IClientProvider client,
        IConnectedAccountsService connectedAccountsService,
        AppSessionStore sessionStore,
        ILogger logger) : base(authService)
    {
        _connectionId = connectionId;
        _provider = provider;
        _parentSession = parentSession;
        _client = client;
        _connectedAccountsService = connectedAccountsService;
        _sessionStore = sessionStore;
        _logger = logger;
        LoggingName = $"ConnectedAccount[{provider}]";
    }

    public override async Task<bool> ValidateTokenAsync(CancellationToken cancellationToken = default)
    {
        var isValid = await base.ValidateTokenAsync(cancellationToken);
        if (isValid)
        {
            _consecutiveRefreshFailures = 0;
        }
        return isValid;
    }

    public override async Task<bool> RefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Attempting to refresh connected account token for {Provider}", _provider);
        var result = await _authService.RefreshAccessTokenAsync(cancellationToken);

        if (result != null)
        {
            _logger.LogInformation("Successfully refreshed connected account token for {Provider}", _provider);

            // Update parent session cookies to include the refreshed token
            var cookieJarId = _sessionStore.RegisterAuthSessionCookies(_parentSession);
            _client.SetAuthCookies(cookieJarId, reloadPage: false, triggerMachineReload: null);

            return true;
        }

        return false;
    }

    public override async Task<bool> OnRefreshFailedAsync()
    {
        _consecutiveRefreshFailures++;
        _logger.LogWarning("ConnectedAccountRefreshLoop[{Provider}]: Refresh failed for {ConnectionId}, attempt {Attempt}/{MaxAttempts}",
            _provider, _connectionId, _consecutiveRefreshFailures, MaxConsecutiveRefreshFailures);

        if (_consecutiveRefreshFailures >= MaxConsecutiveRefreshFailures)
        {
            _logger.LogError("ConnectedAccountRefreshLoop[{Provider}]: {MaxAttempts} consecutive refresh failures for {ConnectionId}, disconnecting account",
                _provider, MaxConsecutiveRefreshFailures, _connectionId);

            try
            {
                await _connectedAccountsService.DisconnectAccountAsync(_provider);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ConnectedAccountRefreshLoop[{Provider}]: Error disconnecting account for {ConnectionId}", _provider, _connectionId);
            }

            return false;
        }

        await Task.Delay(TimeSpan.FromSeconds(5));
        return true;
    }

    public override Task<bool> OnTokenLostAsync()
    {
        _logger.LogInformation("ConnectedAccountRefreshLoop[{Provider}]: Token lost for {ConnectionId}, exiting loop", _provider, _connectionId);
        return Task.FromResult(false);
    }
}
