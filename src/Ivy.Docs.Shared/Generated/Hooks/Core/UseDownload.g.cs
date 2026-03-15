using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Hooks.Core;

[App(order:15, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/03_Hooks/02_Core/15_UseDownload.md", searchHints: ["download", "usedownload", "file-download", "blob", "file-export", "download-link"])]
public class UseDownloadApp(bool onlyBody = false) : ViewBase
{
    public UseDownloadApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("usedownload", "UseDownload", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("common-patterns", "Common Patterns", 2), new ArticleHeading("csv-export", "CSV Export", 3), new ArticleHeading("multiple-format-export", "Multiple Format Export", 3), new ArticleHeading("faq", "Faq", 2), new ArticleHeading("how-do-i-create-multiple-downloads-for-items-in-a-collection", "How do I create multiple downloads for items in a collection?", 3), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# UseDownload").OnLinkClick(onLinkClick)
            | Lead("The `UseDownload` [hook](app://hooks/rules-of-hooks) enables file downloads in your [application](app://onboarding/concepts/apps), generating download links for files created on-demand.")
            | new Markdown(
                """"
                ## Overview
                
                The `UseDownload` [hook](app://hooks/rules-of-hooks) provides file download functionality:
                
                - **On-Demand Generation** - Generate files dynamically when needed
                - **Async Support** - Support for asynchronous file generation
                - **MIME Types** - Specify content types for proper file handling
                - **Automatic Cleanup** - Downloads are automatically cleaned up when components unmount
                
                ## Basic Usage
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class DownloadBasicDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var content = UseState("Hello, World!");
                    
                            var downloadUrl = UseDownload(
                                factory: () => System.Text.Encoding.UTF8.GetBytes(content.Value),
                                mimeType: "text/plain",
                                fileName: "hello.txt"
                            );
                    
                            return Layout.Vertical()
                                | content.ToTextInput("Content")
                                | (downloadUrl.Value != null
                                    ? new Button("Download File").Url(downloadUrl.Value)
                                    : Text.Block("Preparing download..."));
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new DownloadBasicDemo())
            )
            | new Markdown(
                """"
                ## Common Patterns
                
                ### CSV Export
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class DownloadCsvExportDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var name = UseState("John Doe");
                            var email = UseState("john@example.com");
                            var age = UseState(30);
                    
                            var downloadUrl = UseDownload(
                                factory: () =>
                                {
                                    var csv = $"Name,Email,Age\n{name.Value},{email.Value},{age.Value}";
                                    return System.Text.Encoding.UTF8.GetBytes(csv);
                                },
                                mimeType: "text/csv",
                                fileName: $"export-{DateTime.Now:yyyy-MM-dd}.csv"
                            );
                    
                            return Layout.Vertical()
                                | name.ToTextInput("Name")
                                | email.ToTextInput("Email")
                                | age.ToNumberInput("Age")
                                | (downloadUrl.Value != null
                                    ? new Button("Export to CSV").Url(downloadUrl.Value).Variant(ButtonVariant.Primary)
                                    : Text.Block("Preparing export..."));
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new DownloadCsvExportDemo())
            )
            | new Markdown("### Multiple Format Export").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class DownloadMultiFormatDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var id = UseState(1);
                            var name = UseState("Sample Item");
                    
                            var csvUrl = UseDownload(
                                factory: () =>
                                {
                                    var csv = $"Id,Name\n{id.Value},{name.Value}";
                                    return System.Text.Encoding.UTF8.GetBytes(csv);
                                },
                                mimeType: "text/csv",
                                fileName: "export.csv"
                            );
                    
                            var jsonUrl = UseDownload(
                                factory: () => System.Text.Encoding.UTF8.GetBytes(
                                    $"{{\"id\":{id.Value},\"name\":\"{name.Value}\"}}"),
                                mimeType: "application/json",
                                fileName: "export.json"
                            );
                    
                            return Layout.Vertical()
                                | id.ToNumberInput("ID")
                                | name.ToTextInput("Name")
                                | (Layout.Horizontal()
                                    | (csvUrl.Value != null ? new Button("Download CSV").Url(csvUrl.Value) : null)
                                    | (jsonUrl.Value != null ? new Button("Download JSON").Url(jsonUrl.Value) : null));
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new DownloadMultiFormatDemo())
            )
            | new Markdown(
                """"
                ## Faq
                
                ### How do I create multiple downloads for items in a collection?
                
                You cannot call `UseDownload` inside a loop or lambda — hooks must be called at the top level of `Build()`. The idiomatic solution (same as React) is to **extract a child component** for each item. Each child component instance has its own isolated hook state:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ❌ WRONG — hook inside Select lambda (triggers IVYHOOK001B)
                items.Select(item => UseDownload(() => item.Bytes, "image/png", "file.png"));
                
                // ❌ VERBOSE — pre-creating a fixed number of downloads at top level
                var download1 = UseDownload(() => GetBytes(0), "image/png", "item-1.png");
                var download2 = UseDownload(() => GetBytes(1), "image/png", "item-2.png");
                // ... repeating for each item
                
                // ✅ IDIOMATIC — extract a child component per item
                public override object? Build()
                {
                    var items = UseState(GetItems());
                    return Layout.Vertical(
                        items.Value.Select((item, i) => new DownloadItemView(item).Key($"dl-{i}"))
                    );
                }
                
                // Each child component manages its own UseDownload hook
                public class DownloadItemView(ItemData item) : ViewBase
                {
                    public override object? Build()
                    {
                        var downloadUrl = UseDownload(
                            factory: () => item.GenerateBytes(),
                            mimeType: "image/png",
                            fileName: item.FileName
                        );
                
                        return Layout.Horizontal()
                            | Text.Block(item.Name)
                            | (downloadUrl.Value != null
                                ? new Button("Download").Url(downloadUrl.Value)
                                : Text.Block("Preparing..."));
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                For a **fixed, small** number of format exports (e.g., CSV + JSON of the same data), multiple `UseDownload` calls at the top level of a single component is fine — see the [Multiple Format Export](#multiple-format-export) example above.
                
                ## See Also
                
                - [State](app://hooks/core/use-state) - Component state management
                - [Effects](app://hooks/core/use-effect) - Side effects and lifecycle
                - [Clients](app://onboarding/concepts/clients) - Client-side interactions including `DownloadFile()`
                - [Rules of Hooks](app://hooks/rules-of-hooks) - Understanding hook rules and best practices
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Hooks.RulesOfHooksApp), typeof(Onboarding.Concepts.AppsApp), typeof(Hooks.Core.UseStateApp), typeof(Hooks.Core.UseEffectApp), typeof(Onboarding.Concepts.ClientsApp)]; 
        return article;
    }
}


public class DownloadBasicDemo : ViewBase
{
    public override object? Build()
    {
        var content = UseState("Hello, World!");
        
        var downloadUrl = UseDownload(
            factory: () => System.Text.Encoding.UTF8.GetBytes(content.Value),
            mimeType: "text/plain",
            fileName: "hello.txt"
        );

        return Layout.Vertical()
            | content.ToTextInput("Content")
            | (downloadUrl.Value != null
                ? new Button("Download File").Url(downloadUrl.Value)
                : Text.Block("Preparing download..."));
    }
}

public class DownloadCsvExportDemo : ViewBase
{
    public override object? Build()
    {
        var name = UseState("John Doe");
        var email = UseState("john@example.com");
        var age = UseState(30);

        var downloadUrl = UseDownload(
            factory: () =>
            {
                var csv = $"Name,Email,Age\n{name.Value},{email.Value},{age.Value}";
                return System.Text.Encoding.UTF8.GetBytes(csv);
            },
            mimeType: "text/csv",
            fileName: $"export-{DateTime.Now:yyyy-MM-dd}.csv"
        );

        return Layout.Vertical()
            | name.ToTextInput("Name")
            | email.ToTextInput("Email")
            | age.ToNumberInput("Age")
            | (downloadUrl.Value != null
                ? new Button("Export to CSV").Url(downloadUrl.Value).Variant(ButtonVariant.Primary)
                : Text.Block("Preparing export..."));
    }
}

public class DownloadMultiFormatDemo : ViewBase
{
    public override object? Build()
    {
        var id = UseState(1);
        var name = UseState("Sample Item");

        var csvUrl = UseDownload(
            factory: () =>
            {
                var csv = $"Id,Name\n{id.Value},{name.Value}";
                return System.Text.Encoding.UTF8.GetBytes(csv);
            },
            mimeType: "text/csv",
            fileName: "export.csv"
        );

        var jsonUrl = UseDownload(
            factory: () => System.Text.Encoding.UTF8.GetBytes(
                $"{{\"id\":{id.Value},\"name\":\"{name.Value}\"}}"),
            mimeType: "application/json",
            fileName: "export.json"
        );

        return Layout.Vertical()
            | id.ToNumberInput("ID")
            | name.ToTextInput("Name")
            | (Layout.Horizontal()
                | (csvUrl.Value != null ? new Button("Download CSV").Url(csvUrl.Value) : null)
                | (jsonUrl.Value != null ? new Button("Download JSON").Url(jsonUrl.Value) : null));
    }
}
