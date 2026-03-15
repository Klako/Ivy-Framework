using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Other.Ivy.Analyser;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/05_Other/Ivy.Analyser/IVYHOOK003.md")]
public class IVYHOOK003App(bool onlyBody = false) : ViewBase
{
    public IVYHOOK003App() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivyhook003-hook-called-in-loop", "IVYHOOK003: Hook Called in Loop", 1), new ArticleHeading("description", "Description", 2), new ArticleHeading("cause", "Cause", 2), new ArticleHeading("fix", "Fix", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # IVYHOOK003: Hook Called in Loop
                
                **Severity:** Warning
                
                ## Description
                
                Hooks cannot be called inside `for`, `foreach`, `while`, or `do-while` loops. The number of loop iterations may change between renders, which would change the hook call order and corrupt state.
                
                ## Cause
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ❌ Hook inside a foreach — triggers IVYHOOK003
                public override object? Build()
                {
                    var items = UseState(new List<string> { "A", "B" });
                
                    foreach (var item in items.Value)
                    {
                        var count = UseState(0); // IVYHOOK003
                    }
                
                    return Layout.Vertical();
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Fix
                
                Extract a child component so each item gets its own isolated hook state:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ✅ Each item is a separate component
                public override object? Build()
                {
                    var items = UseState(new List<string> { "A", "B" });
                
                    return Layout.Vertical(
                        items.Value.Select((item, i) =>
                            new ItemView(item).Key($"item-{i}"))
                    );
                }
                
                public class ItemView(string name) : ViewBase
                {
                    public override object? Build()
                    {
                        var count = UseState(0);
                        return Layout.Horizontal(
                            Text.Block($"{name}: {count.Value}"),
                            new Button("+", _ => count.Set(count.Value + 1))
                        );
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## See Also
                
                - [Rules of Hooks](app://hooks/rules-of-hooks)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Hooks.RulesOfHooksApp)]; 
        return article;
    }
}

