using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Inputs;

[App(order:5, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/04_Inputs/05_SelectInput.md", searchHints: ["dropdown", "picker", "options", "choice", "select", "menu"])]
public class SelectInputApp(bool onlyBody = false) : ViewBase
{
    public SelectInputApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("selectinput", "SelectInput", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("multiple-selection", "Multiple Selection", 2), new ArticleHeading("event-handling", "Event Handling", 2), new ArticleHeading("styling-and-states", "Styling and States", 2), new ArticleHeading("advanced-features", "Advanced Features", 2), new ArticleHeading("search-support", "Search Support", 3), new ArticleHeading("selection-limits", "Selection Limits", 3), new ArticleHeading("disabled-options", "Disabled Options", 3), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# SelectInput").OnLinkClick(onLinkClick)
            | Lead("Create dropdown [menus](app://onboarding/concepts/navigation) with single or multiple selection capabilities, option grouping, and custom rendering for user choices.")
            | new Markdown(
                """"
                The `SelectInput` [widget](app://onboarding/concepts/widgets) provides a dropdown menu for selecting items from a predefined list of options. It supports single
                and multiple selections, option grouping, and custom rendering of option items.
                
                ## Basic Usage
                
                Here's a simple example of a `SelectInput` with a few options. Use [Size](app://api-reference/ivy/size) for `.Width(Size.Full())` to make the select fill available space:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class SelectVariantDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var favLang = UseState("C#");
                            return favLang.ToSelectInput(["C#", "Java", "Go", "JavaScript", "F#", "Kotlin", "VB.NET", "Rust"])
                                             .Variant(SelectInputVariant.Select)
                                             .WithField()
                                             .Label("Select your favourite programming language")
                                             .Width(Size.Full());
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new SelectVariantDemo())
            )
            | new Markdown(
                """"
                ## Multiple Selection
                
                Multiple selection is automatically enabled when you use a collection type (array, List, etc.) as your state. The framework automatically detects this and enables multi-select functionality.
                
                `SelectInput` supports three variants: **Select** (dropdown), **List** (checkboxes), and **Toggle** (button toggles). Multi-select works with all variants and data types. Here's an example demonstrating different combinations:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new MultiSelectDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class MultiSelectDemo : ViewBase
                    {
                        private enum ProgrammingLanguages
                        {
                            CSharp,
                            Java,
                            Python,
                            JavaScript,
                            Go,
                            Rust
                        }
                    
                        public override object? Build()
                        {
                            var languagesSelect = UseState<ProgrammingLanguages[]>([]);
                            var stringArray = UseState<string[]>([]);
                            var intArray = UseState<int[]>([]);
                    
                            var languageOptions = typeof(ProgrammingLanguages).ToOptions();
                            var stringOptions = new[] { "Option A", "Option B", "Option C", "Option D" };
                            var intOptions = new[] { 1, 2, 3, 4, 5 }.ToOptions();
                    
                            return Layout.Vertical()
                                | Text.Monospaced("Select Variant (Enum)")
                                | languagesSelect.ToSelectInput(languageOptions)
                                    .Variant(SelectInputVariant.Select)
                                    .Placeholder("Choose languages...")
                    
                                | Text.Monospaced("List Variant (String Array)")
                                | stringArray.ToSelectInput(stringOptions.ToOptions())
                                    .Variant(SelectInputVariant.List)
                    
                                | Text.Monospaced("Toggle Variant (Integer Array)")
                                | intArray.ToSelectInput(intOptions)
                                    .Variant(SelectInputVariant.Toggle);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Event Handling
                
                Handle change events and create dynamic option lists that respond to user selections:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new EventHandlingDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class EventHandlingDemo : ViewBase
                    {
                        private static readonly Dictionary<string, string[]> CategoryOptions = new()
                        {
                            ["Programming"] = new[]{"C#", "Java", "Python", "JavaScript"},
                            ["Design"] = new[]{"Photoshop", "Figma", "Sketch"},
                            ["Database"] = new[]{"SQL Server", "PostgreSQL", "MongoDB"}
                        };
                    
                        public override object? Build()
                        {
                            var selectedCategory = UseState("Programming");
                            var selectedSkill = UseState("");
                            var showInfo = UseState(false);
                    
                            var categoryOptions = CategoryOptions.Keys.ToOptions();
                            var skillOptions = CategoryOptions[selectedCategory.Value].ToOptions();
                    
                            return Layout.Vertical()
                                | Layout.Grid().Columns(2)
                                    | selectedCategory.ToSelectInput(categoryOptions)
                                        .Placeholder("Choose a category...")
                                        .WithField()
                                        .Label("Category:")
                    
                                    | new SelectInput<string>(
                                        value: selectedSkill.Value,
                                        onChange: e =>
                                        {
                                            selectedSkill.Set(e.Value);
                                            showInfo.Set(!string.IsNullOrEmpty(e.Value));
                                        },
                                        skillOptions)
                                        .Placeholder("Select a skill...")
                                        .WithField()
                                        .Label("Skill:")
                    
                                | (showInfo.Value
                                    ? Text.Block($"Selected: {selectedCategory.Value} → {selectedSkill.Value}")
                                    : null);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Styling and States
                
                Customize the `SelectInput` with various styling options:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SelectStylingDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SelectStylingDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var normalSelect = UseState("");
                            var invalidSelect = UseState("");
                            var ghostSelect = UseState("");
                            var loadingSelect = UseState("");
                            var disabledSelect = UseState("");
                    
                            var options = new[] { "Option 1", "Option 2", "Option 3" };
                    
                            var isLoading = UseState(true);
                    
                            return Layout.Vertical()
                                | normalSelect.ToSelectInput(options)
                                    .Placeholder("Choose an option...")
                                    .WithField()
                                    .Label("Normal SelectInput:")
                    
                                | invalidSelect.ToSelectInput(options)
                                    .Placeholder("This has an error...")
                                    .Invalid("This field is required")
                                    .WithField()
                                    .Label("Invalid SelectInput:")
                    
                                | ghostSelect.ToSelectInput(options)
                                    .Placeholder("This is ghost...")
                                    .Ghost()
                                    .WithField()
                                    .Label("Ghost SelectInput:")
                    
                                | loadingSelect.ToSelectInput(options)
                                    .Placeholder("This is loading...")
                                    .Loading(isLoading.Value)
                                    .WithField()
                                    .Label("Loading SelectInput")
                    
                                | disabledSelect.ToSelectInput(options)
                                    .Placeholder("This is disabled...")
                                    .Disabled(true)
                                    .WithField()
                                    .Label("Disabled SelectInput:");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Advanced Features
                
                SelectInput supports search, selection limits, and loading state. These work across all variants (Select, List, Toggle).
                
                ### Search Support
                
                Enable search with `.Searchable(true)`, set the matching mode with `.SearchMode()`, and customize the empty state with `.EmptyMessage()`:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class SelectSearchDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var selected = UseState("");
                            var options = new[] { "C#", "Java", "Python", "JavaScript", "Go", "Rust", "F#", "Kotlin", "TypeScript" };
                            return selected.ToSelectInput(options.ToOptions())
                                .Searchable(true)
                                .SearchMode(SearchMode.Fuzzy)
                                .EmptyMessage("No items found")
                                .Placeholder("Search languages...")
                                .WithField()
                                .Label("Language")
                                .Width(Size.Full());;
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new SelectSearchDemo())
            )
            | new Markdown(
                """"
                `SearchMode` can be `SearchMode.Fuzzy`, `SearchMode.CaseInsensitive`, or `SearchMode.CaseSensitive`.
                
                ### Selection Limits
                
                For multi-select variants, use `.MinSelections()` and `.MaxSelections()` to enforce how many options can be selected:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class SelectionLimitsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var colors = UseState<string[]>([]);
                            var options = new[] { "Red", "Green", "Blue", "Yellow", "Purple" }.ToOptions();
                            return colors.ToSelectInput(options)
                                .Variant(SelectInputVariant.Toggle)
                                .MinSelections(1)
                                .MaxSelections(3)
                                .Placeholder("Pick 1 to 3 colors")
                                .WithField()
                                .Label("Colors")
                                .Width(Size.Full());;
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new SelectionLimitsDemo())
            )
            | new Markdown(
                """"
                ### Disabled Options
                
                Individual options can be disabled using the fluent `.Disabled()` method on `Option<T>`. Disabled options appear greyed out and cannot be selected, but remain visible in the list:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new DisabledOptionsDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class DisabledOptionsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var fruit = UseState("apple");
                            var colors = UseState<string[]>([]);
                    
                            var fruitOptions = new IAnyOption[]
                            {
                                new Option<string>("Apple", "apple"),
                                new Option<string>("Orange", "orange"),
                                new Option<string>("Grape (Out of Stock)", "grape").Disabled(),
                                new Option<string>("Banana", "banana"),
                                new Option<string>("Mango (Coming Soon)", "mango").Disabled(),
                            };
                    
                            var colorOptions = new IAnyOption[]
                            {
                                new Option<string>("Red", "red"),
                                new Option<string>("Green", "green"),
                                new Option<string>("Blue (Premium)", "blue").Disabled(),
                                new Option<string>("Yellow", "yellow"),
                            };
                    
                            return Layout.Vertical()
                                | Text.Monospaced("Select Variant")
                                | fruit.ToSelectInput(fruitOptions)
                                    .Placeholder("Select a fruit...")
                    
                                | Text.Monospaced("Toggle Variant")
                                | colors.ToSelectInput(colorOptions)
                                    .Variant(SelectInputVariant.Toggle)
                    
                                | Text.Monospaced("List Variant")
                                | colors.ToSelectInput(colorOptions)
                                    .Variant(SelectInputVariant.List);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("Use Select for single choice dropdowns, List for multiple selection with checkboxes, and Toggle for visual button-based selection. The List variant is particularly useful for [forms](app://onboarding/concepts/forms) where users need to select multiple options.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new WidgetDocsView("Ivy.SelectInput", "Ivy.SelectInputExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Inputs/SelectInput.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Ordering System",
                Vertical().Gap(4)
                | new Markdown("A comprehensive example showing different SelectInput [variants](app://onboarding/concepts/theming) in a real-world scenario:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new CoffeeShopDemo())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class CoffeeShopDemo: ViewBase
                        {
                            private static readonly Dictionary<string, List<string>> CoffeeAccompaniments = new()
                            {
                                ["Cappuccino"] = new List<string>
                                {
                                    "Cinnamon powder", "Cocoa powder", "Sugar cubes", "Biscotti",
                                    "Cantuccini", "Amaretti", "Whipped cream"
                                },
                                ["Espresso"] = new List<string>
                                {
                                    "Lemon peel", "Sugar cubes", "Water", "Chocolate square",
                                    "Praline", "Biscotti"
                                },
                                ["Latte"] = new List<string>
                                {
                                    "Vanilla syrup", "Caramel syrup", "Hazelnut syrup", "Cocoa powder",
                                    "Cinnamon", "Croissant", "Muffin", "Steamed milk art"
                                },
                                ["Mocha"] = new List<string>
                                {
                                    "Whipped cream", "Chocolate shavings", "Cocoa powder", "Marshmallows",
                                    "Cinnamon stick", "Caramel drizzle", "Vanilla syrup"
                                }
                            };
                        
                            string[] coffeeSizes = new string[]{"Short", "Tall", "Grande", "Venti"};
                        
                            public override object? Build()
                            {
                                var coffee = UseState("Cappuccino");
                                var coffeeSize = UseState("Tall");
                                var selectedCondiments = UseState(new string[]{});
                                var previousCoffee = UseState("Cappuccino");
                        
                                if (previousCoffee.Value != coffee.Value)
                                {
                                    selectedCondiments.Set(new string[]{});
                                    previousCoffee.Set(coffee.Value);
                                }
                        
                                var coffeeSizeMenu = coffeeSize.ToSelectInput(coffeeSizes)
                                                               .Variant(SelectInputVariant.List);
                                var availableCondiments = CoffeeAccompaniments[coffee.Value];
                        
                                var condimentMenu = selectedCondiments.ToSelectInput(availableCondiments.ToOptions())
                                    .Variant(SelectInputVariant.Toggle);
                        
                                var orderSummary = BuildOrderSummary(coffee.Value, coffeeSize.Value, selectedCondiments.Value);
                        
                                return Layout.Vertical()
                                        | Layout.Grid().Columns(2)
                                            | coffee.ToSelectInput(CoffeeAccompaniments.Keys.ToOptions())
                                                    .WithField()
                                                    .Label("Coffee Type:")
                        
                                            | coffeeSizeMenu
                                                .WithField()
                                                .Label("Size:")
                        
                                            | condimentMenu
                                                .WithField()
                                                .Label("Condiments:")
                        
                                        | new Icon(Icons.Coffee)
                                        | Text.Block(orderSummary);
                            }
                        
                            private string BuildOrderSummary(string coffee, string size, string[] condiments)
                            {
                                var summary = $"{size} {coffee}";
                        
                                if (condiments.Length > 0)
                                {
                                    if(condiments.Length == 1)
                                    {
                                        summary += $" with {condiments[0]}";
                                    }
                                    else
                                    {
                                         summary += " with " + condiments
                                                                         .Take(condiments.Length - 1)
                                                                         .Aggregate((a,b) =>  a + ", " + b)
                                                                         + " and " + condiments[condiments.Length - 1];
                                    }
                                }
                        
                                return summary;
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.NavigationApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(ApiReference.Ivy.SizeApp), typeof(Onboarding.Concepts.FormsApp)]; 
        return article;
    }
}


public class SelectVariantDemo : ViewBase
{
    public override object? Build()
    {
        var favLang = UseState("C#");
        return favLang.ToSelectInput(["C#", "Java", "Go", "JavaScript", "F#", "Kotlin", "VB.NET", "Rust"])
                         .Variant(SelectInputVariant.Select)
                         .WithField()
                         .Label("Select your favourite programming language")
                         .Width(Size.Full());
    }    
}

public class MultiSelectDemo : ViewBase
{
    private enum ProgrammingLanguages
    {
        CSharp,
        Java,
        Python,
        JavaScript,
        Go,
        Rust
    }
    
    public override object? Build()
    {
        var languagesSelect = UseState<ProgrammingLanguages[]>([]);
        var stringArray = UseState<string[]>([]);
        var intArray = UseState<int[]>([]);
        
        var languageOptions = typeof(ProgrammingLanguages).ToOptions();
        var stringOptions = new[] { "Option A", "Option B", "Option C", "Option D" };
        var intOptions = new[] { 1, 2, 3, 4, 5 }.ToOptions();
        
        return Layout.Vertical()
            | Text.Monospaced("Select Variant (Enum)")
            | languagesSelect.ToSelectInput(languageOptions)
                .Variant(SelectInputVariant.Select)
                .Placeholder("Choose languages...")
            
            | Text.Monospaced("List Variant (String Array)")
            | stringArray.ToSelectInput(stringOptions.ToOptions())
                .Variant(SelectInputVariant.List)
            
            | Text.Monospaced("Toggle Variant (Integer Array)")
            | intArray.ToSelectInput(intOptions)
                .Variant(SelectInputVariant.Toggle);
    }
}

public class EventHandlingDemo : ViewBase
{
    private static readonly Dictionary<string, string[]> CategoryOptions = new()
    {
        ["Programming"] = new[]{"C#", "Java", "Python", "JavaScript"},
        ["Design"] = new[]{"Photoshop", "Figma", "Sketch"},
        ["Database"] = new[]{"SQL Server", "PostgreSQL", "MongoDB"}
    };
    
    public override object? Build()
    {
        var selectedCategory = UseState("Programming");
        var selectedSkill = UseState("");
        var showInfo = UseState(false);
        
        var categoryOptions = CategoryOptions.Keys.ToOptions();
        var skillOptions = CategoryOptions[selectedCategory.Value].ToOptions();
        
        return Layout.Vertical()
            | Layout.Grid().Columns(2)
                | selectedCategory.ToSelectInput(categoryOptions)
                    .Placeholder("Choose a category...")
                    .WithField()
                    .Label("Category:")
                
                | new SelectInput<string>(
                    value: selectedSkill.Value,
                    onChange: e =>
                    {
                        selectedSkill.Set(e.Value);
                        showInfo.Set(!string.IsNullOrEmpty(e.Value));
                    },
                    skillOptions)
                    .Placeholder("Select a skill...")
                    .WithField()
                    .Label("Skill:")
            
            | (showInfo.Value 
                ? Text.Block($"Selected: {selectedCategory.Value} → {selectedSkill.Value}") 
                : null);
    }
}

public class SelectStylingDemo : ViewBase
{
    public override object? Build()
    {
        var normalSelect = UseState("");
        var invalidSelect = UseState("");
        var ghostSelect = UseState("");
        var loadingSelect = UseState("");
        var disabledSelect = UseState("");

        var options = new[] { "Option 1", "Option 2", "Option 3" };

        var isLoading = UseState(true);

        return Layout.Vertical()
            | normalSelect.ToSelectInput(options)
                .Placeholder("Choose an option...")
                .WithField()
                .Label("Normal SelectInput:")

            | invalidSelect.ToSelectInput(options)
                .Placeholder("This has an error...")
                .Invalid("This field is required")
                .WithField()
                .Label("Invalid SelectInput:")

            | ghostSelect.ToSelectInput(options)
                .Placeholder("This is ghost...")
                .Ghost()
                .WithField()
                .Label("Ghost SelectInput:")

            | loadingSelect.ToSelectInput(options)
                .Placeholder("This is loading...")
                .Loading(isLoading.Value)
                .WithField()
                .Label("Loading SelectInput")

            | disabledSelect.ToSelectInput(options)
                .Placeholder("This is disabled...")
                .Disabled(true)
                .WithField()
                .Label("Disabled SelectInput:");
    }
}

public class SelectSearchDemo : ViewBase
{
    public override object? Build()
    {
        var selected = UseState("");
        var options = new[] { "C#", "Java", "Python", "JavaScript", "Go", "Rust", "F#", "Kotlin", "TypeScript" };
        return selected.ToSelectInput(options.ToOptions())
            .Searchable(true)
            .SearchMode(SearchMode.Fuzzy)
            .EmptyMessage("No items found")
            .Placeholder("Search languages...")
            .WithField()
            .Label("Language")
            .Width(Size.Full());;
    }
}

public class SelectionLimitsDemo : ViewBase
{
    public override object? Build()
    {
        var colors = UseState<string[]>([]);
        var options = new[] { "Red", "Green", "Blue", "Yellow", "Purple" }.ToOptions();
        return colors.ToSelectInput(options)
            .Variant(SelectInputVariant.Toggle)
            .MinSelections(1)
            .MaxSelections(3)
            .Placeholder("Pick 1 to 3 colors")
            .WithField()
            .Label("Colors")
            .Width(Size.Full());;
    }
}

public class DisabledOptionsDemo : ViewBase
{
    public override object? Build()
    {
        var fruit = UseState("apple");
        var colors = UseState<string[]>([]);

        var fruitOptions = new IAnyOption[]
        {
            new Option<string>("Apple", "apple"),
            new Option<string>("Orange", "orange"),
            new Option<string>("Grape (Out of Stock)", "grape").Disabled(),
            new Option<string>("Banana", "banana"),
            new Option<string>("Mango (Coming Soon)", "mango").Disabled(),
        };

        var colorOptions = new IAnyOption[]
        {
            new Option<string>("Red", "red"),
            new Option<string>("Green", "green"),
            new Option<string>("Blue (Premium)", "blue").Disabled(),
            new Option<string>("Yellow", "yellow"),
        };

        return Layout.Vertical()
            | Text.Monospaced("Select Variant")
            | fruit.ToSelectInput(fruitOptions)
                .Placeholder("Select a fruit...")

            | Text.Monospaced("Toggle Variant")
            | colors.ToSelectInput(colorOptions)
                .Variant(SelectInputVariant.Toggle)

            | Text.Monospaced("List Variant")
            | colors.ToSelectInput(colorOptions)
                .Variant(SelectInputVariant.List);
    }
}

public class CoffeeShopDemo: ViewBase
{
    private static readonly Dictionary<string, List<string>> CoffeeAccompaniments = new()
    {
        ["Cappuccino"] = new List<string> 
        { 
            "Cinnamon powder", "Cocoa powder", "Sugar cubes", "Biscotti", 
            "Cantuccini", "Amaretti", "Whipped cream" 
        },
        ["Espresso"] = new List<string> 
        { 
            "Lemon peel", "Sugar cubes", "Water", "Chocolate square", 
            "Praline", "Biscotti" 
        },
        ["Latte"] = new List<string> 
        { 
            "Vanilla syrup", "Caramel syrup", "Hazelnut syrup", "Cocoa powder", 
            "Cinnamon", "Croissant", "Muffin", "Steamed milk art" 
        },
        ["Mocha"] = new List<string> 
        { 
            "Whipped cream", "Chocolate shavings", "Cocoa powder", "Marshmallows", 
            "Cinnamon stick", "Caramel drizzle", "Vanilla syrup" 
        }
    };
    
    string[] coffeeSizes = new string[]{"Short", "Tall", "Grande", "Venti"};
    
    public override object? Build()
    {
        var coffee = UseState("Cappuccino");
        var coffeeSize = UseState("Tall");
        var selectedCondiments = UseState(new string[]{});
        var previousCoffee = UseState("Cappuccino");
        
        if (previousCoffee.Value != coffee.Value)
        {
            selectedCondiments.Set(new string[]{});
            previousCoffee.Set(coffee.Value);
        }
        
        var coffeeSizeMenu = coffeeSize.ToSelectInput(coffeeSizes)
                                       .Variant(SelectInputVariant.List);
        var availableCondiments = CoffeeAccompaniments[coffee.Value];
        
        var condimentMenu = selectedCondiments.ToSelectInput(availableCondiments.ToOptions())
            .Variant(SelectInputVariant.Toggle);
        
        var orderSummary = BuildOrderSummary(coffee.Value, coffeeSize.Value, selectedCondiments.Value);
        
        return Layout.Vertical()
                | Layout.Grid().Columns(2)
                    | coffee.ToSelectInput(CoffeeAccompaniments.Keys.ToOptions())
                            .WithField()
                            .Label("Coffee Type:")
                    
                    | coffeeSizeMenu
                        .WithField()
                        .Label("Size:")
                    
                    | condimentMenu
                        .WithField()
                        .Label("Condiments:")
                    
                | new Icon(Icons.Coffee) 
                | Text.Block(orderSummary);
    }
    
    private string BuildOrderSummary(string coffee, string size, string[] condiments)
    {
        var summary = $"{size} {coffee}";
        
        if (condiments.Length > 0)
        {
            if(condiments.Length == 1)
            {
                summary += $" with {condiments[0]}";
            }
            else
            {                  
                 summary += " with " + condiments
                                                 .Take(condiments.Length - 1)
                                                 .Aggregate((a,b) =>  a + ", " + b)
                                                 + " and " + condiments[condiments.Length - 1];
            }
        }
        
        return summary;
    }
}
