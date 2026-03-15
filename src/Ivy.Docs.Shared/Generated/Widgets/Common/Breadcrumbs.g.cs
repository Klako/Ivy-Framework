using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Common;

[App(order:17, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/03_Common/17_Breadcrumbs.md", searchHints: ["breadcrumbs", "navigation", "trail", "hierarchy", "path", "crumbs"])]
public class BreadcrumbsApp(bool onlyBody = false) : ViewBase
{
    public BreadcrumbsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("breadcrumbs", "Breadcrumbs", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("with-navigation", "With Navigation", 2), new ArticleHeading("configuration-options", "Configuration Options", 2), new ArticleHeading("custom-separator", "Custom Separator", 3), new ArticleHeading("with-icons", "With Icons", 3), new ArticleHeading("tooltips", "Tooltips", 3), new ArticleHeading("disabled-state", "Disabled State", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Breadcrumbs").OnLinkClick(onLinkClick)
            | Lead("A secondary navigation component that shows the user's location within a hierarchy. Perfect for multi-level navigation and hierarchical applications.")
            | new Markdown(
                """"
                The `Breadcrumbs` [widget](app://onboarding/concepts/widgets) renders a navigation trail showing the user's location within a hierarchy. Each item is clickable (except the current/last item), enabling quick navigation to ancestor pages.
                
                ## Basic Usage
                
                Create a simple breadcrumb trail:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Breadcrumbs(
                        new BreadcrumbItem("Home", () => { }),
                        new BreadcrumbItem("Products", () => { }),
                        new BreadcrumbItem("Details")
                    )
                    """",Languages.Csharp)
                | new Box().Content(new Breadcrumbs(
    new BreadcrumbItem("Home", () => { }),
    new BreadcrumbItem("Products", () => { }),
    new BreadcrumbItem("Details")
))
            )
            | new Markdown(
                """"
                The last item in the list is rendered as non-clickable, representing the current page.
                
                ## With Navigation
                
                Integrate breadcrumbs with state management for navigation:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BreadcrumbsNavigationDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BreadcrumbsNavigationDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var currentPage = UseState("Details");
                    
                            return new Breadcrumbs(
                                new BreadcrumbItem("Home", () => currentPage.Set("Home")),
                                new BreadcrumbItem("Products", () => currentPage.Set("Products")),
                                new BreadcrumbItem("Product Details")
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Configuration Options
                
                ### Custom Separator
                
                Change the separator character between items:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Breadcrumbs(
                        new BreadcrumbItem("Home", () => { }),
                        new BreadcrumbItem("Products", () => { }),
                        new BreadcrumbItem("Details")
                    ).Separator(">")
                    """",Languages.Csharp)
                | new Box().Content(new Breadcrumbs(
    new BreadcrumbItem("Home", () => { }),
    new BreadcrumbItem("Products", () => { }),
    new BreadcrumbItem("Details")
).Separator(">"))
            )
            | new Markdown(
                """"
                ### With Icons
                
                Add icons to breadcrumb items:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Breadcrumbs(
                        new BreadcrumbItem("Home", () => { }, Icons.House),
                        new BreadcrumbItem("Products", () => { }, Icons.ShoppingCart),
                        new BreadcrumbItem("Details")
                    )
                    """",Languages.Csharp)
                | new Box().Content(new Breadcrumbs(
    new BreadcrumbItem("Home", () => { }, Icons.House),
    new BreadcrumbItem("Products", () => { }, Icons.ShoppingCart),
    new BreadcrumbItem("Details")
))
            )
            | new Markdown(
                """"
                ### Tooltips
                
                Add helpful tooltips to items:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BreadcrumbsTooltipDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BreadcrumbsTooltipDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            return new Breadcrumbs(
                                new BreadcrumbItem("Home", () => { }, Tooltip: "Go to homepage"),
                                new BreadcrumbItem("Products", () => { }, Tooltip: "View all products"),
                                new BreadcrumbItem("Details")
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Disabled State
                
                Disable the entire breadcrumb trail or individual items:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Breadcrumbs(
                        new BreadcrumbItem("Home", () => { }),
                        new BreadcrumbItem("Products", Disabled: true),
                        new BreadcrumbItem("Details")
                    ).Disabled()
                    """",Languages.Csharp)
                | new Box().Content(new Breadcrumbs(
    new BreadcrumbItem("Home", () => { }),
    new BreadcrumbItem("Products", Disabled: true),
    new BreadcrumbItem("Details")
).Disabled())
            )
            | new WidgetDocsView("Ivy.Breadcrumbs", "Ivy.BreadcrumbsExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Breadcrumbs.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp)]; 
        return article;
    }
}


public class BreadcrumbsNavigationDemo : ViewBase
{
    public override object? Build()
    {
        var currentPage = UseState("Details");

        return new Breadcrumbs(
            new BreadcrumbItem("Home", () => currentPage.Set("Home")),
            new BreadcrumbItem("Products", () => currentPage.Set("Products")),
            new BreadcrumbItem("Product Details")
        );
    }
}

public class BreadcrumbsTooltipDemo : ViewBase
{
    public override object? Build()
    {
        return new Breadcrumbs(
            new BreadcrumbItem("Home", () => { }, Tooltip: "Go to homepage"),
            new BreadcrumbItem("Products", () => { }, Tooltip: "View all products"),
            new BreadcrumbItem("Details")
        );
    }
}
