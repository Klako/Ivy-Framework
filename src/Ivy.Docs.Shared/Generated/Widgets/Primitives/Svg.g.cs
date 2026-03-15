using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:18, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/18_Svg.md", searchHints: ["vector", "graphics", "svg", "image", "scalable", "illustration"])]
public class SvgApp(bool onlyBody = false) : ViewBase
{
    public SvgApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("svg", "Svg", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("animated-svg", "Animated Svg", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Svg").OnLinkClick(onLinkClick)
            | Lead("Create beautiful, scalable vector graphics directly in your [app](app://onboarding/concepts/apps) with the `Svg` [widget](app://onboarding/concepts/widgets). Perfect for icons, illustrations, charts, and other graphics that need to scale without losing quality.")
            | new Markdown(
                """"
                The `Svg` widget renders scalable vector graphics directly in your app. SVGs are resolution-independent and perfect for icons, illustrations, charts, and other graphics that need to scale without losing quality.
                
                ## Basic Usage
                
                The simplest way to create an SVG is to pass SVG markup as a string:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class SimpleSvgView : ViewBase
                    {
                        public override object? Build()
                        {
                            var simpleCircle = """
                                <svg width="100" height="100">
                                    <circle cx="50" cy="50" r="40" fill="green" />
                                </svg>
                                """;
                    
                            return new Svg(simpleCircle);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new SimpleSvgView())
            )
            | new Callout(
                """"
                The `Svg` widget has the following properties:
                - Content (string): The SVG markup content to render
                - Width (Size): The width of the SVG container (defaults to Auto)
                - Height (Size): The height of the SVG container (defaults to Auto)
                """", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Animated Svg
                
                This example demonstrates how to create an animated SVG progress bar using the `<animate>` element. The animation continuously cycles the width of the red rectangle between 80 and 160 pixels over 3 seconds, creating a smooth loading effect.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SimpleSvgView1())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SimpleSvgView1 : ViewBase
                    {
                        public override object? Build()
                        {
                            var animatedBar = """
                                        <svg width="200" height="20" viewBox="0 0 200 20">
                                            <rect width="200" height="20" fill="#e5e7eb" rx="10"/>
                                            <rect width="80" height="20" fill="#ef4444" rx="10">
                                                <animate attributeName="width" values="80;160;80" dur="3s" repeatCount="indefinite"/>
                                            </rect>
                                        </svg>
                                        """;
                            return new Svg(animatedBar);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Svg", "Ivy.SvgExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Svg.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.AppsApp), typeof(Onboarding.Concepts.WidgetsApp)]; 
        return article;
    }
}


public class SimpleSvgView : ViewBase
{
    public override object? Build()
    {
        var simpleCircle = """
            <svg width="100" height="100">
                <circle cx="50" cy="50" r="40" fill="green" />
            </svg>
            """;
            
        return new Svg(simpleCircle);
    }
}

public class SimpleSvgView1 : ViewBase
{
    public override object? Build()
    {
        var animatedBar = """
                    <svg width="200" height="20" viewBox="0 0 200 20">
                        <rect width="200" height="20" fill="#e5e7eb" rx="10"/>
                        <rect width="80" height="20" fill="#ef4444" rx="10">
                            <animate attributeName="width" values="80;160;80" dur="3s" repeatCount="indefinite"/>
                        </rect>
                    </svg>
                    """;
        return new Svg(animatedBar);
    }
}
