using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Other.Ivy.Analyser;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/05_Other/Ivy.Analyser/IVYEFFECT001.md")]
public class IVYEFFECT001App(bool onlyBody = false) : ViewBase
{
    public IVYEFFECT001App() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivyeffect001-avoid-taskcontinuewith-inside-useeffect", "IVYEFFECT001: Avoid Task.ContinueWith inside UseEffect", 1), new ArticleHeading("description", "Description", 2), new ArticleHeading("cause", "Cause", 2), new ArticleHeading("fix", "Fix", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # IVYEFFECT001: Avoid Task.ContinueWith inside UseEffect
                
                **Severity:** Warning
                
                ## Description
                
                `Task.ContinueWith()` inside a `UseEffect` callback creates a fire-and-forget continuation that runs on a thread pool thread with no component lifecycle awareness. When the component is disposed (e.g., during navigation or re-render), the continuation may call `.Set()` on disposed state, causing a `NullReferenceException`.
                
                ## Cause
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ❌ ContinueWith inside UseEffect — triggers IVYEFFECT001
                public override object? Build()
                {
                    var shakeRow = UseState(-1);
                
                    this.UseEffect(() => {
                        if (shakeRow.Value >= 0) {
                            Task.Delay(300).ContinueWith(_ => shakeRow.Set(-1)); // IVYEFFECT001
                        }
                        return null!;
                    }, shakeRow.Value);
                
                    return new Text("Hello");
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                The `ContinueWith` continuation fires after the delay regardless of whether the component is still alive. There is no cleanup mechanism to cancel it on unmount.
                
                ## Fix
                
                Use `async`/`await` with a `CancellationTokenSource` and return it as the cleanup disposable:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ✅ Async with cancellation and proper cleanup
                public override object? Build()
                {
                    var shakeRow = UseState(-1);
                
                    this.UseEffect(() => {
                        if (shakeRow.Value >= 0) {
                            var cts = new CancellationTokenSource();
                            _ = ResetAfterDelay(shakeRow, cts.Token);
                            return cts; // CancellationTokenSource implements IDisposable
                        }
                        return null;
                    }, shakeRow.Value);
                
                    return new Text("Hello");
                }
                
                private static async Task ResetAfterDelay(State<int> state, CancellationToken token)
                {
                    await Task.Delay(300, token);
                    state.Set(-1);
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## See Also
                
                - [UseEffect](app://hooks/core/use-effect)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Hooks.Core.UseEffectApp)]; 
        return article;
    }
}

