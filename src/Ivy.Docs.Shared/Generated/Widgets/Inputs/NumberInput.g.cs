using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Inputs;

[App(order:3, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/04_Inputs/03_NumberInput.md", searchHints: ["numeric", "integer", "decimal", "number", "money", "currency"])]
public class NumberInputApp(bool onlyBody = false) : ViewBase
{
    public NumberInputApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("numberinput", "NumberInput", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("variants", "Variants", 2), new ArticleHeading("slider", "Slider", 3), new ArticleHeading("money", "Money", 3), new ArticleHeading("styling", "Styling", 2), new ArticleHeading("precision-and-step", "Precision and Step", 3), new ArticleHeading("formatstyle", "FormatStyle", 3), new ArticleHeading("prefix-and-suffix", "Prefix and Suffix", 2), new ArticleHeading("event-handling", "Event Handling", 2), new ArticleHeading("api", "API", 2), new ArticleHeading("faq", "Faq", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# NumberInput").OnLinkClick(onLinkClick)
            | Lead("Capture [numeric input](app://onboarding/concepts/widgets) with built-in validation, minimum/maximum constraints, step increments, and custom formatting options.")
            | new Markdown(
                """"
                The `NumberInput` [widget](app://onboarding/concepts/widgets) provides an input field specifically for numeric values. It includes validation for numeric entries and options for
                setting minimum/maximum values, step increments, and formatting.
                """").OnLinkClick(onLinkClick)
            | new Callout("Unless you explicitly specify `Min` and `Max` for a `NumberInput`, common default values will be applied based on the numeric type. For example, integer types use their natural limits, while decimal, double, and float types use practical defaults (e.g., ±999,999.99 for sliders). If you need a specific range, always set `Min` and `Max` yourself.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Basic Usage
                
                Here's a simple example of a `NumberInput` that allows users to input a number. It also allows to set a minimum
                and a maximum limit.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class SimpleNumericValueDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var value = UseState(0);
                            return new NumberInput<double>(value)
                                         .Min(-10)
                                         .Max(10);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new SimpleNumericValueDemo())
            )
            | new Markdown(
                """"
                The `NumberInput` allows users to enter numeric values directly.
                
                ## Variants
                
                `NumberInput`s come in several variants to suit different use cases.
                
                ### Slider
                
                This variant helps create a slider that changes the value as the slider is pulled to the right.
                This creates the `NumberInputVariant.Slider` variant.
                
                The following demo shows how a slider can be used to give a visual clue.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class NumberSliderInput : ViewBase
                    {
                        public override object? Build()
                        {
                            var tapes = UseState(1.0);
                            var cart = UseState("");
                            return Layout.Vertical()
                                    | new NumberInput<double>(
                                          tapes.Value,
                                          e => {
                                                 tapes.Set(e);
                                                 cart.Set($"Added {tapes} cm tape to your cart");
                                         })
                                         .Min(30.0)
                                         .Max(500.0)
                                         .Precision(2)
                                         .Step(0.5)
                                         .Variant(NumberInputVariant.Slider)
                                         .WithField()
                                         .Label("Tapes")
                                    | Text.Block(cart);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new NumberSliderInput())
            )
            | new Markdown(
                """"
                ### Money
                
                To enable users to enter money amounts, this variant should be used. The extension function `ToMoneyInput`
                should be used to create this variant. This is the idiomatic way to use Ivy.
                
                The following demo uses `NumberInputVariant.Number` with `NumberFormatStyle.Currency` to create
                `NumberInput`s that can take money inputs. `ToMoneyInput` hides all these complexities.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class MoneyInputDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var moneyInUSD = UseState<decimal>(0.00M);
                            var moneyInGBP = UseState<decimal>(0.00M);
                            var moneyInEUR = UseState<decimal>(0.00M);
                    
                            // Currency Conversion Rates
                            var euroToUSD = 1.80M;
                            var euroToGBP = 0.86M;
                    
                            return Layout.Vertical()
                                    | Text.H3("Simple Currency Converter")
                                    | new NumberInput<decimal>(
                                        moneyInEUR.Value,
                                        e => {
                                            moneyInEUR.Set(e);
                                            moneyInUSD.Set(e * euroToUSD);
                                            moneyInGBP.Set(e * euroToGBP);
                                        }
                                    )
                                    .FormatStyle(NumberFormatStyle.Currency)
                                    .Currency("EUR")
                                    .Placeholder("€0.00")
                                    .WithField()
                                    .Label("Enter EUR amount:")
                    
                                    | moneyInUSD.ToMoneyInput()
                                                .Currency("USD")
                                                .Disabled()
                                                .WithField()
                                                .Label("USD:")
                    
                                    | moneyInGBP.ToMoneyInput()
                                                .Currency("GBP")
                                                .Disabled()
                                                .WithField()
                                                .Label("GBP:");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new MoneyInputDemo())
            )
            | new Markdown(
                """"
                ## Styling
                
                `NumberInput`s can be customized with various styling options, including `Disabled` and `Invalid` states:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class NumberStylingDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var num = UseState(3.14);
                            return Layout.Vertical()
                                    | num.ToNumberInput()
                                         .Disabled()
                                         .WithField().Label("Disabled")
                                    | num.ToNumberInput()
                                         .Invalid(num.Value > 3.1 ? "Value should be less than 3.1" : "")
                                         .WithField().Label("Invalid");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new NumberStylingDemo())
            )
            | new Markdown(
                """"
                ### Precision and Step
                
                To set the precision of a `NumberInput` this style should be used. This can be used via the extension function
                `Precision`. To customize the amount by which the value of a `NumberInput` is changed can be set by `Step`.
                
                The following demo shows these two in action.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class MoneyPrecisionDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var precValue = UseState(0.50M);
                            return Layout.Horizontal()
                                    | new NumberInput<decimal>(precValue)
                                         .Min(0.0)
                                         .Max(100.0)
                                         .Step(0.5)
                                         .Precision(2)
                                         .FormatStyle(NumberFormatStyle.Currency)
                                         .Currency("USD")
                                         .WithField()
                                         .Description("Min 0, Max 100, Step 0.5, Precision 2");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new MoneyPrecisionDemo())
            )
            | new Markdown(
                """"
                ### FormatStyle
                
                There are three different kinds of formats that a `NumberInput` can have. The following shows these in action.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class FormatStyleDemos : ViewBase
                    {
                        public override object? Build()
                        {
                            var num = UseState(3.14);
                            var amount = UseState(30.14);
                            var passingPercentage = UseState(0.35);
                    
                            return Layout.Vertical()
                                    | num.ToNumberInput().FormatStyle(NumberFormatStyle.Decimal)
                                    | amount.ToNumberInput().FormatStyle(NumberFormatStyle.Currency).Currency("GBP")
                                    | passingPercentage.ToNumberInput().FormatStyle(NumberFormatStyle.Percent);
                        }
                    }
                    
                    """",Languages.Csharp)
                | new Box().Content(new FormatStyleDemos())
            )
            | new Markdown(
                """"
                ## Prefix and Suffix
                
                In certain scenarios, it is beneficial to prepend or append static content—such as text fragments or icons—to an input field. This practice is particularly useful for displaying a currency symbol, a unit label, or an icon that denotes the expected input.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class NumberPrefixSuffixDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var price = UseState(99.99m);
                            var weight = UseState(5.5);
                            var temperature = UseState(22);
                    
                            return Layout.Vertical()
                                    | price.ToNumberInput()
                                           .Prefix("$")
                                           .Precision(2)
                                           .WithField()
                                           .Label("Price")
                                    | weight.ToNumberInput()
                                            .Suffix("kg")
                                            .Precision(1)
                                            .WithField()
                                            .Label("Weight")
                                    | temperature.ToNumberInput()
                                                 .Prefix(Icons.Thermometer)
                                                 .Suffix("°C")
                                                 .WithField()
                                                 .Label("Temperature");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new NumberPrefixSuffixDemo())
            )
            | new Markdown(
                """"
                The `Prefix` and `Suffix` methods accept either a `string` or an `Icons` value, thereby providing flexibility for augmenting the contextual information of the input.
                
                ## Event Handling
                
                `NumberInput`s can handle change and blur events:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var onChangedState = UseState(0);
                var onChangeLabel = UseState("");
                
                new NumberInput<int>(onChangedState.Value, e =>
                {
                    onChangedState.Set(e);
                    onChangeLabel.Set("Changed");
                });
                """",Languages.Csharp)
            | new WidgetDocsView("Ivy.NumberInput", "Ivy.NumberInputExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Inputs/NumberInput.cs")
            | new Markdown("## Faq").OnLinkClick(onLinkClick)
            | new Expandable("Simple Grocery App",
                Vertical().Gap(4)
                | new Markdown("The following shows a realistic example of how several `NumberInput`s can be used.").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new GroceryAppDemo())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class GroceryAppDemo : ViewBase
                        {
                            public override object? Build()
                            {
                                var eggs = UseState(0);
                                var breads = UseState(0);
                                var eggCost = 3.45M;
                                var breadCost = 6.13M;
                                return Layout.Vertical()
                                        | (Layout.Horizontal()
                                           | eggs.ToNumberInput()
                                                 .Min(0)
                                                 .Max(12)
                                                 .Width(Size.Units(10))
                                                 .WithField()
                                                 .Label("Egg")
                                                 .Description("Maximum 12"))
                        
                                        | (Layout.Horizontal()
                                           | breads.ToNumberInput()
                                                      .Min(0)
                                                      .Max(5)
                                                      .Width(Size.Units(10))
                                                      .WithField()
                                                      .Label("Bread")
                                                      .Description("Maximum 5"))
                                        | Text.P($"{eggs} eggs and {breads} breads").Large()
                                        | (Layout.Horizontal()
                                           | Text.P("Bill : ").Large()
                                           // Since it is disabled, no need to have an onChange event
                                           | new NumberInput<decimal>(eggs.Value * eggCost + breadCost * breads.Value,_ => { })
                                                             .Disabled()
                                                             .Variant(NumberInputVariant.Number)
                                                             .Precision(2)
                                                             .FormatStyle(NumberFormatStyle.Currency)
                                                             .Currency("EUR"));
                        
                            }
                        }
                        
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("How do I set min/max values on a NumberInput?",
                Vertical().Gap(4)
                | new Markdown("You can pass `min` and `max` directly as optional parameters to `ToNumberInput()`:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    var count = UseState(1);
                    count.ToNumberInput(min: 1, max: 100).Placeholder("Enter count")
                    """",Languages.Csharp)
                | new Markdown("Alternatively, use the `.Min()` and `.Max()` fluent extension methods:").OnLinkClick(onLinkClick)
                | new CodeBlock("count.ToNumberInput().Min(1).Max(100)",Languages.Csharp)
            )
            | new Expandable("How do I format a NumberInput as currency, percent, or decimal?",
                Vertical().Gap(4)
                | new Markdown("Use the `.FormatStyle()` fluent method with the `NumberFormatStyle` enum:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    var price = UseState(99.99m);
                    var taxRate = UseState(0.08);
                    
                    // Currency formatting
                    price.ToNumberInput().FormatStyle(NumberFormatStyle.Currency).Currency("USD")
                    
                    // Percent formatting
                    taxRate.ToNumberInput().FormatStyle(NumberFormatStyle.Percent)
                    
                    // Decimal formatting (default)
                    price.ToNumberInput().FormatStyle(NumberFormatStyle.Decimal)
                    """",Languages.Csharp)
                | new Markdown(
                    """"
                    Available `NumberFormatStyle` values: `Decimal` (default), `Currency`, `Percent`. For currency inputs, the recommended state type is `decimal`. Use `.Currency("USD")` to specify the currency code.
                    """").OnLinkClick(onLinkClick)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp)]; 
        return article;
    }
}


public class SimpleNumericValueDemo : ViewBase
{
    public override object? Build()
    {
        var value = UseState(0);
        return new NumberInput<double>(value)
                     .Min(-10)
                     .Max(10);
    }
}

public class NumberSliderInput : ViewBase 
{
    public override object? Build()
    {        
        var tapes = UseState(1.0);
        var cart = UseState("");
        return Layout.Vertical()
                | new NumberInput<double>(
                      tapes.Value,
                      e => {
                             tapes.Set(e);
                             cart.Set($"Added {tapes} cm tape to your cart"); 
                     })
                     .Min(30.0)
                     .Max(500.0)
                     .Precision(2)
                     .Step(0.5)
                     .Variant(NumberInputVariant.Slider)
                     .WithField()
                     .Label("Tapes")
                | Text.Block(cart);
    }
}

public class MoneyInputDemo : ViewBase 
{
    public override object? Build()
    {
        var moneyInUSD = UseState<decimal>(0.00M);
        var moneyInGBP = UseState<decimal>(0.00M);
        var moneyInEUR = UseState<decimal>(0.00M);

        // Currency Conversion Rates
        var euroToUSD = 1.80M;
        var euroToGBP = 0.86M;
        
        return Layout.Vertical()
                | Text.H3("Simple Currency Converter")
                | new NumberInput<decimal>(
                    moneyInEUR.Value,
                    e => {
                        moneyInEUR.Set(e);
                        moneyInUSD.Set(e * euroToUSD);
                        moneyInGBP.Set(e * euroToGBP);
                    }
                )
                .FormatStyle(NumberFormatStyle.Currency)
                .Currency("EUR")
                .Placeholder("€0.00")
                .WithField()
                .Label("Enter EUR amount:")
                    
                | moneyInUSD.ToMoneyInput()
                            .Currency("USD")
                            .Disabled()
                            .WithField()
                            .Label("USD:")
                    
                | moneyInGBP.ToMoneyInput()
                            .Currency("GBP")
                            .Disabled()
                            .WithField()
                            .Label("GBP:");
    }
}

public class NumberStylingDemo : ViewBase
{
    public override object? Build()
    {
        var num = UseState(3.14);
        return Layout.Vertical()
                | num.ToNumberInput()
                     .Disabled()
                     .WithField().Label("Disabled")
                | num.ToNumberInput()
                     .Invalid(num.Value > 3.1 ? "Value should be less than 3.1" : "")
                     .WithField().Label("Invalid");
    }
}

public class MoneyPrecisionDemo : ViewBase
{
    public override object? Build()
    {
        var precValue = UseState(0.50M);
        return Layout.Horizontal() 
                | new NumberInput<decimal>(precValue)
                     .Min(0.0)
                     .Max(100.0)
                     .Step(0.5)
                     .Precision(2)
                     .FormatStyle(NumberFormatStyle.Currency)
                     .Currency("USD")
                     .WithField()
                     .Description("Min 0, Max 100, Step 0.5, Precision 2");
    }
}

public class FormatStyleDemos : ViewBase
{
    public override object? Build()
    {
        var num = UseState(3.14);
        var amount = UseState(30.14);
        var passingPercentage = UseState(0.35);
        
        return Layout.Vertical()
                | num.ToNumberInput().FormatStyle(NumberFormatStyle.Decimal)
                | amount.ToNumberInput().FormatStyle(NumberFormatStyle.Currency).Currency("GBP")
                | passingPercentage.ToNumberInput().FormatStyle(NumberFormatStyle.Percent);
    }
}


public class NumberPrefixSuffixDemo : ViewBase
{
    public override object? Build()
    {
        var price = UseState(99.99m);
        var weight = UseState(5.5);
        var temperature = UseState(22);

        return Layout.Vertical()
                | price.ToNumberInput()
                       .Prefix("$")
                       .Precision(2)
                       .WithField()
                       .Label("Price")
                | weight.ToNumberInput()
                        .Suffix("kg")
                        .Precision(1)
                        .WithField()
                        .Label("Weight")
                | temperature.ToNumberInput()
                             .Prefix(Icons.Thermometer)
                             .Suffix("°C")
                             .WithField()
                             .Label("Temperature");
    }
}

public class GroceryAppDemo : ViewBase
{
    public override object? Build()
    {
        var eggs = UseState(0);
        var breads = UseState(0);
        var eggCost = 3.45M;
        var breadCost = 6.13M;
        return Layout.Vertical()
                | (Layout.Horizontal() 
                   | eggs.ToNumberInput()
                         .Min(0)
                         .Max(12)
                         .Width(Size.Units(10))
                         .WithField()
                         .Label("Egg")
                         .Description("Maximum 12"))
        
                | (Layout.Horizontal()
                   | breads.ToNumberInput()
                              .Min(0)
                              .Max(5)
                              .Width(Size.Units(10))
                              .WithField()
                              .Label("Bread")
                              .Description("Maximum 5"))
                | Text.P($"{eggs} eggs and {breads} breads").Large()
                | (Layout.Horizontal()
                   | Text.P("Bill : ").Large()
                   // Since it is disabled, no need to have an onChange event
                   | new NumberInput<decimal>(eggs.Value * eggCost + breadCost * breads.Value,_ => { })
                                     .Disabled()
                                     .Variant(NumberInputVariant.Number)
                                     .Precision(2)
                                     .FormatStyle(NumberFormatStyle.Currency)
                                     .Currency("EUR"));
                   
    }
}

