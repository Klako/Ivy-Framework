using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Hooks.Core;

[App(order:9, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/03_Hooks/02_Core/09_UseQuery.md", searchHints: ["usequery", "swr", "stale-while-revalidate", "fetch", "cache", "async", "loading", "revalidate"])]
public class UseQueryApp(bool onlyBody = false) : ViewBase
{
    public UseQueryApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("usequery", "UseQuery", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("query-result", "Query Result", 2), new ArticleHeading("query-options", "Query Options", 2), new ArticleHeading("query-scopes", "Query Scopes", 2), new ArticleHeading("conditional-fetching", "Conditional Fetching", 2), new ArticleHeading("dependent-fetching", "Dependent Fetching", 2), new ArticleHeading("tag-based-invalidation", "Tag-Based Invalidation", 2), new ArticleHeading("mutations", "Mutations", 2), new ArticleHeading("cross-component-mutations", "Cross-Component Mutations", 3), new ArticleHeading("faq", "Faq", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# UseQuery").OnLinkClick(onLinkClick)
            | Lead("Fetch, cache, and synchronize server data with the [UseQuery](app://hooks/rules-of-hooks) hook.")
            | new Markdown(
                """"
                The `UseQuery` [hook](app://hooks/rules-of-hooks) provides a powerful way to fetch and cache asynchronous data. Inspired by [SWR](https://swr.vercel.app/) (stale-while-revalidate), it returns cached data immediately while revalidating in the background, keeping your [views](app://onboarding/concepts/views) fast and your data fresh.
                
                ## Basic Usage
                
                The simplest form of `UseQuery` takes a key and a fetcher function:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicQueryView : ViewBase
                    {
                        public override object? Build()
                        {
                            var query = UseQuery(
                                key: "user-profile",
                                fetcher: async ct =>
                                {
                                    await Task.Delay(1000, ct);
                                    return new { Name = "Alice", Email = "alice@example.com" };
                                });
                    
                            if (query.Loading) return "Loading...";
                    
                            return Layout.Vertical()
                                | query //query has a Build extension that produces a debug view
                                | query.Value?.Name
                                | query.Value?.Email
                                | (Layout.Horizontal()
                                    | new Button("Revalidate", _ => query.Mutator.Revalidate()).Variant(ButtonVariant.Primary)
                                    | new Button("Invalidate", _ => query.Mutator.Invalidate()).Variant(ButtonVariant.Primary))
                                    ;
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicQueryView())
            )
            | new Markdown(
                """"
                ## Query Result
                
                `UseQuery` returns a `QueryResult<T>` with the following properties:
                
                | Property | Type | Description |
                |----------|------|-------------|
                | `Value` | `T?` | The fetched data |
                | `Loading` | `bool` | True during initial fetch |
                | `Validating` | `bool` | True during background revalidation |
                | `Previous` | `bool` | True when showing stale data during key change |
                | `Error` | `Exception?` | The error if fetch failed |
                | `Mutator` | `QueryMutator<T>` | Methods to mutate the cache |
                
                ## Query Options
                
                Configure query behavior with `QueryOptions`:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var query = UseQuery(
                    key: "data",
                    fetcher: FetchData,
                    options: new QueryOptions
                    {
                        Scope = QueryScope.Server,                  // Cache scope
                        Expiration = TimeSpan.FromMinutes(5),       // TTL before revalidation
                        KeepPrevious = true,                        // Keep previous data during key change
                        RefreshInterval = TimeSpan.FromSeconds(30), // Auto-refresh interval
                        RevalidateOnMount = true                    // Fetch on mount
                    });
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Query Scopes
                
                Control where query data is cached and shared:
                
                | Scope | Description                                           |
                |-------|-------------------------------------------------------|
                | `Server` | Shared across all users (default)                     |
                | `App` | Shared within a app session                           |
                | `Device` | Shared across apps on same device                     |
                | `View` | Isolated to component instance, cleaned up on unmount |
                
                ## Conditional Fetching
                
                When the key is `null` (often controlled by [UseState](app://hooks/core/use-state)), UseQuery returns an idle result without fetching:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class ConditionalQueryView : ViewBase
                    {
                        public override object? Build()
                        {
                            var shouldFetch = UseState(false);
                    
                            var query = UseQuery(
                                key: shouldFetch.Value ? "data" : null,
                                fetcher: async ct =>
                                {
                                    await Task.Delay(1000, ct);
                                    return $"Fetched at {DateTime.Now:HH:mm:ss}";
                                });
                    
                            return Layout.Vertical()
                                | shouldFetch.ToBoolInput().Label("Enable fetching")
                                | (shouldFetch.Value
                                    ? query.Loading
                                        ? Text.Literal("Loading...")
                                        : Text.Literal(query.Value ?? "")
                                    : Text.Muted("Fetching disabled"));
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new ConditionalQueryView())
            )
            | new Markdown(
                """"
                ## Dependent Fetching
                
                Use a key factory to fetch data that depends on another query:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class DependentQueryView : ViewBase
                    {
                        public override object? Build()
                        {
                            var user = UseQuery(
                                key: "user",
                                fetcher: async ct =>
                                {
                                    await Task.Delay(800, ct);
                                    return new { Id = 42, Name = "Alice" };
                                });
                    
                            // Only fetches when user is loaded
                            var projects = UseQuery(
                                () => user.Value?.Id,
                                async (userId, ct) =>
                                {
                                    await Task.Delay(800, ct);
                                    return new[] { $"Project A (user {userId})", $"Project B (user {userId})" };
                                });
                    
                            return Layout.Vertical()
                                | Text.Literal($"User: {(user.Loading ? "Loading..." : user.Value?.Name)}")
                                | Text.Literal($"Projects: {(projects.Loading ? "Loading..." : string.Join(", ", projects.Value ?? []))}");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new DependentQueryView())
            )
            | new Markdown(
                """"
                ## Tag-Based Invalidation
                
                Assign tags to queries for bulk invalidation (using [UseService](app://hooks/core/use-service)). Tags are serializable the same way as keys.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class TaggedQueriesView : ViewBase
                    {
                        public override object? Build()
                        {
                            var queryService = UseService<IQueryService>();
                    
                            var users = UseQuery(
                                key: "dashboard/users",
                                fetcher: async ct =>
                                {
                                    await Task.Delay(500, ct);
                                    return $"Users: {Random.Shared.Next(100, 500)}";
                                },
                                tags: ["dashboard", "users"]);
                    
                            var orders = UseQuery(
                                key: "dashboard/orders",
                                fetcher: async ct =>
                                {
                                    await Task.Delay(500, ct);
                                    return $"Orders: {Random.Shared.Next(50, 200)}";
                                },
                                tags: ["dashboard", "orders"]);
                    
                            return Layout.Vertical()
                                | Text.Literal(users.Loading ? "Loading..." : users.Value ?? "")
                                | Text.Literal(orders.Loading ? "Loading..." : orders.Value ?? "")
                                | (Layout.Horizontal()
                                    | new Button("Refresh All", _ => queryService.RevalidateByTag("dashboard"))
                                    | new Button("Invalidate All", _ => queryService.InvalidateByTag("dashboard"))
                                );
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new TaggedQueriesView())
            )
            | new Markdown(
                """"
                ## Mutations
                
                The `Mutator` provides methods to update cached data:
                
                | Method | Description |
                |--------|-------------|
                | `Mutate(value, revalidate)` | Update cache with new value |
                | `Revalidate()` | Trigger background revalidation |
                | `Invalidate()` | Clear cache and refetch |
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class MutationView : ViewBase
                    {
                        public override object? Build()
                        {
                            var query = UseQuery(
                                key: "counter",
                                fetcher: async ct =>
                                {
                                    await Task.Delay(500, ct);
                                    return Random.Shared.Next(1, 100);
                                });
                    
                            if (query.Loading)
                                return Text.Literal("Loading...");
                    
                            return Layout.Vertical()
                                | (Layout.Horizontal()
                                    | new Button("+10 (Optimistic)", _ =>
                                        query.Mutator.Mutate(query.Value + 10, revalidate: true))
                                        .Variant(ButtonVariant.Primary)
                                    | new Button("Set 999", _ =>
                                        query.Mutator.Mutate(999, revalidate: false))
                                        .Variant(ButtonVariant.Secondary)
                                    | new Button("Refresh", _ => query.Mutator.Revalidate())
                                        .Variant(ButtonVariant.Outline))
                                    | Text.Literal($"Value: {query.Value}")
                                    | (query.Validating ? Text.Muted("Syncing...") : null!);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new MutationView())
            )
            | new Markdown(
                """"
                ### Cross-Component Mutations
                
                Use `UseMutation` to control a query by key (e.g. from another component):
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class UseMutationView : ViewBase
                    {
                        public override object? Build()
                        {
                            var query = UseQuery(
                                key: "shared-data",
                                fetcher: async ct =>
                                {
                                    await Task.Delay(500, ct);
                                    return $"Data: {Guid.NewGuid().ToString()[..8]}";
                                });
                            var mutator = UseMutation("shared-data");
                    
                            return Layout.Vertical()
                                | (Layout.Horizontal()
                                    | Text.Literal(query.Loading ? "Loading..." : query.Value ?? "")
                                    | (query.Validating ? Text.Muted(" (updating...)") : null!))
                                | (Layout.Horizontal()
                                    | new Button("Revalidate", _ => mutator.Revalidate())
                                        .Variant(ButtonVariant.Outline)
                                    | new Button("Invalidate", _ => mutator.Invalidate())
                                        .Variant(ButtonVariant.Destructive));
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new UseMutationView())
            )
            | new Markdown("## Faq").OnLinkClick(onLinkClick)
            | new Expandable("Polling",
                Vertical().Gap(4)
                | new Markdown("Use `RefreshInterval` to revalidate at intervals, or trigger revalidation manually on a timer. This example uses manual `Revalidate()` every 5 seconds for 30 seconds after you click the button, so the UI updates and no permanent polling runs.").OnLinkClick(onLinkClick)
                | (Vertical() 
                    | new CodeBlock(
                        """"
                        public class PollingView : ViewBase
                        {
                            public override object? Build()
                            {
                                var pollingEnabled = UseState(false);
                                var liveData = UseQuery(
                                    key: "live-data",
                                    fetcher: async ct =>
                                    {
                                        await Task.Delay(300, ct);
                                        return new
                                        {
                                            Value = Random.Shared.Next(100, 999),
                                            Timestamp = DateTime.Now
                                        };
                                    });
                        
                                UseEffect(() =>
                                {
                                    if (!pollingEnabled.Value) return new CancellationTokenSource();
                                    var cts = new CancellationTokenSource();
                                    var endAt = DateTime.UtcNow.AddSeconds(30);
                                    _ = Task.Run(async () =>
                                    {
                                        while (!cts.Token.IsCancellationRequested && DateTime.UtcNow < endAt)
                                        {
                                            liveData.Mutator.Revalidate();
                                            await Task.Delay(TimeSpan.FromSeconds(5), cts.Token);
                                        }
                                        if (!cts.Token.IsCancellationRequested)
                                            pollingEnabled.Set(false);
                                    });
                                    return cts;
                                }, pollingEnabled);
                        
                                return Layout.Vertical()
                                    | Text.Literal($"Value: {liveData.Value?.Value}")
                                    | Text.Muted($"Updated: {liveData.Value?.Timestamp:HH:mm:ss}")
                                    | (liveData.Validating ? Text.Muted("Refreshing...") : null!)
                                    | new Button(
                                        pollingEnabled.Value ? "Stop polling" : "Start polling (5s × 30s)",
                                        _ => pollingEnabled.Set(!pollingEnabled.Value));
                            }
                        }
                        """",Languages.Csharp)
                    | new Box().Content(new PollingView())
                )
            )
            | new Expandable("Pagination",
                Vertical().Gap(4)
                | new Markdown("Use `KeepPrevious` to show previous page data while loading the next. Combine [UseQuery](app://use-query) with the [Pagination](app://widgets/common/pagination) widget:").OnLinkClick(onLinkClick)
                | (Vertical() 
                    | new CodeBlock(
                        """"
                        public class PaginatedView : ViewBase
                        {
                            private const int PageSize = 5;
                            private const int TotalPages = 10;
                        
                            public override object? Build()
                            {
                                var page = UseState(1);
                        
                                var items = UseQuery(
                                    key: $"items?page={page.Value}",
                                    fetcher: async ct =>
                                    {
                                        await Task.Delay(800, ct);
                                        var start = (page.Value - 1) * PageSize;
                                        return Enumerable.Range(start + 1, PageSize)
                                            .Select(i => i.ToString())
                                            .ToList();
                                    },
                                    options: new QueryOptions { KeepPrevious = true });
                        
                                return Layout.Vertical()
                                    | (items.Previous ? Text.Muted("Loading next page...") : null!)
                                    | Layout.Horizontal(items.Value?.Select(Text.Literal) ?? [])
                                    | new Pagination(page.Value, TotalPages, p => page.Set(p.Value));
                            }
                        }
                        """",Languages.Csharp)
                    | new Box().Content(new PaginatedView())
                )
            )
            | new Expandable("Pre-Populated Data",
                Vertical().Gap(4)
                | new Markdown("Skip initial fetch when you already have data (e.g., from a list view):").OnLinkClick(onLinkClick)
                | (Vertical() 
                    | new CodeBlock(
                        """"
                        public class ProductListView : ViewBase
                        {
                            public override object? Build()
                            {
                                var products = UseQuery(
                                    key: "products",
                                    fetcher: async ct =>
                                    {
                                        await Task.Delay(1000, ct);
                                        return new[]
                                        {
                                            new Product(1, "Widget", 9.99m),
                                            new Product(2, "Gadget", 19.99m)
                                        };
                                    });
                        
                                if (products.Loading)
                                    return Text.Literal("Loading...");
                        
                                return Layout.Vertical(
                                    products.Value?.Select(p => new ProductDetailView(p))
                                );
                            }
                        }
                        
                        public record Product(int Id, string Name, decimal Price);
                        
                        public class ProductDetailView(Product initialProduct) : ViewBase
                        {
                            public override object? Build()
                            {
                                // Use list data immediately, skip initial fetch
                                var product = UseQuery(
                                    key: $"product/{initialProduct.Id}",
                                    fetcher: ct => FetchProduct(initialProduct.Id, ct),
                                    options: new QueryOptions { RevalidateOnMount = false },
                                    initialValue: initialProduct);
                        
                                return new Card(
                                    Layout.Vertical()
                                    | Text.Literal(product.Value?.Name ?? "")
                                    | Text.Literal($"${product.Value?.Price}")
                                ).Small();
                            }
                        
                            private async Task<Product> FetchProduct(int id, CancellationToken ct)
                            {
                                await Task.Delay(500, ct);
                                return new Product(id, "Updated Name", 29.99m);
                            }
                        }
                        """",Languages.Csharp)
                    | new Box().Content(new ProductListView())
                )
            )
            | new Expandable("Error Handling",
                Vertical().Gap(4)
                | new Markdown("Errors are captured in the `Error` property:").OnLinkClick(onLinkClick)
                | (Vertical() 
                    | new CodeBlock(
                        """"
                        public class ErrorHandlingView : ViewBase
                        {
                            public override object? Build()
                            {
                                var query = UseQuery(
                                    key: "risky-data",
                                    fetcher: async ct =>
                                    {
                                        await Task.Delay(1000, ct);
                                        if (Random.Shared.NextDouble() > 0.5)
                                            throw new Exception("Network error");
                                        return "Success!";
                                    });
                        
                                if (query.Loading)
                                    return Text.Literal("Loading...");
                        
                                if (query.Error is { } error)
                                {
                                    return Layout.Vertical()
                                        | Callout.Error(error.Message)
                                        | new Button("Retry", _ => query.Mutator.Revalidate())
                                            .Variant(ButtonVariant.Outline);
                                }
                        
                                return Layout.Vertical()
                                    | Text.Literal(query.Value ?? "")
                                    | new Button("Refresh", _ => query.Mutator.Revalidate())
                                        .Variant(ButtonVariant.Primary);
                            }
                        }
                        """",Languages.Csharp)
                    | new Box().Content(new ErrorHandlingView())
                )
            )
            | new Expandable("How do I use UseQuery to fetch data from a database with Entity Framework in Ivy?",
                Vertical().Gap(4)
                | new Markdown("Use `UseQuery` with a `IDbContextFactory<T>` (resolved via `UseService`) to fetch data reactively. Never inject `DbContext` directly — always use a factory. The query re-runs when dependencies change:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    var dbFactory = UseService<IDbContextFactory<MyDbContext>>();
                    var refreshToken = UseRefreshToken();
                    
                    var query = UseQuery(async () =>
                    {
                        await using var db = await dbFactory.CreateDbContextAsync();
                        return await db.Items.OrderBy(i => i.Name).ToListAsync();
                    }, refreshToken);
                    
                    if (query.Loading) return Text.P("Loading...");
                    if (query.Value is not { } items) return Callout.Info("No data.");
                    
                    return items.ToDataTable();
                    """",Languages.Csharp)
                | new Markdown(
                    """"
                    **Key points:**
                    
                    - Always use `IDbContextFactory<T>` — never inject `DbContext` directly. Create a scoped instance with `CreateDbContextAsync()` inside the query lambda and dispose it with `await using`.
                    - `UseQuery` returns a `QueryResult<T>` with `.Value`, `.Loading`, `.Error` properties (NOT `.Data` or `.IsLoading`)
                    - Pass a `RefreshToken` as a dependency to re-fetch data after mutations
                    - Always call `.ToListAsync()` inside the query lambda — do NOT return `IQueryable` directly
                    - For mutations (add/edit/delete), use a separate method that creates its own `DbContext` from the factory, calls `db.SaveChangesAsync()`, then `refreshToken.Refresh()`
                    """").OnLinkClick(onLinkClick)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Hooks.RulesOfHooksApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Hooks.Core.UseStateApp), typeof(Hooks.Core.UseServiceApp)]; 
        return article;
    }
}


public class BasicQueryView : ViewBase
{
    public override object? Build()
    {
        var query = UseQuery(
            key: "user-profile",
            fetcher: async ct =>
            {
                await Task.Delay(1000, ct);
                return new { Name = "Alice", Email = "alice@example.com" };
            });

        if (query.Loading) return "Loading...";

        return Layout.Vertical()
            | query //query has a Build extension that produces a debug view
            | query.Value?.Name
            | query.Value?.Email
            | (Layout.Horizontal() 
                | new Button("Revalidate", _ => query.Mutator.Revalidate()).Variant(ButtonVariant.Primary) 
                | new Button("Invalidate", _ => query.Mutator.Invalidate()).Variant(ButtonVariant.Primary)) 
                ;
    }
}

public class ConditionalQueryView : ViewBase
{
    public override object? Build()
    {
        var shouldFetch = UseState(false);

        var query = UseQuery(
            key: shouldFetch.Value ? "data" : null,
            fetcher: async ct =>
            {
                await Task.Delay(1000, ct);
                return $"Fetched at {DateTime.Now:HH:mm:ss}";
            });

        return Layout.Vertical()
            | shouldFetch.ToBoolInput().Label("Enable fetching")
            | (shouldFetch.Value
                ? query.Loading
                    ? Text.Literal("Loading...")
                    : Text.Literal(query.Value ?? "")
                : Text.Muted("Fetching disabled"));
    }
}

public class DependentQueryView : ViewBase
{
    public override object? Build()
    {
        var user = UseQuery(
            key: "user",
            fetcher: async ct =>
            {
                await Task.Delay(800, ct);
                return new { Id = 42, Name = "Alice" };
            });

        // Only fetches when user is loaded
        var projects = UseQuery(
            () => user.Value?.Id,
            async (userId, ct) =>
            {
                await Task.Delay(800, ct);
                return new[] { $"Project A (user {userId})", $"Project B (user {userId})" };
            });

        return Layout.Vertical()
            | Text.Literal($"User: {(user.Loading ? "Loading..." : user.Value?.Name)}")
            | Text.Literal($"Projects: {(projects.Loading ? "Loading..." : string.Join(", ", projects.Value ?? []))}");
    }
}

public class TaggedQueriesView : ViewBase
{
    public override object? Build()
    {
        var queryService = UseService<IQueryService>();

        var users = UseQuery(
            key: "dashboard/users",
            fetcher: async ct =>
            {
                await Task.Delay(500, ct);
                return $"Users: {Random.Shared.Next(100, 500)}";
            },
            tags: ["dashboard", "users"]);

        var orders = UseQuery(
            key: "dashboard/orders",
            fetcher: async ct =>
            {
                await Task.Delay(500, ct);
                return $"Orders: {Random.Shared.Next(50, 200)}";
            },
            tags: ["dashboard", "orders"]);

        return Layout.Vertical()
            | Text.Literal(users.Loading ? "Loading..." : users.Value ?? "")
            | Text.Literal(orders.Loading ? "Loading..." : orders.Value ?? "")
            | (Layout.Horizontal()
                | new Button("Refresh All", _ => queryService.RevalidateByTag("dashboard"))
                | new Button("Invalidate All", _ => queryService.InvalidateByTag("dashboard"))
            );
    }
}

public class MutationView : ViewBase
{
    public override object? Build()
    {
        var query = UseQuery(
            key: "counter",
            fetcher: async ct =>
            {
                await Task.Delay(500, ct);
                return Random.Shared.Next(1, 100);
            });

        if (query.Loading)
            return Text.Literal("Loading...");

        return Layout.Vertical()
            | (Layout.Horizontal()
                | new Button("+10 (Optimistic)", _ =>
                    query.Mutator.Mutate(query.Value + 10, revalidate: true))
                    .Variant(ButtonVariant.Primary)
                | new Button("Set 999", _ =>
                    query.Mutator.Mutate(999, revalidate: false))
                    .Variant(ButtonVariant.Secondary)
                | new Button("Refresh", _ => query.Mutator.Revalidate())
                    .Variant(ButtonVariant.Outline))
                | Text.Literal($"Value: {query.Value}")
                | (query.Validating ? Text.Muted("Syncing...") : null!);
    }
}

public class UseMutationView : ViewBase
{
    public override object? Build()
    {
        var query = UseQuery(
            key: "shared-data",
            fetcher: async ct =>
            {
                await Task.Delay(500, ct);
                return $"Data: {Guid.NewGuid().ToString()[..8]}";
            });
        var mutator = UseMutation("shared-data");

        return Layout.Vertical()
            | (Layout.Horizontal()
                | Text.Literal(query.Loading ? "Loading..." : query.Value ?? "")
                | (query.Validating ? Text.Muted(" (updating...)") : null!))
            | (Layout.Horizontal()
                | new Button("Revalidate", _ => mutator.Revalidate())
                    .Variant(ButtonVariant.Outline)
                | new Button("Invalidate", _ => mutator.Invalidate())
                    .Variant(ButtonVariant.Destructive));
    }
}

public class PollingView : ViewBase
{
    public override object? Build()
    {
        var pollingEnabled = UseState(false);
        var liveData = UseQuery(
            key: "live-data",
            fetcher: async ct =>
            {
                await Task.Delay(300, ct);
                return new
                {
                    Value = Random.Shared.Next(100, 999),
                    Timestamp = DateTime.Now
                };
            });

        UseEffect(() =>
        {
            if (!pollingEnabled.Value) return new CancellationTokenSource();
            var cts = new CancellationTokenSource();
            var endAt = DateTime.UtcNow.AddSeconds(30);
            _ = Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested && DateTime.UtcNow < endAt)
                {
                    liveData.Mutator.Revalidate();
                    await Task.Delay(TimeSpan.FromSeconds(5), cts.Token);
                }
                if (!cts.Token.IsCancellationRequested)
                    pollingEnabled.Set(false);
            });
            return cts;
        }, pollingEnabled);

        return Layout.Vertical()
            | Text.Literal($"Value: {liveData.Value?.Value}")
            | Text.Muted($"Updated: {liveData.Value?.Timestamp:HH:mm:ss}")
            | (liveData.Validating ? Text.Muted("Refreshing...") : null!)
            | new Button(
                pollingEnabled.Value ? "Stop polling" : "Start polling (5s × 30s)",
                _ => pollingEnabled.Set(!pollingEnabled.Value));
    }
}

public class PaginatedView : ViewBase
{
    private const int PageSize = 5;
    private const int TotalPages = 10;

    public override object? Build()
    {
        var page = UseState(1);

        var items = UseQuery(
            key: $"items?page={page.Value}",
            fetcher: async ct =>
            {
                await Task.Delay(800, ct);
                var start = (page.Value - 1) * PageSize;
                return Enumerable.Range(start + 1, PageSize)
                    .Select(i => i.ToString())
                    .ToList();
            },
            options: new QueryOptions { KeepPrevious = true });

        return Layout.Vertical()
            | (items.Previous ? Text.Muted("Loading next page...") : null!)
            | Layout.Horizontal(items.Value?.Select(Text.Literal) ?? [])
            | new Pagination(page.Value, TotalPages, p => page.Set(p.Value));
    }
}

public class ProductListView : ViewBase
{
    public override object? Build()
    {
        var products = UseQuery(
            key: "products",
            fetcher: async ct =>
            {
                await Task.Delay(1000, ct);
                return new[]
                {
                    new Product(1, "Widget", 9.99m),
                    new Product(2, "Gadget", 19.99m)
                };
            });

        if (products.Loading)
            return Text.Literal("Loading...");

        return Layout.Vertical(
            products.Value?.Select(p => new ProductDetailView(p))
        );
    }
}

public record Product(int Id, string Name, decimal Price);

public class ProductDetailView(Product initialProduct) : ViewBase
{
    public override object? Build()
    {
        // Use list data immediately, skip initial fetch
        var product = UseQuery(
            key: $"product/{initialProduct.Id}",
            fetcher: ct => FetchProduct(initialProduct.Id, ct),
            options: new QueryOptions { RevalidateOnMount = false },
            initialValue: initialProduct);

        return new Card(
            Layout.Vertical()
            | Text.Literal(product.Value?.Name ?? "")
            | Text.Literal($"${product.Value?.Price}")
        ).Small();
    }

    private async Task<Product> FetchProduct(int id, CancellationToken ct)
    {
        await Task.Delay(500, ct);
        return new Product(id, "Updated Name", 29.99m);
    }
}

public class ErrorHandlingView : ViewBase
{
    public override object? Build()
    {
        var query = UseQuery(
            key: "risky-data",
            fetcher: async ct =>
            {
                await Task.Delay(1000, ct);
                if (Random.Shared.NextDouble() > 0.5)
                    throw new Exception("Network error");
                return "Success!";
            });

        if (query.Loading)
            return Text.Literal("Loading...");

        if (query.Error is { } error)
        {
            return Layout.Vertical()
                | Callout.Error(error.Message)
                | new Button("Retry", _ => query.Mutator.Revalidate())
                    .Variant(ButtonVariant.Outline);
        }

        return Layout.Vertical()
            | Text.Literal(query.Value ?? "")
            | new Button("Refresh", _ => query.Mutator.Revalidate())
                .Variant(ButtonVariant.Primary);
    }
}
