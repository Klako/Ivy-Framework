using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:19, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/19_Embed.md", searchHints: ["iframe", "external", "integration", "widget", "embed", "content"])]
public class EmbedApp(bool onlyBody = false) : ViewBase
{
    public EmbedApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("embed", "Embed", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("social-media-posts", "Social Media Posts", 3), new ArticleHeading("github-content", "GitHub Content", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Embed").OnLinkClick(onLinkClick)
            | Lead("Embed external content from social media platforms, code repositories, and other web resources with automatic responsive containers and platform-specific optimizations.")
            | new Markdown(
                """"
                The `Embed` [widget](app://onboarding/concepts/widgets) allows you to incorporate external content such as videos, social media posts, code repositories, and other web resources into your [app](app://onboarding/concepts/apps). It automatically detects the content type and creates an appropriate responsive container.
                
                ## Basic Usage
                
                Simply provide a URL to embed content from supported platforms:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Embed("https://www.youtube.com/watch?v=dQw4w9WgXcQ")
                    """",Languages.Csharp)
                | new Box().Content(new Embed("https://www.youtube.com/watch?v=dQw4w9WgXcQ"))
            )
            | new Markdown("### Social Media Posts").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(4)
    | Text.H4("Twitter Tweet")
    | new Embed("https://publish.twitter.com/?url=https://twitter.com/_devJNS/status/1969643853691949555#")
    | Text.H4("Instagram Post")
    | new Embed("https://www.instagram.com/p/CSGnc0GlZ7R/?img_index=1")
    | Text.H4("LinkedIn Post")
    | new Embed("https://www.linkedin.com/posts/ivy-interactive_ai-dotnet-opensource-activity-7377309652004331520-YjqC")
    | Text.H4("Reddit Post")
    | new Embed("https://www.reddit.com/r/cats/comments/1nr7fbs/show_them/")
    | Text.H4("Pinterest Pin")
    | new Embed("https://pin.it/i/4yA1hkh77/")
    | Text.H4("Facebook post")
    | new Embed("https://www.facebook.com/share/p/1NRYEoLAnJ/")
    | Text.H4("TikTok Video")
    | new Embed("https://www.tiktok.com/@ivan.wllb/video/7550352363689741590"))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(4)
                        | Text.H4("Twitter Tweet")
                        | new Embed("https://publish.twitter.com/?url=https://twitter.com/_devJNS/status/1969643853691949555#")
                        | Text.H4("Instagram Post")
                        | new Embed("https://www.instagram.com/p/CSGnc0GlZ7R/?img_index=1")
                        | Text.H4("LinkedIn Post")
                        | new Embed("https://www.linkedin.com/posts/ivy-interactive_ai-dotnet-opensource-activity-7377309652004331520-YjqC")
                        | Text.H4("Reddit Post")
                        | new Embed("https://www.reddit.com/r/cats/comments/1nr7fbs/show_them/")
                        | Text.H4("Pinterest Pin")
                        | new Embed("https://pin.it/i/4yA1hkh77/")
                        | Text.H4("Facebook post")
                        | new Embed("https://www.facebook.com/share/p/1NRYEoLAnJ/")
                        | Text.H4("TikTok Video")
                        | new Embed("https://www.tiktok.com/@ivan.wllb/video/7550352363689741590")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### GitHub Content").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(4)
    | Text.H4("Repository")
    | new Embed("https://github.com/Ivy-Interactive/Ivy-Framework")
    | Text.H4("Issue")
    | new Embed("https://github.com/Ivy-Interactive/Ivy-Framework/issues/935")
    | Text.H4("Pull Request")
    | new Embed("https://github.com/Ivy-Interactive/Ivy-Framework/pull/123")
    | Text.H4("Gist")
    | new Embed("https://gist.github.com/username/gistid")
    | Text.H4("GitHub Codespace")
    | new Embed("https://github.com/codespaces/new?hide_repo_select=true&ref=main&repo=Ivy-Interactive%2FIvy-Examples&machine=standardLinux32gb&devcontainer_path=.devcontainer%2Fqrcoder%2Fdevcontainer.json&location=EuropeWest"))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(4)
                        | Text.H4("Repository")
                        | new Embed("https://github.com/Ivy-Interactive/Ivy-Framework")
                        | Text.H4("Issue")
                        | new Embed("https://github.com/Ivy-Interactive/Ivy-Framework/issues/935")
                        | Text.H4("Pull Request")
                        | new Embed("https://github.com/Ivy-Interactive/Ivy-Framework/pull/123")
                        | Text.H4("Gist")
                        | new Embed("https://gist.github.com/username/gistid")
                        | Text.H4("GitHub Codespace")
                        | new Embed("https://github.com/codespaces/new?hide_repo_select=true&ref=main&repo=Ivy-Interactive%2FIvy-Examples&machine=standardLinux32gb&devcontainer_path=.devcontainer%2Fqrcoder%2Fdevcontainer.json&location=EuropeWest")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Embed", "Ivy.EmbedExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Embed.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.AppsApp)]; 
        return article;
    }
}

