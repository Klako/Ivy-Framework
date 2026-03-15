using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.Authentication;

[App(order:1, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/04_Authentication/01_AuthenticationOverview.md", searchHints: ["authentication", "login", "security", "auth", "oauth", "identity"])]
public class AuthenticationOverviewApp(bool onlyBody = false) : ViewBase
{
    public AuthenticationOverviewApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("authentication-overview", "Authentication Overview", 1), new ArticleHeading("adding-an-authentication-provider", "Adding an Authentication Provider", 2), new ArticleHeading("command-options", "Command Options", 3), new ArticleHeading("how-ivy-updates-programcs", "How Ivy Updates Program.cs", 3), new ArticleHeading("security-and-secrets-management", "Security and Secrets Management", 3), new ArticleHeading("environment-variables", "Environment Variables", 4), new ArticleHeading("authentication-flow", "Authentication Flow", 2), new ArticleHeading("oauth2-flow-auth0-supabase-microsoft-entra", "OAuth2 Flow (Auth0, Supabase, Microsoft Entra)", 3), new ArticleHeading("emailpassword-flow-basic-auth-authelia", "Email/Password Flow (Basic Auth, Authelia)", 3), new ArticleHeading("using-iauthservice-in-views", "Using IAuthService in Views", 2), new ArticleHeading("supported-authentication-providers", "Supported Authentication Providers", 2), new ArticleHeading("examples", "Examples", 2), new ArticleHeading("complete-custom-login-view", "Complete Custom Login View", 2), new ArticleHeading("customizing-authentication-cookies", "Customizing Authentication Cookies", 2), new ArticleHeading("default-cookie-settings", "Default Cookie Settings", 3), new ArticleHeading("customizing-cookie-options", "Customizing Cookie Options", 3), new ArticleHeading("best-practices", "Best Practices", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("related-commands", "Related Commands", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Authentication Overview").OnLinkClick(onLinkClick)
            | Lead("Secure your Ivy application with integrated authentication providers including Auth0, Clerk, Supabase, Authelia, and Microsoft Entra ID.")
            | new Markdown(
                """"
                The `ivy auth add` command lets you add and configure authentication in your Ivy project. Ivy supports multiple providers and automatically updates your project setup to integrate them.
                
                ## Adding an Authentication Provider
                
                To add authentication to your Ivy project, run:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy auth add")
                
            | new Markdown(
                """"
                If you run this command without additional options, Ivy will guide you through an interactive setup:
                
                1. **Select a Provider**: Choose from the available authentication providers
                2. **Configure the Provider**: Enter the necessary configuration details (for example, domain and client ID for Auth0, or project URL and API key for Supabase)
                3. **Project Setup**: Ivy updates your [Program.cs](app://onboarding/concepts/program) and stores sensitive values in [.NET user secrets](app://onboarding/concepts/secrets), so your project is ready to use authentication.
                
                ### Command Options
                
                `--provider <PROVIDER>` or `-p <PROVIDER>` - Specify the authentication provider directly:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy auth add --provider Auth0")
                
            | new Markdown(
                """"
                Available providers: `Auth0`, `Clerk`, `Supabase`, `MicrosoftEntra`, `Authelia`, `Basic`
                
                `--connection-string <CONNECTION_STRING>` - Provide provider-specific configuration using connection string syntax. When used with `--provider`, this option allows you pass all required configuration inline, rather than being prompted interactively during setup:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy auth add --provider Auth0 --connection-string YourConnectionString")
                
            | new Markdown("`--verbose` or `-v` - Enable verbose output for detailed logging:").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy auth add --verbose")
                
            | new Markdown(
                """"
                ### How Ivy Updates Program.cs
                
                Ivy automatically updates your [Program.cs](app://onboarding/concepts/program) to configure authentication. Here are a few examples of code that it may add:
                
                **Basic Auth**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("server.UseAuth<BasicAuthProvider>();",Languages.Csharp)
            | new Markdown("**Auth0**").OnLinkClick(onLinkClick)
            | new CodeBlock("server.UseAuth<Auth0AuthProvider>(c => c.UseEmailPassword().UseGoogle().UseApple());",Languages.Csharp)
            | new Markdown("**Supabase**").OnLinkClick(onLinkClick)
            | new CodeBlock("server.UseAuth<SupabaseAuthProvider>(c => c.UseEmailPassword().UseGoogle().UseGithub());",Languages.Csharp)
            | new Markdown(
                """"
                Before making any changes to your Program.cs, Ivy checks whether an authentication provider is already configured.
                
                - If you selected a **different provider** than what is already configured, Ivy will ask you to confirm before overwriting the existing configuration.
                - If you selected the **same provider**, Ivy reuses the existing configuration as defaults for your new setup.
                
                > **Note:** Only one authentication provider can be active at a time. However, some providers (such as Auth0 or Supabase) support multiple login methods (like Google, GitHub, or username/password), so you can still offer users a variety of login options.
                
                ### Security and Secrets Management
                
                Ivy automatically configures [.NET user secrets](app://onboarding/concepts/secrets) for secure authentication configuration. To view configured secrets:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("dotnet user-secrets list")
                
            | new Markdown(
                """"
                #### Environment Variables
                
                Instead of .NET user secrets, you can also use environment variables to store authentication secrets. For example, you might configure Auth0 like so:
                
                **Windows (PowerShell):**
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("$env:Auth0__Domain=\"your-domain.auth0.com\"")
                .AddCommand("$env:Auth0__ClientId=\"your-client-id\"")
                .AddCommand("$env:Auth0__ClientSecret=\"your-client-secret\"")
                .AddCommand("$env:Auth0__Audience=\"https://your-domain.auth0.com/api/v2\"")
                .AddCommand("$env:Auth0__Namespace=\"https://ivy.app/\"")
                
            | new Markdown("**Mac/Linux (Bash):**").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("export Auth0__Domain=\"your-domain.auth0.com\"")
                .AddCommand("export Auth0__ClientId=\"your-client-id\"")
                .AddCommand("export Auth0__ClientSecret=\"your-client-secret\"")
                .AddCommand("export Auth0__Audience=\"https://your-domain.auth0.com/api/v2\"")
                .AddCommand("export Auth0__Namespace=\"https://ivy.app/\"")
                
            | new Markdown(
                """"
                If configuration is present in both .NET user secrets and environment variables, Ivy will use the values in [user secrets](app://onboarding/concepts/secrets).
                
                ## Authentication Flow
                
                ### OAuth2 Flow (Auth0, Supabase, Microsoft Entra)
                
                1. User visits your application.
                2. User clicks "Login" and is redirected to the identity provider.
                3. User authenticates with the identity provider (e.g., Google, Microsoft, GitHub).
                4. The identity provider redirects back to your application's callback URL with an authorization code.
                5. Ivy exchanges the authorization code for an access token, and in some cases a refresh token.
                6. Ivy uses the token(s) to establish an authenticated session for the user.
                
                ### Email/Password Flow (Basic Auth, Authelia)
                
                1. User visits your application.
                2. User is prompted for a username and password.
                3. Credentials are validated by the configured authentication provider.
                4. If valid, Ivy establishes an authenticated session for the user.
                
                ## Using IAuthService in Views
                
                Use [UseService](app://onboarding/concepts/program) to obtain `IAuthService` in your [views](app://onboarding/concepts/views):
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var auth = UseService<IAuthService>();
                
                await auth.LoginAsync(email, password);
                var user = await auth.GetUserInfoAsync();
                await auth.LogoutAsync();
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Supported Authentication Providers
                
                Ivy supports the following authentication providers. Click on any provider for detailed setup instructions:
                
                - **[Auth0](app://onboarding/cli/authentication/auth0)** - Universal authentication with social logins and enterprise integrations
                - **[Clerk](app://onboarding/cli/authentication/clerk)** - Modern authentication platform with passwordless login, social connections, and comprehensive user management
                - **[Supabase](app://onboarding/cli/authentication/supabase)** - Email/password, magic links, social auth, and Row Level Security integration
                - **[Microsoft Entra](app://onboarding/cli/authentication/microsoft-entra)** - Enterprise SSO, conditional access, and Microsoft Graph integration
                - **[Authelia](app://onboarding/cli/authentication/authelia)** - Self-hosted identity provider with LDAP and forward auth
                - **[Sliplane](app://onboarding/cli/authentication/sliplane)** - OAuth 2.0 sign-in for apps deployed or integrated with Sliplane
                - **[Basic Auth](app://onboarding/cli/authentication/basic-auth)** - Simple username/password authentication for development and internal tools
                
                ## Examples
                
                **Auth0 Setup**
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy auth add --provider Auth0 --connection-string YourConnectionString")
                
            | new Markdown("**Clerk Setup**").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy auth add --provider Clerk --connection-string YourConnectionString")
                
            | new Markdown("**Supabase Auth Setup**").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy auth add --provider Supabase --connection-string YourConnectionString")
                
            | new Markdown("**Basic Auth Setup**").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy auth add --provider Basic --connection-string YourConnectionString")
                
            | new Markdown(
                """"
                ## Complete Custom Login View
                
                For complete control over the login experience, you can replace the entire login view:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("server.UseAuth<BasicAuthProvider>(viewFactory: () => new MyCustomLoginApp());",Languages.Csharp)
            | new Markdown(
                """"
                ## Customizing Authentication Cookies
                
                Ivy allows you to customize authentication cookie settings globally from your [Program.cs](app://onboarding/concepts/program) using the `Server.ConfigureAuthCookieOptions` static property. This enables you to override default cookie settings (such as expiration time, SameSite policy, Secure flag, etc.) based on your application's specific security requirements.
                
                ### Default Cookie Settings
                
                By default, Ivy authentication cookies are configured with:
                
                - **HttpOnly**: `true` (prevents JavaScript access)
                - **Secure**: `true` in production, `false` in development (requires HTTPS)
                - **SameSite**: `Lax` (provides CSRF protection while allowing cross-site navigation)
                - **Expires**: 1 year from creation
                - **Path**: `/` (available site-wide)
                
                ### Customizing Cookie Options
                
                To override these defaults, set `Server.ConfigureAuthCookieOptions` in your `Program.cs` before calling `server.RunAsync()`:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Server.ConfigureAuthCookieOptions = options =>
                {
                    options.Expires = DateTimeOffset.UtcNow.AddDays(30);
                };
                """",Languages.Csharp)
            | new Markdown(
                """"
                > **Note**: Custom configuration is applied after Ivy sets the default values, allowing you to override any setting. It's recommended to keep `HttpOnly = true` for security.
                
                ## Best Practices
                
                **Security** - Always use HTTPS in production, store sensitive configuration in user secrets or environment variables, regularly rotate client secrets, use strong passwords for Basic Auth, and implement proper session management.
                
                **Configuration** - Use descriptive names for your authentication providers, keep configuration separate from code, use environment-specific settings, and document your authentication setup.
                
                **Testing** - Test authentication flows in development, verify token validation works correctly, and ensure logout functionality works properly.
                
                ## Troubleshooting
                
                **Authentication Provider Issues** - Verify your provider configuration is correct, check that your project is properly registered with the identity provider, ensure callback URLs are correctly configured, and verify network connectivity to the authentication provider.
                
                **Token Validation Issues** - Check that your JWT tokens are properly signed, verify audience and issuer claims, and ensure your system clock is set correctly.
                
                **Configuration Issues** - Ensure authentication settings are properly stored in [user secrets](app://onboarding/concepts/secrets) (or verify environment variables are correctly set), and check that your [Program.cs](app://onboarding/concepts/program) includes the necessary authentication config.
                
                ### Related Commands
                
                - `ivy init` - Initialize a new Ivy project
                - `ivy db add` - Add database connections
                - `ivy app create` - Create apps
                - `ivy deploy` - Deploy your project
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ProgramApp), typeof(Onboarding.Concepts.SecretsApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.CLI.Authentication.Auth0App), typeof(Onboarding.CLI.Authentication.ClerkApp), typeof(Onboarding.CLI.Authentication.SupabaseApp), typeof(Onboarding.CLI.Authentication.MicrosoftEntraApp), typeof(Onboarding.CLI.Authentication.AutheliaApp), typeof(Onboarding.CLI.Authentication.SliplaneApp), typeof(Onboarding.CLI.Authentication.BasicAuthApp)]; 
        return article;
    }
}

