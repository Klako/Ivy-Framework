using Ivy.Core;
using Ivy.Core.Apps;
using Ivy.Core.Auth;
using Ivy.Core.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

namespace Ivy.Test.Auth;

public class AuthControllerTests
{
    private class FakeAuthService : IAuthService
    {
        private readonly Uri _redirectUri;

        public FakeAuthService(Uri redirectUri)
        {
            _redirectUri = redirectUri;
        }

        public IAuthSession GetAuthSession() => new AuthSession(authToken: null);

        public AuthOption[] GetAuthOptions() =>
            [new AuthOption(AuthFlow.OAuth, Id: "test-option")];

        public Task<Uri> GetOAuthUriAsync(AuthOption option, WebhookEndpoint callback, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_redirectUri);
        }

        public Task<AuthToken?> HandleOAuthCallbackAsync(HttpRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResult> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<BrokeredSessionsResult> GetBrokeredSessionsAsync(bool skipCache = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        void IAuthService.SetAuthCookies(bool reloadPage, bool? triggerMachineReload, bool triggerMachineAuthSync)
        {
        }

        public Task<AuthToken?> RefreshAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<UserInfo?> GetUserInfoAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TokenLifetime?> GetAccessTokenLifetimeAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public AuthToken? GetCurrentToken()
        {
            throw new NotImplementedException();
        }

        public string? GetCurrentSessionData()
        {
            throw new NotImplementedException();
        }

        public IAuthTokenHandlerSession GetAuthTokenHandlerSession()
        {
            throw new NotImplementedException();
        }

        void IAuthTokenHandlerService.SetAuthTokenCookies(bool reloadPage, bool? triggerMachineReload)
        {
        }

        void IAuthTokenHandlerService.SetAuthSessionDataCookies(bool reloadPage, bool? triggerMachineReload)
        {
        }
    }

    private class FakeConnectedAccountsService : IConnectedAccountsService
    {
        private readonly Uri _redirectUri;

        public FakeConnectedAccountsService(Uri redirectUri)
        {
            _redirectUri = redirectUri;
        }

        public Task<Uri> ConnectAccountAsync(string provider, WebhookEndpoint callback, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_redirectUri);
        }

        public Task DisconnectAccountAsync(string provider, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IAuthSession? GetAccountSession(string provider)
        {
            throw new NotImplementedException();
        }

        public string[] GetAvailableProviders()
        {
            throw new NotImplementedException();
        }

        public Task<AuthToken?> HandleConnectCallbackAsync(string provider, HttpRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

#pragma warning disable CS0067 // Event is never used
        public event Action<string>? AccountConnected;
        public event Action<string>? AccountDisconnected;
#pragma warning restore CS0067
    }

    private static (AuthController controller, AppSessionStore sessionStore, string connectionId) SetupController(Uri redirectUri, bool useConnectedAccounts = false)
    {
        var controller = new AuthController();
        var sessionStore = new AppSessionStore();
        var connectionId = "test-connection";

        var services = new ServiceCollection();
        if (useConnectedAccounts)
        {
            services.AddSingleton<IConnectedAccountsService>(new FakeConnectedAccountsService(redirectUri));
        }
        else
        {
            services.AddSingleton<IAuthService>(new FakeAuthService(redirectUri));
        }

        var serviceProvider = services.BuildServiceProvider();
        var appSession = new AppSession
        {
            ConnectionId = connectionId,
            AppId = "",
            MachineId = "",
            ParentId = null,
            WidgetTree = null!,
            AppDescriptor = null!,
            App = null!,
            ContentBuilder = null!,
            AppServices = serviceProvider,
            LastInteraction = DateTime.UtcNow
        };
        sessionStore.Sessions[connectionId] = appSession;

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                Request =
                {
                    Scheme = "https",
                    Host = new HostString("localhost:5000")
                }
            }
        };

        return (controller, sessionStore, connectionId);
    }

    [Fact]
    public async Task OAuthLogin_WithHttpsRedirect_AllowsRedirect()
    {
        // Arrange
        var httpsUri = new Uri("https://oauth.example.com/authorize");
        var (controller, sessionStore, connectionId) = SetupController(httpsUri);

        var loginRegistry = new OAuthLoginRegistry();
        var loginId = loginRegistry.RegisterPending(connectionId, "test-option");

        var callbackRegistry = new OAuthCallbackRegistry();
        var serverArgs = new ServerArgs { BasePath = null };
        var logger = NullLogger<AuthController>.Instance;

        // Act
        var result = await controller.OAuthLogin(loginId, loginRegistry, callbackRegistry, sessionStore, serverArgs, logger);

        // Assert
        var redirectResult = Assert.IsType<RedirectResult>(result);
        Assert.Equal("https://oauth.example.com/authorize", redirectResult.Url);
    }

    [Fact]
    public async Task OAuthLogin_WithRelativeRedirect_AllowsRedirect()
    {
        // Arrange
        var relativeUri = new Uri("/oauth/callback", UriKind.Relative);
        var (controller, sessionStore, connectionId) = SetupController(relativeUri);

        var loginRegistry = new OAuthLoginRegistry();
        var loginId = loginRegistry.RegisterPending(connectionId, "test-option");

        var callbackRegistry = new OAuthCallbackRegistry();
        var serverArgs = new ServerArgs { BasePath = null };
        var logger = NullLogger<AuthController>.Instance;

        // Act
        var result = await controller.OAuthLogin(loginId, loginRegistry, callbackRegistry, sessionStore, serverArgs, logger);

        // Assert
        var redirectResult = Assert.IsType<RedirectResult>(result);
        Assert.Equal("/oauth/callback", redirectResult.Url);
    }

    [Fact]
    public async Task OAuthLogin_WithHttpRedirect_RejectsRedirect()
    {
        // Arrange
        var httpUri = new Uri("http://malicious.example.com/phishing");
        var (controller, sessionStore, connectionId) = SetupController(httpUri);

        var loginRegistry = new OAuthLoginRegistry();
        var loginId = loginRegistry.RegisterPending(connectionId, "test-option");

        var callbackRegistry = new OAuthCallbackRegistry();
        var serverArgs = new ServerArgs { BasePath = null };
        var logger = NullLogger<AuthController>.Instance;

        // Act
        var result = await controller.OAuthLogin(loginId, loginRegistry, callbackRegistry, sessionStore, serverArgs, logger);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid OAuth redirect URL", badRequestResult.Value);
    }

    [Fact]
    public async Task OAuthLogin_ConnectedAccount_WithHttpsRedirect_AllowsRedirect()
    {
        // Arrange
        var httpsUri = new Uri("https://oauth.example.com/authorize");
        var (controller, sessionStore, connectionId) = SetupController(httpsUri, useConnectedAccounts: true);

        var loginRegistry = new OAuthLoginRegistry();
        var loginId = loginRegistry.RegisterPending(connectionId, "github", "github");

        var callbackRegistry = new OAuthCallbackRegistry();
        var serverArgs = new ServerArgs { BasePath = null };
        var logger = NullLogger<AuthController>.Instance;

        // Act
        var result = await controller.OAuthLogin(loginId, loginRegistry, callbackRegistry, sessionStore, serverArgs, logger);

        // Assert
        var redirectResult = Assert.IsType<RedirectResult>(result);
        Assert.Equal("https://oauth.example.com/authorize", redirectResult.Url);
    }

    [Fact]
    public async Task OAuthLogin_ConnectedAccount_WithHttpRedirect_RejectsRedirect()
    {
        // Arrange
        var httpUri = new Uri("http://malicious.example.com/phishing");
        var (controller, sessionStore, connectionId) = SetupController(httpUri, useConnectedAccounts: true);

        var loginRegistry = new OAuthLoginRegistry();
        var loginId = loginRegistry.RegisterPending(connectionId, "github", "github");

        var callbackRegistry = new OAuthCallbackRegistry();
        var serverArgs = new ServerArgs { BasePath = null };
        var logger = NullLogger<AuthController>.Instance;

        // Act
        var result = await controller.OAuthLogin(loginId, loginRegistry, callbackRegistry, sessionStore, serverArgs, logger);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid OAuth redirect URL", badRequestResult.Value);
    }
}
