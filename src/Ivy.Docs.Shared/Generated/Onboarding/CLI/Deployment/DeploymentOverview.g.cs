using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.Deployment;

[App(order:1, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/06_Deployment/01_DeploymentOverview.md", searchHints: ["deployment", "cloud", "production", "aws", "azure", "docker"])]
public class DeploymentOverviewApp(bool onlyBody = false) : ViewBase
{
    public DeploymentOverviewApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivy-deployment-overview", "Ivy Deployment Overview", 1), new ArticleHeading("supported-deployment-providers", "Supported Deployment Providers", 2), new ArticleHeading("cloud-platforms", "Cloud Platforms", 3), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("deploying-your-project", "Deploying Your Project", 3), new ArticleHeading("command-options", "Command Options", 3), new ArticleHeading("interactive-mode", "Interactive Mode", 3), new ArticleHeading("deployment-process", "Deployment Process", 3), new ArticleHeading("containerization", "Containerization", 3), new ArticleHeading("security-and-configuration", "Security and Configuration", 3), new ArticleHeading("monitoring-and-logging", "Monitoring and Logging", 3), new ArticleHeading("deployment-options", "Deployment Options", 3), new ArticleHeading("troubleshooting", "Troubleshooting", 3), new ArticleHeading("examples", "Examples", 2), new ArticleHeading("best-practices", "Best Practices", 3), new ArticleHeading("post-deployment", "Post-deployment", 3), new ArticleHeading("related-commands", "Related Commands", 3), new ArticleHeading("cloud-provider-documentation", "Cloud Provider Documentation", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Ivy Deployment Overview").OnLinkClick(onLinkClick)
            | Lead("Deploy your Ivy applications to cloud platforms with automated containerization, infrastructure setup, and configuration management.")
            | new Markdown(
                """"
                The `ivy deploy` command allows you to deploy your Ivy project to various cloud platforms. Ivy supports multiple deployment providers and automatically handles the deployment process including containerization, configuration, and infrastructure setup. Your [Program.cs](app://onboarding/concepts/program) and [secrets](app://onboarding/concepts/secrets) are applied in the deployed environment.
                
                ## Supported Deployment Providers
                
                Ivy supports the following cloud deployment providers:
                
                ### Cloud Platforms
                
                - **AWS** - Amazon Web Services
                - **Azure** - Microsoft Azure
                - **GCP** - Google Cloud Platform
                - **Sliplane** - Container deployment platform
                
                ## Basic Usage
                
                ### Deploying Your Project
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy deploy")
                
            | new Markdown(
                """"
                This command will:
                
                - Prompt you to select a deployment provider
                - Build and containerize your project
                - Configure the necessary cloud resources
                - Deploy your project to the selected platform
                - Provide you with the deployment URL
                
                ### Command Options
                
                `--project-path <PATH>` - Specify the path to your project directory. Defaults to the current directory.
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy deploy --project-path /path/to/your/project")
                
            | new Markdown("`--verbose` - Enable verbose output for detailed logging during deployment.").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy deploy --verbose")
                
            | new Markdown(
                """"
                ### Interactive Mode
                
                When you run `ivy deploy` without specifying options, Ivy will guide you through an interactive deployment process:
                
                1. **Select Deployment Provider**: Choose from AWS, Azure, GCP, or Sliplane
                2. **Configuration Setup**: Configure provider-specific settings
                3. **Build Process**: Ivy will build and containerize your project
                4. **Deployment**: Deploy to the selected cloud platform
                
                ### Deployment Process
                
                **1. Project Validation** - Ivy validates your project before deployment: ensures it's an Ivy project, checks for required files and configuration, and validates authentication status.
                
                **2. Build Process** - Ivy builds your project: restores NuGet packages, builds the project, creates Docker container, and pushes to container registry.
                
                **3. Infrastructure Setup** - Ivy configures cloud resources: creates necessary cloud services, configures networking and security, and sets up monitoring and logging.
                
                **4. Project Deployment** - Ivy deploys your project: deploys container to cloud platform, configures environment variables, sets up custom domains (if configured), and provides deployment URL.
                
                ### Containerization
                
                **Docker Configuration** - Ivy automatically generates a `Dockerfile` for your project:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                # Base runtime image
                FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
                WORKDIR /app
                EXPOSE 80
                
                # Build stage
                FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
                ARG BUILD_CONFIGURATION=Release
                WORKDIR /src
                
                # Copy and restore
                COPY ["YourProject.csproj", "./"]
                RUN dotnet restore "YourProject.csproj"
                
                # Copy everything and build
                COPY . .
                RUN dotnet build "YourProject.csproj" -c $BUILD_CONFIGURATION -o /app/build
                
                # Publish stage
                FROM build AS publish
                ARG BUILD_CONFIGURATION=Release
                RUN dotnet publish "YourProject.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true
                
                # Final runtime image
                FROM base AS final
                WORKDIR /app
                COPY --from=publish /app/publish .
                
                # Set environment variables
                ENV PORT=80
                ENV ASPNETCORE_URLS="http://+:80"
                
                # Run the executable
                ENTRYPOINT ["dotnet","./YourProject.dll"]
                """",Languages.Text)
            | new Markdown("**Environment Configuration** - Ivy configures environment variables for your deployed application:").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                ### Database connection strings
                ConnectionStrings__MyDatabase="your-connection-string"
                
                ### Authentication settings
                AUTH0_DOMAIN="your-auth0-domain"
                AUTH0_CLIENT_ID="your-client-id"
                
                ### Project settings
                ASPNETCORE_ENVIRONMENT="Production"
                """",Languages.Text)
            | new Markdown(
                """"
                ### Security and Configuration
                
                **Secrets Management** - Ivy handles [secrets](app://onboarding/concepts/secrets) securely during deployment: connection strings stored as environment variables, authentication secrets configured securely, and API keys managed through cloud provider secrets.
                
                **Network Security** - Ivy configures network security: automatic SSL/TLS configuration, appropriate firewall rules, and network isolation (where applicable).
                
                ### Monitoring and Logging
                
                **Application Monitoring** - Ivy sets up monitoring for your deployed application: health checks (available at `/ivy/health`), performance and resource metrics, centralized log collection, and automated alerting for issues.
                
                **Cloud Provider Monitoring** - Each cloud provider offers specific monitoring tools: AWS CloudWatch, Azure Application Insights, and GCP Cloud Monitoring.
                
                ### Deployment Options
                
                **Build Choices** - Ivy offers different build options:
                
                **Local Build** - Builds container locally, requires Docker installed, and faster for development.
                
                **Cloud Build** - Builds in cloud provider, no local Docker required, and consistent build environment.
                
                **Scaling Configuration** - Configure application scaling: auto-scaling (automatic scaling based on demand), manual scaling (fixed number of instances), and min/max instances (scaling boundaries).
                
                ### Troubleshooting
                
                **Common Deployment Issues**
                
                **Build Failures** - Check that project builds locally, verify all dependencies are included, and check for compilation errors.
                
                **Container Registry Issues** - Verify container registry credentials, check network connectivity, and ensure proper permissions.
                
                **Cloud Provider Issues** - Verify cloud provider credentials, check resource quotas and limits, and ensure required services are enabled.
                
                **Debugging Deployment**
                
                **Enable Verbose Logging**
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy deploy --verbose")
                
            | new Markdown(
                """"
                **Check Cloud Provider Logs** - AWS CloudWatch logs, Azure Application Insights, and GCP Cloud Logging.
                
                **Verify Deployment Status**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                # Check deployment status in cloud console
                # Verify application is accessible
                # Check environment variables and configuration
                """",Languages.Text)
            | new Markdown(
                """"
                ## Examples
                
                ### Best Practices
                
                **Pre-deployment Checklist** - Test locally (ensure your project runs locally), check dependencies (verify all required services are configured), review configuration (check environment variables and settings), and security review (verify authentication and authorization setup).
                
                **Deployment Strategy** - Blue-green deployment (zero-downtime deployments), rolling updates (gradual deployment across instances), and canary deployments (test with subset of users).
                
                **Cost Optimization** - Right-sizing (choose appropriate instance sizes), auto-scaling (configure scaling based on actual usage), resource cleanup (remove unused resources), and monitoring (track resource usage and costs).
                
                **Security Best Practices** - Secrets management (use cloud provider secrets services), network security (configure appropriate firewall rules), access control (implement least-privilege access), and regular updates (keep dependencies and runtime updated).
                
                ### Post-deployment
                
                **Application Management** - After deployment, you can monitor performance (use cloud provider monitoring tools), scale application (adjust instance count based on demand), update application (deploy new versions), and configure domains (set up custom domains and SSL).
                
                **Maintenance** - Regular maintenance tasks include security updates (keep dependencies updated), performance monitoring (monitor and optimize performance), cost monitoring (track and optimize costs), and backup management (configure and test backups).
                
                ### Related Commands
                
                - `ivy init` - Initialize a new Ivy project
                - `ivy db add` - Add database connections
                - `ivy auth add` - Add authentication providers
                - `ivy app create` - Create apps
                
                ### Cloud Provider Documentation
                
                For detailed information about each cloud provider:
                
                - **AWS**: [AWS Documentation](https://docs.aws.amazon.com/)
                - **Azure**: [Azure Documentation](https://docs.microsoft.com/azure/)
                - **GCP**: [Google Cloud Documentation](https://cloud.google.com/docs/)
                - **Sliplane**: [Sliplane Documentation](https://docs.sliplane.io/)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ProgramApp), typeof(Onboarding.Concepts.SecretsApp)]; 
        return article;
    }
}

