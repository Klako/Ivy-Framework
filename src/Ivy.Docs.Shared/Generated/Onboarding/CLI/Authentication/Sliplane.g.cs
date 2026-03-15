using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.Authentication;

[App(order:2, title:"Sliplane", documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/04_Authentication/02_Sliplane.md", searchHints: ["sliplane", "authentication", "oauth", "deployment", "hosting"])]
public class SliplaneApp(bool onlyBody = false) : ViewBase
{
    public SliplaneApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("sliplane-authentication-provider", "Sliplane Authentication Provider", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("getting-your-sliplane-configuration", "Getting Your Sliplane Configuration", 2), new ArticleHeading("step-1-request-oauth-credentials-from-sliplane", "Step 1: Request OAuth credentials from Sliplane", 3), new ArticleHeading("step-2-get-your-configuration-values", "Step 2: Get your configuration values", 3), new ArticleHeading("adding-authentication", "Adding Authentication", 2), new ArticleHeading("manual-configuration", "Manual configuration", 3), new ArticleHeading("authentication-flow", "Authentication flow", 2), new ArticleHeading("sliplane-specific-behavior", "Sliplane-specific behavior", 2), new ArticleHeading("security-best-practices", "Security best practices", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("related-documentation", "Related documentation", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Sliplane Authentication Provider").OnLinkClick(onLinkClick)
            | Lead("Authenticate users with Sliplane OAuth 2.0, enabling sign-in for applications deployed or integrated with Sliplane.")
            | new Markdown(
                """"
                ## Overview
                
                The Sliplane authentication provider uses the OAuth 2.0 authorization code flow so users can sign in to your Ivy application with their Sliplane account. This is useful when your app is hosted on or integrated with [Sliplane](https://sliplane.io).
                
                ## Getting Your Sliplane Configuration
                
                Before using Sliplane authentication with Ivy, you need OAuth credentials and must register your app’s callback URL with Sliplane.
                
                ### Step 1: Request OAuth credentials from Sliplane
                
                Sliplane does not offer a self-service developer portal for OAuth apps. To get a **Client ID** and **Client Secret**:
                
                1. **Reach out to Sliplane** (e.g. via [sliplane.io](https://sliplane.io) or their support).
                2. **Request OAuth credentials** for your application.
                3. **Provide your callback URL** so Sliplane can redirect users after sign-in. Ivy uses the standard auth callback path:
                   - **Local:** `http://localhost:PORT/ivy/auth/callback` (replace `PORT` with your app’s port)
                   - **Production:** `https://your-domain.com/ivy/auth/callback`
                4. **Save the Client ID and Client Secret** that Sliplane sends you — you’ll use them in Step 2.
                
                ### Step 2: Get your configuration values
                
                Once Sliplane has given you credentials:
                
                1. **Client ID** — use as `Sliplane:ClientId` in configuration.
                2. **Client Secret** — use as `Sliplane:ClientSecret` in configuration. Store it securely (e.g. [.NET user secrets](app://onboarding/concepts/secrets) or environment variables); never commit it to version control.
                3. **Callback URL** — ensure the URL you registered with Sliplane matches your app exactly (e.g. `https://your-domain.com/ivy/auth/callback`). For local development, use `http://localhost:PORT/ivy/auth/callback`.
                
                ## Adding Authentication
                
                ### Manual configuration
                
                **1. Register the Sliplane auth provider:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                using Ivy.Auth.Sliplane;
                
                var server = new Server();
                
                server.UseAuth<SliplaneAuthProvider>();
                
                await server.RunAsync();
                """",Languages.Csharp)
            | new Markdown(
                """"
                **2. Add configuration** via [.NET user secrets](app://onboarding/concepts/secrets) or environment variables. Set these keys:
                
                | Key | Required | Default |
                |-----|----------|---------|
                | `Sliplane:ClientId` | Yes | — |
                | `Sliplane:ClientSecret` | Yes | — |
                | `Sliplane:AuthorizationUrl` | No | `https://api.sliplane.io/web/oauth/authorize` |
                | `Sliplane:TokenUrl` | No | `https://api.sliplane.io/web/oauth/token` |
                | `Sliplane:Scope` | No | `full` |
                
                **Development (user secrets):**
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("dotnet user-secrets set \"Sliplane:ClientId\" \"your_client_id\"")
                .AddCommand("dotnet user-secrets set \"Sliplane:ClientSecret\" \"your_client_secret\"")
                
            | new Markdown("**Production (environment variables):**").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddOutput("$env:Sliplane__ClientId=\"your_client_id\"")
                .AddOutput("$env:Sliplane__ClientSecret=\"your_client_secret\"")
                
            | new Markdown(
                """"
                > **Important:** Store your Client Secret securely. Never commit secrets to version control.
                
                ## Authentication flow
                
                1. The user clicks the "Sliplane" login option in your Ivy app.
                2. The user is redirected to Sliplane’s authorization page.
                3. After signing in, Sliplane redirects back to your app’s auth callback URL (`/ivy/auth/callback`) with an authorization code.
                4. Ivy exchanges the code for an access token (and optional refresh token).
                5. The token is stored in the session and used for API calls. The user is considered authenticated.
                
                ## Sliplane-specific behavior
                
                - **OAuth only:** Sliplane does not support email/password login; only the OAuth flow is available.
                - **No user-info endpoint:** Sliplane does not expose a user-info API. After token validation, Ivy returns a placeholder user (e.g. `user@sliplane.io`). You can wrap the provider if you need custom user data.
                - **Token validation:** Tokens are validated by calling `GET https://ctrl.sliplane.io/v0/projects` with the Bearer token.
                - **Token lifetime:** Sliplane does not embed expiration in the token; Ivy does not expose a token lifetime for this provider.
                - **Refresh tokens:** The provider supports refreshing the access token using the stored refresh token when available.
                
                ## Security best practices
                
                - Use **HTTPS** in production.
                - Store **Client Secret** in user secrets or environment variables, never in source code.
                - Do **not commit** secrets to version control.
                - Ensure the **callback URL** registered with Sliplane matches your app’s auth callback URL (`/ivy/auth/callback`) exactly (including protocol and port).
                - Rotate the Client Secret if it may have been compromised.
                
                ## Troubleshooting
                
                **Invalid or missing credentials**
                
                - Confirm Client ID and Client Secret are set correctly (user secrets or environment variables).
                - Remember: use double underscore `__` for nested keys in environment variables (e.g. `Sliplane__ClientId`).
                
                **Callback URL mismatch**
                
                - The redirect URI used by Ivy must match what is registered with Sliplane (e.g. `https://your-domain.com/ivy/auth/callback`).
                - Check protocol, host, port, and path.
                
                **Authentication or token exchange fails**
                
                - Verify your app is reachable at the callback URL.
                - Check that Sliplane has issued credentials for your application and that the callback URL is registered.
                
                ## Related documentation
                
                - [Authentication overview](app://onboarding/cli/authentication/authentication-overview)
                - [GitHub authentication](app://onboarding/cli/authentication/git-hub)
                - [Auth0 authentication](app://onboarding/cli/authentication/auth0)
                - [Basic auth](app://onboarding/cli/authentication/basic-auth)
                - [Supabase authentication](app://onboarding/cli/authentication/supabase)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.SecretsApp), typeof(Onboarding.CLI.Authentication.AuthenticationOverviewApp), typeof(Onboarding.CLI.Authentication.GitHubApp), typeof(Onboarding.CLI.Authentication.Auth0App), typeof(Onboarding.CLI.Authentication.BasicAuthApp), typeof(Onboarding.CLI.Authentication.SupabaseApp)]; 
        return article;
    }
}

