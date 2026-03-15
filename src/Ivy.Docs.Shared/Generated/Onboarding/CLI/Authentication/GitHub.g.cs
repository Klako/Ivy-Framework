using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.Authentication;

[App(order:2, title:"GitHub", documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/04_Authentication/02_GitHub.md", searchHints: ["github", "authentication", "oauth", "social-login", "identity", "git"])]
public class GitHubApp(bool onlyBody = false) : ViewBase
{
    public GitHubApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("github-authentication-provider", "GitHub Authentication Provider", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("getting-your-github-configuration", "Getting Your GitHub Configuration", 2), new ArticleHeading("step-1-create-a-github-oauth-app", "Step 1: Create a GitHub OAuth App", 3), new ArticleHeading("step-2-get-your-configuration-values", "Step 2: Get Your Configuration Values", 3), new ArticleHeading("adding-authentication", "Adding Authentication", 2), new ArticleHeading("manual-configuration", "Manual Configuration", 3), new ArticleHeading("configuration-parameters", "Configuration Parameters", 3), new ArticleHeading("authentication-flow", "Authentication Flow", 2), new ArticleHeading("oauth-flow", "OAuth Flow", 3), new ArticleHeading("github-specific-features", "GitHub-Specific Features", 2), new ArticleHeading("required-scopes", "Required Scopes", 3), new ArticleHeading("security-best-practices", "Security Best Practices", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("common-issues", "Common Issues", 3), new ArticleHeading("related-documentation", "Related Documentation", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# GitHub Authentication Provider").OnLinkClick(onLinkClick)
            | Lead("Secure your Ivy application with GitHub OAuth 2.0 authentication, enabling users to sign in using their GitHub accounts.")
            | new Markdown(
                """"
                ## Overview
                
                The GitHub authentication provider enables OAuth 2.0 authentication using GitHub accounts. This allows users to sign in to your Ivy application using their existing GitHub credentials, providing a seamless authentication experience for developers and technical users.
                
                ## Getting Your GitHub Configuration
                
                Before using GitHub authentication with Ivy, you'll need to create a GitHub OAuth App and obtain your configuration values:
                
                ### Step 1: Create a GitHub OAuth App
                
                1. **Go to** [GitHub Settings > Developer settings > OAuth Apps](https://github.com/settings/developers)
                2. **Click "New OAuth App"**
                3. Fill in the application details:
                   - **Application name**: Your application name (e.g., "My Ivy Application")
                   - **Homepage URL**: Your application URL (e.g., `http://localhost:5010` for local development or `https://your-app.com` for production)
                   - **Authorization callback URL**: Your application's callback endpoint (e.g., `http://localhost:5010/ivy/auth/callback` for local development or `https://your-app.com/ivy/auth/callback` for production)
                4. **Click "Register application"**
                
                > **Note**: The Authorization callback URL must match your application's auth callback endpoint. For local development, this is typically `http://localhost:PORT/ivy/auth/callback` where PORT is your application's port.
                
                ### Step 2: Get Your Configuration Values
                
                After creating your OAuth App, you'll see the application's settings page. Copy these values:
                
                - **Client ID**: Found in the "Client ID" field
                - **Client Secret**: Click "Generate a new client secret" to create one, then copy it immediately (you won't be able to see it again)
                - **Redirect Uri**: The authorization callback URL you configured during OAuth App creation (e.g., `http://localhost:5010/ivy/auth/callback` for local development or `https://your-domain.com/ivy/auth/callback` for production)
                
                > **Important**: Store your Client Secret securely. The Client Secret is sensitive and won't be retrievable from GitHub after creation - if you lose it, you'll need to generate a new one. Use [.NET user secrets](app://onboarding/concepts/secrets) for local development and environment variables or secure key management for production. Never commit secrets to version control.
                
                ## Adding Authentication
                
                To set up GitHub Authentication with Ivy, you need to manually configure it in your project:
                
                ### Manual Configuration
                
                **1: Configure the GitHub Auth Provider**:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                using Ivy.Auth.GitHub;
                
                var server = new Server();
                
                server.UseAuth<GitHubAuthProvider>();
                
                await server.RunAsync();
                """",Languages.Csharp)
            | new Markdown(
                """"
                **2: Add configuration via [.NET user secrets](app://onboarding/concepts/secrets) or environment variables. See [Configuration Parameters](#configuration-parameters) below for detailed instructions.**
                
                ### Configuration Parameters
                
                Configure the following parameters using .NET user secrets (recommended for development) or environment variables (recommended for production):
                
                - **GitHub:ClientId**: Required. Your GitHub OAuth App's Client ID.
                - **GitHub:ClientSecret**: Required. Your GitHub OAuth App's Client Secret.
                - **GitHub:RedirectUri**: Required. The authorization callback URL that matches your GitHub OAuth App settings.
                - **GitHub:UserAgent**: Optional. Custom User-Agent header for GitHub API requests. Defaults to `Ivy-Framework/{version}` where version is the Ivy assembly version.
                
                **Using .NET User Secrets (Development):**
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("dotnet user-secrets set \"GitHub:ClientId\" \"your_client_id\"")
                .AddCommand("dotnet user-secrets set \"GitHub:ClientSecret\" \"your_client_secret\"")
                .AddCommand("dotnet user-secrets set \"GitHub:RedirectUri\" \"your_redirect_uri\"")
                .AddCommand("dotnet user-secrets set \"GitHub:UserAgent\" \"MyApp/1.0\"")
                
            | new Markdown("**Using Environment Variables (Production):**").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddOutput("$env:GitHub__ClientId=\"your_client_id\"")
                .AddOutput("$env:GitHub__ClientSecret=\"your_client_secret\"")
                .AddOutput("$env:GitHub__RedirectUri=\"your_redirect_uri\"")
                .AddOutput("$env:GitHub__UserAgent=\"MyApp/1.0\"")
                
            | new Markdown(
                """"
                > **Note:** If configuration is present in both .NET user secrets and environment variables, Ivy will use the values in **.NET user secrets over environment variables**. Never commit secrets to version control.
                
                ## Authentication Flow
                
                ### OAuth Flow
                
                1. User clicks the "GitHub" login button in your Ivy application
                2. User is redirected to GitHub's authorization page
                3. User authorizes your application to access their GitHub account
                4. GitHub redirects back to your application's auth callback endpoint (`/ivy/auth/callback`)
                5. Ivy receives the authorization code and exchanges it for an access token
                6. User is authenticated and redirected back to your application
                7. User can now access your Ivy application with their GitHub identity
                
                ## GitHub-Specific Features
                
                Key features of the GitHub authentication provider:
                
                - **OAuth 2.0 Flow**: Standard OAuth 2.0 authorization code flow
                - **User Information**: Automatically retrieves user ID, email, display name, and avatar
                - **Email Handling**: Automatically selects the primary email address or first verified email
                - **Long-lived Tokens**: GitHub OAuth tokens are long-lived and don't expire
                - **No Refresh Tokens**: GitHub doesn't provide refresh tokens, but tokens are long-lived
                
                ### Required Scopes
                
                The GitHub OAuth app requests the following scopes:
                
                - `user:email` - Access to user's email addresses (required for user identification)
                
                ## Security Best Practices
                
                - **Always use HTTPS** in production environments
                - **Store Client Secret securely** - use .NET user secrets for development and environment variables or secure key management for production
                - **Never commit secrets** to version control
                - **Rotate Client Secret** if it's compromised
                - **Use appropriate callback URLs** - ensure the callback URL in your GitHub OAuth App matches your application's webhook endpoint exactly
                - **Monitor authentication logs** in your application for suspicious activity
                
                ## Troubleshooting
                
                ### Common Issues
                
                **Invalid Client Credentials**
                
                - Verify Client ID and Client Secret are correct in your configuration
                - Check that credentials haven't been regenerated in GitHub OAuth App settings
                - Ensure there are no extra spaces or characters in the configuration values
                
                **Callback URL Mismatch**
                
                - Verify Authorization callback URL in GitHub OAuth App settings matches your application's auth callback endpoint (e.g., `http://localhost:5010/ivy/auth/callback`)
                - Check that the URL matches exactly, including the protocol (`http://` vs `https://`) and port number
                - Ensure HTTPS is used in production
                
                **Authentication Failed**
                
                - Check that your GitHub OAuth App is properly configured
                - Verify the OAuth App is not suspended or deleted
                - Ensure users have authorized the application (they may need to revoke and re-authorize)
                
                **Email Not Available**
                
                - Verify the user's GitHub account has a verified email address
                - Check that the `user:email` scope is properly requested (this is automatic)
                - Some GitHub accounts may not have public email addresses
                
                **Token Exchange Errors**
                
                - Verify your application is accessible at the configured callback URL
                - Check network connectivity and firewall settings
                - Ensure your application is running and the webhook endpoint is accessible
                
                ## Related Documentation
                
                - [Authentication Overview](app://onboarding/cli/authentication/authentication-overview)
                - [Auth0 Authentication](app://onboarding/cli/authentication/auth0)
                - [Basic Auth](app://onboarding/cli/authentication/basic-auth)
                - [Supabase Authentication](app://onboarding/cli/authentication/supabase)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.SecretsApp), typeof(Onboarding.CLI.Authentication.AuthenticationOverviewApp), typeof(Onboarding.CLI.Authentication.Auth0App), typeof(Onboarding.CLI.Authentication.BasicAuthApp), typeof(Onboarding.CLI.Authentication.SupabaseApp)]; 
        return article;
    }
}

