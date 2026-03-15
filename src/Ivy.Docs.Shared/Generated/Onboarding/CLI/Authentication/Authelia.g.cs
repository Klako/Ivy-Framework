using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.Authentication;

[App(order:2, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/04_Authentication/02_Authelia.md", searchHints: ["authelia", "authentication", "self-hosted", "ldap", "forward-auth", "sso"])]
public class AutheliaApp(bool onlyBody = false) : ViewBase
{
    public AutheliaApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("authelia-authentication-provider", "Authelia Authentication Provider", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("setting-up-your-authelia-server", "Setting Up Your Authelia Server", 2), new ArticleHeading("adding-authentication", "Adding Authentication", 2), new ArticleHeading("advanced-configuration", "Advanced Configuration", 3), new ArticleHeading("connection-strings", "Connection Strings", 4), new ArticleHeading("manual-configuration", "Manual Configuration", 4), new ArticleHeading("configuration-parameters", "Configuration Parameters", 4), new ArticleHeading("authentication-flow", "Authentication Flow", 2), new ArticleHeading("authelia-specific-features", "Authelia-Specific Features", 2), new ArticleHeading("security-best-practices", "Security Best Practices", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("common-issues", "Common Issues", 3), new ArticleHeading("related-documentation", "Related Documentation", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Authelia Authentication Provider").OnLinkClick(onLinkClick)
            | Lead("Secure your Ivy application with Authelia's self-hosted identity provider supporting LDAP and forward auth.")
            | new Markdown(
                """"
                ## Overview
                
                Authelia is an open-source authentication and authorization server providing comprehensive identity verification and access control features. It offers single sign-on and supports various authentication backends including LDAP and file-based users, making it ideal for self-hosted environments.
                
                ## Setting Up Your Authelia Server
                
                Before using Authelia with Ivy, you must have a running Authelia instance. You can start with Authelia's [Get started](https://www.authelia.com/integration/prologue/get-started/) guide. Then, continue with the deployment instructions for your environment:
                
                - [Docker](https://www.authelia.com/integration/deployment/docker/)
                - [Kubernetes](https://www.authelia.com/integration/kubernetes/introduction/)
                - [Bare-Metal](https://www.authelia.com/integration/deployment/bare-metal/)
                
                ## Adding Authentication
                
                To set up Authelia Authentication with Ivy, run the following command and choose `Authelia` when asked to select an auth provider:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy auth add")
                
            | new Markdown(
                """"
                You will be prompted to provide your Authelia server URL (e.g., `https://127.0.0.1:9091` or `https://auth.yourdomain.com`).
                
                > **Note:** Authelia requires the use of HTTPS, even for local testing.
                
                Your configuration will be stored securely in [.NET user secrets](app://onboarding/concepts/secrets). Ivy then finishes configuring your application automatically:
                
                1. Adds the `Ivy.Auth.Authelia` package to your project.
                2. Adds `server.UseAuth<AutheliaAuthProvider>();` to your [Program.cs](app://onboarding/concepts/program).
                3. Adds `Ivy.Auth.Authelia` to your global usings.
                
                ### Advanced Configuration
                
                #### Connection Strings
                
                To skip the interactive prompts, you can provide configuration via a connection string:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy auth add --provider Authelia --connection-string \"Authelia:Url=https://auth.yourdomain.com\"")
                
            | new Markdown(
                """"
                For a list of connection string parameters, see [Configuration Parameters](#configuration-parameters) below.
                
                #### Manual Configuration
                
                When deploying an Ivy project without using `ivy deploy`, your local [.NET user secrets](app://onboarding/concepts/secrets) are not automatically transferred. In that case, you can configure Authelia auth by setting environment variables or .NET user secrets. See Configuration Parameters below.
                
                > **Note:** If configuration is present in both .NET user secrets and environment variables, Ivy will use the values in **[.NET user secrets](app://onboarding/concepts/secrets) over environment variables**.
                
                For more information, see [Authentication Overview](app://onboarding/cli/authentication/authentication-overview).
                
                #### Configuration Parameters
                
                The following parameters are supported via connection string, environment variables, or .NET user secrets:
                
                - **Authelia:Url**: Required. The base URL of your Authelia instance.
                - **Authelia:UserAgent**: Optional. Custom User-Agent header for HTTP requests. Defaults to `Ivy-Framework/{version}` where version is the Ivy assembly version.
                
                ## Authentication Flow
                
                1. User provides credentials directly in your Ivy application
                2. Ivy sends credentials to your Authelia instance for validation
                3. Authelia validates credentials against configured backend (file-based users, LDAP, etc.)
                4. If valid, Authelia returns a session token
                5. Ivy uses the session token for subsequent authenticated requests
                
                ## Authelia-Specific Features
                
                Key features of the Authelia provider:
                
                - **Self-hosted Control**: Complete control over your authentication infrastructure
                - **Multiple Backends**: Supports file-based users, LDAP, Active Directory integration on the Authelia server
                - **Direct Integration**: Ivy communicates directly with Authelia's API for credential validation
                - **Granular Access Control**: Fine-grained rules based on users, groups, and resources
                
                ## Security Best Practices
                
                - **Always use HTTPS** for all Authelia communications
                - **Generate strong secrets** for JWT and session encryption keys
                - **Use secure password hashing** (argon2id recommended)
                - **Configure rate limiting** to prevent brute force attacks
                - **Monitor authentication logs** for suspicious activity
                - **Keep Authelia updated** to the latest version
                
                ## Troubleshooting
                
                ### Common Issues
                
                **Connection Refused**
                
                - Verify Authelia service is running and accessible
                - Check firewall settings allow connections to your Authelia port
                - Ensure network connectivity between your application and Authelia instance
                
                **Configuration Issues**
                
                - Verify your Authelia URL is correct and accessible from your Ivy application
                - Check that Authelia is properly configured and running
                - Ensure your Authelia instance has the required API endpoints enabled
                
                **Authentication Failed**
                
                - Check user credentials exist in your configured authentication backend
                - Verify password hashing matches Authelia's configuration
                - Ensure authentication backend (file, LDAP) is properly configured
                
                ## Related Documentation
                
                - [Authentication Overview](app://onboarding/cli/authentication/authentication-overview)
                - [Auth0 Provider](app://onboarding/cli/authentication/auth0)
                - [Microsoft Entra Provider](app://onboarding/cli/authentication/microsoft-entra)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.SecretsApp), typeof(Onboarding.Concepts.ProgramApp), typeof(Onboarding.CLI.Authentication.AuthenticationOverviewApp), typeof(Onboarding.CLI.Authentication.Auth0App), typeof(Onboarding.CLI.Authentication.MicrosoftEntraApp)]; 
        return article;
    }
}

