using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:17, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/17_Json.md", searchHints: ["data", "format", "json", "syntax", "structure", "object"])]
public class JsonApp(bool onlyBody = false) : ViewBase
{
    public JsonApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("json", "Json", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # Json
                
                The `Json` [widget](app://onboarding/concepts/widgets) displays JSON data in a formatted, syntax-highlighted view. It's useful for debugging, data visualization, and displaying API responses.
                
                ## Basic Usage
                
                The simplest way to display JSON data is by passing a serialized string directly to the Json widget.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicJsonExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicJsonExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var simpleData = new
                            {
                                name = "John Doe",
                                age = 30,
                                isActive = true,
                                tags = new[] { "developer", "designer", "architect" }
                            };
                    
                            return Layout.Vertical().Gap(4)
                                | new Json(System.Text.Json.JsonSerializer.Serialize(simpleData));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Json", "Ivy.JsonExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Json.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp)]; 
        return article;
    }
}


public class BasicJsonExample : ViewBase
{
    public override object? Build()
    {
        var simpleData = new
        {
            name = "John Doe",
            age = 30,
            isActive = true,
            tags = new[] { "developer", "designer", "architect" }
        };
        
        return Layout.Vertical().Gap(4)
            | new Json(System.Text.Json.JsonSerializer.Serialize(simpleData));
    }
}
