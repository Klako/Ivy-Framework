using Ivy;
using Ivy.Auth.Examples.Shared;

namespace BasicAuthExample;

[App(id: "auth-test", title: "Auth Test")]
public class MainApp : ViewBase
{
    public override object? Build()
    {
        var auth = UseService<IAuthService>();
        var connectedAccounts = UseService<IConnectedAccountsService>();
        var userInfo = UseState<UserInfo?>();

        UseEffect(async () =>
        {
            var info = await auth.GetUserInfoAsync();
            userInfo.Set(info);
        });

        if (userInfo.Value is null)
        {
            return Text.P("Loading user data...");
        }

        var user = userInfo.Value;

        return Layout.Vertical(
            // Success Header
            Text.H2("Authentication Successful!").Color(Colors.Success),

            // Profile info
            Layout.Horizontal(
                 new Image(user.AvatarUrl ?? "").Size(Size.Units(64)),
                 Layout.Vertical(
                     Text.H3(user.FullName ?? "User"),
                     Text.Muted(user.Email)
                 ).Gap(4).AlignContent(Align.Center)
            ).Gap(20).AlignContent(Align.Center),

            // Connected Accounts section
            new ConnectedGitHubSection(connectedAccounts)

        ).Gap(40).Padding(50).AlignContent(Align.Center).Height(Size.Full());
    }
}

public class ConnectedGitHubSection : ViewBase
{
    private readonly IConnectedAccountsService _connectedAccounts;

    public ConnectedGitHubSection(IConnectedAccountsService connectedAccounts)
    {
        _connectedAccounts = connectedAccounts;
    }

    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var connected = UseState(() => _connectedAccounts.GetAccountSession(OAuthProviders.GitHub)?.AuthToken != null);
        var callback = UseWebhook(async request =>
        {
            Console.WriteLine($"Received GitHub OAuth callback, request data: {request.QueryString}");
            await _connectedAccounts.HandleConnectCallbackAsync(OAuthProviders.GitHub, request);
            connected.Set(true);
        });

        var gitHubSession = _connectedAccounts.GetAccountSession(OAuthProviders.GitHub);
        var isConnected = gitHubSession?.AuthToken != null;

        if (!isConnected)
        {
            return Layout.Vertical(
                Text.H3("Connected Accounts"),
                new Button("Connect GitHub", async () =>
                {
                    var uri = await _connectedAccounts.ConnectAccountAsync(OAuthProviders.GitHub, callback);
                    client.OpenUrl(uri.ToString());
                }, icon: Icons.Github, variant: ButtonVariant.Outline)
            ).Gap(10);
        }

        return Layout.Vertical(
            Text.H3("Connected Accounts"),
            Layout.Horizontal(
                Text.P("GitHub").Bold(),
                new Badge("Connected").Variant(BadgeVariant.Success),
                new Button("Disconnect", async () =>
                {
                    await _connectedAccounts.DisconnectAccountAsync(OAuthProviders.GitHub);
                    connected.Set(false);
                }, variant: ButtonVariant.Destructive)
            ).Gap(10).AlignContent(Align.Center),
            new OAuthProviderTestView(OAuthProviders.GitHub, gitHubSession!)
        ).Gap(10);
    }
}
