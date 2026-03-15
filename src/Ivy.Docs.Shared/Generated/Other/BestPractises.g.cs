using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Other;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/05_Other/BestPractises.md")]
public class BestPractisesApp(bool onlyBody = false) : ViewBase
{
    public BestPractisesApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("best-practises", "Best Practises", 1), new ArticleHeading("use-usequery-for-data-fetching", "Use UseQuery for Data Fetching", 2), new ArticleHeading("dont-use-padding-and-gap", "Don't Use Padding and Gap", 2), new ArticleHeading("use-totable-instead-of-tableview", "Use ToTable Instead of TableView", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # Best Practises
                
                ## Use UseQuery for Data Fetching
                
                Prefer `UseQuery` over manual `UseEffect` + `UseState` combinations for data fetching. UseQuery provides built-in loading, error, and caching (SWR) support.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Bad - manual state management
                var data = UseState(ImmutableArray.Create<Item>());
                var error = UseState<string?>();
                var loaded = UseState(false);
                
                UseEffect(async () =>
                {
                    try
                    {
                        var response = await client.GetItemsAsync();
                        data.Set(response.ToImmutableArray());
                    }
                    catch (Exception ex)
                    {
                        error.Set(ex.Message);
                    }
                    finally
                    {
                        loaded.Set(true);
                    }
                }, EffectTrigger.OnMount());
                
                // Good - UseQuery handles loading, error, and caching
                var query = UseQuery(
                    key: "items",
                    fetcher: async ct =>
                    {
                        var response = await client.GetItemsAsync();
                        return response.ToImmutableArray();
                    });
                
                if (query.Loading) return Text.Muted("Loading...");
                if (query.Error is { } error) return Callout.Error(error.Message);
                """",Languages.Csharp)
            | new Markdown("For conditional fetching, pass `null` as the key to disable the query:").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var search = UseQuery(
                    key: searchTerm is { } term ? $"search:{term}" : null,
                    fetcher: async ct => await client.SearchAsync(searchTerm!, ct));
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Don't Use Padding and Gap
                
                Layouts use a default gap of 4. Avoid adding `.Padding()` and `.Gap()` unless absolutely necessary.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Bad
                Layout.Vertical().Padding(6).Gap(6)
                    | Text.H2("Title")
                    | Text.Muted("Subtitle");
                
                // Good
                Layout.Vertical()
                    | Text.H2("Title")
                    | Text.Muted("Subtitle");
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Use ToTable Instead of TableView
                
                Prefer `.ToTable()` over manually constructing `TableView` with `TableColumn` definitions. `ToTable()` auto-scaffolds columns from the model's properties with smart defaults (link builders for URL fields, right-alignment for numbers, etc.).
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Bad - verbose manual column definitions
                new TableView<RepoRow>(
                    query.Value,
                    new TableColumn<RepoRow, string>(r => r.FullName, "Repository"),
                    new TableColumn<RepoRow, string>(r => r.Language, "Language"),
                    new TableColumn<RepoRow, int>(r => r.Stars, "Stars"),
                    new TableColumn<RepoRow, int>(r => r.Forks, "Forks"));
                
                // Good - auto-scaffolded with header overrides where needed
                query.Value.ToTable()
                    .Header(r => r.FullName, "Repository");
                """",Languages.Csharp)
            ;
        return article;
    }
}

