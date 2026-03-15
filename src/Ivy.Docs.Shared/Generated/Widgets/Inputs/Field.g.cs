using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Inputs;

[App(order:1, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/04_Inputs/01_Field.md", searchHints: ["label", "wrapper", "form-field", "input", "description", "help"])]
public class FieldApp(bool onlyBody = false) : ViewBase
{
    public FieldApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("field", "Field", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("properties", "Properties", 2), new ArticleHeading("layout-configuration", "Layout Configuration", 3), new ArticleHeading("properties-usage", "Properties Usage", 3), new ArticleHeading("wrapping-other-inputs", "Wrapping Other Inputs", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Field").OnLinkClick(onLinkClick)
            | Lead("Group any input with a label, description, help text, and required indicator for a consistent, accessible [form](app://onboarding/concepts/forms) design.")
            | new Markdown(
                """"
                The `Field` [widget](app://onboarding/concepts/widgets) acts as a **wrapper** around any input (such as `TextInput`, `Select`, `DateTime`, etc.).
                It provides a standardized way to display a label, optional description, help text [tooltips](app://widgets/common/tooltip), and visual cues like a required asterisk.
                
                This makes [forms](app://onboarding/concepts/forms) easier to build and ensures inputs remain consistent in layout and accessibility.
                
                ## Basic Usage
                
                Here's how to wrap a `TextInput` in a `Field`:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicFieldDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicFieldDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var name = UseState("");
                            return new Field(
                                new TextInput(name)
                                    .Placeholder("Enter your name")
                            )
                            .Label("Name")
                            .Description("Your full name as it appears on official documents")
                            .Required();
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("`Field` does not provide inputs by itself - it always wraps an input widget like `TextInput`, `Select`, or `Checkbox`.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Properties
                
                A `Field` supports the following common properties:
                
                * **Label(string)** - The display label above the input.
                * **Description(string)** - An optional helper text shown below the input.
                * **Help(string)** - An optional help text displayed as a tooltip on an info icon next to the label.
                * **Required(bool)** - Marks the input as required (adds an asterisk or style cue).
                
                ### Layout Configuration
                
                * **LabelPosition(LabelPosition)** - Controls whether the label is positioned above the input (`Top`) or beside it (`Left`).
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class LabelPositionExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var email = UseState("");
                            return email.ToTextInput()
                                .Placeholder("admin@company.com")
                                .WithField()
                                .Label("Work Email")
                                .LabelPosition(LabelPosition.Left)
                                .Description("We'll use this for account recovery");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new LabelPositionExample())
            )
            | new Markdown("### Properties Usage").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class FieldExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var username = UseState("");
                            return username.ToTextInput()
                                .Placeholder("Enter username")
                                .WithField()
                                .Label("Username")
                                .Description("Must be at least 8 characters long")
                                .Help("Your username must be unique and contain only letters, numbers, and underscores")
                                .Required();
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new FieldExample())
            )
            | new Markdown(
                """"
                ### Wrapping Other Inputs
                
                Since `Field` works generically, it can wrap **any widget**:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class MixedInputsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var dateState = UseState(DateTime.Today);
                    
                            var accepted = UseState(false);
                            var options = new List<string>() { "I read the terms and conditions and I agree"};
                            var selectedNotice = UseState(new string[]{});
                            return Layout.Vertical()
                                | dateState.ToDateTimeInput()
                                    .Variant(DateTimeInputVariant.Date)
                                    .WithField()
                                    .Label("Date of birth")
                                | selectedNotice.ToSelectInput(options.ToOptions())
                                    .Variant(SelectInputVariant.List)
                                    .WithField()
                                    .Label("Terms & Conditions")
                                    .Description("You must agree before continuing")
                                    .Required();
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new MixedInputsDemo())
            )
            | new Callout("Use `Field` whenever you want **consistent form layout** across your application with labels, description and required asterisk.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new WidgetDocsView("Ivy.Field", null, "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Inputs/Field.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.FormsApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Widgets.Common.TooltipApp)]; 
        return article;
    }
}


public class BasicFieldDemo : ViewBase
{
    public override object? Build()
    {
        var name = UseState("");
        return new Field(
            new TextInput(name)
                .Placeholder("Enter your name")
        )
        .Label("Name")
        .Description("Your full name as it appears on official documents")
        .Required();
    }
}

public class LabelPositionExample : ViewBase
{
    public override object? Build()
    {
        var email = UseState("");
        return email.ToTextInput()
            .Placeholder("admin@company.com")
            .WithField()
            .Label("Work Email")
            .LabelPosition(LabelPosition.Left)
            .Description("We'll use this for account recovery");
    }
}

public class FieldExample : ViewBase
{
    public override object? Build()
    {
        var username = UseState("");
        return username.ToTextInput()
            .Placeholder("Enter username")
            .WithField()
            .Label("Username")
            .Description("Must be at least 8 characters long")
            .Help("Your username must be unique and contain only letters, numbers, and underscores")
            .Required();
    }
}

public class MixedInputsDemo : ViewBase
{
    public override object? Build()
    {
        var dateState = UseState(DateTime.Today);

        var accepted = UseState(false);
        var options = new List<string>() { "I read the terms and conditions and I agree"};
        var selectedNotice = UseState(new string[]{});
        return Layout.Vertical()
            | dateState.ToDateTimeInput()
                .Variant(DateTimeInputVariant.Date)
                .WithField()
                .Label("Date of birth")
            | selectedNotice.ToSelectInput(options.ToOptions())
                .Variant(SelectInputVariant.List)
                .WithField()
                .Label("Terms & Conditions")
                .Description("You must agree before continuing")
                .Required();
    }
}
