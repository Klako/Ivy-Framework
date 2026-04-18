using Ivy.Core;
using Microsoft.AspNetCore.Http;

// ReSharper disable once CheckNamespace
namespace Ivy;

public interface IConnectedAccountsService
{
    string[] GetAvailableProviders();

    Task<Uri> ConnectAccountAsync(string provider, WebhookEndpoint callback, CancellationToken cancellationToken = default);

    Task<AuthToken?> HandleConnectCallbackAsync(string provider, HttpRequest request, CancellationToken cancellationToken = default);

    Task DisconnectAccountAsync(string provider, CancellationToken cancellationToken = default);

    IAuthSession? GetAccountSession(string provider);

    Task RefreshAllAsync(CancellationToken cancellationToken = default);

    event Action<string>? AccountConnected;
    event Action<string>? AccountDisconnected;
}
