using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Hooks.Core;

[App(order:14, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/03_Hooks/02_Core/14_UseMutation.md", searchHints: ["mutation", "usemutation", "query-mutation", "data-mutation", "update", "invalidate"])]
public class UseMutationApp(bool onlyBody = false) : ViewBase
{
    public UseMutationApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("usemutation", "UseMutation", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("mutation-flow", "Mutation Flow", 2), new ArticleHeading("methods", "Methods", 2), new ArticleHeading("query-scopes", "Query Scopes", 2), new ArticleHeading("best-practices--troubleshooting", "Best Practices & Troubleshooting", 2), new ArticleHeading("see-also", "See Also", 2), new ArticleHeading("faq", "Faq", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# UseMutation").OnLinkClick(onLinkClick)
            | Lead("The `UseMutation` [hook](app://hooks/rules-of-hooks) provides a way to control [query](app://hooks/core/use-query) caches from different components, enabling optimistic updates, cache invalidation, and cross-component data synchronization.")
            | new Markdown(
                """"
                ## Overview
                
                `UseMutation` enables you to control query caches irrespective of where they are used. It supports:
                
                - **Optimistic Updates**: Update cache immediately before server confirmation.
                - **Cross-Component Control**: Trigger updates from components that don't consume the data.
                - **Background Revalidation**: Refresh data without clearing the current cache.
                
                ## Basic Usage
                
                Update the UI immediately while the server processes the request.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class LikeButton : ViewBase
                    {
                        public record Post(int Likes, bool IsLiked);
                    
                        public override object? Build()
                        {
                            var mutator = UseMutation<Post, string>("post-123");
                            var query = UseQuery("post-123", _ => Task.FromResult(new Post(10, false)));
                            var current = query.Value ?? new Post(0, false);
                    
                            return new Button($"Like ({current.Likes})", _ =>
                            {
                                if (query.Value is not {} p) return;
                                mutator.Mutate(p with {
                                    Likes = p.IsLiked ? p.Likes - 1 : p.Likes + 1,
                                    IsLiked = !p.IsLiked
                                }, revalidate: false);
                            }).Variant(current.IsLiked ? ButtonVariant.Primary : ButtonVariant.Outline);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new LikeButton())
            )
            | new Markdown("## Mutation Flow").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                sequenceDiagram
                    participant C as Component
                    participant M as UseMutation
                    participant Q as Query Cache
                    participant S as Server
                
                    Note over C,S: Optimistic Update
                    C->>M: Mutate(newValue)
                    M->>Q: Update cache immediately
                    Q-->>C: UI updates instantly
                    M->>S: Revalidate in background
                    S-->>Q: Return confirmed data
                    Q-->>C: UI updates with server data
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Methods
                
                The hook returns a `QueryMutator` object. Use the typed generic version for optimistic updates.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Typed (Recommended for optimistic updates)
                var mutator = UseMutation<User, string>("user-profile");
                
                // Untyped (Good for simple invalidation)
                var mutator = UseMutation("user-profile");
                """",Languages.Csharp)
            | new Markdown(
                """"
                | Method | Description | Usage |
                |--------|-------------|-------|
                | `Mutate(value, revalidate)` | Updates cache immediately with `value`. If `revalidate` is true, triggers a background fetch after. | Optimistic UI updates (e.g., Like button). |
                | `Revalidate()` | Triggers a background refresh. Keeps showing stale data until new data arrives. | Non-destructive updates (e.g., Edit form save). |
                | `Invalidate()` | Clears the cache and forces a refetch. UI enters "switching" or "loading" state. | Destructive operations (e.g., Delete item). |
                
                ## Query Scopes
                
                `UseMutation` supports the same scopes as `UseQuery`, **except `View` scope**.
                
                | Scope | Support | Reason |
                |-------|---------|--------|
                | `Server`, `App`, `Device` | ✅ Supported | Shared state can be accessed by key. |
                | `View` | ❌ Not Supported | View-scoped queries are isolated to a specific component instance and cannot be targeted externally. |
                
                ## Best Practices & Troubleshooting
                
                *   **Keys Must Match Exactly**: "user-data" and "User-Data" are different keys.
                *   **Use Typed Mutations**: You cannot call `Mutate(value)` on an untyped `UseMutation("key")`. You must provide types: `UseMutation<T, TKey>("key")`.
                *   **Revalidate vs Invalidate**:
                    *   Use **Revalidate** when you want to keep showing the current data while updating (e.g., "Refresh" button).
                    *   Use **Invalidate** when the current data is definitely wrong or deleted (e.g., "Delete" button).
                """").OnLinkClick(onLinkClick)
            | new Callout("If your mutation isn't working, check if the target `UseQuery` is using Scope = QueryScope.View. UseMutation cannot see View-scoped queries.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## See Also
                
                - [UseQuery](app://hooks/core/use-query)
                - [Rules of Hooks](app://hooks/rules-of-hooks)
                
                ## Faq
                """").OnLinkClick(onLinkClick)
            | new Expandable("Form Submission",
                Vertical().Gap(4)
                | new Markdown("Update data locally then sync with server.").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new UserForm())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class UserForm : ViewBase
                        {
                            public record User(string Name);
                        
                            public override object? Build()
                            {
                                var name = UseState("");
                                var mutator = UseMutation<User, string>("user-profile");
                        
                                var query = UseQuery("user-profile", async ct =>
                                {
                                    await Task.Delay(100);
                                    return new User("Guest");
                                });
                        
                                return Layout.Vertical(
                                    Text.Literal($"Current Profile: {query.Value?.Name ?? "Loading..."}"),
                                    Layout.Horizontal(
                                        name.ToTextInput("Enter Name"),
                                        new Button("Save", async _ =>
                                        {
                                            if (string.IsNullOrEmpty(name.Value)) return;
                        
                                            mutator.Mutate(new User(name.Value), revalidate: false);
                        
                                            name.Set("");
                        
                                            await Task.CompletedTask;
                                        })
                                    )
                                );
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("Shared Control (Cross-Component)",
                Vertical().Gap(4)
                | new Markdown("Control a query from a completely separate component (e.g., a header button controlling a list).").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new SharedControlDemo())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class SharedControlDemo : ViewBase
                        {
                            public override object? Build()
                            {
                                return Layout.Vertical(
                                    new RefreshHeader(),
                                    new Separator(),
                                    new StatsDisplay()
                                );
                            }
                        }
                        
                        public class RefreshHeader : ViewBase
                        {
                            public override object? Build()
                            {
                                var mutator = UseMutation("dashboard-stats");
                        
                                return Layout.Horizontal(
                                    new Button("Refresh (Revalidate)", _ => mutator.Revalidate()),
                                    new Button("Force Reload (Invalidate)", _ => mutator.Invalidate())
                                );
                            }
                        }
                        
                        public class StatsDisplay : ViewBase
                        {
                            public override object? Build()
                            {
                                var query = UseQuery("dashboard-stats", async ct =>
                                {
                                    await Task.Delay(1000);
                                    return $"Stats Updated: {DateTime.Now:HH:mm:ss}";
                                });
                        
                                if (query.Loading) return Text.Literal("Loading new stats...");
                        
                                return Layout.Vertical(
                                    Text.H4("Dashboard Stats"),
                                    Text.Literal(query.Value ?? ""),
                                    query.Validating ? Text.Muted("Refreshing in background...") : null
                                );
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("How do I use UseMutation for async operations in Ivy?",
                Vertical().Gap(4)
                | new Markdown("`UseMutation` runs an async function on demand (e.g., when a button is clicked) and tracks loading/error state:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    var mutation = UseMutation(async () =>
                    {
                        var result = await myService.CallApiAsync(input.Value);
                        output.Set(result);
                    });
                    
                    return Layout.Vertical()
                        | input.ToTextInput().Placeholder("Enter input")
                        | new Button("Submit", mutation.Trigger).Loading(mutation.IsLoading)
                        | (mutation.Error != null ? Callout.Error(mutation.Error.Message) : null)
                        | Text.P(output.Value);
                    """",Languages.Csharp)
                | new Markdown("`mutation.Trigger` is the action to invoke. `mutation.IsLoading` indicates if the operation is in progress. `mutation.Error` contains any exception that was thrown.").OnLinkClick(onLinkClick)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Hooks.RulesOfHooksApp), typeof(Hooks.Core.UseQueryApp)]; 
        return article;
    }
}


public class LikeButton : ViewBase
{
    public record Post(int Likes, bool IsLiked);

    public override object? Build()
    {
        var mutator = UseMutation<Post, string>("post-123");
        var query = UseQuery("post-123", _ => Task.FromResult(new Post(10, false)));
        var current = query.Value ?? new Post(0, false);

        return new Button($"Like ({current.Likes})", _ => 
        {
            if (query.Value is not {} p) return;
            mutator.Mutate(p with { 
                Likes = p.IsLiked ? p.Likes - 1 : p.Likes + 1, 
                IsLiked = !p.IsLiked 
            }, revalidate: false);
        }).Variant(current.IsLiked ? ButtonVariant.Primary : ButtonVariant.Outline);
    }
}

public class UserForm : ViewBase
{
    public record User(string Name);

    public override object? Build()
    {
        var name = UseState("");
        var mutator = UseMutation<User, string>("user-profile");
        
        var query = UseQuery("user-profile", async ct => 
        {
            await Task.Delay(100);
            return new User("Guest");
        });

        return Layout.Vertical(
            Text.Literal($"Current Profile: {query.Value?.Name ?? "Loading..."}"),
            Layout.Horizontal(
                name.ToTextInput("Enter Name"),
                new Button("Save", async _ => 
                {
                    if (string.IsNullOrEmpty(name.Value)) return;

                    mutator.Mutate(new User(name.Value), revalidate: false);
                    
                    name.Set("");
                    
                    await Task.CompletedTask;
                })
            )
        );
    }
}

public class SharedControlDemo : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical(
            new RefreshHeader(),
            new Separator(),
            new StatsDisplay()
        );
    }
}

public class RefreshHeader : ViewBase
{
    public override object? Build()
    {
        var mutator = UseMutation("dashboard-stats");

        return Layout.Horizontal(
            new Button("Refresh (Revalidate)", _ => mutator.Revalidate()),
            new Button("Force Reload (Invalidate)", _ => mutator.Invalidate())
        );
    }
}

public class StatsDisplay : ViewBase
{
    public override object? Build()
    {
        var query = UseQuery("dashboard-stats", async ct =>
        {
            await Task.Delay(1000);
            return $"Stats Updated: {DateTime.Now:HH:mm:ss}";
        });

        if (query.Loading) return Text.Literal("Loading new stats...");
        
        return Layout.Vertical(
            Text.H4("Dashboard Stats"),
            Text.Literal(query.Value ?? ""),
            query.Validating ? Text.Muted("Refreshing in background...") : null
        );
    }
}
