using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Other.Ivy.Analyser;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/05_Other/Ivy.Analyser/IVYHOOK006.md")]
public class IVYHOOK006App(bool onlyBody = false) : ViewBase
{
    public IVYHOOK006App() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivyhook006-hook-result-stored-in-class-member", "IVYHOOK006: Hook Result Stored in Class Member", 1), new ArticleHeading("description", "Description", 2), new ArticleHeading("cause", "Cause", 2), new ArticleHeading("fix", "Fix", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # IVYHOOK006: Hook Result Stored in Class Member
                
                **Severity:** Error
                
                ## Description
                
                Hook results must not be stored in class fields or properties. The state object is captured once and reused across renders, causing hooks to receive wrong indices and corrupting the reactive system.
                
                ## Cause
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ❌ Hook result assigned to a field — triggers IVYHOOK006
                public class MyView : ViewBase
                {
                    private IState<int> _count;
                
                    public override object? Build()
                    {
                        _count = UseState(0); // IVYHOOK006
                        return new Button("Click", _ => _count.Set(_count.Value + 1));
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Fix
                
                Use a local variable instead:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ✅ Hook result in a local variable
                public class MyView : ViewBase
                {
                    public override object? Build()
                    {
                        var count = UseState(0);
                        return new Button("Click", _ => count.Set(count.Value + 1));
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

