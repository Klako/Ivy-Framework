using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:20, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/20_HtmlPipeline.md", searchHints: ["html", "pipeline", "filter", "meta", "head", "index", "shell"])]
public class HtmlPipelineApp(bool onlyBody = false) : ViewBase
{
    public HtmlPipelineApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("htmlpipeline", "HtmlPipeline", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("built-in-filters", "Built-in Filters", 2), new ArticleHeading("creating-a-custom-filter", "Creating a Custom Filter", 2), new ArticleHeading("registering-filters", "Registering Filters", 2), new ArticleHeading("append-a-single-filter", "Append a single filter", 3), new ArticleHeading("full-pipeline-customization", "Full pipeline customization", 3), new ArticleHeading("inspecting-the-pipeline", "Inspecting the Pipeline", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# HtmlPipeline").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Overview
                
                When a browser requests your Ivy application, the server serves an `index.html` shell that bootstraps the frontend. The HtmlPipeline runs a series of **filters** over this HTML document before it reaches the client. Each filter can inspect and modify the parsed HTML structure using `XDocument`.
                
                ## Built-in Filters
                
                | Filter | What it injects |
                |---|---|
                | **LicenseFilter** | `<meta name="ivy-license">` tag from `Ivy:License` configuration |
                | **DevToolsFilter** | `<meta name="ivy-enable-dev-tools">` when `EnableDevTools` is true |
                | **MetaDescriptionFilter** | `<meta name="description">` from `ServerArgs.MetaDescription` |
                | **TitleFilter** | Updates the existing `<title>` element with `ServerArgs.MetaTitle` |
                | **ThemeFilter** | Injects theme CSS `<style>` and `<meta name="ivy-theme">` from `IThemeService` |
                | **ManifestFilter** | `<link rel="manifest" href="/manifest.json">` when `ManifestOptions` is registered |
                
                ## Creating a Custom Filter
                
                Implement the `IHtmlFilter` interface. Filters receive a parsed `XDocument` and the `HtmlPipelineContext`, which provides access to `IServiceProvider` and `ServerArgs`.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                using System.Xml.Linq;
                using Ivy.Core.Server.HtmlPipeline;
                
                public class OpenGraphFilter : IHtmlFilter
                {
                    public void Process(HtmlPipelineContext context, XDocument document)
                    {
                        var head = document.Root?.Element("head");
                        if (head == null) return;
                
                        head.Add(new XElement("meta",
                            new XAttribute("property", "og:title"),
                            new XAttribute("content", "My App")));
                
                        head.Add(new XElement("meta",
                            new XAttribute("property", "og:description"),
                            new XAttribute("content", "Built with Ivy")));
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Registering Filters
                
                ### Append a single filter
                
                Use `Server.UseHtmlFilter()` to add a filter after all built-in filters:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var server = new Server();
                server.UseHtmlFilter(new OpenGraphFilter());
                await server.RunAsync();
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Full pipeline customization
                
                Use `Server.UseHtmlPipeline()` to access the full pipeline. You can clear, reorder, or replace filters entirely:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Replace the entire pipeline with a single custom filter
                server.UseHtmlPipeline(pipeline =>
                {
                    pipeline.Clear();
                    pipeline.Use<OpenGraphFilter>();
                });
                """",Languages.Csharp)
            | new CodeBlock(
                """"
                // Append additional filters via the pipeline configurator
                server.UseHtmlPipeline(pipeline =>
                {
                    pipeline.Use<OpenGraphFilter>();
                });
                """",Languages.Csharp)
            | new Markdown(
                """"
                The pipeline configurator runs **after** all built-in and custom filters have been added, so `Clear()` removes everything, giving you full control.
                
                ## Inspecting the Pipeline
                
                The `HtmlPipeline.Filters` property returns a read-only list of the currently registered filters:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                server.UseHtmlPipeline(pipeline =>
                {
                    foreach (var filter in pipeline.Filters)
                    {
                        Console.WriteLine(filter.GetType().Name);
                    }
                });
                """",Languages.Csharp)
            ;
        return article;
    }
}

