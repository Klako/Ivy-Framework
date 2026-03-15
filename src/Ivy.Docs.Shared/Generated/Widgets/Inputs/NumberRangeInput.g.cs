using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Inputs;

[App(order:12, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/04_Inputs/12_NumberRangeInput.md", searchHints: ["numeric", "range", "slider", "min", "max", "interval"])]
public class NumberRangeInputApp(bool onlyBody = false) : ViewBase
{
    public NumberRangeInputApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("numberrangeinput", "NumberRangeInput", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("supported-types", "Supported Types", 2), new ArticleHeading("variants", "Variants", 2), new ArticleHeading("format-styles", "Format Styles", 2), new ArticleHeading("prefix-and-suffix", "Prefix and Suffix", 2), new ArticleHeading("step-increments", "Step Increments", 2), new ArticleHeading("event-handling", "Event Handling", 2), new ArticleHeading("api", "API", 2), new ArticleHeading("faq", "FAQ", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# NumberRangeInput").OnLinkClick(onLinkClick)
            | Lead("Select numeric ranges with dual draggable handles on a single slider, perfect for filtering by price, thresholds, or any min/max numeric bounds.")
            | new Markdown("The `NumberRangeInput` [widget](app://onboarding/concepts/widgets) provides an intuitive range slider for selecting minimum and maximum numeric values. It uses two draggable handles on a single track, with the filled region between handles representing the selected range.").OnLinkClick(onLinkClick)
            | new Callout("Like `NumberInput`, if you don't explicitly specify `Min` and `Max` for a `NumberRangeInput`, common default values will be applied based on the numeric type. Always set `Min` and `Max` to define your desired range bounds.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Basic Usage
                
                Here's a simple example of a `NumberRangeInput` that allows users to select a numeric range:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicNumberRangeDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var range = UseState<(int, int)>(() => (25, 75));
                            var lower = range.Value.Item1;
                            var upper = range.Value.Item2;
                    
                            return Layout.Vertical()
                                    | range.ToNumberRangeInput()
                                           .Min(0)
                                           .Max(100)
                                    | Text.P($"Selected range: {lower} to {upper}").Large();
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicNumberRangeDemo())
            )
            | new Markdown(
                """"
                The lower and upper bounds can be accessed using `range.Value.Item1` and `range.Value.Item2`.
                
                ## Supported Types
                
                NumberRangeInput supports all numeric tuple types:
                
                - `(short, short)` and `(short?, short?)`
                - `(int, int)` and `(int?, int?)`
                - `(long, long)` and `(long?, long?)`
                - `(byte, byte)` and `(byte?, byte?)`
                - `(float, float)` and `(float?, float?)`
                - `(double, double)` and `(double?, double?)`
                - `(decimal, decimal)` and `(decimal?, decimal?)`
                
                ## Variants
                
                The `NumberRangeInput` can be customized with various states including **Disabled**, **Invalid**, and **Nullable**:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class NumberRangeVariantsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var range = UseState<(int, int)>(() => (30, 70));
                            var nullableRange = UseState<(int?, int?)>(() => (30, 70));
                    
                            return Layout.Vertical().Gap(4)
                                | Text.P("Normal State").Small()
                                | range.ToNumberRangeInput().Min(0).Max(100)
                                | Text.P("Disabled State").Small()
                                | range.ToNumberRangeInput().Min(0).Max(100).Disabled()
                                | Text.P("Invalid State").Small()
                                | range.ToNumberRangeInput().Min(0).Max(100).Invalid("Invalid range")
                                | Text.P("Nullable State").Small()
                                | nullableRange.ToNumberRangeInput().Min(0).Max(100);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new NumberRangeVariantsDemo())
            )
            | new Markdown(
                """"
                ## Format Styles
                
                NumberRangeInput supports three format styles: **Decimal** (default), **Currency**, and **Percent**:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class NumberRangeFormatDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var decimalRange = UseState<(int, int)>(() => (25, 75));
                            var priceRange = UseState<(decimal, decimal)>(() => (50.0m, 200.0m));
                            var percentRange = UseState<(double, double)>(() => (0.25, 0.75));
                    
                            return Layout.Vertical().Gap(4)
                                | Text.P("Decimal Format").Small()
                                | decimalRange.ToNumberRangeInput()
                                              .Min(0)
                                              .Max(100)
                                              .FormatStyle(NumberFormatStyle.Decimal)
                                | Text.P("Currency Format (USD)").Small()
                                | priceRange.ToNumberRangeInput()
                                            .Min(0)
                                            .Max(500)
                                            .FormatStyle(NumberFormatStyle.Currency)
                                            .Currency("USD")
                                | Text.P("Percent Format").Small()
                                | percentRange.ToNumberRangeInput()
                                              .Min(0)
                                              .Max(1)
                                              .Step(0.01)
                                              .FormatStyle(NumberFormatStyle.Percent)
                                              .Precision(2);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new NumberRangeFormatDemo())
            )
            | new Markdown("For currency ranges, use `ToMoneyRangeInput()` as a convenient shorthand:").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class MoneyRangeDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var budgetRange = UseState<(decimal, decimal)>(() => (50.0m, 200.0m));
                    
                            return Layout.Vertical()
                                | Text.P("Budget Filter").Small()
                                | budgetRange.ToMoneyRangeInput()
                                             .Currency("USD")
                                             .Min(0)
                                             .Max(500)
                                             .Step(10);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new MoneyRangeDemo())
            )
            | new Markdown(
                """"
                ## Prefix and Suffix
                
                Add contextual information with text or icon prefixes and suffixes:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class NumberRangePrefixSuffixDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var priceRange = UseState<(decimal, decimal)>(() => (50.0m, 200.0m));
                            var percentRange = UseState<(double, double)>(() => (0.25, 0.75));
                            var tempRange = UseState<(int, int)>(() => (18, 24));
                    
                            return Layout.Vertical().Gap(4)
                                | Text.P("Text Prefix ($)").Small()
                                | priceRange.ToNumberRangeInput()
                                            .Min(0)
                                            .Max(500)
                                            .Prefix("$")
                                            .Precision(2)
                                | Text.P("Text Suffix (%)").Small()
                                | percentRange.ToNumberRangeInput()
                                              .Min(0)
                                              .Max(1)
                                              .Step(0.01)
                                              .Suffix("%")
                                              .Precision(2)
                                | Text.P("Icon Prefix + Text Suffix").Small()
                                | tempRange.ToNumberRangeInput()
                                           .Min(-10)
                                           .Max(40)
                                           .Prefix(Icons.Thermometer)
                                           .Suffix("°C");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new NumberRangePrefixSuffixDemo())
            )
            | new Markdown(
                """"
                The `Prefix` and `Suffix` methods accept either a `string` or an `Icons` value.
                
                ## Step Increments
                
                Control how the slider handles move using the `Step` property:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class NumberRangeStepDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var range = UseState<(int, int)>(() => (20, 80));
                    
                            return Layout.Vertical().Gap(4)
                                | Text.P("Step: 10 (snaps to multiples of 10)").Small()
                                | range.ToNumberRangeInput()
                                       .Min(0)
                                       .Max(100)
                                       .Step(10);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new NumberRangeStepDemo())
            )
            | new Markdown(
                """"
                ## Event Handling
                
                NumberRangeInput supports change events that receive the full tuple value:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var rangeState = UseState<(int, int)>(() => (0, 100));
                var changeLabel = UseState("");
                
                new NumberRangeInput<int>(rangeState.Value, e =>
                {
                    rangeState.Set(e);
                    changeLabel.Set($"Range changed to: {e.Item1} - {e.Item2}");
                })
                {
                    Min = 0,
                    Max = 100
                }
                """",Languages.Csharp)
            | new WidgetDocsView("Ivy.NumberRangeInput", "Ivy.NumberRangeInputExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Inputs/NumberRangeInput.cs")
            | new Markdown("## FAQ").OnLinkClick(onLinkClick)
            | new Expandable("Price Filter Example",
                Vertical().Gap(4)
                | new Markdown("A realistic example demonstrating a price range filter with currency formatting:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new PriceFilterDemo())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class PriceFilterDemo : ViewBase
                        {
                            public override object? Build()
                            {
                                var priceRange = UseState<(decimal, decimal)>(() => (50.0m, 200.0m));
                                var products = new[]
                                {
                                    ("Laptop", 899.99m),
                                    ("Mouse", 29.99m),
                                    ("Keyboard", 79.99m),
                                    ("Monitor", 299.99m),
                                    ("Headphones", 149.99m),
                                    ("Webcam", 89.99m)
                                };
                        
                                var lower = priceRange.Value.Item1;
                                var upper = priceRange.Value.Item2;
                        
                                var filteredProducts = products
                                    .Where(p => p.Item2 >= lower && p.Item2 <= upper)
                                    .ToArray();
                        
                                return Layout.Vertical().Gap(4)
                                    | Text.P("Filter by Price").Large().Bold()
                                    | priceRange.ToMoneyRangeInput()
                                                .Currency("USD")
                                                .Min(0)
                                                .Max(1000)
                                                .Step(10)
                                                .WithField()
                                                .Label("Price Range")
                                    | Text.P($"Showing {filteredProducts.Length} of {products.Length} products").Small().Muted()
                                    | (filteredProducts.Length > 0
                                        ? Layout.Vertical().Gap(2)
                                            | filteredProducts.Select(p =>
                                                Layout.Horizontal().Gap(2)
                                                    | Text.P(p.Item1)
                                                    | Text.P($"${p.Item2:F2}").Bold()
                                            ).ToArray()
                                        : Text.P("No products in this price range").Muted());
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("How do I access the lower and upper values?",
                Vertical().Gap(4)
                | new Markdown("The `NumberRangeInput` stores its value as a tuple. Access the bounds using `Item1` (lower) and `Item2` (upper):").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    var range = UseState<(int, int)>(() => (25, 75));
                    
                    var lowerBound = range.Value.Item1;  // 25
                    var upperBound = range.Value.Item2;  // 75
                    """",Languages.Csharp)
                | new Markdown("You can also use tuple deconstruction:").OnLinkClick(onLinkClick)
                | new CodeBlock("var (lower, upper) = range.Value;",Languages.Csharp)
            )
            | new Expandable("How do the handles prevent crossing?",
                new Markdown("The `NumberRangeInput` automatically enforces that the lower handle cannot exceed the upper handle and vice versa. The slider component handles this constraint internally—you don't need to add any validation logic. When a user drags a handle, it will stop at the position of the other handle.").OnLinkClick(onLinkClick)
            )
            | new Expandable("How do I format a NumberRangeInput as currency or percent?",
                Vertical().Gap(4)
                | new Markdown("Use the `.FormatStyle()` fluent method with the `NumberFormatStyle` enum:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    var priceRange = UseState<(decimal, decimal)>(() => (50.0m, 200.0m));
                    var percentRange = UseState<(double, double)>(() => (0.25, 0.75));
                    
                    // Currency formatting
                    priceRange.ToNumberRangeInput()
                              .FormatStyle(NumberFormatStyle.Currency)
                              .Currency("USD")
                    
                    // Shorthand for currency
                    priceRange.ToMoneyRangeInput().Currency("USD")
                    
                    // Percent formatting
                    percentRange.ToNumberRangeInput()
                                .FormatStyle(NumberFormatStyle.Percent)
                    """",Languages.Csharp)
                | new Markdown("Available `NumberFormatStyle` values: `Decimal` (default), `Currency`, `Percent`. For currency ranges, the recommended state type is `(decimal, decimal)`.").OnLinkClick(onLinkClick)
            )
            | new Expandable("Can I use nullable numeric ranges?",
                Vertical().Gap(4)
                | new Markdown("Yes! NumberRangeInput supports nullable tuple types like `(int?, int?)`, `(decimal?, decimal?)`, etc. When nullable, a clear button (X) appears to reset both values to `null`:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    var nullableRange = UseState<(decimal?, decimal?)>(() => (50.0m, 200.0m));
                    
                    nullableRange.ToMoneyRangeInput()
                                 .Currency("USD")
                                 .Min(0)
                                 .Max(1000);
                    """",Languages.Csharp)
                | new Markdown("Users can click the X button to clear the selection.").OnLinkClick(onLinkClick)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp)]; 
        return article;
    }
}


public class BasicNumberRangeDemo : ViewBase
{
    public override object? Build()
    {
        var range = UseState<(int, int)>(() => (25, 75));
        var lower = range.Value.Item1;
        var upper = range.Value.Item2;

        return Layout.Vertical()
                | range.ToNumberRangeInput()
                       .Min(0)
                       .Max(100)
                | Text.P($"Selected range: {lower} to {upper}").Large();
    }
}

public class NumberRangeVariantsDemo : ViewBase
{
    public override object? Build()
    {
        var range = UseState<(int, int)>(() => (30, 70));
        var nullableRange = UseState<(int?, int?)>(() => (30, 70));

        return Layout.Vertical().Gap(4)
            | Text.P("Normal State").Small()
            | range.ToNumberRangeInput().Min(0).Max(100)
            | Text.P("Disabled State").Small()
            | range.ToNumberRangeInput().Min(0).Max(100).Disabled()
            | Text.P("Invalid State").Small()
            | range.ToNumberRangeInput().Min(0).Max(100).Invalid("Invalid range")
            | Text.P("Nullable State").Small()
            | nullableRange.ToNumberRangeInput().Min(0).Max(100);
    }
}

public class NumberRangeFormatDemo : ViewBase
{
    public override object? Build()
    {
        var decimalRange = UseState<(int, int)>(() => (25, 75));
        var priceRange = UseState<(decimal, decimal)>(() => (50.0m, 200.0m));
        var percentRange = UseState<(double, double)>(() => (0.25, 0.75));

        return Layout.Vertical().Gap(4)
            | Text.P("Decimal Format").Small()
            | decimalRange.ToNumberRangeInput()
                          .Min(0)
                          .Max(100)
                          .FormatStyle(NumberFormatStyle.Decimal)
            | Text.P("Currency Format (USD)").Small()
            | priceRange.ToNumberRangeInput()
                        .Min(0)
                        .Max(500)
                        .FormatStyle(NumberFormatStyle.Currency)
                        .Currency("USD")
            | Text.P("Percent Format").Small()
            | percentRange.ToNumberRangeInput()
                          .Min(0)
                          .Max(1)
                          .Step(0.01)
                          .FormatStyle(NumberFormatStyle.Percent)
                          .Precision(2);
    }
}

public class MoneyRangeDemo : ViewBase
{
    public override object? Build()
    {
        var budgetRange = UseState<(decimal, decimal)>(() => (50.0m, 200.0m));

        return Layout.Vertical()
            | Text.P("Budget Filter").Small()
            | budgetRange.ToMoneyRangeInput()
                         .Currency("USD")
                         .Min(0)
                         .Max(500)
                         .Step(10);
    }
}

public class NumberRangePrefixSuffixDemo : ViewBase
{
    public override object? Build()
    {
        var priceRange = UseState<(decimal, decimal)>(() => (50.0m, 200.0m));
        var percentRange = UseState<(double, double)>(() => (0.25, 0.75));
        var tempRange = UseState<(int, int)>(() => (18, 24));

        return Layout.Vertical().Gap(4)
            | Text.P("Text Prefix ($)").Small()
            | priceRange.ToNumberRangeInput()
                        .Min(0)
                        .Max(500)
                        .Prefix("$")
                        .Precision(2)
            | Text.P("Text Suffix (%)").Small()
            | percentRange.ToNumberRangeInput()
                          .Min(0)
                          .Max(1)
                          .Step(0.01)
                          .Suffix("%")
                          .Precision(2)
            | Text.P("Icon Prefix + Text Suffix").Small()
            | tempRange.ToNumberRangeInput()
                       .Min(-10)
                       .Max(40)
                       .Prefix(Icons.Thermometer)
                       .Suffix("°C");
    }
}

public class NumberRangeStepDemo : ViewBase
{
    public override object? Build()
    {
        var range = UseState<(int, int)>(() => (20, 80));

        return Layout.Vertical().Gap(4)
            | Text.P("Step: 10 (snaps to multiples of 10)").Small()
            | range.ToNumberRangeInput()
                   .Min(0)
                   .Max(100)
                   .Step(10);
    }
}

public class PriceFilterDemo : ViewBase
{
    public override object? Build()
    {
        var priceRange = UseState<(decimal, decimal)>(() => (50.0m, 200.0m));
        var products = new[]
        {
            ("Laptop", 899.99m),
            ("Mouse", 29.99m),
            ("Keyboard", 79.99m),
            ("Monitor", 299.99m),
            ("Headphones", 149.99m),
            ("Webcam", 89.99m)
        };

        var lower = priceRange.Value.Item1;
        var upper = priceRange.Value.Item2;

        var filteredProducts = products
            .Where(p => p.Item2 >= lower && p.Item2 <= upper)
            .ToArray();

        return Layout.Vertical().Gap(4)
            | Text.P("Filter by Price").Large().Bold()
            | priceRange.ToMoneyRangeInput()
                        .Currency("USD")
                        .Min(0)
                        .Max(1000)
                        .Step(10)
                        .WithField()
                        .Label("Price Range")
            | Text.P($"Showing {filteredProducts.Length} of {products.Length} products").Small().Muted()
            | (filteredProducts.Length > 0
                ? Layout.Vertical().Gap(2)
                    | filteredProducts.Select(p =>
                        Layout.Horizontal().Gap(2)
                            | Text.P(p.Item1)
                            | Text.P($"${p.Item2:F2}").Bold()
                    ).ToArray()
                : Text.P("No products in this price range").Muted());
    }
}
