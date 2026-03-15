using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Common;

[App(order:15, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/03_Common/15_Dialog.md", searchHints: ["modal", "popup", "window", "dialog", "overlay", "confirm", "alert"])]
public class DialogApp(bool onlyBody = false) : ViewBase
{
    public DialogApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("dialog", "Dialog", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("simple-content", "Simple Content", 2), new ArticleHeading("forms-in-dialogs", "Forms in Dialogs", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Dialog").OnLinkClick(onLinkClick)
            | Lead("Modal windows that interrupt the current workflow to request information or confirmation. Dialogs consist of three sections: Header, Body, and Footer.")
            | new Markdown(
                """"
                A `Dialog` consists of three sections:
                
                - **`DialogHeader`**: Contains the title and close button. Accepts a `string` title.
                - **`DialogBody`**: The main content area. Accepts any widgets or views as children.
                - **`DialogFooter`**: Typically contains action buttons (Cancel, Confirm, etc.). Accepts any widgets as children.
                
                ## Basic Usage
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicDialogExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var isOpen = UseState(false);
                            var client = UseService<IClientProvider>();
                    
                            return Layout.Vertical(
                                new Button("Open Dialog", _ => isOpen.Set(true)),
                                isOpen.Value
                                    ? new Dialog(
                                        _ => isOpen.Set(false),
                                        new DialogHeader("Confirm Action"),
                                        new DialogBody(
                                            Text.P("Are you sure you want to proceed with this action?")
                                        ),
                                        new DialogFooter(
                                            new Button("Cancel", _ => isOpen.Set(false)).Outline(),
                                            new Button("Confirm", _ =>
                                            {
                                                isOpen.Set(false);
                                                client.Toast("Action confirmed!");
                                            })
                                        )
                                    )
                                    : null
                            );
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicDialogExample())
            )
            | new Markdown(
                """"
                ## Simple Content
                
                Use the `ToDialog()` extension method to display any content in a dialog:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class SimpleDialogExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var isOpen = UseState(false);
                    
                            var content = Layout.Vertical()
                                | Text.P("This is some content")
                                | new Badge("Info")
                                | Text.P("You can put any widgets here.");
                    
                            return Layout.Vertical(
                                new Button("Open Dialog", _ => isOpen.Set(true)),
                                content.ToDialog(isOpen, title: "Simple Dialog", description: "This dialog contains simple content.")
                            );
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new SimpleDialogExample())
            )
            | new Markdown(
                """"
                ## Forms in Dialogs
                
                Use the `ToDialog()` extension method to display [forms](app://onboarding/concepts/forms) in dialogs:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class FormDialogExample : ViewBase
                    {
                        public record NewItemModel(string Name, string Description);
                    
                        public override object? Build()
                        {
                            var isOpen = UseState(false);
                            var client = UseService<IClientProvider>();
                            var model = UseState(() => new NewItemModel("", ""));
                    
                            async Task OnSubmit(NewItemModel submittedModel)
                            {
                                await Task.Delay(500);
                                client.Toast($"Created: {submittedModel.Name}");
                                model.Set(new NewItemModel("", ""));
                            }
                    
                            return Layout.Vertical(
                                new Button("Create Item", _ => isOpen.Set(true)),
                                model.Value
                                    .ToForm()
                                    .OnSubmit(OnSubmit)
                                    .Builder(e => e.Name, e => e.ToTextInput().Placeholder("Enter item name"))
                                    .Builder(e => e.Description, e => e.ToTextareaInput().Placeholder("Enter description"))
                                    .ToDialog(isOpen, title: "Create New Item", description: "Fill in the details below.", submitTitle: "Create")
                            );
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new FormDialogExample())
            )
            | new WidgetDocsView("Ivy.Dialog", null, "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Dialogs/Dialog.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.FormsApp)]; 
        return article;
    }
}


public class BasicDialogExample : ViewBase
{
    public override object? Build()
    {
        var isOpen = UseState(false);
        var client = UseService<IClientProvider>();

        return Layout.Vertical(
            new Button("Open Dialog", _ => isOpen.Set(true)),
            isOpen.Value
                ? new Dialog(
                    _ => isOpen.Set(false),
                    new DialogHeader("Confirm Action"),
                    new DialogBody(
                        Text.P("Are you sure you want to proceed with this action?")
                    ),
                    new DialogFooter(
                        new Button("Cancel", _ => isOpen.Set(false)).Outline(),
                        new Button("Confirm", _ =>
                        {
                            isOpen.Set(false);
                            client.Toast("Action confirmed!");
                        })
                    )
                )
                : null
        );
    }
}

public class SimpleDialogExample : ViewBase
{
    public override object? Build()
    {
        var isOpen = UseState(false);

        var content = Layout.Vertical()
            | Text.P("This is some content")
            | new Badge("Info")
            | Text.P("You can put any widgets here.");

        return Layout.Vertical(
            new Button("Open Dialog", _ => isOpen.Set(true)),
            content.ToDialog(isOpen, title: "Simple Dialog", description: "This dialog contains simple content.")
        );
    }
}

public class FormDialogExample : ViewBase
{
    public record NewItemModel(string Name, string Description);

    public override object? Build()
    {
        var isOpen = UseState(false);
        var client = UseService<IClientProvider>();
        var model = UseState(() => new NewItemModel("", ""));

        async Task OnSubmit(NewItemModel submittedModel)
        {
            await Task.Delay(500);
            client.Toast($"Created: {submittedModel.Name}");
            model.Set(new NewItemModel("", ""));
        }

        return Layout.Vertical(
            new Button("Create Item", _ => isOpen.Set(true)),
            model.Value
                .ToForm()
                .OnSubmit(OnSubmit)
                .Builder(e => e.Name, e => e.ToTextInput().Placeholder("Enter item name"))
                .Builder(e => e.Description, e => e.ToTextareaInput().Placeholder("Enter description"))
                .ToDialog(isOpen, title: "Create New Item", description: "Fill in the details below.", submitTitle: "Create")
        );
    }
}
