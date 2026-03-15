using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Other.Ivy.Analyser;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/05_Other/Ivy.Analyser/IVYHOOK002.md")]
public class IVYHOOK002App(bool onlyBody = false) : ViewBase
{
    public IVYHOOK002App() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivyhook002-hook-called-conditionally", "IVYHOOK002: Hook Called Conditionally", 1), new ArticleHeading("description", "Description", 2), new ArticleHeading("cause", "Cause", 2), new ArticleHeading("fix", "Fix", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # IVYHOOK002: Hook Called Conditionally
                
                **Severity:** Warning
                
                ## Description
                
                Hooks must be called in the same order on every render. Placing a hook inside an `if` statement, ternary expression, `try/catch`, or `using` block means it may not execute on every render, breaking hook ordering.
                
                ## Cause
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ❌ Hook inside an if statement — triggers IVYHOOK002
                public override object? Build()
                {
                    if (someCondition)
                    {
                        var state = UseState(false); // IVYHOOK002
                    }
                    return new Button();
                }
                """",Languages.Csharp)
            | new CodeBlock(
                """"
                // ❌ Hook inside a ternary — triggers IVYHOOK002
                public override object? Build()
                {
                    var result = condition ? UseState(0) : UseState(1); // IVYHOOK002
                    return new Button();
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Fix
                
                Always call hooks unconditionally, then use the condition afterwards:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ✅ Hook called unconditionally
                public override object? Build()
                {
                    var state = UseState(false);
                
                    return someCondition
                        ? Text.Block($"Value: {state.Value}")
                        : Text.Block("Hidden");
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

