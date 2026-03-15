using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Common;

[App(order:5, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/03_Common/05_Details.md", searchHints: ["properties", "fields", "data", "information", "key-value", "details", "detail", "expandable", "label-value", "display data", "ToDetails", "copy to clipboard"])]
public class DetailsApp(bool onlyBody = false) : ViewBase
{
    public DetailsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("details", "Details", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("automatic-field-removal", "Automatic Field Removal", 3), new ArticleHeading("custom-field-removal", "Custom Field Removal", 3), new ArticleHeading("multi-line-fields", "Multi-Line Fields", 3), new ArticleHeading("custom-builders", "Custom Builders", 2), new ArticleHeading("copy-to-clipboard", "Copy to Clipboard", 3), new ArticleHeading("links", "Links", 3), new ArticleHeading("nested-objects", "Nested Objects", 2), new ArticleHeading("working-with-state", "Working with State", 2), new ArticleHeading("faq", "Faq", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Details").OnLinkClick(onLinkClick)
            | Lead("Display structured label-value pairs from models with automatic formatting using the [ToDetails()](app://onboarding/concepts/content-builders) extension method.")
            | new Markdown(
                """"
                `Detail` [widgets](app://onboarding/concepts/widgets) display label and value pairs. They are usually generated from a model using ToDetails().
                
                ## Basic Usage
                
                The simplest way to create details is by calling ToDetails() on any object:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new { Name = "John Doe", Email = "john@example.com", Age = 30 }
                        .ToDetails()
                    """",Languages.Csharp)
                | new Box().Content(new { Name = "John Doe", Email = "john@example.com", Age = 30 }
    .ToDetails())
            )
            | new Markdown(
                """"
                ### Automatic Field Removal
                
                Remove empty or null fields using the `RemoveEmpty()` method. This removes fields that are:
                
                - `null` values
                - Empty or whitespace strings
                - `false` boolean values
                
                Use this when you want to hide fields that don't have meaningful values, keeping your details clean and focused:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new { FirstName = "John", LastName = "Doe", Age = 30, MiddleName = "" }
    .ToDetails()
    .RemoveEmpty())),
                new Tab("Code", new CodeBlock(
                    """"
                    new { FirstName = "John", LastName = "Doe", Age = 30, MiddleName = "" }
                        .ToDetails()
                        .RemoveEmpty()
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Custom Field Removal
                
                Selectively remove specific fields using the `Remove()` method. This is useful when you want to hide sensitive information like IDs or internal fields from the [user interface](app://onboarding/concepts/views):
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new { Id = 123, Name = "John Doe", Email = "john@example.com" }
    .ToDetails()
    .Remove(x => x.Id))),
                new Tab("Code", new CodeBlock(
                    """"
                    new { Id = 123, Name = "John Doe", Email = "john@example.com" }
                        .ToDetails()
                        .Remove(x => x.Id)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Multi-Line Fields
                
                Mark specific fields as multi-line for better text display. This is perfect for long descriptions, notes, or any text content that would benefit from wrapping across multiple lines:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new { Name = "Widget", Description = "Long description text" }
    .ToDetails()
    .Multiline(x => x.Description))),
                new Tab("Code", new CodeBlock(
                    """"
                    new { Name = "Widget", Description = "Long description text" }
                        .ToDetails()
                        .Multiline(x => x.Description)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Custom Builders
                
                Override the default rendering for specific fields using custom builders. This allows you to customize how individual fields are displayed and add interactive functionality.
                
                ### Copy to Clipboard
                
                Make values copyable to clipboard. This is especially useful for IDs, email addresses, or any text that users might want to copy for use elsewhere:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new { Id = "ABC-123", Name = "John Doe" }
    .ToDetails()
    .Builder(x => x.Id, b => b.CopyToClipboard()))),
                new Tab("Code", new CodeBlock(
                    """"
                    new { Id = "ABC-123", Name = "John Doe" }
                        .ToDetails()
                        .Builder(x => x.Id, b => b.CopyToClipboard())
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Links
                
                Convert values to clickable [links](app://onboarding/concepts/navigation). Automatically transform URLs, email addresses, or any text into clickable links that users can interact with:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new { Name = "John Doe", Website = "https://example.com" }
    .ToDetails()
    .Builder(x => x.Website, b => b.Link()))),
                new Tab("Code", new CodeBlock(
                    """"
                    new { Name = "John Doe", Website = "https://example.com" }
                        .ToDetails()
                        .Builder(x => x.Website, b => b.Link())
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Nested Objects
                
                Details automatically handle nested objects by converting them to their own detail views. This creates a hierarchical display that's perfect for complex data structures with parent-child relationships:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new { 
    Name = "John", 
    Address = new { Street = "123 Main St", City = "Anytown" }.ToDetails() 
}.ToDetails())),
                new Tab("Code", new CodeBlock(
                    """"
                    new {
                        Name = "John",
                        Address = new { Street = "123 Main St", City = "Anytown" }.ToDetails()
                    }.ToDetails()
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Working with State
                
                Details work seamlessly with [reactive state](app://hooks/core/use-state). When the underlying data changes, the details automatically update to reflect the new values, making it perfect for dynamic, interactive interfaces:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(UseState(() => new { Name = "John Doe", Age = 30 })
    .ToDetails())),
                new Tab("Code", new CodeBlock(
                    """"
                    UseState(() => new { Name = "John Doe", Age = 30 })
                        .ToDetails()
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("## Faq").OnLinkClick(onLinkClick)
            | new Expandable("How do I customize field labels in Details.ToDetails()?",
                Vertical().Gap(4)
                | new Markdown(
                    """"
                    By default, `ToDetails()` generates labels from property names using PascalCase splitting (e.g., `NetBurn` becomes "Net Burn"). To override a label, use the `.Label()` method:
                    """").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    var data = new RunwayData(5000m, 10000m, 12, new DateTime(2027, 3, 1));
                    data.ToDetails()
                        .Label(x => x.NetBurn, "Net Monthly Burn")
                        .Label(x => x.RunwayDate, "Projected Runway End")
                        .Build();
                    """",Languages.Csharp)
                | new Markdown("Alternatively, for simple cases you can use anonymous types where property names become the labels:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    new { NetBurn = "$5,000", GrossBurn = "$10,000" }.ToDetails()
                    """",Languages.Csharp)
                | new Markdown("Use `.Builder(x => x.Field, b => ...)` to customize how a value is *rendered*, not to change the label text.").OnLinkClick(onLinkClick)
            )
            | new WidgetDocsView("Ivy.Details", "Ivy.DetailsBuilderExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Views/Builders/DetailsBuilder.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ContentBuildersApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.NavigationApp), typeof(Hooks.Core.UseStateApp)]; 
        return article;
    }
}

