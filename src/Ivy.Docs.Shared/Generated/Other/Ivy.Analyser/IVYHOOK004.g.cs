using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Other.Ivy.Analyser;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/05_Other/Ivy.Analyser/IVYHOOK004.md")]
public class IVYHOOK004App(bool onlyBody = false) : ViewBase
{
    public IVYHOOK004App() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivyhook004-hook-called-in-switch-statement", "IVYHOOK004: Hook Called in Switch Statement", 1), new ArticleHeading("description", "Description", 2), new ArticleHeading("cause", "Cause", 2), new ArticleHeading("fix", "Fix", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # IVYHOOK004: Hook Called in Switch Statement
                
                **Severity:** Warning
                
                ## Description
                
                Hooks cannot be called inside a `switch` statement. Only one case branch executes per render, so hooks inside a case may not run on every render, breaking hook ordering.
                
                ## Cause
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ❌ Hook inside a switch case — triggers IVYHOOK004
                public override object? Build()
                {
                    switch (mode)
                    {
                        case "edit":
                            var draft = UseState(""); // IVYHOOK004
                            return draft.ToTextInput();
                    }
                    return Text.Block("Read-only");
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Fix
                
                Call all hooks unconditionally at the top of `Build()`, then branch in the return:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ✅ Hooks called unconditionally
                public override object? Build()
                {
                    var draft = UseState("");
                
                    return mode switch
                    {
                        "edit" => draft.ToTextInput(),
                        _ => Text.Block("Read-only")
                    };
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

