using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.Authentication;

[App(order:2, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/04_Authentication/02_Supabase.md", searchHints: ["supabase", "authentication", "email", "social-login", "backend", "auth"])]
public class SupabaseApp(bool onlyBody = false) : ViewBase
{
    public SupabaseApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("supabase-authentication-provider", "Supabase Authentication Provider", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("getting-your-supabase-configuration", "Getting Your Supabase Configuration", 2), new ArticleHeading("step-1-create-a-supabase-account-and-project", "Step 1: Create a Supabase Account and Project", 3), new ArticleHeading("step-2-get-your-configuration-values", "Step 2: Get Your Configuration Values", 3), new ArticleHeading("step-3-configure-authentication-optional", "Step 3: Configure Authentication (Optional)", 3), new ArticleHeading("step-4-enable-social-providers-optional", "Step 4: Enable Social Providers (Optional)", 3), new ArticleHeading("adding-authentication", "Adding Authentication", 2), new ArticleHeading("advanced-configuration", "Advanced Configuration", 3), new ArticleHeading("connection-strings", "Connection Strings", 4), new ArticleHeading("manual-configuration", "Manual Configuration", 4), new ArticleHeading("configuration-parameters", "Configuration Parameters", 4), new ArticleHeading("authentication-flow", "Authentication Flow", 2), new ArticleHeading("emailpassword-flow", "Email/Password Flow", 3), new ArticleHeading("social-login-flow", "Social Login Flow", 3), new ArticleHeading("supabase-specific-features", "Supabase-Specific Features", 2), new ArticleHeading("security-best-practices", "Security Best Practices", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("common-issues", "Common Issues", 3), new ArticleHeading("related-documentation", "Related Documentation", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Supabase Authentication Provider").OnLinkClick(onLinkClick)
            | Lead("Secure your Ivy application with Supabase's built-in authentication supporting email/password and social logins.")
            | new Markdown(
                """"
                ## Overview
                
                Supabase Auth provides a complete authentication system built on top of PostgreSQL. It offers email/password authentication, social logins, and integrates seamlessly with Supabase's database and real-time features for a comprehensive backend-as-a-service solution.
                
                ## Getting Your Supabase Configuration
                
                Before using Supabase with Ivy, you'll need to create a Supabase project and obtain your configuration values:
                
                ### Step 1: Create a Supabase Account and Project
                
                1. **Sign up** at [Supabase](https://supabase.com) if you don't have an account
                2. **Click "New Project"**
                3. **Choose your organization** (or create one)
                4. **Enter project details**:
                   - **Name**: Your project name
                   - **Database Password**: A secure password
                   - **Region**: Choose closest to your users
                5. **Click "Create new project"**
                6. **Wait** for the project to be created (this can take a few minutes)
                
                ### Step 2: Get Your Configuration Values
                
                Once your project is ready:
                
                1. **Go to your project dashboard**
                2. **Click on "Project Settings"** in the sidebar
                3. **Click on "Data API"** in the sidebar
                4. **Copy your Project URL** (e.g., `https://your-project-id.supabase.co`)
                5. **Click on "API Keys"** in the sidebar
                6. **Copy your API Key**:
                   - If using legacy API keys, use "anon" as your API key
                   - Otherwise, use the publishable key
                
                If your project is still using a legacy JWT secret:
                
                1. **Click on "JWT Keys"** in the sidebar
                2. **Copy your legacy JWT secret**
                
                ### Step 3: Configure Authentication (Optional)
                
                To customize your authentication settings:
                
                1. **Go to Authentication** in the sidebar
                2. **Click "URL Configuration"**
                3. **Configure**:
                   - **Site URL**: Your application's URL (e.g., `http://localhost:5010`)
                   - **Redirect URLs**: Add your callback URLs (e.g., `http://localhost:5010/ivy/auth/callback/*`)
                
                ### Step 4: Enable Social Providers (Optional)
                
                If you want to use social login providers:
                
                1. **Go to Authentication > Providers**
                2. **Click on the provider** you want to enable (Google, GitHub, etc.)
                3. **Toggle "Enable sign in with [Provider]"**
                4. **Enter the required credentials** (Client ID, Client Secret from the provider)
                5. **Save**
                
                ## Adding Authentication
                
                To set up Supabase Authentication with Ivy, run the following command and choose `Supabase` when asked to select an auth provider:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy auth add")
                
            | new Markdown(
                """"
                You will be prompted to provide the following Supabase configuration:
                
                - **Project URL**: Your Supabase project URL (e.g., `https://your-project.supabase.co`)
                - **API Key**: Your Supabase project's anonymous (anon) or publishable key
                - **Legacy JWT Secret** (optional): Your Supabase project's legacy JWT secret, if it is still using one.
                
                Your credentials will be stored securely in [.NET user secrets](app://onboarding/concepts/secrets). Ivy then finishes configuring your application automatically:
                
                1. Adds the `Ivy.Auth.Supabase` package to your project.
                2. Adds `server.UseAuth<SupabaseAuthProvider>(c => c.UseEmailPassword().UseGoogle().UseApple());` to your [Program.cs](app://onboarding/concepts/program).
                3. Adds `Ivy.Auth.Supabase` to your global usings.
                
                ### Advanced Configuration
                
                #### Connection Strings
                
                To skip the interactive prompts, you can provide configuration via a connection string:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy auth add --provider Supabase --connection-string \"Supabase:Url=https://your-project.supabase.co;Supabase:ApiKey=your-api-key; Supabase:LegacyJwtSecret=your-jwt-secret\"")
                
            | new Callout("`LegacyJwtSecret` parameter is only required if your Supabase project is still using legacy JWT secrets. If you don't need legacy JWT support, you can omit this parameter from the connection string and leave the field empty during interactive setup.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                For a list of connection string parameters, see [Configuration Parameters](#configuration-parameters) below.
                
                #### Manual Configuration
                
                When deploying an Ivy project without using `ivy deploy`, your local [.NET user secrets](app://onboarding/concepts/secrets) are not automatically transferred. In that case, you can configure basic auth by setting environment variables or .NET user secrets. See Configuration Parameters below.
                
                > **Note:** If configuration is present in both .NET user secrets and environment variables, Ivy will use the values in **[.NET user secrets](app://onboarding/concepts/secrets) over environment variables**.
                
                For more information, see [Authentication Overview](app://onboarding/cli/authentication/authentication-overview).
                
                #### Configuration Parameters
                
                The following parameters are supported via connection string, environment variables, or .NET user secrets:
                
                - **Supabase:Url**: Required. Your Supabase project URL.
                - **Supabase:ApiKey**: Required. Your Supabase project's anonymous (anon) or publishable key.
                - **Supabase:LegacyJwtSecret**: Required if still using a legacy JWT secret, otherwise optional. Your Supabase project's legacy JWT secret.
                - **Supabase:UserAgent**: Optional. Custom User-Agent header for HTTP requests. Defaults to `Ivy-Framework/{version}` where version is the Ivy assembly version.
                
                ## Authentication Flow
                
                ### Email/Password Flow
                
                1. User provides credentials in your Ivy application
                2. Supabase validates credentials and creates a session
                3. User receives access token for authenticated requests
                4. Ivy manages session state automatically
                
                ### Social Login Flow
                
                1. User clicks social login button
                2. User is redirected to social provider (Google, Apple, etc.)
                3. User authorizes application
                4. Provider redirects to Supabase with authorization code
                5. Supabase creates user session and redirects to your app
                
                ## Supabase-Specific Features
                
                Key features of the Supabase provider:
                
                - **Row Level Security**: Database-level security that automatically filters data based on authenticated user
                - **Real-time Subscriptions**: Live data updates that respect authentication context
                - **Social Providers**: Built-in support for multiple OAuth providers
                - **User Management**: Built-in user management APIs and dashboard
                
                ## Security Best Practices
                
                - **Always use HTTPS** in production environments
                - **Store keys securely** in user secrets or environment variables
                - **Migrate to Supabase's new API keys and JWT signing keys** away from the legacy versions
                - **Enable Row Level Security** on database tables containing user data
                - **Configure email rate limiting** to prevent abuse
                - **Validate tokens server-side** for sensitive operations
                - **Monitor authentication logs** in Supabase Dashboard
                
                ## Troubleshooting
                
                ### Common Issues
                
                **Invalid API Key**
                
                - Verify API key is correct and copied from Settings > API in your Supabase dashboard
                - Check that the key hasn't been regenerated
                - Ensure you're using the API key, not service role key for client authentication
                
                **Email Not Delivered**
                
                - Check spam/junk folders for authentication emails
                - Verify SMTP settings in Authentication > Settings
                - Test with different email providers
                - Check Supabase email quota limits
                
                **Authentication Failed**
                
                - Verify users exist and are not blocked in Supabase Dashboard
                - Check that social providers are properly configured in Authentication > Providers
                - Ensure redirect URLs match exactly in both Supabase and provider settings
                
                **Row Level Security Issues**
                
                - Check Row Level Security policies are correctly configured
                - Verify `auth.uid()` matches expected user ID in your policies
                - Test policies using the Supabase SQL editor
                - Ensure user is properly authenticated before accessing data
                
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

