using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Common;

[App(order:8, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/03_Common/08_Table.md", searchHints: ["grid", "data", "rows", "columns", "cells", "table"])]
public class TableApp(bool onlyBody = false) : ViewBase
{
    public TableApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("table", "Table", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("custom-column-builders", "Custom Column Builders", 3), new ArticleHeading("column-management-examples", "Column Management Examples", 3), new ArticleHeading("advanced-aggregations", "Advanced Aggregations", 3), new ArticleHeading("pivot-tables", "Pivot Tables", 3), new ArticleHeading("empty-columns-handling", "Empty Columns Handling", 3), new ArticleHeading("reset-and-rebuild", "Reset and Rebuild", 3), new ArticleHeading("manual-table", "Manual Table", 3), new ArticleHeading("builder-factory-methods", "Builder Factory Methods", 3), new ArticleHeading("progress-builder", "Progress Builder", 3), new ArticleHeading("automatic-table-conversion", "Automatic Table Conversion", 3), new ArticleHeading("integration-with-other-widgets", "Integration with Other Widgets", 3), new ArticleHeading("faq", "Faq", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Table").OnLinkClick(onLinkClick)
            | Lead("Display structured data in a clean, organized format with powerful table [widgets](app://onboarding/concepts/widgets) that support sorting, filtering, and custom formatting.")
            | new Markdown(
                """"
                The `Table` [widget](app://onboarding/concepts/widgets) is a layout container designed to render data in a tabular format. It accepts rows composed of `TableRow` elements, making it suitable for structured display of content like data listings, reports, or grids.
                
                ## Basic Usage
                
                There is a recommended way to create tables from data arrays.
                The [ToTable()](app://onboarding/concepts/content-builders) extension method automatically converts collections into formatted tables.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicRowTable())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicRowTable : ViewBase
                    {
                        public class Product
                        {
                            public required string Sku { get; set; }
                            public required string Name { get; set; }
                            public required decimal Price { get; set; }
                            public required string Url { get; set; }
                        }
                    
                        public override object? Build()
                        {
                            var products = new[] {
                                new {Sku = "1234", Name = "T-shirt", Price = 10, Url = "http://example.com/tshirt"},
                                new {Sku = "1235", Name = "Jeans", Price = 20, Url = "http://example.com/jeans"},
                                new {Sku = "1236", Name = "Sneakers", Price = 30, Url = "http://example.com/sneakers"},
                            };
                    
                            return products.ToTable()
                                .Width(Size.Full());
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Custom Column Builders
                
                **Width([Size](app://api-reference/ivy/size).Full())** - sets the overall table width
                
                **ColumnWidth(p => p.ColumnName, Size.Units())** – sets the column width with [Size](app://api-reference/ivy/size)
                
                **ColumnWidth(p => p.ColumnName, Size.Fraction())** – sets the column width as a fraction (percentage) of available space
                
                Long text in cells automatically gets truncated with ellipsis (...) and shows full content in tooltips on hover
                
                **Header(p => p.ColumnName)** is used to show custom header text of the table
                
                **Align(p => p.ColumnName, [Align](app://api-reference/ivy/align).Left|Center|Right)** - sets the alignment for both the header and data cells in the selected column. The alignment applies to the content within cells, not the entire column structure.
                
                **Order(p => p.ColumnNameFirst, p.ColumnNameSecond, p.ColumnNameThird, ...)** - is used to order columns in a specific way
                
                **Remove(p => p.ColumnName)** - makes possible not to show column in the table.
                
                **Totals(p => p.ColumnName)** calculates the sum of the column if it contains numbers
                
                **Empty(new [Card](app://widgets/common/card)(""))** shows content when the table is empty.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TableConfigurationExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class TableConfigurationExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var products = new[] {
                                new {Sku = "1234", Name = "T-shirt", Price = 10, Url = "http://example.com/tshirt", _hiddenNotes = "archived"},
                                new {Sku = "1235", Name = "Jeans", Price = 20, Url = "http://example.com/jeans", _hiddenNotes = "best-seller"}
                            };
                    
                            return products.ToTable()
                                .Width(Size.Full())
                                .ColumnWidth(p => p.Price, Size.Units(100))
                                .ColumnWidth(p => p.Sku, Size.Fraction(0.15f))
                                .ColumnWidth(p => p.Name, Size.Fraction(0.3f))
                                .ColumnWidth(p => p.Url, Size.Fraction(0.55f))
                                .Header(p => p.Price, "Unit Price")
                                .Header(p => p._hiddenNotes, "Internal Notes") // underscore + letter hidden automatically
                                .Align(p => p.Price, Align.Right)
                                .Order(p => p.Name, p => p.Price, p => p.Sku)
                                .Remove(p => p.Url)
                                .Totals(p => p.Price)
                                .Empty(new Card("No products found").Width(Size.Full()));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("Columns whose names start with an underscore followed by a letter (for example `_hidden`, `_internalId`) are automatically removed by default. Properties that use an underscore followed by a digit or symbol (such as `_1` or `_$special`) now stay visible unless you explicitly hide them.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Column Management Examples
                
                The `Clear()` method hides all columns, allowing you to selectively show only the columns you need.
                Use `Add()` to show specific columns in the order you want them to appear.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ColumnManagementTable())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ColumnManagementTable : ViewBase
                    {
                        public override object? Build()
                        {
                            var products = new[] {
                                new {Sku = "1234", Name = "T-shirt", Price = 10, Category = "Clothing", Stock = 50, _hiddenInternal = "archived"},
                                new {Sku = "1235", Name = "Jeans", Price = 20, Category = "Clothing", Stock = 30, _hiddenInternal = "featured"},
                                new {Sku = "1236", Name = "Sneakers", Price = 30, Category = "Footwear", Stock = 25, _hiddenInternal = "featured"}
                            };
                    
                            return products.ToTable()
                                .Width(Size.Full())
                                .Clear()                                    // Hide all columns first
                                .Add(p => p.Name)                          // Show only Name column
                                .Add(p => p.Price)                         // Add Price column
                                .Add(p => p.Stock)                         // Add Stock column
                                .Header(p => p._hiddenInternal, "Internal Flag") // hidden by default due to underscore
                                .Header(p => p.Price, "Unit Price")
                                .Align(p => p.Price, Align.Right)
                                .Align(p => p.Stock, Align.Center);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Advanced Aggregations
                
                The `Totals()` method supports custom aggregation functions.
                You can use LINQ methods like `Count()`, `Average()`, `Sum()`, `Max()`, and `Min()` to create sophisticated calculations for your data.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new AdvancedAggregationsTable())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class AdvancedAggregationsTable : ViewBase
                    {
                        public override object? Build()
                        {
                            var products = new[] {
                                new {Sku = "1234", Name = "T-shirt", Price = 10, Stock = 50},
                                new {Sku = "1235", Name = "Jeans", Price = 20, Stock = 30},
                                new {Sku = "1236", Name = "Sneakers", Price = 30, Stock = 25}
                            };
                    
                            return products.ToTable()
                                .Width(Size.Full())
                                .Header(p => p.Price, "Unit Price")
                                .Header(p => p.Stock, "In Stock")
                                .Align(p => p.Price, Align.Right)
                                .Align(p => p.Stock, Align.Center)
                                .Totals(p => p.Price)                      // Sum of prices
                                .Totals(p => p.Stock, items => items.Count()) // Count of items
                                .Totals(p => p.Price, items => items.Average(p => p.Price)) // Average price
                                .Totals(p => p.Stock, items => items.Sum(p => p.Stock)); // Total stock
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Pivot Tables
                
                Pivot tables allow you to aggregate and summarize data by grouping on dimensions and calculating measures. Use the `ToPivotTable()` extension method to transform data into aggregated results that can be displayed as tables.
                
                **Dimension** - A field to group by (e.g., Category, Region, Date)
                
                **Measure** - An aggregated calculation (e.g., Sum, Count, Average, Max, Min)
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new PivotTableExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class PivotTableExample : ViewBase
                    {
                        record SalesData(string Browser, string Region, int Sessions, decimal Revenue);
                        record BrowserSummary(string Browser, int TotalSessions, decimal TotalRevenue, decimal AverageRevenue);
                    
                        public override async Task<object?> Build()
                        {
                            var rawData = new[]
                            {
                                new SalesData("Chrome", "North", 150, 4500m),
                                new SalesData("Chrome", "South", 120, 3600m),
                                new SalesData("Firefox", "North", 80, 2400m),
                                new SalesData("Firefox", "South", 60, 1800m),
                                new SalesData("Safari", "North", 50, 1500m),
                                new SalesData("Safari", "South", 40, 1200m)
                            };
                    
                            var pivotByBrowser = await rawData.ToPivotTable()
                                .Dimension("Browser", d => d.Browser)
                                .Measure("Total Sessions", g => g.Sum(s => s.Sessions))
                                .Measure("Total Revenue", g => g.Sum(s => s.Revenue))
                                .Measure("Average Revenue", g => g.Average(s => s.Revenue))
                                .ExecuteAsync();
                    
                            var pivotByBrowserAndRegion = await rawData.ToPivotTable()
                                .Dimension("Browser", d => d.Browser)
                                .Dimension("Region", d => d.Region)
                                .Measure("Sessions", g => g.Sum(s => s.Sessions))
                                .Measure("Revenue", g => g.Sum(s => s.Revenue))
                                .ExecuteAsync();
                    
                            var typedResults = new List<BrowserSummary>();
                            await foreach (var item in rawData.ToPivotTable()
                                .Dimension("Browser", d => d.Browser)
                                .Measure("TotalSessions", g => g.Sum(s => s.Sessions))
                                .Measure("TotalRevenue", g => g.Sum(s => s.Revenue))
                                .Measure("AverageRevenue", g => g.Average(s => s.Revenue))
                                .Produces<BrowserSummary>()
                                .ExecuteAsync())
                            {
                                typedResults.Add(item);
                            }
                    
                            return Layout.Vertical().Gap(4)
                                | Text.H3("Raw Data")
                                | rawData.ToTable().Width(Size.Full())
                                | pivotByBrowser.ToExpando().ToTable().Width(Size.Full())
                                | pivotByBrowserAndRegion.ToExpando().ToTable().Width(Size.Full())
                                | Text.H3("Pivot Table - Strongly Typed Result")
                                | typedResults.ToTable()
                                    .Width(Size.Full())
                                    .Header(r => r.Browser, "Browser")
                                    .Header(r => r.TotalSessions, "Total Sessions")
                                    .Header(r => r.TotalRevenue, "Revenue")
                                    .Header(r => r.AverageRevenue, "Avg Revenue")
                                    .Align(r => r.TotalSessions, Align.Right)
                                    .Align(r => r.TotalRevenue, Align.Right)
                                    .Align(r => r.AverageRevenue, Align.Right);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Empty Columns Handling
                
                The `RemoveEmptyColumns()` method automatically hides columns that contain no data (empty strings, null values, or zero values). This is useful for dynamic data where some columns might be empty across all rows.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new EmptyColumnsTable())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class EmptyColumnsTable : ViewBase
                    {
                        public override object? Build()
                        {
                            var products = new[] {
                                new {Sku = "1234", Name = "T-shirt", Price = 10, Category = "Clothing", Notes = ""},
                                new {Sku = "1235", Name = "Jeans", Price = 20, Category = "Clothing", Notes = ""},
                                new {Sku = "1236", Name = "Sneakers", Price = 30, Category = "Footwear", Notes = ""}
                            };
                    
                            return products.ToTable()
                                .Width(Size.Full())
                                .RemoveEmptyColumns()                      // Automatically hides "Notes" because it's empty in all rows
                                .Header(p => p.Price, "Unit Price")
                                .Align(p => p.Price, Align.Right);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Reset and Rebuild
                
                The `Reset()` method restores all column settings to their default values. This is useful when you want to start fresh with a new configuration or when building dynamic table configurations.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ResetTableExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ResetTableExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var products = new[] {
                                new {Sku = "1234", Name = "T-shirt", Price = 10, Category = "Clothing", _hiddenMetadata = "legacy"},
                                new {Sku = "1235", Name = "Jeans", Price = 20, Category = "Clothing", _hiddenMetadata = "seasonal"}
                            };
                    
                            return products.ToTable()
                                .Width(Size.Full())
                                .Remove(p => p.Category)                   // Hide Category column
                                .Align(p => p.Price, Align.Right)          // Set alignment
                                .Header(p => p.Price, "Unit Price")        // Custom header
                                .Header(p => p._hiddenMetadata, "Metadata") // underscore + letter hidden automatically
                                .Reset()                                   // Reset all settings to defaults
                                .Order(p => p.Name, p => p.Price)          // Apply new order
                                .Totals(p => p.Price);                     // Add totals
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Manual Table
                
                It's also possible to create manual tables with headers and other methods using rows and cells:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ManualTableDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ManualTableDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            return new Table(
                                new TableRow(
                                    new TableCell("Name").IsHeader().Align(Align.Left),
                                    new TableCell("Age").IsHeader().Align(Align.Center),
                                    new TableCell("Email").IsHeader().Align(Align.Left)
                                ),
                                new TableRow(
                                    new TableCell("Alice"),
                                    new TableCell("30").Align(Align.Center),
                                    new TableCell("alice@example.com")
                                )
                            ).Width(Size.Full());
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Builder Factory Methods
                
                The `Builder()` method allows you to specify how different data types should be rendered. Use the builder factory methods to create appropriate renderers for your data.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new CellBuildersExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class CellBuildersExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var products = new[] {
                                new {Sku = "1234", Name = "T-shirt", Price = 10, Url = "http://example.com/tshirt", Description = "High quality cotton T-shirt with a comfortable fit and durable construction. Perfect for everyday wear and available in multiple colors."},
                                new {Sku = "1235", Name = "Jeans", Price = 20, Url = "http://example.com/jeans", Description = "Classic denim jeans with a modern cut and premium stitching. Features include reinforced pockets, comfortable waistband, and fade-resistant fabric."}
                            };
                    
                            return products.ToTable()
                                .Width(Size.Full())
                                .ColumnWidth(p => p.Sku, Size.Fraction(0.15f))          // 15% for SKU
                                .ColumnWidth(p => p.Name, Size.Fraction(0.25f))         // 25% for Name
                                .ColumnWidth(p => p.Price, Size.Fraction(0.15f))        // 15% for Price
                                .ColumnWidth(p => p.Url, Size.Fraction(0.2f))           // 20% for URL
                                .ColumnWidth(p => p.Description, Size.Fraction(0.25f))  // 25% for Description
                                .Multiline(p => p.Description)                    // Enable multiline for the Description column
                                .Builder(p => p.Url, f => f.Link())               // Link builder
                                .Builder(p => p.Description, f => f.Text())       // Text builder
                                .Builder(p => p.Sku, f => f.CopyToClipboard())    // Copy to clipboard
                                .Builder(p => p.Name, f => f.Default())           // Default builder
                                .Header(p => p.Price, "Unit Price")
                                .Align(p => p.Price, Align.Right);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Progress Builder
                
                The `Progress()` builder renders numeric values as inline [progress](app://widgets/common/progress) bars within table cells.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ProgressBuilderExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ProgressBuilderExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var tasks = new[] {
                                new {Name = "Design Review", Progress = 100},
                                new {Name = "Implementation", Progress = 75},
                                new {Name = "Testing", Progress = 45},
                                new {Name = "Documentation", Progress = 20}
                            };
                    
                            return tasks.ToTable()
                                .Width(Size.Full())
                                .Builder(t => t.Progress, f => f.Progress().AutoColor().Format("%d%"))
                                .ColumnWidth(t => t.Name, Size.Fraction(0.5f))
                                .ColumnWidth(t => t.Progress, Size.Fraction(0.5f));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Automatic Table Conversion
                
                Any `IEnumerable` is automatically converted to a table when returned from a view. This works through the [DefaultContentBuilder](app://onboarding/concepts/content-builders) which detects collections and converts them to tables.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new AutomaticTableConversion())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class AutomaticTableConversion : ViewBase
                    {
                        public override object? Build()
                        {
                            // Any IEnumerable is automatically converted to a table
                            object data = GetProductData();
                            return data; // Automatically becomes a table via DefaultContentBuilder
                        }
                    
                        private object GetProductData()
                        {
                            return new[] {
                                new {Sku = "1234", Name = "T-shirt", Price = 10},
                                new {Sku = "1235", Name = "Jeans", Price = 20}
                            };
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Integration with Other Widgets
                
                Tables integrate seamlessly with other Ivy widgets, allowing you to create rich, interactive [interfaces](app://onboarding/concepts/views).
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TableIntegrationExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class TableIntegrationExample : ViewBase
                    {
                        record Product(string Sku, string Name, double Price);
                        public override object? Build()
                        {
                    
                            var products = UseState<Product[]>(
                                [new Product("1234", "T-shirt", 10.0), new Product("1235", "Jeans", 20.0)]
                                );
                    
                            var client = UseService<IClientProvider>();
                    
                            var addProduct = (Event<Button> e) =>
                            {
                                var currentCount = products.Value.Length;
                    
                                var newProduct = new Product(
                                    Sku: $"SKU{1000 + currentCount}",
                                    Name: $"Product {currentCount + 1}",
                                    Price: 15.0 + currentCount
                                );
                    
                                var updatedProducts = products.Value.Append(newProduct).ToArray();
                                products.Set(updatedProducts);
                    
                                client.Toast($"Added {newProduct.Name}", "Product Added");
                            };
                    
                            var clearProducts = (Event<Button> e) =>
                            {
                                products.Set(new Product[0]);
                                client.Toast("All products cleared", "Products Cleared");
                            };
                    
                            return Layout.Vertical()
                                | new Card(
                                    Layout.Vertical()
                                        | products.Value.ToTable().Width(Size.Full())
                                        | (Layout.Horizontal().Gap(2)
                                            | new Button("Add Product", addProduct).Variant(ButtonVariant.Secondary)
                                            | new Button("Clear All", clearProducts).Variant(ButtonVariant.Destructive))
                                        | Text.Block($"Total Products: {products.Value.Length}")
                                ).Title("Product List").Width(Size.Full());
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("## Faq").OnLinkClick(onLinkClick)
            | new Expandable("How do I create a simple Table with headers and rows in Ivy?",
                Vertical().Gap(4)
                | new Markdown("For simple tabular display, use `.ToTable()` on a collection of records:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    var data = new[]
                    {
                        new { Name = "Alice", Age = 30, City = "NYC" },
                        new { Name = "Bob", Age = 25, City = "LA" },
                    };
                    return data.ToTable();
                    """",Languages.Csharp)
                | new Markdown("For manual Table construction with `TableRow` and `TableCell`:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    new Table(
                        new TableRow(new TableCell("Name"), new TableCell("Age")).IsHeader(),
                        new TableRow(new TableCell("Alice"), new TableCell("30")),
                        new TableRow(new TableCell("Bob"), new TableCell("25"))
                    )
                    """",Languages.Csharp)
                | new Markdown("**Important:** `Table` takes `TableRow[]`, NOT `string[]`. There is no `.Row()` method. For data-heavy tables with sorting, filtering, and pagination, use `.ToDataTable()` on `IQueryable<T>` instead.").OnLinkClick(onLinkClick)
            )
            | new WidgetDocsView("Ivy.Table", "Ivy.TableExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Tables/Table.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.ContentBuildersApp), typeof(ApiReference.Ivy.SizeApp), typeof(ApiReference.Ivy.AlignApp), typeof(Widgets.Common.CardApp), typeof(Widgets.Common.ProgressApp), typeof(Onboarding.Concepts.ViewsApp)]; 
        return article;
    }
}


public class BasicRowTable : ViewBase
{
    public class Product
    {
        public required string Sku { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required string Url { get; set; }
    }

    public override object? Build()
    {
        var products = new[] {
            new {Sku = "1234", Name = "T-shirt", Price = 10, Url = "http://example.com/tshirt"},
            new {Sku = "1235", Name = "Jeans", Price = 20, Url = "http://example.com/jeans"},
            new {Sku = "1236", Name = "Sneakers", Price = 30, Url = "http://example.com/sneakers"},
        };

        return products.ToTable()
            .Width(Size.Full());
    }
}

public class TableConfigurationExample : ViewBase
{
    public override object? Build()
    {
        var products = new[] {
            new {Sku = "1234", Name = "T-shirt", Price = 10, Url = "http://example.com/tshirt", _hiddenNotes = "archived"},
            new {Sku = "1235", Name = "Jeans", Price = 20, Url = "http://example.com/jeans", _hiddenNotes = "best-seller"}
        };

        return products.ToTable()
            .Width(Size.Full())
            .ColumnWidth(p => p.Price, Size.Units(100))
            .ColumnWidth(p => p.Sku, Size.Fraction(0.15f))
            .ColumnWidth(p => p.Name, Size.Fraction(0.3f))
            .ColumnWidth(p => p.Url, Size.Fraction(0.55f))
            .Header(p => p.Price, "Unit Price")
            .Header(p => p._hiddenNotes, "Internal Notes") // underscore + letter hidden automatically
            .Align(p => p.Price, Align.Right)
            .Order(p => p.Name, p => p.Price, p => p.Sku)
            .Remove(p => p.Url)
            .Totals(p => p.Price)
            .Empty(new Card("No products found").Width(Size.Full()));
    }
}

public class ColumnManagementTable : ViewBase
{
    public override object? Build()
    {
        var products = new[] {
            new {Sku = "1234", Name = "T-shirt", Price = 10, Category = "Clothing", Stock = 50, _hiddenInternal = "archived"},
            new {Sku = "1235", Name = "Jeans", Price = 20, Category = "Clothing", Stock = 30, _hiddenInternal = "featured"},
            new {Sku = "1236", Name = "Sneakers", Price = 30, Category = "Footwear", Stock = 25, _hiddenInternal = "featured"}
        };

        return products.ToTable()
            .Width(Size.Full())
            .Clear()                                    // Hide all columns first
            .Add(p => p.Name)                          // Show only Name column
            .Add(p => p.Price)                         // Add Price column
            .Add(p => p.Stock)                         // Add Stock column
            .Header(p => p._hiddenInternal, "Internal Flag") // hidden by default due to underscore
            .Header(p => p.Price, "Unit Price")
            .Align(p => p.Price, Align.Right)
            .Align(p => p.Stock, Align.Center);
    }
}

public class AdvancedAggregationsTable : ViewBase
{
    public override object? Build()
    {
        var products = new[] {
            new {Sku = "1234", Name = "T-shirt", Price = 10, Stock = 50},
            new {Sku = "1235", Name = "Jeans", Price = 20, Stock = 30},
            new {Sku = "1236", Name = "Sneakers", Price = 30, Stock = 25}
        };

        return products.ToTable()
            .Width(Size.Full())
            .Header(p => p.Price, "Unit Price")
            .Header(p => p.Stock, "In Stock")
            .Align(p => p.Price, Align.Right)
            .Align(p => p.Stock, Align.Center)
            .Totals(p => p.Price)                      // Sum of prices
            .Totals(p => p.Stock, items => items.Count()) // Count of items
            .Totals(p => p.Price, items => items.Average(p => p.Price)) // Average price
            .Totals(p => p.Stock, items => items.Sum(p => p.Stock)); // Total stock
    }
}

public class PivotTableExample : ViewBase
{
    record SalesData(string Browser, string Region, int Sessions, decimal Revenue);
    record BrowserSummary(string Browser, int TotalSessions, decimal TotalRevenue, decimal AverageRevenue);

    public override async Task<object?> Build()
    {
        var rawData = new[]
        {
            new SalesData("Chrome", "North", 150, 4500m),
            new SalesData("Chrome", "South", 120, 3600m),
            new SalesData("Firefox", "North", 80, 2400m),
            new SalesData("Firefox", "South", 60, 1800m),
            new SalesData("Safari", "North", 50, 1500m),
            new SalesData("Safari", "South", 40, 1200m)
        };

        var pivotByBrowser = await rawData.ToPivotTable()
            .Dimension("Browser", d => d.Browser)
            .Measure("Total Sessions", g => g.Sum(s => s.Sessions))
            .Measure("Total Revenue", g => g.Sum(s => s.Revenue))
            .Measure("Average Revenue", g => g.Average(s => s.Revenue))
            .ExecuteAsync();

        var pivotByBrowserAndRegion = await rawData.ToPivotTable()
            .Dimension("Browser", d => d.Browser)
            .Dimension("Region", d => d.Region)
            .Measure("Sessions", g => g.Sum(s => s.Sessions))
            .Measure("Revenue", g => g.Sum(s => s.Revenue))
            .ExecuteAsync();

        var typedResults = new List<BrowserSummary>();
        await foreach (var item in rawData.ToPivotTable()
            .Dimension("Browser", d => d.Browser)
            .Measure("TotalSessions", g => g.Sum(s => s.Sessions))
            .Measure("TotalRevenue", g => g.Sum(s => s.Revenue))
            .Measure("AverageRevenue", g => g.Average(s => s.Revenue))
            .Produces<BrowserSummary>()
            .ExecuteAsync())
        {
            typedResults.Add(item);
        }

        return Layout.Vertical().Gap(4)
            | Text.H3("Raw Data")
            | rawData.ToTable().Width(Size.Full())
            | pivotByBrowser.ToExpando().ToTable().Width(Size.Full())
            | pivotByBrowserAndRegion.ToExpando().ToTable().Width(Size.Full())
            | Text.H3("Pivot Table - Strongly Typed Result")
            | typedResults.ToTable()
                .Width(Size.Full())
                .Header(r => r.Browser, "Browser")
                .Header(r => r.TotalSessions, "Total Sessions")
                .Header(r => r.TotalRevenue, "Revenue")
                .Header(r => r.AverageRevenue, "Avg Revenue")
                .Align(r => r.TotalSessions, Align.Right)
                .Align(r => r.TotalRevenue, Align.Right)
                .Align(r => r.AverageRevenue, Align.Right);
    }
}

public class EmptyColumnsTable : ViewBase
{
    public override object? Build()
    {
        var products = new[] {
            new {Sku = "1234", Name = "T-shirt", Price = 10, Category = "Clothing", Notes = ""},
            new {Sku = "1235", Name = "Jeans", Price = 20, Category = "Clothing", Notes = ""},
            new {Sku = "1236", Name = "Sneakers", Price = 30, Category = "Footwear", Notes = ""}
        };

        return products.ToTable()
            .Width(Size.Full())
            .RemoveEmptyColumns()                      // Automatically hides "Notes" because it's empty in all rows
            .Header(p => p.Price, "Unit Price")
            .Align(p => p.Price, Align.Right);
    }
}

public class ResetTableExample : ViewBase
{
    public override object? Build()
    {
        var products = new[] {
            new {Sku = "1234", Name = "T-shirt", Price = 10, Category = "Clothing", _hiddenMetadata = "legacy"},
            new {Sku = "1235", Name = "Jeans", Price = 20, Category = "Clothing", _hiddenMetadata = "seasonal"}
        };

        return products.ToTable()
            .Width(Size.Full())
            .Remove(p => p.Category)                   // Hide Category column
            .Align(p => p.Price, Align.Right)          // Set alignment
            .Header(p => p.Price, "Unit Price")        // Custom header
            .Header(p => p._hiddenMetadata, "Metadata") // underscore + letter hidden automatically
            .Reset()                                   // Reset all settings to defaults
            .Order(p => p.Name, p => p.Price)          // Apply new order
            .Totals(p => p.Price);                     // Add totals
    }
}

public class ManualTableDemo : ViewBase
{
    public override object? Build()
    {
        return new Table(
            new TableRow(
                new TableCell("Name").IsHeader().Align(Align.Left),
                new TableCell("Age").IsHeader().Align(Align.Center),
                new TableCell("Email").IsHeader().Align(Align.Left)
            ),
            new TableRow(
                new TableCell("Alice"),
                new TableCell("30").Align(Align.Center),
                new TableCell("alice@example.com")
            )
        ).Width(Size.Full());
    }
}

public class CellBuildersExample : ViewBase
{
    public override object? Build()
    {
        var products = new[] {
            new {Sku = "1234", Name = "T-shirt", Price = 10, Url = "http://example.com/tshirt", Description = "High quality cotton T-shirt with a comfortable fit and durable construction. Perfect for everyday wear and available in multiple colors."},
            new {Sku = "1235", Name = "Jeans", Price = 20, Url = "http://example.com/jeans", Description = "Classic denim jeans with a modern cut and premium stitching. Features include reinforced pockets, comfortable waistband, and fade-resistant fabric."}
        };

        return products.ToTable()
            .Width(Size.Full())
            .ColumnWidth(p => p.Sku, Size.Fraction(0.15f))          // 15% for SKU
            .ColumnWidth(p => p.Name, Size.Fraction(0.25f))         // 25% for Name
            .ColumnWidth(p => p.Price, Size.Fraction(0.15f))        // 15% for Price
            .ColumnWidth(p => p.Url, Size.Fraction(0.2f))           // 20% for URL
            .ColumnWidth(p => p.Description, Size.Fraction(0.25f))  // 25% for Description
            .Multiline(p => p.Description)                    // Enable multiline for the Description column
            .Builder(p => p.Url, f => f.Link())               // Link builder
            .Builder(p => p.Description, f => f.Text())       // Text builder
            .Builder(p => p.Sku, f => f.CopyToClipboard())    // Copy to clipboard
            .Builder(p => p.Name, f => f.Default())           // Default builder
            .Header(p => p.Price, "Unit Price")
            .Align(p => p.Price, Align.Right);
    }
}

public class ProgressBuilderExample : ViewBase
{
    public override object? Build()
    {
        var tasks = new[] {
            new {Name = "Design Review", Progress = 100},
            new {Name = "Implementation", Progress = 75},
            new {Name = "Testing", Progress = 45},
            new {Name = "Documentation", Progress = 20}
        };

        return tasks.ToTable()
            .Width(Size.Full())
            .Builder(t => t.Progress, f => f.Progress().AutoColor().Format("%d%"))
            .ColumnWidth(t => t.Name, Size.Fraction(0.5f))
            .ColumnWidth(t => t.Progress, Size.Fraction(0.5f));
    }
}

public class AutomaticTableConversion : ViewBase
{
    public override object? Build()
    {
        // Any IEnumerable is automatically converted to a table
        object data = GetProductData();
        return data; // Automatically becomes a table via DefaultContentBuilder
    }

    private object GetProductData()
    {
        return new[] {
            new {Sku = "1234", Name = "T-shirt", Price = 10},
            new {Sku = "1235", Name = "Jeans", Price = 20}
        };
    }
}

public class TableIntegrationExample : ViewBase
{
    record Product(string Sku, string Name, double Price);
    public override object? Build()
    {

        var products = UseState<Product[]>(
            [new Product("1234", "T-shirt", 10.0), new Product("1235", "Jeans", 20.0)]
            );

        var client = UseService<IClientProvider>();

        var addProduct = (Event<Button> e) =>
        {
            var currentCount = products.Value.Length;

            var newProduct = new Product(
                Sku: $"SKU{1000 + currentCount}",
                Name: $"Product {currentCount + 1}",
                Price: 15.0 + currentCount
            );

            var updatedProducts = products.Value.Append(newProduct).ToArray();
            products.Set(updatedProducts);

            client.Toast($"Added {newProduct.Name}", "Product Added");
        };

        var clearProducts = (Event<Button> e) =>
        {
            products.Set(new Product[0]);
            client.Toast("All products cleared", "Products Cleared");
        };

        return Layout.Vertical()
            | new Card(
                Layout.Vertical()
                    | products.Value.ToTable().Width(Size.Full())
                    | (Layout.Horizontal().Gap(2)
                        | new Button("Add Product", addProduct).Variant(ButtonVariant.Secondary)
                        | new Button("Clear All", clearProducts).Variant(ButtonVariant.Destructive))
                    | Text.Block($"Total Products: {products.Value.Length}")
            ).Title("Product List").Width(Size.Full());
    }
}
