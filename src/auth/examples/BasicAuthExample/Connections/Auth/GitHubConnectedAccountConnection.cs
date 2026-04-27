using Ivy;
using Ivy.Auth.GitHub;
using Microsoft.Extensions.Configuration;

namespace BasicAuthExample.Connections.Auth;

public class GitHubConnectedAccountConnection : IConnection, IHaveSecrets
{
    public string GetContext(string connectionPath) => string.Empty;

    public string GetName() => "GitHubConnectedAccount";

    public string GetNamespace() => typeof(GitHubConnectedAccountConnection).Namespace ?? "";

    public string GetConnectionType() => "Auth";

    public ConnectionEntity[] GetEntities() => [];

    public void RegisterServices(Server server)
    {
        server.RegisterConnectedAccountProvider<GitHubAuthProvider>(OAuthProviders.GitHub);
    }

    public Secret[] GetSecrets() =>
    [
        new("GitHub:ClientId"),
        new("GitHub:ClientSecret"),
        new("GitHub:RedirectUri")
    ];

    public async Task<(bool ok, string? message)> TestConnection(IConfiguration config)
    {
        await Task.CompletedTask;
        return (true, "GitHub connected account configured");
    }
}
