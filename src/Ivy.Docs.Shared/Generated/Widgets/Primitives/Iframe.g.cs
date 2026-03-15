using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:20, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/20_Iframe.md", searchHints: ["embed", "external", "frame", "integration", "website", "content"])]
public class IframeApp(bool onlyBody = false) : ViewBase
{
    public IframeApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("iframe", "Iframe", 1), new ArticleHeading("use-cases", "Use Cases", 2), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("common-patterns", "Common Patterns", 2), new ArticleHeading("conditional-render-of-iframes", "Conditional render of iframes", 3), new ArticleHeading("refreshable-content", "Refreshable Content", 3), new ArticleHeading("responsive-iframe", "Responsive Iframe", 3), new ArticleHeading("refresh-token", "Refresh Token", 2), new ArticleHeading("how-refresh-token-works", "How Refresh Token Works", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Iframe").OnLinkClick(onLinkClick)
            | Lead("Embed external web pages securely within your application using contained browsing contexts with proper security boundaries.")
            | new Markdown(
                """"
                The `Iframe` [widget](app://onboarding/concepts/widgets) embeds external web pages into your [app](app://onboarding/concepts/apps). It creates a contained browsing context that can display content from other websites while maintaining security boundaries.
                
                ## Use Cases
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                graph LR
                    A[Iframe Widget] --> B[External Dashboards]
                    A --> C[Third-party Tools]
                    A --> D[Documentation Sites]
                    A --> E[Media Content]
                    A --> F[Legacy Applications]
                
                    B --> B1[Grafana, Kibana]
                    C --> C1[Calendar, Maps, Forms]
                    D --> D1[Help pages, Wikis]
                    E --> E1[Videos, Interactive content]
                    F --> F1[Existing web apps]
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Basic Usage
                
                The simplest iframe displays external content:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new Iframe("https://example.com"))),
                new Tab("Code", new CodeBlock(
                    """"
                    new Iframe("https://example.com")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Common Patterns
                
                ### Conditional render of iframes
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ToolsDashboardView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ToolsDashboardView : ViewBase
                    {
                        public override object? Build()
                        {
                            var selectedTool = UseState("docs");
                    
                            object GetSelectedTool() => selectedTool.Value switch
                            {
                                "docs" => new Iframe("https://docs.ivy.app/")
                                    .Width(Size.Full())
                                    .Height(Size.Units(150)),
                    
                                "samples" => new Iframe("https://samples.ivy.app")
                                    .Width(Size.Full())
                                    .Height(Size.Units(150)),
                    
                                "website" => new Iframe("https://ivy.app")
                                    .Width(Size.Full())
                                    .Height(Size.Units(150)),
                    
                                _ => Text.H3("Please select a tool")
                            };
                    
                            return Layout.Vertical().Gap(4)
                                | Text.P("Ivy Framework Pages")
                                | (Layout.Horizontal().Gap(2)
                                    | new Button("Ivy Docs", onClick: _ => selectedTool.Set("docs"))
                                    | new Button("Ivy Samples", onClick: _ => selectedTool.Set("samples"))
                                    | new Button("Ivy Website", onClick: _ => selectedTool.Set("website")))
                                | GetSelectedTool();
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Refreshable Content").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new RefreshableIframeView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class RefreshableIframeView : ViewBase
                    {
                        public override object? Build()
                        {
                            var refreshToken = UseRefreshToken();
                            var url = UseState("https://httpbin.org/uuid");
                    
                            return Layout.Vertical().Gap(4)
                                | Layout.Horizontal().Gap(2)
                                    | url.ToTextInput(placeholder: "Enter URL...")
                                    | new Button("Refresh", onClick: _ => refreshToken.Refresh())
                                        .Icon(Icons.RotateCcw)
                                | new Iframe(url.Value, refreshToken.Token.GetHashCode())
                                    .Width(Size.Full())
                                    .Height(Size.Units(120));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Responsive Iframe").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ResponsiveIframeView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ResponsiveIframeView : ViewBase
                    {
                        public override object? Build()
                        {
                            var aspectRatio = UseState("16:9");
                            var url = UseState("https://www.youtube.com/embed/dQw4w9WgXcQ");
                    
                            const int baseWidth = 120;
                    
                            var height = aspectRatio.Value switch
                            {
                                "16:9" => Size.Units(baseWidth * 9 / 16),  // 67
                                "4:3" => Size.Units(baseWidth * 3 / 4),   // 90
                                "1:1" => Size.Units(baseWidth),           // 120
                                _ => Size.Units(baseWidth * 9 / 16)       // Default to 16:9
                            };
                    
                            return Layout.Vertical().Gap(4)
                                | (Layout.Horizontal().Gap(2)
                                    | Text.Label("Aspect Ratio:")
                                    | new Button("16:9", onClick: _ => aspectRatio.Set("16:9"))
                                    | new Button("4:3", onClick: _ => aspectRatio.Set("4:3"))
                                    | new Button("1:1", onClick: _ => aspectRatio.Set("1:1")))
                                | new Iframe(url.Value)
                                    .Width(Size.Units(baseWidth))
                                    .Height(height);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Refresh Token
                
                The `RefreshToken` parameter forces the iframe to reload when changed:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class RefreshTokenExample : ViewBase
                {
                    public override object? Build()
                    {
                        var refreshToken = UseRefreshToken();
                
                        return Layout.Vertical()
                            | new Button("Reload Content", onClick: _ => refreshToken.Refresh())
                            | new Iframe("https://example.com", refreshToken.Token.GetHashCode());
                    }
                }
                """",Languages.Csharp)
            | new Markdown("### How Refresh Token Works").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                sequenceDiagram
                    participant U as User
                    participant C as Component
                    participant I as Iframe
                    participant S as Server
                
                    U->>C: Click "Refresh"
                    C->>C: refreshToken.Refresh()
                    Note over C: Generates new GUID
                    C->>I: Update RefreshToken prop
                    I->>I: Change iframe key
                    Note over I: Forces DOM re-creation
                    I->>S: New HTTP request
                    S-->>I: Fresh content
                ```
                """").OnLinkClick(onLinkClick)
            | new WidgetDocsView("Ivy.Iframe", "Ivy.IframeExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Iframe.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.AppsApp)]; 
        return article;
    }
}


public class ToolsDashboardView : ViewBase
{
    public override object? Build()
    {
        var selectedTool = UseState("docs");
        
        object GetSelectedTool() => selectedTool.Value switch
        {
            "docs" => new Iframe("https://docs.ivy.app/")
                .Width(Size.Full())
                .Height(Size.Units(150)),
            
            "samples" => new Iframe("https://samples.ivy.app")
                .Width(Size.Full())
                .Height(Size.Units(150)),
            
            "website" => new Iframe("https://ivy.app")
                .Width(Size.Full())
                .Height(Size.Units(150)),
                
            _ => Text.H3("Please select a tool")
        };
        
        return Layout.Vertical().Gap(4)
            | Text.P("Ivy Framework Pages")
            | (Layout.Horizontal().Gap(2)
                | new Button("Ivy Docs", onClick: _ => selectedTool.Set("docs"))
                | new Button("Ivy Samples", onClick: _ => selectedTool.Set("samples"))
                | new Button("Ivy Website", onClick: _ => selectedTool.Set("website")))
            | GetSelectedTool();
    }
}

public class RefreshableIframeView : ViewBase
{
    public override object? Build()
    {
        var refreshToken = UseRefreshToken();
        var url = UseState("https://httpbin.org/uuid");
        
        return Layout.Vertical().Gap(4)
            | Layout.Horizontal().Gap(2)
                | url.ToTextInput(placeholder: "Enter URL...")
                | new Button("Refresh", onClick: _ => refreshToken.Refresh())
                    .Icon(Icons.RotateCcw)
            | new Iframe(url.Value, refreshToken.Token.GetHashCode())
                .Width(Size.Full())
                .Height(Size.Units(120));
    }
}

public class ResponsiveIframeView : ViewBase
{
    public override object? Build()
    {
        var aspectRatio = UseState("16:9");
        var url = UseState("https://www.youtube.com/embed/dQw4w9WgXcQ");
        
        const int baseWidth = 120;
        
        var height = aspectRatio.Value switch
        {
            "16:9" => Size.Units(baseWidth * 9 / 16),  // 67
            "4:3" => Size.Units(baseWidth * 3 / 4),   // 90
            "1:1" => Size.Units(baseWidth),           // 120
            _ => Size.Units(baseWidth * 9 / 16)       // Default to 16:9
        };
        
        return Layout.Vertical().Gap(4)
            | (Layout.Horizontal().Gap(2)
                | Text.Label("Aspect Ratio:")
                | new Button("16:9", onClick: _ => aspectRatio.Set("16:9"))
                | new Button("4:3", onClick: _ => aspectRatio.Set("4:3"))
                | new Button("1:1", onClick: _ => aspectRatio.Set("1:1")))
            | new Iframe(url.Value)
                .Width(Size.Units(baseWidth))
                .Height(height);
    }
}
