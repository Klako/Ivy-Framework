using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Inputs;

[App(order:14, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/04_Inputs/14_ReadOnlyInput.md", searchHints: ["disabled", "readonly", "display", "static", "non-editable", "locked"])]
public class ReadOnlyInputApp(bool onlyBody = false) : ViewBase
{
    public ReadOnlyInputApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("readonlyinput", "ReadOnlyInput", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# ReadOnlyInput").OnLinkClick(onLinkClick)
            | Lead("Display [form](app://onboarding/concepts/forms) data in a consistent input-like style that maintains visual coherence while preventing user modification.")
            | new Markdown(
                """"
                The `ReadOnlyInput` [widget](app://onboarding/concepts/widgets) displays data in an input-like format that cannot be edited by the user. It's useful for showing form values in a consistent style with other [inputs](app://onboarding/concepts/widgets), while preventing modification.
                
                ## Basic Usage
                
                Here's a simple example of a `ReadOnlyInput` displaying a value:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class ReadOnlyDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            double value = 123.45;
                            var readOnlyInput = new ReadOnlyInput<double>(value);
                            return readOnlyInput;
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new ReadOnlyDemo())
            )
            | new WidgetDocsView("Ivy.ReadOnlyInput", "Ivy.ReadOnlyInputExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Inputs/ReadOnlyInput.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("ReadOnlyInput can be used to display computed or derived values in a form alongside editable inputs.",
                Vertical().Gap(4)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new ReadOnlyFormDemo())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class ReadOnlyFormDemo : ViewBase
                        {
                            public override object? Build()
                            {
                                var price = UseState(100.0);
                                var quantity = UseState(5);
                                var total = price.Value * quantity.Value;
                        
                                return Layout.Vertical().Gap(2)
                                    | new NumberInput<double>(price)
                                        .WithField().Label("Price")
                                    | new NumberInput<int>(quantity)
                                        .WithField().Label("Quantity")
                                    | new ReadOnlyInput<double>(total)
                                        .WithField().Label("Total");
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.FormsApp), typeof(Onboarding.Concepts.WidgetsApp)]; 
        return article;
    }
}


public class ReadOnlyDemo : ViewBase
{    
    public override object? Build()
    {    
        double value = 123.45;
        var readOnlyInput = new ReadOnlyInput<double>(value);
        return readOnlyInput;
    }    
}    

public class ReadOnlyFormDemo : ViewBase
{
    public override object? Build()
    {
        var price = UseState(100.0);
        var quantity = UseState(5);
        var total = price.Value * quantity.Value;
        
        return Layout.Vertical().Gap(2)
            | new NumberInput<double>(price)
                .WithField().Label("Price")
            | new NumberInput<int>(quantity)
                .WithField().Label("Quantity")
            | new ReadOnlyInput<double>(total)
                .WithField().Label("Total");
    }
}
