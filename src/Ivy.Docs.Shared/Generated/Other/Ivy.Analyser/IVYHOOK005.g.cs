using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Other.Ivy.Analyser;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/05_Other/Ivy.Analyser/IVYHOOK005.md")]
public class IVYHOOK005App(bool onlyBody = false) : ViewBase
{
    public IVYHOOK005App() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivyhook005-hook-not-at-top-of-build-method", "IVYHOOK005: Hook Not at Top of Build Method", 1), new ArticleHeading("description", "Description", 2), new ArticleHeading("cause", "Cause", 2), new ArticleHeading("fix", "Fix", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # IVYHOOK005: Hook Not at Top of Build Method
                
                **Severity:** Warning
                
                ## Description
                
                All hooks must be called at the very top of the `Build()` method, before any other non-hook statements. This ensures hooks are called in a consistent order on every render and prevents accidental early returns from skipping hook calls.
                
                ## Cause
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ❌ Hook after a non-hook statement — triggers IVYHOOK005
                public override object? Build()
                {
                    var x = SomeMethod();        // Non-hook statement
                    var state = UseState(false); // IVYHOOK005
                    return new Button();
                }
                """",Languages.Csharp)
            | new CodeBlock(
                """"
                // ❌ Hook after early return — triggers IVYHOOK005
                public override object? Build()
                {
                    if (user == null) return Text.Block("Login required");
                    var state = UseState(0); // IVYHOOK005
                    return Text.Block($"Count: {state.Value}");
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Fix
                
                Move all hook calls to the top of `Build()`, before any other logic:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ✅ Hooks first, then logic
                public override object? Build()
                {
                    var state = UseState(0);
                
                    if (user == null) return Text.Block("Login required");
                
                    return Text.Block($"Count: {state.Value}");
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

