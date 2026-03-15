using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Hooks.Core;

[App(order:16, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/03_Hooks/02_Core/16_UseRefreshToken.md", searchHints: ["userefreshtoken", "refresh", "reload", "trigger", "manual-update", "reactive"])]
public class UseRefreshTokenApp(bool onlyBody = false) : ViewBase
{
    public UseRefreshTokenApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("userefreshtoken", "UseRefreshToken", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("when-to-use-refreshtoken", "When to Use RefreshToken", 2), new ArticleHeading("passing-return-values", "Passing Return Values", 2), new ArticleHeading("token-properties", "Token Properties", 2), new ArticleHeading("refresh-tokens-vs-event-handlers", "Refresh Tokens vs Event Handlers", 2), new ArticleHeading("best-practices", "Best Practices", 2), new ArticleHeading("use-return-values-for-data-flow", "Use Return Values for Data Flow", 3), new ArticleHeading("combine-with-onmount-trigger", "Combine with OnMount Trigger", 3), new ArticleHeading("guard-against-unnecessary-actions", "Guard Against Unnecessary Actions", 3), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # UseRefreshToken
                
                Refresh tokens provide a mechanism to manually trigger UI updates and effect executions in Ivy, enabling you to reload data, refresh components, or trigger actions on demand.
                
                ## Basic Usage
                
                The `UseRefreshToken` hook creates a token that can be manually refreshed to trigger [effects](app://hooks/core/use-effect):
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicRefreshExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var refreshToken = UseRefreshToken();
                            var timestamp = UseState(DateTime.Now);
                    
                            // Effect runs when refresh token changes
                            UseEffect(() =>
                            {
                                timestamp.Set(DateTime.Now);
                            }, [refreshToken]);
                    
                            return Layout.Vertical()
                                | Text.Muted("Click the button to manually trigger a refresh")
                                | new Button("Refresh", onClick: _ => refreshToken.Refresh())
                                | Text.P($"Last refreshed: {timestamp.Value:HH:mm:ss.fff}");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicRefreshExample())
            )
            | new Markdown("## When to Use RefreshToken").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                sequenceDiagram
                    participant U as User/Background
                    participant R as RefreshToken
                    participant E as UseEffect
                    participant UI as UI
                
                    U->>R: refreshToken.Refresh()
                    R->>R: Generate new GUID
                    R->>E: Trigger dependent effects
                    E->>UI: Update component
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Passing Return Values
                
                Refresh tokens can carry return values to pass data between operations:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class ReturnValueExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var refreshToken = UseRefreshToken();
                            var selectedColor = UseState("No color selected");
                    
                            UseEffect(() =>
                            {
                                if (refreshToken.IsRefreshed && refreshToken.ReturnValue is string color)
                                {
                                    selectedColor.Set($"Selected: {color}");
                                }
                            }, [refreshToken]);
                    
                            return Layout.Vertical()
                                | Layout.Horizontal(
                                    new Button("Red", onClick: _ => refreshToken.Refresh("Red")),
                                    new Button("Green", onClick: _ => refreshToken.Refresh("Green")),
                                    new Button("Blue", onClick: _ => refreshToken.Refresh("Blue")))
                                | Text.P(selectedColor.Value);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new ReturnValueExample())
            )
            | new Markdown(
                """"
                Return values can be any type, including complex objects like records or classes.
                
                ## Token Properties
                
                | Property | Type | Description |
                |----------|------|-------------|
                | `Token` | `Guid` | A unique identifier that changes with each refresh |
                | `IsRefreshed` | `bool` | `true` if the token has been refreshed at least once |
                | `ReturnValue` | `object?` | The value passed to the last `Refresh()` call |
                
                ## Refresh Tokens vs Event Handlers
                
                | Feature | Event Handlers | Refresh Tokens |
                |---------|---------------|----------------|
                | **Trigger** | User interaction (click, blur, change) | Programmatic call to `Refresh()` |
                | **Timing** | Synchronous, immediate | Can trigger async effects |
                | **Scope** | Single component/element | Can trigger multiple effects |
                | **Use Case** | Direct UI interactions | Background operations, coordinated updates |
                | **Data Flow** | Event args (e.g., Event<Button>) | Return values via `ReturnValue` |
                
                ## Best Practices
                
                ### Use Return Values for Data Flow
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Good: Pass important data through return values
                refreshToken.Refresh(newProductId);
                
                UseEffect(() =>
                {
                    if (refreshToken.ReturnValue is Guid productId)
                    {
                        // Load the new product
                    }
                }, [refreshToken]);
                """",Languages.Csharp)
            | new Markdown("### Combine with OnMount Trigger").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Load data on mount AND when manually refreshed
                UseEffect(async () =>
                {
                    var data = await LoadData();
                    // ...
                }, [EffectTrigger.OnMount(), refreshToken]);
                """",Languages.Csharp)
            | new Markdown("### Guard Against Unnecessary Actions").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Check IsRefreshed to avoid running on initial render
                UseEffect(() =>
                {
                    if (refreshToken.IsRefreshed)
                    {
                        ShowNotification("Data refreshed!");
                    }
                }, [refreshToken]);
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## See Also
                
                - [Effects](app://hooks/core/use-effect) - Learn about the UseEffect hook
                - [State Management](app://hooks/core/use-state) - Managing component state
                - [Signals](app://hooks/core/use-signal) - Cross-component communication
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Hooks.Core.UseEffectApp), typeof(Hooks.Core.UseStateApp), typeof(Hooks.Core.UseSignalApp)]; 
        return article;
    }
}


public class BasicRefreshExample : ViewBase
{
    public override object? Build()
    {
        var refreshToken = UseRefreshToken();
        var timestamp = UseState(DateTime.Now);
        
        // Effect runs when refresh token changes
        UseEffect(() =>
        {
            timestamp.Set(DateTime.Now);
        }, [refreshToken]);
        
        return Layout.Vertical()
            | Text.Muted("Click the button to manually trigger a refresh")
            | new Button("Refresh", onClick: _ => refreshToken.Refresh())
            | Text.P($"Last refreshed: {timestamp.Value:HH:mm:ss.fff}");
    }
}

public class ReturnValueExample : ViewBase
{
    public override object? Build()
    {
        var refreshToken = UseRefreshToken();
        var selectedColor = UseState("No color selected");
        
        UseEffect(() =>
        {
            if (refreshToken.IsRefreshed && refreshToken.ReturnValue is string color)
            {
                selectedColor.Set($"Selected: {color}");
            }
        }, [refreshToken]);
        
        return Layout.Vertical()
            | Layout.Horizontal(
                new Button("Red", onClick: _ => refreshToken.Refresh("Red")),
                new Button("Green", onClick: _ => refreshToken.Refresh("Green")),
                new Button("Blue", onClick: _ => refreshToken.Refresh("Blue")))
            | Text.P(selectedColor.Value);
    }
}
