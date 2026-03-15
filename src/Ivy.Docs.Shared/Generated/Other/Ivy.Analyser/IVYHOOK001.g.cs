using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Other.Ivy.Analyser;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/05_Other/Ivy.Analyser/IVYHOOK001.md")]
public class IVYHOOK001App(bool onlyBody = false) : ViewBase
{
    public IVYHOOK001App() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivyhook001-invalid-hook-usage", "IVYHOOK001: Invalid Hook Usage", 1), new ArticleHeading("description", "Description", 2), new ArticleHeading("cause", "Cause", 2), new ArticleHeading("fix", "Fix", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # IVYHOOK001: Invalid Hook Usage
                
                **Severity:** Error
                
                ## Description
                
                Ivy hooks must be called directly inside the `Build()` method of a `ViewBase` class (or a custom hook). This error fires when a hook is called from a regular method, a service class, or any other location outside of `Build()`.
                
                ## Cause
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ❌ Hook called in a helper method — triggers IVYHOOK001
                public override object? Build()
                {
                    Initialize();
                    return new Button();
                }
                
                private void Initialize()
                {
                    var state = UseState(false); // IVYHOOK001
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Fix
                
                Move the hook call to the top level of `Build()`:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ✅ Hook called directly inside Build()
                public override object? Build()
                {
                    var state = UseState(false);
                    return new Button("Click me");
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

