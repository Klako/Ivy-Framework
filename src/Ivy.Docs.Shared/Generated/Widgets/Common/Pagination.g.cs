using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Common;

[App(order:9, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/03_Common/09_Pagination.md", searchHints: ["paging", "navigation", "pages", "next", "previous", "page-numbers"])]
public class PaginationApp(bool onlyBody = false) : ViewBase
{
    public PaginationApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("pagination", "Pagination", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("configuration", "Configuration", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Pagination").OnLinkClick(onLinkClick)
            | Lead("Display page [navigation](app://onboarding/concepts/navigation) controls for traversing large sets of data, with customizable appearance and dynamic updates.")
            | new Markdown(
                """"
                The `Pagination` [widget](app://onboarding/concepts/widgets) is used to provide navigation between multiple pages of content, such as [lists](app://widgets/common/list) or [tables](app://widgets/common/table). It displays a set of page links, previous/next buttons, and optional ellipsis for skipped ranges. The widget can be customized for appearance and supports binding to [state](app://hooks/core/use-state) for dynamic updates.
                
                ## Basic Usage
                
                Here's a simple example of a pagination control initialized at page 5 of 10.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicPaginationApp : ViewBase
                    {
                        public override object? Build() {
                            var page = UseState(5);
                    
                            return new Pagination(page.Value, 10, newPage => page.Set(newPage.Value));
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicPaginationApp())
            )
            | new Markdown(
                """"
                ## Configuration
                
                You can configure the number of visible pages adjacent to the current page (siblings) and at the start/end of the range (boundaries).
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new PaginationConfigurationApp())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class PaginationConfigurationApp : ViewBase
                    {
                        public override object? Build() {
                            var page = UseState(5);
                    
                            return Layout.Vertical().Gap(4)
                                | Text.P("Siblings").Large()
                                | (Layout.Vertical()
                                    | new Pagination(page.Value, 20, newPage => page.Set(newPage.Value)).Siblings(1)
                                    | new Pagination(page.Value, 20, newPage => page.Set(newPage.Value)).Siblings(2))
                                | Text.P("Boundaries").Large()
                                | (Layout.Vertical()
                                    | new Pagination(page.Value, 20, newPage => page.Set(newPage.Value)).Boundaries(1)
                                    | new Pagination(page.Value, 20, newPage => page.Set(newPage.Value)).Boundaries(2));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Pagination", "Ivy.PaginationExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Pagination.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.NavigationApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Widgets.Common.ListApp), typeof(Widgets.Common.TableApp), typeof(Hooks.Core.UseStateApp)]; 
        return article;
    }
}


public class BasicPaginationApp : ViewBase
{
    public override object? Build() {
        var page = UseState(5);

        return new Pagination(page.Value, 10, newPage => page.Set(newPage.Value));
    }
}

public class PaginationConfigurationApp : ViewBase
{
    public override object? Build() {
        var page = UseState(5);

        return Layout.Vertical().Gap(4)
            | Text.P("Siblings").Large()
            | (Layout.Vertical()
                | new Pagination(page.Value, 20, newPage => page.Set(newPage.Value)).Siblings(1)
                | new Pagination(page.Value, 20, newPage => page.Set(newPage.Value)).Siblings(2))
            | Text.P("Boundaries").Large()
            | (Layout.Vertical()
                | new Pagination(page.Value, 20, newPage => page.Set(newPage.Value)).Boundaries(1)
                | new Pagination(page.Value, 20, newPage => page.Set(newPage.Value)).Boundaries(2));
    }
}
