using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Inputs;

[App(order:4, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/04_Inputs/04_BoolInput.md", searchHints: ["checkbox", "switch", "toggle", "boolean", "true-false", "option"])]
public class BoolInputApp(bool onlyBody = false) : ViewBase
{
    public BoolInputApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("boolinput", "BoolInput", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("creating-boolinput-instances", "Creating BoolInput Instances", 3), new ArticleHeading("nullable-bool-inputs", "Nullable Bool Inputs", 2), new ArticleHeading("variants", "Variants", 2), new ArticleHeading("checkbox", "CheckBox", 3), new ArticleHeading("switch", "Switch", 3), new ArticleHeading("toggle", "Toggle", 3), new ArticleHeading("styling-and-states", "Styling and States", 2), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# BoolInput").OnLinkClick(onLinkClick)
            | Lead("Handle boolean input with elegant checkboxes, switches, and toggles for true/false values in [forms](app://onboarding/concepts/forms) and [interfaces](app://onboarding/concepts/views).")
            | new Markdown(
                """"
                The `BoolInput` [widget](app://onboarding/concepts/widgets) provides a checkbox, switch and toggle for boolean (true/false) input values. It allows users to easily switch between two states in a form or configuration interface.
                
                ## Basic Usage
                
                Here's a simple example of a `BoolInput` used as a checkbox:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BoolInputDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var state = UseState(false);
                            return new BoolInput(state).Label("Accept Terms");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BoolInputDemo())
            )
            | new Markdown(
                """"
                ### Creating BoolInput Instances
                
                You can create `BoolInput` instances in several ways:
                
                **Using the non-generic constructor (defaults to `bool` type):**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var input = new BoolInput(); // Creates BoolInput<bool> with default values
                var labeledInput = new BoolInput("My Label"); // With custom label
                """",Languages.Csharp)
            | new Markdown("**Using the generic constructor for specific types:**").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var nullableInput = new BoolInput<bool?>(); // For nullable boolean
                var intInput = new BoolInput<int>(); // For integer-based boolean (0/1)
                """",Languages.Csharp)
            | new Markdown("**Using extension methods from [state](app://hooks/core/use-state):**").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var state = UseState(false);
                var input = state.ToBoolInput(); // Creates BoolInput from state
                """",Languages.Csharp)
            | new Markdown("The non-generic `BoolInput` constructor is the most convenient when you need a simple boolean input without nullable types or other boolean-like representations.").OnLinkClick(onLinkClick)
            | new Callout("Use extension methods `ToBoolInput()`, `ToSwitchInput()`, and `ToToggleInput()` to quickly create BoolInput from state. See examples in the Variants section below.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Nullable Bool Inputs
                
                Null values are supported for boolean values. The following example shows it in action.
                These values are useful in situations where boolean values can be either not set (`null`)
                or set (`true` or `false`). These can be really handy to capture different answers from
                questions in a survey.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class NullableBoolDemo: ViewBase
                    {
                        public override object? Build()
                        {
                            var going = UseState((bool?)null);
                            var status = UseState("");
                            if(going.Value == null)
                                status.Set("Not answered");
                            else
                                status.Set(going.Value == true ? "Yes!" : "No, not yet!");
                            return Layout.Vertical()
                                    | Text.Html($"<i>{status}</i>")
                                    | going.ToSwitchInput()
                                           .WithField()
                                           .Label("Have you booked return tickets?");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new NullableBoolDemo())
            )
            | new Markdown(
                """"
                ## Variants
                
                There are three variants of `BoolInput`s. The following blocks show how to create and use them.
                
                ### CheckBox
                
                To make the bool input appear like a checkbox, this variant should be used.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class CheckBoxDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var agreed = UseState(false);
                    
                            return Layout.Horizontal()
                                | agreed.ToBoolInput()
                                    .Variant(BoolInputVariant.Checkbox)
                                    .Label("Agree to terms and conditions")
                                | (agreed.Value ? Text.Monospaced("You are all set!") : null);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new CheckBoxDemo())
            )
            | new Markdown(
                """"
                ### Switch
                
                To make the bool input appear like a switch, this variant should be used. This is most suitable for toggling
                some settings values on and off.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BoolInputSwitchDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var read = UseState(false);
                            var readMessage  = UseState("");
                            var write = UseState(false);
                            var delete = UseState(false);
                            var dark =  UseState(false);
                    
                            return Layout.Vertical()
                                   | (Layout.Horizontal()
                                     | read.ToSwitchInput(Icons.Eye).Label("Readonly")
                                     | Text.Block(readMessage))
                                   | write.ToSwitchInput(Icons.Pencil)
                                       .Label("Can write")
                                       .Disabled(read.Value)
                                   | delete.ToSwitchInput(Icons.Trash)
                                       .Label("Can delete")
                                       .Disabled(read.Value)
                                   | dark.ToSwitchInput(Icons.Moon)
                                       .Label("Dark Mode");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BoolInputSwitchDemo())
            )
            | new Markdown(
                """"
                The `ToSwitchInput` extension method also supports an optional `icon` parameter, allowing you to display an icon inside the switch thumb.
                
                ### Toggle
                
                `Toggle` is a button-style boolean input that switches between two states (on/off, enabled/disabled) with a single click.
                It appears as a pressable [button](app://widgets/common/button) that visually indicates its current state through styling and optional icons.
                This is represented by `BoolInputVariant.Toggle`
                
                `ToToggleInput` extension function can be used to create such a `BoolInput.Toggle` variant.
                The following is a small demo showing how such a control may be used.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class SingleToggleDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var isFavorite = UseState(false);
                            return Layout.Vertical()
                                    | (Layout.Horizontal()
                                        |  isFavorite.ToToggleInput(isFavorite.Value ? Icons.Heart : Icons.HeartOff)
                                                     .Label(isFavorite.Value ? "Remove from Favorites" : "Add to Favorites")
                                        | Text.Block(isFavorite.Value ? "❤️ Favorited!" : "🤍 Not favourite!"))
                                    | Text.P(isFavorite.Value
                                        ? "This article has been added to your favorites."
                                        : "Click the heart to save this article.").Small();
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new SingleToggleDemo())
            )
            | new Markdown(
                """"
                ## Styling and States
                
                Customize the `BoolInput` with various styling options:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BoolInputStylingDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BoolInputStylingDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var normalBool = UseState(true);
                            var invalidBool = UseState(false);
                            var loadingBool = UseState(true);
                            var disabledBool = UseState(false);
                    
                            var isLoading = UseState(true);
                    
                            return Layout.Vertical()
                                | normalBool.ToSwitchInput()
                                    .Label("Normal BoolInput")
                    
                                | invalidBool.ToSwitchInput()
                                    .Label("Invalid BoolInput")
                                    .Invalid("This field has an error")
                    
                                | loadingBool.ToSwitchInput()
                                    .Label("Loading BoolInput")
                                    .Loading(isLoading.Value)
                    
                                | disabledBool.ToSwitchInput()
                                    .Label("Disabled BoolInput")
                                    .Disabled(true);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("BoolInput also supports integer-based boolean values (0 = false, 1 = true) for compatibility with legacy systems. Simply use `UseState(0)` or `UseState(1)` with standard extension methods like `ToBoolInput()`, `ToSwitchInput()`, or `ToToggleInput()`.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new WidgetDocsView("Ivy.BoolInput", "Ivy.BoolInputExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Inputs/BoolInput.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Round trip example",
                Vertical().Gap(4)
                | new Markdown(
                    """"
                    The following example shows a demo of how `Switch` variant can be used in a possible situation where it makes sense
                    to do so.
                    """").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new SimpleFlightBooking())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class SimpleFlightBooking : ViewBase
                        {
                            public override object? Build()
                            {
                                var isRoundTrip = UseState(false);
                                var departureDate = UseState(DateTime.Today.AddDays(1));
                                var returnDate = UseState(DateTime.Today.AddDays(7));
                        
                                return Layout.Vertical()
                                        | Text.P("Book Flight")
                                        // Round Trip Switch
                                        | isRoundTrip.ToSwitchInput().Label("Round Trip")
                                        // Departure Date (always visible)
                                        | departureDate.ToDateTimeInput()
                                                      .Variant(DateTimeInputVariant.Date)
                                                      .Placeholder("Select departure date")
                                                      .WithField()
                                                      .Label("Departure Date:")
                                        // Return Date (only visible when round trip is on)
                                        | returnDate.ToDateTimeInput()
                                                   .Variant(DateTimeInputVariant.Date)
                                                   .Placeholder("Select return date")
                                                   .Disabled(!isRoundTrip.Value)
                                                   .WithField()
                                                   .Label("Return Date:")
                                        // Summary
                                        | Text.P($"Round trip: {departureDate.Value:MMM dd} → {returnDate.Value:MMM dd}").Small()
                                        | Text.P($"One way: {departureDate.Value:MMM dd}").Small();
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.FormsApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Hooks.Core.UseStateApp), typeof(Widgets.Common.ButtonApp)]; 
        return article;
    }
}


public class BoolInputDemo : ViewBase
{
    public override object? Build()
    {
        var state = UseState(false);
        return new BoolInput(state).Label("Accept Terms");
    }
}

public class NullableBoolDemo: ViewBase
{
    public override object? Build()
    {
        var going = UseState((bool?)null);
        var status = UseState("");
        if(going.Value == null)
            status.Set("Not answered");
        else 
            status.Set(going.Value == true ? "Yes!" : "No, not yet!");
        return Layout.Vertical()
                | Text.Html($"<i>{status}</i>")
                | going.ToSwitchInput()
                       .WithField()
                       .Label("Have you booked return tickets?");        
    }    
}

public class CheckBoxDemo : ViewBase
{
    public override object? Build()
    {
        var agreed = UseState(false);
        
        return Layout.Horizontal()
            | agreed.ToBoolInput()
                .Variant(BoolInputVariant.Checkbox)
                .Label("Agree to terms and conditions")
            | (agreed.Value ? Text.Monospaced("You are all set!") : null);
    }
}

public class BoolInputSwitchDemo : ViewBase
{
    public override object? Build()
    {
        var read = UseState(false);
        var readMessage  = UseState("");
        var write = UseState(false);
        var delete = UseState(false);
        var dark =  UseState(false);
        
        return Layout.Vertical()
               | (Layout.Horizontal()
                 | read.ToSwitchInput(Icons.Eye).Label("Readonly")
                 | Text.Block(readMessage))
               | write.ToSwitchInput(Icons.Pencil)
                   .Label("Can write")
                   .Disabled(read.Value)
               | delete.ToSwitchInput(Icons.Trash)
                   .Label("Can delete")
                   .Disabled(read.Value)
               | dark.ToSwitchInput(Icons.Moon)
                   .Label("Dark Mode");
    }
}

public class SingleToggleDemo : ViewBase 
{
    public override object? Build()
    {        
        var isFavorite = UseState(false);        
        return Layout.Vertical()            
                | (Layout.Horizontal()
                    |  isFavorite.ToToggleInput(isFavorite.Value ? Icons.Heart : Icons.HeartOff)
                                 .Label(isFavorite.Value ? "Remove from Favorites" : "Add to Favorites")
                    | Text.Block(isFavorite.Value ? "❤️ Favorited!" : "🤍 Not favourite!"))            
                | Text.P(isFavorite.Value 
                    ? "This article has been added to your favorites." 
                    : "Click the heart to save this article.").Small();
    }
}

public class BoolInputStylingDemo : ViewBase
{
    public override object? Build()
    {
        var normalBool = UseState(true);
        var invalidBool = UseState(false);
        var loadingBool = UseState(true);
        var disabledBool = UseState(false);

        var isLoading = UseState(true);

        return Layout.Vertical()
            | normalBool.ToSwitchInput()
                .Label("Normal BoolInput")

            | invalidBool.ToSwitchInput()
                .Label("Invalid BoolInput")
                .Invalid("This field has an error")

            | loadingBool.ToSwitchInput()
                .Label("Loading BoolInput")
                .Loading(isLoading.Value)

            | disabledBool.ToSwitchInput()
                .Label("Disabled BoolInput")
                .Disabled(true);
    }
}

public class SimpleFlightBooking : ViewBase
{
    public override object? Build()
    {        
        var isRoundTrip = UseState(false);
        var departureDate = UseState(DateTime.Today.AddDays(1));
        var returnDate = UseState(DateTime.Today.AddDays(7));

        return Layout.Vertical()
                | Text.P("Book Flight")
                // Round Trip Switch
                | isRoundTrip.ToSwitchInput().Label("Round Trip")
                // Departure Date (always visible)
                | departureDate.ToDateTimeInput()
                              .Variant(DateTimeInputVariant.Date)
                              .Placeholder("Select departure date")
                              .WithField()
                              .Label("Departure Date:")
                // Return Date (only visible when round trip is on)
                | returnDate.ToDateTimeInput()
                           .Variant(DateTimeInputVariant.Date)
                           .Placeholder("Select return date")
                           .Disabled(!isRoundTrip.Value)
                           .WithField()
                           .Label("Return Date:")
                // Summary
                | Text.P($"Round trip: {departureDate.Value:MMM dd} → {returnDate.Value:MMM dd}").Small()
                | Text.P($"One way: {departureDate.Value:MMM dd}").Small();
    }
}
