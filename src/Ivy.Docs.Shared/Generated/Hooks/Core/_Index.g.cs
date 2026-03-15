using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Hooks.Core;

[App(order:2, title:"Core", documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/03_Hooks/02_Core/_Index.md")]
public class _IndexApp(bool onlyBody = false) : ViewBase
{
    public _IndexApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("hooks", "Hooks", 1), new ArticleHeading("core-hooks", "Core Hooks", 2), new ArticleHeading("performance-hooks", "Performance Hooks", 2), new ArticleHeading("other-hooks", "Other Hooks", 2), new ArticleHeading("streaming-hooks", "Streaming Hooks", 2), new ArticleHeading("creating-custom-hooks", "Creating Custom Hooks", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # Hooks
                
                Hooks are functions that let you "hook into" Ivy state and lifecycle features from [views](app://onboarding/concepts/views). They allow you to use state and other features without writing a class. See the [Rules of Hooks](app://hooks/rules-of-hooks) for call order and context rules.
                
                ## Core Hooks
                
                - [UseState](app://hooks/core/use-state): Add local state to your components.
                - [UseEffect](app://hooks/core/use-effect): Perform side effects in your components.
                
                ## Performance Hooks
                
                - [UseMemo](app://hooks/core/use-memo): Memoize expensive calculations.
                - [UseCallback](app://hooks/core/use-callback): Memoize callback functions.
                
                ## Other Hooks
                
                - [UseRef](app://hooks/core/use-ref): Store mutable values.
                
                ## Streaming Hooks
                
                - [UseStream](app://hooks/core/use-stream): Create a stream to push real-time data to frontend widgets.
                
                ## Creating Custom Hooks
                
                You can build your own hooks to reuse stateful logic between components. A custom hook is a function whose name starts with "Use" and that may call other hooks.
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Hooks.RulesOfHooksApp), typeof(Hooks.Core.UseStateApp), typeof(Hooks.Core.UseEffectApp), typeof(Hooks.Core.UseMemoApp), typeof(Hooks.Core.UseCallbackApp), typeof(Hooks.Core.UseRefApp), typeof(Hooks.Core.UseStreamApp)]; 
        return article;
    }
}

