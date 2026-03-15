using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Other.Ivy.Analyser;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/05_Other/Ivy.Analyser/IVYAPP001.md")]
public class IVYAPP001App(bool onlyBody = false) : ViewBase
{
    public IVYAPP001App() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivyapp001-app-must-have-parameterless-constructor", "IVYAPP001: App Must Have Parameterless Constructor", 1), new ArticleHeading("description", "Description", 2), new ArticleHeading("cause", "Cause", 2), new ArticleHeading("fix", "Fix", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # IVYAPP001: App Must Have Parameterless Constructor
                
                **Severity:** Error
                
                ## Description
                
                Classes decorated with `[App]` are instantiated via `Activator.CreateInstance` at runtime, which requires a parameterless constructor. Use `UseService<T>()` inside `Build()` for dependency injection instead of constructor parameters.
                
                ## Cause
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ❌ App with constructor parameters — triggers IVYAPP001
                [App]
                public class MyApp(IMyService service) : ViewBase
                {
                    public override object? Build()
                    {
                        return Text.Block(service.GetData());
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Fix
                
                Remove constructor parameters and use `UseService<T>()` inside `Build()`:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ✅ Parameterless constructor with UseService
                [App]
                public class MyApp : ViewBase
                {
                    public override object? Build()
                    {
                        var service = UseService<IMyService>();
                        return Text.Block(service.GetData());
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## See Also
                
                - [Apps](app://onboarding/concepts/apps)
                - [UseService](app://hooks/core/use-service)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.AppsApp), typeof(Hooks.Core.UseServiceApp)]; 
        return article;
    }
}

