using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Inputs;

[App(order:9, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/04_Inputs/09_ColorInput.md", searchHints: ["picker", "palette", "hex", "rgb", "swatch", "color"])]
public class ColorInputApp(bool onlyBody = false) : ViewBase
{
    public ColorInputApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("colorinput", "ColorInput", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("using-the-non-generic-constructor", "Using the Non-Generic Constructor", 3), new ArticleHeading("variants", "Variants", 2), new ArticleHeading("swatch-variant", "Swatch Variant", 3), new ArticleHeading("event-handling", "Event Handling", 2), new ArticleHeading("styling", "Styling", 2), new ArticleHeading("alpha-channel", "Alpha Channel", 2), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# ColorInput").OnLinkClick(onLinkClick)
            | Lead("Select colors visually with an intuitive color picker [interface](app://onboarding/concepts/views) that returns values suitable for [styling](app://onboarding/concepts/views) and [theming](app://onboarding/concepts/theming) applications.")
            | new Markdown(
                """"
                The `ColorInput` [widget](app://onboarding/concepts/widgets) provides a color picker interface for selecting color values. It allows users to visually choose colors and returns the selected color in a format suitable for use in styles and [themes](app://onboarding/concepts/theming).
                
                ## Basic Usage
                
                Here's a simple example of a `ColorInput` that updates a [state](app://hooks/core/use-state) with the selected color:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class ColorDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var colorState = UseState("#ff0000");
                            return colorState.ToColorInput();
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new ColorDemo())
            )
            | new Markdown(
                """"
                ### Using the Non-Generic Constructor
                
                For convenience, you can create a `ColorInput` without specifying the generic type, which defaults to `string`:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Using the non-generic constructor (defaults to string)
                var colorInput = new ColorInput();
                
                // With placeholder
                var colorInputWithPlaceholder = new ColorInput("Choose a color");
                
                // With all options
                var colorInputFull = new ColorInput(
                    placeholder: "Select your favorite color",
                    disabled: false,
                    variant: ColorInputVariant.TextAndPicker
                );
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Variants
                
                `ColorInput` has four variants:
                
                | Variant                           | Description                                  |
                | --------------------------------- | -------------------------------------------- |
                | `ColorInputVariant.Text`          | Text input for entering hex codes manually   |
                | `ColorInputVariant.Picker`        | Color picker only                            |
                | `ColorInputVariant.TextAndPicker` | Text input with color picker (default)       |
                | `ColorInputVariant.Swatch`        | Grid of predefined colors from `Colors` enum |
                
                The following code shows all variants in action:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class ColorVariantsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var colorState = UseState("red");
                            return Layout.Grid().Columns(2).ColumnWidths(Size.Units(30), null)
                                | Text.P("Just Text").Small() | colorState.ToColorInput().Variant(ColorInputVariant.Text)
                                | Text.P("Just Picker").Small() | colorState.ToColorInput().Variant(ColorInputVariant.Picker)
                                | Text.P("Text and Picker").Small() | colorState.ToColorInput().Variant(ColorInputVariant.TextAndPicker)
                                | Text.P("Swatch").Small() | colorState.ToColorInput().Variant(ColorInputVariant.Swatch);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new ColorVariantsDemo())
            )
            | new Markdown(
                """"
                ### Swatch Variant
                
                The `Swatch` variant displays a grid of predefined colors from the `Colors` enum. This is useful when you want users to select from a specific set of theme-aware colors rather than arbitrary hex values.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class ColorSwatchDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var colorState = UseState(Colors.Blue);
                            return Layout.Vertical()
                                | colorState.ToColorInput().Variant(ColorInputVariant.Swatch)
                                | Text.P($"Selected: {colorState.Value}");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new ColorSwatchDemo())
            )
            | new Markdown(
                """"
                ## Event Handling
                
                ColorInput can handle change events using the `onChange` parameter.
                The following demo shows how the `Picker` variant can be used with a code
                block so that
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class ColorChangedDemo : ViewBase
                    {
                    
                        public override object? Build()
                        {
                            var colorState = UseState("#ff0000");
                            var colorName = UseState(colorState.Value);
                            var onChangeHandler = (Event<IInput<string>, string> e) =>
                            {
                                colorName.Set(e.Value);
                                colorState.Set(e.Value);
                            };
                            return Layout.Vertical()
                                    | H3("Hex Color Picker")
                                    | (Layout.Horizontal()
                                    | new ColorInput<string>
                                           (colorState.Value, onChangeHandler)
                                          .Variant(ColorInputVariant.Picker)
                                    | new CodeBlock(colorName.Value)
                                        .ShowCopyButton()
                                        .ShowBorder());
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new ColorChangedDemo())
            )
            | new Markdown(
                """"
                ## Styling
                
                `ColorInput` can be customized with various styling options, such as setting a placeholder or disabling the input.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class ColorStylingDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var colorState = UseState("#ff0000");
                            return Layout.Grid().Columns(2).ColumnWidths(Size.Units(30), null)
                                | Text.P("Disabled").Small() | colorState.ToColorInput().Disabled()
                                | Text.P("Invalid").Small() | colorState.ToColorInput().Invalid("Invalid color value");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new ColorStylingDemo())
            )
            | new Markdown(
                """"
                ## Alpha Channel
                
                When building apps that require semi-transparent colors (e.g., overlays, backgrounds with opacity), you can enable the `AllowAlpha` option. This adds an opacity slider next to the color picker, and the value is stored in `#RRGGBBAA` format.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class ColorAlphaDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var colorState = UseState("#ff000080");
                            return Layout.Vertical()
                                | colorState.ToColorInput().AllowAlpha()
                                | Text.P($"Selected: {colorState.Value}");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new ColorAlphaDemo())
            )
            | new WidgetDocsView("Ivy.ColorInput", "Ivy.ColorInputExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Inputs/ColorInput.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("ColorPicker control can be used in a developer tool setting that generates CSS blocks.",
                Vertical().Gap(4)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new CSSColorDemo())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class CSSColorDemo : ViewBase
                        {
                            public override object? Build()
                            {
                                var color = UseState("#333333");
                                var bgColor = UseState("#F5F5F5");
                                var border = UseState("#CCCCCC");
                                var template = """
                                             .my-element {
                                                    color: [COLOR];
                                                    background-color: [BG_COLOR];
                                                    border: 1px solid [BORDER];
                                              }
                                """;
                                var genCode = UseState("");
                                genCode.Set(template.Replace("[COLOR]",color.Value)
                                                    .Replace("[BG_COLOR]",bgColor.Value)
                                                    .Replace("[BORDER]",border.Value));
                                return Layout.Vertical()
                                        | H3("CSS Block Generator")
                                        | (Layout.Horizontal()
                                           | Text.Monospaced("color")
                                                 .Width(Size.Units(35))
                                           | color.ToColorInput()
                                                  .Variant(ColorInputVariant.Picker))
                                        | (Layout.Horizontal()
                                           | Text.Monospaced("background-color")
                                                 .Width(Size.Units(35))
                                           | bgColor.ToColorInput()
                                                  .Variant(ColorInputVariant.Picker))
                                        | (Layout.Horizontal()
                                           | Text.Monospaced("border")
                                                 .Width(Size.Units(35))
                                           | border.ToColorInput()
                                                  .Variant(ColorInputVariant.Picker))
                                           | new CodeBlock(genCode.Value)
                                                 .Language(Languages.Css)
                                                 .ShowCopyButton();
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.ThemingApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Hooks.Core.UseStateApp)]; 
        return article;
    }
}


public class ColorDemo : ViewBase
{
    public override object? Build()
    {
        var colorState = UseState("#ff0000");
        return colorState.ToColorInput();
    }
}

public class ColorVariantsDemo : ViewBase
{
    public override object? Build()
    {
        var colorState = UseState("red");
        return Layout.Grid().Columns(2).ColumnWidths(Size.Units(30), null)
            | Text.P("Just Text").Small() | colorState.ToColorInput().Variant(ColorInputVariant.Text)
            | Text.P("Just Picker").Small() | colorState.ToColorInput().Variant(ColorInputVariant.Picker)
            | Text.P("Text and Picker").Small() | colorState.ToColorInput().Variant(ColorInputVariant.TextAndPicker)
            | Text.P("Swatch").Small() | colorState.ToColorInput().Variant(ColorInputVariant.Swatch);
    }
}

public class ColorSwatchDemo : ViewBase
{
    public override object? Build()
    {
        var colorState = UseState(Colors.Blue);
        return Layout.Vertical()
            | colorState.ToColorInput().Variant(ColorInputVariant.Swatch)
            | Text.P($"Selected: {colorState.Value}");
    }
}

public class ColorChangedDemo : ViewBase
{

    public override object? Build()
    {
        var colorState = UseState("#ff0000");
        var colorName = UseState(colorState.Value);
        var onChangeHandler = (Event<IInput<string>, string> e) =>
        {
            colorName.Set(e.Value);
            colorState.Set(e.Value);
        };
        return Layout.Vertical()
                | H3("Hex Color Picker")
                | (Layout.Horizontal()
                | new ColorInput<string>
                       (colorState.Value, onChangeHandler)
                      .Variant(ColorInputVariant.Picker)
                | new CodeBlock(colorName.Value)
                    .ShowCopyButton()
                    .ShowBorder());
    }
}

public class ColorStylingDemo : ViewBase
{
    public override object? Build()
    {
        var colorState = UseState("#ff0000");
        return Layout.Grid().Columns(2).ColumnWidths(Size.Units(30), null)
            | Text.P("Disabled").Small() | colorState.ToColorInput().Disabled()
            | Text.P("Invalid").Small() | colorState.ToColorInput().Invalid("Invalid color value");
    }
}

public class ColorAlphaDemo : ViewBase
{
    public override object? Build()
    {
        var colorState = UseState("#ff000080");
        return Layout.Vertical()
            | colorState.ToColorInput().AllowAlpha()
            | Text.P($"Selected: {colorState.Value}");
    }
}

public class CSSColorDemo : ViewBase
{
    public override object? Build()
    {
        var color = UseState("#333333");
        var bgColor = UseState("#F5F5F5");
        var border = UseState("#CCCCCC");
        var template = """
                     .my-element {
                            color: [COLOR];
                            background-color: [BG_COLOR];
                            border: 1px solid [BORDER];
                      }
        """;
        var genCode = UseState("");
        genCode.Set(template.Replace("[COLOR]",color.Value)
                            .Replace("[BG_COLOR]",bgColor.Value)
                            .Replace("[BORDER]",border.Value));
        return Layout.Vertical()
                | H3("CSS Block Generator")
                | (Layout.Horizontal()
                   | Text.Monospaced("color")
                         .Width(Size.Units(35))
                   | color.ToColorInput()
                          .Variant(ColorInputVariant.Picker))
                | (Layout.Horizontal()
                   | Text.Monospaced("background-color")
                         .Width(Size.Units(35))
                   | bgColor.ToColorInput()
                          .Variant(ColorInputVariant.Picker))
                | (Layout.Horizontal()
                   | Text.Monospaced("border")
                         .Width(Size.Units(35))
                   | border.ToColorInput()
                          .Variant(ColorInputVariant.Picker))
                   | new CodeBlock(genCode.Value)
                         .Language(Languages.Css)
                         .ShowCopyButton();
    }
}
