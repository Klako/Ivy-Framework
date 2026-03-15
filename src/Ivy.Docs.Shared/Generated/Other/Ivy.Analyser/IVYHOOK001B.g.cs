using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Other.Ivy.Analyser;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/05_Other/Ivy.Analyser/IVYHOOK001B.md")]
public class IVYHOOK001BApp(bool onlyBody = false) : ViewBase
{
    public IVYHOOK001BApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivyhook001b-hook-used-in-nested-closure", "IVYHOOK001B: Hook Used in Nested Closure", 1), new ArticleHeading("description", "Description", 2), new ArticleHeading("cause", "Cause", 2), new ArticleHeading("fix", "Fix", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # IVYHOOK001B: Hook Used in Nested Closure
                
                **Severity:** Error
                
                ## Description
                
                Ivy hooks cannot be called inside lambdas, local functions, or anonymous methods — even when those are defined within `Build()`. Hooks must always execute in the same order on every render, and closures may execute conditionally, multiple times, or not at all.
                
                ## Cause
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ❌ Hook inside a lambda — triggers IVYHOOK001B
                public override object? Build()
                {
                    var items = UseState(new[] { "a", "b", "c" });
                
                    var urls = items.Value.Select(item =>
                        UseDownload(() => Encoding.UTF8.GetBytes(item), "text/plain", $"{item}.txt") // IVYHOOK001B
                    );
                
                    return Layout.Vertical();
                }
                """",Languages.Csharp)
            | new CodeBlock(
                """"
                // ❌ Hook inside a local function — triggers IVYHOOK001B
                public override object? Build()
                {
                    void SetupState()
                    {
                        var s = UseState(0); // IVYHOOK001B
                    }
                
                    SetupState();
                    return new Button();
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Fix
                
                Move the hook to the top level of `Build()`. If you need a hook per item in a collection, extract a child component so each instance manages its own hooks:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ✅ Extract a child component per item
                public override object? Build()
                {
                    var items = UseState(new[] { "a", "b", "c" });
                    return Layout.Vertical(
                        items.Value.Select((item, i) => new ItemDownloadView(item).Key($"dl-{i}"))
                    );
                }
                
                public class ItemDownloadView(string item) : ViewBase
                {
                    public override object? Build()
                    {
                        var url = UseDownload(
                            () => Encoding.UTF8.GetBytes(item), "text/plain", $"{item}.txt");
                
                        return url.Value != null
                            ? new Button($"Download {item}").Url(url.Value)
                            : Text.Block("Preparing...");
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## See Also
                
                - [Rules of Hooks](app://hooks/rules-of-hooks)
                - [UseDownload FAQ](app://hooks/core/use-download#faq)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Hooks.RulesOfHooksApp), typeof(Hooks.Core.UseDownloadApp)]; 
        return article;
    }
}

