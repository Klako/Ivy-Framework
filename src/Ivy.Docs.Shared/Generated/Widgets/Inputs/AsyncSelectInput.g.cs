using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Inputs;

[App(order:6, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/04_Inputs/06_AsyncSelectInput.md", searchHints: ["dropdown", "autocomplete", "search", "async", "picker", "options"])]
public class AsyncSelectInputApp(bool onlyBody = false) : ViewBase
{
    public AsyncSelectInputApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("asyncselectinput", "AsyncSelectInput", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("data-types", "Data Types", 2), new ArticleHeading("advanced-patterns", "Advanced Patterns", 2), new ArticleHeading("custom-query-logic", "Custom Query Logic", 3), new ArticleHeading("styling-and-states", "Styling and States", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# AsyncSelectInput").OnLinkClick(onLinkClick)
            | Lead("Create dropdown selectors that load options asynchronously from [APIs](app://hooks/core/use-service) or [databases](app://onboarding/concepts/connections), perfect for large datasets and on-demand loading.")
            | new Markdown(
                """"
                The `AsyncSelectInput` [widget](app://onboarding/concepts/widgets) provides a select dropdown that loads options asynchronously. It's useful for scenarios where options need to be fetched from an [API](app://hooks/core/use-service) or when the list of options is large and should be loaded on-demand.
                
                ## Basic Usage
                
                Here's a simple example of an `AsyncSelectInput` that fetches categories:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class AsyncSelectBasicDemo : ViewBase
                    {
                        private static readonly string[] Categories = { "Electronics", "Clothing", "Books", "Home & Garden", "Sports" };
                    
                        public override object? Build()
                        {
                            var selectedCategory = UseState<string?>(default(string?));
                    
                            QueryResult<Option<string>[]> QueryCategories(IViewContext context, string query)
                            {
                                return context.UseQuery<Option<string>[], (string, string)>(
                                    key: (nameof(QueryCategories), query),
                                    fetcher: ct => Task.FromResult(Categories
                                        .Where(c => c.Contains(query, StringComparison.OrdinalIgnoreCase))
                                        .Select(c => new Option<string>(c))
                                        .ToArray()));
                            }
                    
                            QueryResult<Option<string>?> LookupCategory(IViewContext context, string? category)
                            {
                                return context.UseQuery<Option<string>?, (string, string?)>(
                                    key: (nameof(LookupCategory), category),
                                    fetcher: ct => Task.FromResult(category != null ? new Option<string>(category) : null));
                            }
                    
                            return selectedCategory.ToAsyncSelectInput(QueryCategories, LookupCategory, "Search categories...")
                                    .WithField()
                                    .Label("Select a category:")
                                    .Width(Size.Full());
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new AsyncSelectBasicDemo())
            )
            | new Markdown(
                """"
                ## Data Types
                
                AsyncSelectInput supports various data types. Here's an example showing String, Integer, and Enum-based AsyncSelects:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new DataTypesDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class DataTypesDemo : ViewBase
                    {
                        private static readonly Dictionary<string, string> CountryRegions = new()
                        {
                            { "Germany", "Europe" },
                            { "France", "Europe" },
                            { "Japan", "Asia" },
                            { "China", "Asia" },
                            { "USA", "North America" },
                            { "Canada", "North America" },
                            { "Australia", "Oceania" },
                            { "Brazil", "South America" }
                        };
                    
                        private enum ProgrammingLanguage
                        {
                            CSharp,
                            Java,
                            Python,
                            JavaScript,
                            Go,
                            Rust,
                            FSharp,
                            Kotlin,
                            Swift,
                            TypeScript
                        }
                    
                        public override object? Build()
                        {
                            var selectedCountry = UseState<string?>(default(string));
                            var selectedYear = UseState<int?>(default(int));
                            var selectedLanguage = UseState(ProgrammingLanguage.CSharp);
                    
                            QueryResult<Option<string>[]> QueryCountries(IViewContext context, string query)
                            {
                                return context.UseQuery<Option<string>[], (string, string)>(
                                    key: (nameof(QueryCountries), query),
                                    fetcher: ct => Task.FromResult(CountryRegions.Keys
                                        .Where(c => c.Contains(query, StringComparison.OrdinalIgnoreCase))
                                        .Select(c => new Option<string>(c, c, description: CountryRegions[c]))
                                        .ToArray()));
                            }
                    
                            QueryResult<Option<string>?> LookupCountry(IViewContext context, string? country)
                            {
                                return context.UseQuery<Option<string>?, (string, string?)>(
                                    key: (nameof(LookupCountry), country),
                                    fetcher: ct =>
                                    {
                                        if (string.IsNullOrEmpty(country)) return Task.FromResult<Option<string>?>(null);
                                        return Task.FromResult<Option<string>?>(new Option<string>(country, country, description: CountryRegions.GetValueOrDefault(country)));
                                    });
                            }
                    
                            QueryResult<Option<int>[]> QueryYears(IViewContext context, string query)
                            {
                                return context.UseQuery<Option<int>[], (string, string)>(
                                    key: (nameof(QueryYears), query),
                                    fetcher: ct =>
                                    {
                                        var currentYear = DateTime.Now.Year;
                                        var years = Enumerable.Range(currentYear - 100, 101).ToArray();
                    
                                        if (string.IsNullOrEmpty(query))
                                            return Task.FromResult(years.Take(20).Select(y => new Option<int>(y.ToString(), y)).ToArray());
                    
                                        if (int.TryParse(query, out var yearQuery))
                                        {
                                            return Task.FromResult(years
                                                .Where(y => y >= yearQuery && y <= yearQuery + 10)
                                                .Take(20)
                                                .Select(y => new Option<int>(y.ToString(), y))
                                                .ToArray());
                                        }
                    
                                        return Task.FromResult(years
                                            .Where(y => y.ToString().Contains(query))
                                            .Take(20)
                                            .Select(y => new Option<int>(y.ToString(), y))
                                            .ToArray());
                                    });
                            }
                    
                            QueryResult<Option<int>?> LookupYear(IViewContext context, int year)
                            {
                                return context.UseQuery<Option<int>?, (string, int)>(
                                    key: (nameof(LookupYear), year),
                                    fetcher: ct => Task.FromResult<Option<int>?>(new Option<int>(year.ToString(), year)));
                            }
                    
                            QueryResult<Option<ProgrammingLanguage>[]> QueryLanguages(IViewContext context, string query)
                            {
                                return context.UseQuery<Option<ProgrammingLanguage>[], (string, string)>(
                                    key: (nameof(QueryLanguages), query),
                                    fetcher: ct =>
                                    {
                                        var languages = new[]
                                        {
                                            ProgrammingLanguage.CSharp,
                                            ProgrammingLanguage.Java,
                                            ProgrammingLanguage.Python,
                                            ProgrammingLanguage.JavaScript,
                                            ProgrammingLanguage.Go,
                                            ProgrammingLanguage.Rust,
                                            ProgrammingLanguage.FSharp,
                                            ProgrammingLanguage.Kotlin,
                                            ProgrammingLanguage.Swift,
                                            ProgrammingLanguage.TypeScript
                                        };
                    
                                        if (string.IsNullOrEmpty(query))
                                            return Task.FromResult(languages.Select(l => new Option<ProgrammingLanguage>(l.ToString(), l)).ToArray());
                    
                                        return Task.FromResult(languages
                                            .Where(l => l.ToString().Contains(query, StringComparison.OrdinalIgnoreCase))
                                            .Select(l => new Option<ProgrammingLanguage>(l.ToString(), l))
                                            .ToArray());
                                    });
                            }
                    
                            QueryResult<Option<ProgrammingLanguage>?> LookupLanguage(IViewContext context, ProgrammingLanguage language)
                            {
                                return context.UseQuery<Option<ProgrammingLanguage>?, (string, ProgrammingLanguage)>(
                                    key: (nameof(LookupLanguage), language),
                                    fetcher: ct => Task.FromResult<Option<ProgrammingLanguage>?>(new Option<ProgrammingLanguage>(language.ToString(), language)));
                            }
                    
                            return Layout.Vertical()
                                | selectedCountry.ToAsyncSelectInput(QueryCountries, LookupCountry, placeholder: "Search countries...")
                                    .WithField()
                                    .Label("String-based AsyncSelect:")
                                    .Width(Size.Full())
                    
                                | selectedYear.ToAsyncSelectInput(QueryYears, LookupYear, placeholder: "Search years...")
                                    .WithField()
                                    .Label("Integer-based AsyncSelect:")
                                    .Width(Size.Full())
                    
                                | selectedLanguage.ToAsyncSelectInput(QueryLanguages, LookupLanguage, placeholder: "Search languages...")
                                    .WithField()
                                    .Label("Enum-based AsyncSelect:")
                                    .Width(Size.Full());
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Advanced Patterns
                
                ### Custom Query Logic
                
                Implement complex search logic with multiple criteria:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new AdvancedQueryDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class AdvancedQueryDemo : ViewBase
                    {
                        public record User
                        {
                            public Guid Id { get; init; }
                            public string Name { get; init; } = "";
                            public string Email { get; init; } = "";
                            public string Department { get; init; } = "";
                            public bool IsActive { get; init; }
                        }
                    
                        // Use consistent GUIDs for demo purposes
                        private static readonly Guid JohnId = Guid.NewGuid();
                        private static readonly Guid JaneId = Guid.NewGuid();
                        private static readonly Guid BobId = Guid.NewGuid();
                        private static readonly Guid AliceId = Guid.NewGuid();
                        private static readonly Guid CharlieId = Guid.NewGuid();
                    
                        // Single source of user data
                        private static readonly User[] Users = new[]
                        {
                            new User { Id = JohnId, Name = "John Doe", Email = "john@example.com", Department = "Engineering", IsActive = true },
                            new User { Id = JaneId, Name = "Jane Smith", Email = "jane@example.com", Department = "Design", IsActive = true },
                            new User { Id = BobId, Name = "Bob Johnson", Email = "bob@example.com", Department = "Marketing", IsActive = false },
                            new User { Id = AliceId, Name = "Alice Brown", Email = "alice@example.com", Department = "Engineering", IsActive = true },
                            new User { Id = CharlieId, Name = "Charlie Wilson", Email = "charlie@example.com", Department = "Sales", IsActive = true }
                        };
                    
                        public override object? Build()
                        {
                            var selectedUser = UseState<Guid>(default(Guid));
                            var selectedUserInfo = UseState<string>("No user selected");
                    
                            // Update display when selection changes
                            UseEffect(() =>
                            {
                                var user = Users.FirstOrDefault(u => u.Id == selectedUser.Value);
                                selectedUserInfo.Set(user != null ? $"{user.Name} - {user.Email} ({user.Department})" : "No user selected");
                            }, [selectedUser]);
                    
                            QueryResult<Option<Guid>[]> QueryUsers(IViewContext context, string query)
                            {
                                return context.UseQuery<Option<Guid>[], (string, string)>(
                                    key: (nameof(QueryUsers), query),
                                    fetcher: ct =>
                                    {
                                        if (string.IsNullOrEmpty(query))
                                            return Task.FromResult(Users.Where(u => u.IsActive).Take(5)
                                                .Select(u => new Option<Guid>($"{u.Name} ({u.Department})", u.Id, description: u.Email))
                                                .ToArray());
                    
                                        var queryLower = query.ToLowerInvariant();
                                        return Task.FromResult(Users
                                            .Where(u => u.IsActive &&
                                                       (u.Name.ToLowerInvariant().Contains(queryLower) ||
                                                        u.Email.ToLowerInvariant().Contains(queryLower) ||
                                                        u.Department.ToLowerInvariant().Contains(queryLower)))
                                            .Take(10)
                                            .Select(u => new Option<Guid>($"{u.Name} ({u.Department})", u.Id, description: u.Email))
                                            .ToArray());
                                    });
                            }
                    
                            QueryResult<Option<Guid>?> LookupUser(IViewContext context, Guid id)
                            {
                                return context.UseQuery<Option<Guid>?, (string, Guid)>(
                                    key: (nameof(LookupUser), id),
                                    fetcher: ct =>
                                    {
                                        var user = Users.FirstOrDefault(u => u.Id == id);
                                        return Task.FromResult(user != null
                                            ? new Option<Guid>($"{user.Name} ({user.Department})", user.Id, description: user.Email)
                                            : null);
                                    });
                            }
                    
                            var customAsyncSelect = new AsyncSelectInputView<Guid>(
                                selectedUser.Value,
                                e => { selectedUser.Set(e.Value); return ValueTask.CompletedTask; },
                                QueryUsers,
                                LookupUser,
                                placeholder: "Search by name, email, or department..."
                            );
                    
                            return customAsyncSelect
                                    .WithField()
                                    .Label("Search and select a user:")
                                    .Width(Size.Full());
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("Use AsyncSelectInput for foreign key relationships, large datasets, and when you need to provide search functionality. It's perfect for scenarios where the full list of options would be too large to load upfront.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Styling and States
                
                Customize the `AsyncSelectInput` with various styling options:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new StylingDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class StylingDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var normalSelect = UseState<string?>(default(string));
                            var invalidSelect = UseState<string?>(default(string));
                            var disabledSelect = UseState<string?>(default(string));
                    
                            QueryResult<Option<string>[]> QueryOptions(IViewContext context, string query)
                            {
                                var options = new[] { "Option 1", "Option 2", "Option 3" };
                                return context.UseQuery<Option<string>[], (string, string)>(
                                    key: (nameof(QueryOptions), query),
                                    fetcher: ct => Task.FromResult(options
                                        .Where(opt => opt.Contains(query, StringComparison.OrdinalIgnoreCase))
                                        .Select(opt => new Option<string>(opt))
                                        .ToArray()));
                            }
                    
                            QueryResult<Option<string>?> LookupOption(IViewContext context, string? option)
                            {
                                return context.UseQuery<Option<string>?, (string, string?)>(
                                    key: (nameof(LookupOption), option),
                                    fetcher: ct => Task.FromResult<Option<string>?>(option != null ? new Option<string>(option) : null));
                            }
                    
                            return Layout.Vertical()
                                | normalSelect.ToAsyncSelectInput(QueryOptions, LookupOption, placeholder: "Choose an option...")
                                    .WithField()
                                    .Label("Normal AsyncSelectInput:")
                    
                                | invalidSelect.ToAsyncSelectInput(QueryOptions, LookupOption, placeholder: "This has an error...")
                                    .Invalid("This field is required")
                                    .WithField()
                                    .Label("Invalid AsyncSelectInput:")
                    
                                | disabledSelect.ToAsyncSelectInput(QueryOptions, LookupOption, placeholder: "This is disabled...")
                                    .Disabled(true)
                                    .WithField()
                                    .Label("Disabled AsyncSelectInput:");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("AsyncSelectInput automatically handles loading states and provides a smooth user [experience](app://onboarding/concepts/views). The query function is called as the user types, and the lookup function is called when displaying the selected value.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new WidgetDocsView("Ivy.AsyncSelectInput", "Ivy.AsyncSelectInputExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Inputs/AsyncSelectInput.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Hooks.Core.UseServiceApp), typeof(Onboarding.Concepts.ConnectionsApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.ViewsApp)]; 
        return article;
    }
}


public class AsyncSelectBasicDemo : ViewBase
{
    private static readonly string[] Categories = { "Electronics", "Clothing", "Books", "Home & Garden", "Sports" };

    public override object? Build()
    {
        var selectedCategory = UseState<string?>(default(string?));

        QueryResult<Option<string>[]> QueryCategories(IViewContext context, string query)
        {
            return context.UseQuery<Option<string>[], (string, string)>(
                key: (nameof(QueryCategories), query),
                fetcher: ct => Task.FromResult(Categories
                    .Where(c => c.Contains(query, StringComparison.OrdinalIgnoreCase))
                    .Select(c => new Option<string>(c))
                    .ToArray()));
        }

        QueryResult<Option<string>?> LookupCategory(IViewContext context, string? category)
        {
            return context.UseQuery<Option<string>?, (string, string?)>(
                key: (nameof(LookupCategory), category),
                fetcher: ct => Task.FromResult(category != null ? new Option<string>(category) : null));
        }

        return selectedCategory.ToAsyncSelectInput(QueryCategories, LookupCategory, "Search categories...")
                .WithField()
                .Label("Select a category:")
                .Width(Size.Full());
    }
}

public class DataTypesDemo : ViewBase
{
    private static readonly Dictionary<string, string> CountryRegions = new()
    {
        { "Germany", "Europe" },
        { "France", "Europe" },
        { "Japan", "Asia" },
        { "China", "Asia" },
        { "USA", "North America" },
        { "Canada", "North America" },
        { "Australia", "Oceania" },
        { "Brazil", "South America" }
    };

    private enum ProgrammingLanguage
    {
        CSharp,
        Java,
        Python,
        JavaScript,
        Go,
        Rust,
        FSharp,
        Kotlin,
        Swift,
        TypeScript
    }

    public override object? Build()
    {
        var selectedCountry = UseState<string?>(default(string));
        var selectedYear = UseState<int?>(default(int));
        var selectedLanguage = UseState(ProgrammingLanguage.CSharp);

        QueryResult<Option<string>[]> QueryCountries(IViewContext context, string query)
        {
            return context.UseQuery<Option<string>[], (string, string)>(
                key: (nameof(QueryCountries), query),
                fetcher: ct => Task.FromResult(CountryRegions.Keys
                    .Where(c => c.Contains(query, StringComparison.OrdinalIgnoreCase))
                    .Select(c => new Option<string>(c, c, description: CountryRegions[c]))
                    .ToArray()));
        }

        QueryResult<Option<string>?> LookupCountry(IViewContext context, string? country)
        {
            return context.UseQuery<Option<string>?, (string, string?)>(
                key: (nameof(LookupCountry), country),
                fetcher: ct =>
                {
                    if (string.IsNullOrEmpty(country)) return Task.FromResult<Option<string>?>(null);
                    return Task.FromResult<Option<string>?>(new Option<string>(country, country, description: CountryRegions.GetValueOrDefault(country)));
                });
        }

        QueryResult<Option<int>[]> QueryYears(IViewContext context, string query)
        {
            return context.UseQuery<Option<int>[], (string, string)>(
                key: (nameof(QueryYears), query),
                fetcher: ct =>
                {
                    var currentYear = DateTime.Now.Year;
                    var years = Enumerable.Range(currentYear - 100, 101).ToArray();

                    if (string.IsNullOrEmpty(query))
                        return Task.FromResult(years.Take(20).Select(y => new Option<int>(y.ToString(), y)).ToArray());

                    if (int.TryParse(query, out var yearQuery))
                    {
                        return Task.FromResult(years
                            .Where(y => y >= yearQuery && y <= yearQuery + 10)
                            .Take(20)
                            .Select(y => new Option<int>(y.ToString(), y))
                            .ToArray());
                    }

                    return Task.FromResult(years
                        .Where(y => y.ToString().Contains(query))
                        .Take(20)
                        .Select(y => new Option<int>(y.ToString(), y))
                        .ToArray());
                });
        }

        QueryResult<Option<int>?> LookupYear(IViewContext context, int year)
        {
            return context.UseQuery<Option<int>?, (string, int)>(
                key: (nameof(LookupYear), year),
                fetcher: ct => Task.FromResult<Option<int>?>(new Option<int>(year.ToString(), year)));
        }

        QueryResult<Option<ProgrammingLanguage>[]> QueryLanguages(IViewContext context, string query)
        {
            return context.UseQuery<Option<ProgrammingLanguage>[], (string, string)>(
                key: (nameof(QueryLanguages), query),
                fetcher: ct =>
                {
                    var languages = new[]
                    {
                        ProgrammingLanguage.CSharp,
                        ProgrammingLanguage.Java,
                        ProgrammingLanguage.Python,
                        ProgrammingLanguage.JavaScript,
                        ProgrammingLanguage.Go,
                        ProgrammingLanguage.Rust,
                        ProgrammingLanguage.FSharp,
                        ProgrammingLanguage.Kotlin,
                        ProgrammingLanguage.Swift,
                        ProgrammingLanguage.TypeScript
                    };

                    if (string.IsNullOrEmpty(query))
                        return Task.FromResult(languages.Select(l => new Option<ProgrammingLanguage>(l.ToString(), l)).ToArray());

                    return Task.FromResult(languages
                        .Where(l => l.ToString().Contains(query, StringComparison.OrdinalIgnoreCase))
                        .Select(l => new Option<ProgrammingLanguage>(l.ToString(), l))
                        .ToArray());
                });
        }

        QueryResult<Option<ProgrammingLanguage>?> LookupLanguage(IViewContext context, ProgrammingLanguage language)
        {
            return context.UseQuery<Option<ProgrammingLanguage>?, (string, ProgrammingLanguage)>(
                key: (nameof(LookupLanguage), language),
                fetcher: ct => Task.FromResult<Option<ProgrammingLanguage>?>(new Option<ProgrammingLanguage>(language.ToString(), language)));
        }

        return Layout.Vertical()
            | selectedCountry.ToAsyncSelectInput(QueryCountries, LookupCountry, placeholder: "Search countries...")
                .WithField()
                .Label("String-based AsyncSelect:")
                .Width(Size.Full())
            
            | selectedYear.ToAsyncSelectInput(QueryYears, LookupYear, placeholder: "Search years...")
                .WithField()
                .Label("Integer-based AsyncSelect:")
                .Width(Size.Full())
            
            | selectedLanguage.ToAsyncSelectInput(QueryLanguages, LookupLanguage, placeholder: "Search languages...")
                .WithField()
                .Label("Enum-based AsyncSelect:")
                .Width(Size.Full());
    }
}

public class AdvancedQueryDemo : ViewBase
{
    public record User
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = "";
        public string Email { get; init; } = "";
        public string Department { get; init; } = "";
        public bool IsActive { get; init; }
    }

    // Use consistent GUIDs for demo purposes
    private static readonly Guid JohnId = Guid.NewGuid();
    private static readonly Guid JaneId = Guid.NewGuid();
    private static readonly Guid BobId = Guid.NewGuid();
    private static readonly Guid AliceId = Guid.NewGuid();
    private static readonly Guid CharlieId = Guid.NewGuid();

    // Single source of user data
    private static readonly User[] Users = new[]
    {
        new User { Id = JohnId, Name = "John Doe", Email = "john@example.com", Department = "Engineering", IsActive = true },
        new User { Id = JaneId, Name = "Jane Smith", Email = "jane@example.com", Department = "Design", IsActive = true },
        new User { Id = BobId, Name = "Bob Johnson", Email = "bob@example.com", Department = "Marketing", IsActive = false },
        new User { Id = AliceId, Name = "Alice Brown", Email = "alice@example.com", Department = "Engineering", IsActive = true },
        new User { Id = CharlieId, Name = "Charlie Wilson", Email = "charlie@example.com", Department = "Sales", IsActive = true }
    };

    public override object? Build()
    {
        var selectedUser = UseState<Guid>(default(Guid));
        var selectedUserInfo = UseState<string>("No user selected");

        // Update display when selection changes
        UseEffect(() =>
        {
            var user = Users.FirstOrDefault(u => u.Id == selectedUser.Value);
            selectedUserInfo.Set(user != null ? $"{user.Name} - {user.Email} ({user.Department})" : "No user selected");
        }, [selectedUser]);

        QueryResult<Option<Guid>[]> QueryUsers(IViewContext context, string query)
        {
            return context.UseQuery<Option<Guid>[], (string, string)>(
                key: (nameof(QueryUsers), query),
                fetcher: ct =>
                {
                    if (string.IsNullOrEmpty(query))
                        return Task.FromResult(Users.Where(u => u.IsActive).Take(5)
                            .Select(u => new Option<Guid>($"{u.Name} ({u.Department})", u.Id, description: u.Email))
                            .ToArray());

                    var queryLower = query.ToLowerInvariant();
                    return Task.FromResult(Users
                        .Where(u => u.IsActive &&
                                   (u.Name.ToLowerInvariant().Contains(queryLower) ||
                                    u.Email.ToLowerInvariant().Contains(queryLower) ||
                                    u.Department.ToLowerInvariant().Contains(queryLower)))
                        .Take(10)
                        .Select(u => new Option<Guid>($"{u.Name} ({u.Department})", u.Id, description: u.Email))
                        .ToArray());
                });
        }

        QueryResult<Option<Guid>?> LookupUser(IViewContext context, Guid id)
        {
            return context.UseQuery<Option<Guid>?, (string, Guid)>(
                key: (nameof(LookupUser), id),
                fetcher: ct =>
                {
                    var user = Users.FirstOrDefault(u => u.Id == id);
                    return Task.FromResult(user != null
                        ? new Option<Guid>($"{user.Name} ({user.Department})", user.Id, description: user.Email)
                        : null);
                });
        }

        var customAsyncSelect = new AsyncSelectInputView<Guid>(
            selectedUser.Value,
            e => { selectedUser.Set(e.Value); return ValueTask.CompletedTask; },
            QueryUsers,
            LookupUser,
            placeholder: "Search by name, email, or department..."
        );

        return customAsyncSelect
                .WithField()
                .Label("Search and select a user:")
                .Width(Size.Full());
    }
}

public class StylingDemo : ViewBase
{
    public override object? Build()
    {
        var normalSelect = UseState<string?>(default(string));
        var invalidSelect = UseState<string?>(default(string));
        var disabledSelect = UseState<string?>(default(string));

        QueryResult<Option<string>[]> QueryOptions(IViewContext context, string query)
        {
            var options = new[] { "Option 1", "Option 2", "Option 3" };
            return context.UseQuery<Option<string>[], (string, string)>(
                key: (nameof(QueryOptions), query),
                fetcher: ct => Task.FromResult(options
                    .Where(opt => opt.Contains(query, StringComparison.OrdinalIgnoreCase))
                    .Select(opt => new Option<string>(opt))
                    .ToArray()));
        }

        QueryResult<Option<string>?> LookupOption(IViewContext context, string? option)
        {
            return context.UseQuery<Option<string>?, (string, string?)>(
                key: (nameof(LookupOption), option),
                fetcher: ct => Task.FromResult<Option<string>?>(option != null ? new Option<string>(option) : null));
        }

        return Layout.Vertical()
            | normalSelect.ToAsyncSelectInput(QueryOptions, LookupOption, placeholder: "Choose an option...")
                .WithField()
                .Label("Normal AsyncSelectInput:")
            
            | invalidSelect.ToAsyncSelectInput(QueryOptions, LookupOption, placeholder: "This has an error...")
                .Invalid("This field is required")
                .WithField()
                .Label("Invalid AsyncSelectInput:")
            
            | disabledSelect.ToAsyncSelectInput(QueryOptions, LookupOption, placeholder: "This is disabled...")
                .Disabled(true)
                .WithField()
                .Label("Disabled AsyncSelectInput:");
    }
}
