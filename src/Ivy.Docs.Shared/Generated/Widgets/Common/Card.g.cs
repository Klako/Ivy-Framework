using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Common;

[App(order:4, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/03_Common/04_Card.md", searchHints: ["container", "panel", "box", "section", "wrapper", "border"])]
public class CardApp(bool onlyBody = false) : ViewBase
{
    public CardApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("card", "Card", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("header-content-and-footer", "Header, Content, and Footer", 2), new ArticleHeading("click-listener", "Click Listener", 2), new ArticleHeading("disabled-state", "Disabled State", 2), new ArticleHeading("dashboard-metrics", "Dashboard Metrics", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Card").OnLinkClick(onLinkClick)
            | Lead("Organize content in visually grouped containers with headers, footers, and actions to create structured, professional [layouts](app://widgets/layouts/_index).")
            | new Markdown(
                """"
                The `Card` [widget](app://onboarding/concepts/widgets) is a versatile container used to group related content and actions in Ivy [apps](app://onboarding/concepts/apps). It can hold text, buttons, charts, and other [widgets](app://onboarding/concepts/widgets), making it a fundamental [building block](app://onboarding/concepts/views) for creating structured layouts.
                
                ## Basic Usage
                
                Here's a simple example of a card containing text and a button that shows a [toast message](app://onboarding/concepts/clients) when clicked. Use [Size](app://api-reference/ivy/size) for `.Width()` to control card width.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Card(
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nec purus nec nunc",
                        new Button("Sign Me Up", _ => client.Toast("You have signed up!"))
                    ).Title("Card App").Description("This is a card app.").Width(Size.Units(100))
                    """",Languages.Csharp)
                | new Box().Content(new Card(
    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nec purus nec nunc",
    new Button("Sign Me Up", _ => client.Toast("You have signed up!"))
).Title("Card App").Description("This is a card app.").Width(Size.Units(100)))
            )
            | new Markdown(
                """"
                ## Header, Content, and Footer
                
                Cards have three named slots: **Header**, **Content**, and **Footer**. Use the fluent API to set each slot independently.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Card()
                        .Header(Text.H4("Fluent API"))
                        .Content(Layout.Vertical()
                            | Text.P("Content and footer set fluently.")
                            | Text.Block("Second paragraph in content.")
                        )
                        .Footer(new Button("Action", _ => client.Toast("Footer action!")))
                        .Width(Size.Units(100))
                    """",Languages.Csharp)
                | new Box().Content(new Card()
    .Header(Text.H4("Fluent API"))
    .Content(Layout.Vertical()
        | Text.P("Content and footer set fluently.")
        | Text.Block("Second paragraph in content.")
    )
    .Footer(new Button("Action", _ => client.Toast("Footer action!")))
    .Width(Size.Units(100)))
            )
            | new Markdown(
                """"
                ## Click Listener
                
                OnClick attaches an event listener and makes the card clickable.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Card(
                        "This card is clickable."
                    ).Title("Clickable Card")
                     .Description("Demonstrating click and mouse hover.")
                     .OnClick(_ => client.Toast("Card clicked!"))
                     .Width(Size.Units(100))
                    """",Languages.Csharp)
                | new Box().Content(new Card(
    "This card is clickable."
).Title("Clickable Card")
 .Description("Demonstrating click and mouse hover.")
 .OnClick(_ => client.Toast("Card clicked!"))
 .Width(Size.Units(100)))
            )
            | new Markdown(
                """"
                ## Disabled State
                
                Use the `Disabled()` extension method to prevent user interaction with a card. This is useful for indicating unavailable options or read-only states.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Card(
                        "This card cannot be clicked."
                    ).Title("Disabled Card")
                     .Description("User interaction is disabled.")
                     .OnClick(_ => client.Toast("This won't fire!"))
                     .Disabled()
                     .Width(Size.Units(100))
                    """",Languages.Csharp)
                | new Box().Content(new Card(
    "This card cannot be clicked."
).Title("Disabled Card")
 .Description("User interaction is disabled.")
 .OnClick(_ => client.Toast("This won't fire!"))
 .Disabled()
 .Width(Size.Units(100)))
            )
            | new Markdown(
                """"
                ## Dashboard Metrics
                
                For dashboard applications, Ivy provides the specialized `MetricView` component that extends Card functionality with KPI-specific features like trend indicators and goal tracking. It uses [UseQuery](app://hooks/core/use-query) hooks for data fetching.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new MetricView(
                        "Revenue",
                        Icons.DollarSign,
                        ctx => ctx.UseQuery(
                            key: "revenue",
                            fetcher: () => Task.FromResult(new MetricRecord(
                                "$125,430",
                                0.12, // 12% increase
                                0.85, // 85% of goal
                                "Target: $150,000"
                            ))
                        )
                    )
                    """",Languages.Csharp)
                | new Box().Content(new MetricView(
    "Revenue",
    Icons.DollarSign,
    ctx => ctx.UseQuery(
        key: "revenue",
        fetcher: () => Task.FromResult(new MetricRecord(
            "$125,430",
            0.12, // 12% increase
            0.85, // 85% of goal
            "Target: $150,000"
        ))
    )
))
            )
            | new Markdown("The `MetricView` uses UseQuery hooks for data loading, which automatically handles loading states, error handling, and caching. It also displays trend arrows with color-coded indicators for performance tracking. See the [MetricView documentation](app://widgets/common/metric-view) for more details.").OnLinkClick(onLinkClick)
            | new WidgetDocsView("Ivy.Card", "Ivy.CardExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Card.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Widgets.Layouts._IndexApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.AppsApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.ClientsApp), typeof(ApiReference.Ivy.SizeApp), typeof(Hooks.Core.UseQueryApp), typeof(Widgets.Common.MetricViewApp)]; 
        return article;
    }
}

