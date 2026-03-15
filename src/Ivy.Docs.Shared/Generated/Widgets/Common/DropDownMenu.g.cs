using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Common;

[App(order:11, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/03_Common/11_DropDownMenu.md", searchHints: ["menu", "dropdown", "context", "options", "actions", "popup"])]
public class DropDownMenuApp(bool onlyBody = false) : ViewBase
{
    public DropDownMenuApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("dropdownmenu", "DropDownMenu", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("default-menu-items", "Default Menu Items", 3), new ArticleHeading("checkbox-menu-items", "Checkbox Menu Items", 3), new ArticleHeading("separators", "Separators", 3), new ArticleHeading("nested-menu-items", "Nested Menu Items", 3), new ArticleHeading("positioning", "Positioning", 2), new ArticleHeading("advanced-features", "Advanced Features", 2), new ArticleHeading("menu-headers", "Menu Headers", 3), new ArticleHeading("fluent-syntax", "Fluent Syntax", 3), new ArticleHeading("custom-event-handling", "Custom Event Handling", 3), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# DropDownMenu").OnLinkClick(onLinkClick)
            | Lead("Create interactive dropdown menus with customizable options, actions, and styling for [navigation](app://onboarding/concepts/navigation) and user choices. DropDownMenu provides a flexible way to display hierarchical menus with various item types including checkboxes, radio buttons, separators, and nested submenus.")
            | new Markdown(
                """"
                ## Basic Usage
                
                Here's a simple example of a `DropDownMenu` that shows a [toast message](app://onboarding/concepts/clients) when an item is selected:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value),
                        new Button("Basic Menu"),
                        MenuItem.Default("Profile"),
                        MenuItem.Default("Settings"),
                        MenuItem.Default("Logout"))
                    """",Languages.Csharp)
                | new Box().Content(new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value), 
    new Button("Basic Menu"),
    MenuItem.Default("Profile"), 
    MenuItem.Default("Settings"), 
    MenuItem.Default("Logout")))
            )
            | new Markdown(
                """"
                ### Default Menu Items
                
                Default menu items are the most common type, providing simple clickable options. The second example shows how to add custom tags for more advanced [event handling](app://onboarding/concepts/event-handlers).
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new DefaultMenuItemsDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class DefaultMenuItemsDemo : ViewBase
                    {
                        private enum MenuAction { Save, Export, Import }
                    
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            return Layout.Horizontal().Gap(2).Center()
                                | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value),
                                    new Button("Default Items"),
                                    MenuItem.Default("Copy"),
                                    MenuItem.Default("Paste"),
                                    MenuItem.Default("Cut"),
                                    MenuItem.Default("Delete"))
                                | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value),
                                    new Button("With Tags"),
                                    MenuItem.Default("Save").Tag(MenuAction.Save),
                                    MenuItem.Default("Export").Tag(MenuAction.Export),
                                    MenuItem.Default("Import").Tag(MenuAction.Import));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                Default menu items are the most common type, providing simple clickable options. The second example shows how to add custom tags for more advanced event handling.
                
                ### Checkbox Menu Items
                
                Checkbox menu items allow users to toggle options on/off. The second example demonstrates mixing different menu item types for more complex [interfaces](app://onboarding/concepts/views).
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal().Gap(2).Center()
    | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value), 
        new Button("Checkboxes"),
        MenuItem.Checkbox("Dark Theme").Checked(),
        MenuItem.Checkbox("Notifications"),
        MenuItem.Checkbox("Auto-save").Checked(),
        MenuItem.Checkbox("Debug Mode"))
    | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value), 
        new Button("Mixed Types"),
        MenuItem.Default("Profile"),
        MenuItem.Separator(),
        MenuItem.Checkbox("Email Notifications").Checked(),
        MenuItem.Checkbox("SMS Notifications"),
        MenuItem.Checkbox("Push Notifications").Checked()))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal().Gap(2).Center()
                        | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value),
                            new Button("Checkboxes"),
                            MenuItem.Checkbox("Dark Theme").Checked(),
                            MenuItem.Checkbox("Notifications"),
                            MenuItem.Checkbox("Auto-save").Checked(),
                            MenuItem.Checkbox("Debug Mode"))
                        | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value),
                            new Button("Mixed Types"),
                            MenuItem.Default("Profile"),
                            MenuItem.Separator(),
                            MenuItem.Checkbox("Email Notifications").Checked(),
                            MenuItem.Checkbox("SMS Notifications"),
                            MenuItem.Checkbox("Push Notifications").Checked())
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Separators
                
                Separators help organize menu items into logical groups, making the [interface](app://onboarding/concepts/views) more readable and user-friendly.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value), 
    new Button("With Separators"))
    | MenuItem.Default("New File")
    | MenuItem.Default("Open File")
    | MenuItem.Separator()
    | MenuItem.Default("Save")
    | MenuItem.Default("Save As")
    | MenuItem.Separator()
    | MenuItem.Default("Print")
    | MenuItem.Default("Export"))),
                new Tab("Code", new CodeBlock(
                    """"
                    new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value),
                        new Button("With Separators"))
                        | MenuItem.Default("New File")
                        | MenuItem.Default("Open File")
                        | MenuItem.Separator()
                        | MenuItem.Default("Save")
                        | MenuItem.Default("Save As")
                        | MenuItem.Separator()
                        | MenuItem.Default("Print")
                        | MenuItem.Default("Export")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Nested Menu Items
                
                Nested menu items create submenus for better organization of complex menu structures.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value), 
    new Button("Nested Items"))
    | MenuItem.Default("File")
        .Children(
            MenuItem.Default("New"),
            MenuItem.Default("Open"),
            MenuItem.Default("Save")
        )
    | MenuItem.Default("Edit")
        .Children(
            MenuItem.Default("Undo"),
            MenuItem.Default("Redo"),
            MenuItem.Default("Cut")
        ))),
                new Tab("Code", new CodeBlock(
                    """"
                    new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value),
                        new Button("Nested Items"))
                        | MenuItem.Default("File")
                            .Children(
                                MenuItem.Default("New"),
                                MenuItem.Default("Open"),
                                MenuItem.Default("Save")
                            )
                        | MenuItem.Default("Edit")
                            .Children(
                                MenuItem.Default("Undo"),
                                MenuItem.Default("Redo"),
                                MenuItem.Default("Cut")
                            )
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Positioning
                
                Control which side of the trigger the menu appears on:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal().Gap(2).Center()
    | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value), 
        new Button("Top"), MenuItem.Default("Item 1"), MenuItem.Default("Item 2"))
        .Top()
    | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value), 
        new Button("Right"), MenuItem.Default("Item 1"), MenuItem.Default("Item 2"))
        .Right()
    | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value), 
        new Button("Bottom"), MenuItem.Default("Item 1"), MenuItem.Default("Item 2"))
        .Bottom()
    | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value), 
        new Button("Left"), MenuItem.Default("Item 1"), MenuItem.Default("Item 2"))
        .Left())),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal().Gap(2).Center()
                        | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value),
                            new Button("Top"), MenuItem.Default("Item 1"), MenuItem.Default("Item 2"))
                            .Top()
                        | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value),
                            new Button("Right"), MenuItem.Default("Item 1"), MenuItem.Default("Item 2"))
                            .Right()
                        | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value),
                            new Button("Bottom"), MenuItem.Default("Item 1"), MenuItem.Default("Item 2"))
                            .Bottom()
                        | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value),
                            new Button("Left"), MenuItem.Default("Item 1"), MenuItem.Default("Item 2"))
                            .Left()
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Advanced Features
                
                ### Menu Headers
                
                Headers provide context and user information, making menus more informative and professional-looking.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal().Gap(2).Center()
    | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value), 
        new Button("Muted Header"),
        MenuItem.Separator(),
        MenuItem.Default("Profile"),
        MenuItem.Default("Settings"),
        MenuItem.Default("Logout"))
        .Header(Text.Muted("Signed in as user@example.com"))
    | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value), 
        new Button("Label Header"),
        MenuItem.Separator(),
        MenuItem.Default("View Profile"),
        MenuItem.Default("Account Settings"))
        .Header(Text.Label("John Doe - Administrator")))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal().Gap(2).Center()
                        | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value),
                            new Button("Muted Header"),
                            MenuItem.Separator(),
                            MenuItem.Default("Profile"),
                            MenuItem.Default("Settings"),
                            MenuItem.Default("Logout"))
                            .Header(Text.Muted("Signed in as user@example.com"))
                        | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value),
                            new Button("Label Header"),
                            MenuItem.Separator(),
                            MenuItem.Default("View Profile"),
                            MenuItem.Default("Account Settings"))
                            .Header(Text.Label("John Doe - Administrator"))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Fluent Syntax
                
                The `WithDropDown` extension method provides a clean, fluent API for quickly adding dropdown functionality to existing buttons.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal().Gap(2).Center()
    | new Button("Quick Menu")
        .WithDropDown(
            MenuItem.Default("Option 1"),
            MenuItem.Default("Option 2"),
            MenuItem.Default("Option 3")
        )
    | new Button("Settings")
        .Secondary()
        .WithDropDown(
            MenuItem.Default("Preferences"),
            MenuItem.Default("Account"),
            MenuItem.Default("Help")
        ))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal().Gap(2).Center()
                        | new Button("Quick Menu")
                            .WithDropDown(
                                MenuItem.Default("Option 1"),
                                MenuItem.Default("Option 2"),
                                MenuItem.Default("Option 3")
                            )
                        | new Button("Settings")
                            .Secondary()
                            .WithDropDown(
                                MenuItem.Default("Preferences"),
                                MenuItem.Default("Account"),
                                MenuItem.Default("Help")
                            )
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Custom Event Handling
                
                Custom event handling allows you to implement complex [business logic](app://hooks/core/use-service) based on menu selections, making your dropdowns more interactive and useful.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new CustomActionsDropDownDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class CustomActionsDropDownDemo : ViewBase
                    {
                        private enum ItemAction { View, Edit, Delete, Export, Share }
                    
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            return new DropDownMenu(@evt => {
                                if (Enum.TryParse<ItemAction>(@evt.Value?.ToString(), ignoreCase: true, out var action))
                                {
                                    switch (action)
                                    {
                                        case ItemAction.Delete: client.Toast("Deleting item..."); break;
                                        case ItemAction.Export: client.Toast("Exporting data..."); break;
                                        default: client.Toast($"Selected: {action}"); break;
                                    }
                                }
                            },
                            new Button("Custom Actions"))
                                | MenuItem.Default("View").Tag(ItemAction.View)
                                | MenuItem.Default("Edit").Tag(ItemAction.Edit)
                                | MenuItem.Default("Delete").Tag(ItemAction.Delete)
                                | MenuItem.Separator()
                                | MenuItem.Default("Export").Tag(ItemAction.Export)
                                | MenuItem.Default("Share").Tag(ItemAction.Share);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.DropDownMenu", "Ivy.DropDownMenuExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/DropDownMenu.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Complex using",
                Vertical().Gap(4)
                | new Markdown("Here's a comprehensive example combining multiple features:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new ComplexDropDownDemo())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class ComplexDropDownDemo : ViewBase
                        {
                            private enum UserMenuAction { Profile, Settings, Preferences, ThemeLight, ThemeDark, ThemeSystem, NotifyEmail, NotifyPush, NotifySms, Help, About, Logout }
                            private enum SettingsMenuAction { General, Appearance, Privacy, Security, Updates, Support }
                        
                            public override object? Build()
                            {
                                var client = UseService<IClientProvider>();
                                return Layout.Horizontal().Gap(2).Center()
                                    | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value),
                                        new Button("User Menu"),
                                        MenuItem.Separator(),
                                        MenuItem.Default("View Profile").Tag(UserMenuAction.Profile),
                                        MenuItem.Default("Account Settings").Tag(UserMenuAction.Settings),
                                        MenuItem.Default("Preferences").Tag(UserMenuAction.Preferences),
                                        MenuItem.Separator(),
                                        MenuItem.Default("Theme")
                                            .Children(
                                                MenuItem.Checkbox("Light").Tag(UserMenuAction.ThemeLight),
                                                MenuItem.Checkbox("Dark").Checked().Tag(UserMenuAction.ThemeDark),
                                                MenuItem.Checkbox("System").Tag(UserMenuAction.ThemeSystem)
                                            ),
                                        MenuItem.Default("Notifications")
                                            .Children(
                                                MenuItem.Checkbox("Email").Checked().Tag(UserMenuAction.NotifyEmail),
                                                MenuItem.Checkbox("Push").Checked().Tag(UserMenuAction.NotifyPush),
                                                MenuItem.Checkbox("SMS").Tag(UserMenuAction.NotifySms)
                                            ),
                                        MenuItem.Separator(),
                                        MenuItem.Default("Help & Support").Tag(UserMenuAction.Help),
                                        MenuItem.Default("About").Tag(UserMenuAction.About),
                                        MenuItem.Separator(),
                                        MenuItem.Default("Logout").Tag(UserMenuAction.Logout))
                                        .Header(Text.Muted("Signed in as john.doe@company.com"))
                                        .Top()
                                        .Align(DropDownMenu.AlignOptions.End)
                                    | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value),
                                        new Button("Settings Menu"),
                                        MenuItem.Default("General").Tag(SettingsMenuAction.General),
                                        MenuItem.Default("Appearance").Tag(SettingsMenuAction.Appearance),
                                        MenuItem.Default("Privacy").Tag(SettingsMenuAction.Privacy),
                                        MenuItem.Default("Security").Tag(SettingsMenuAction.Security),
                                        MenuItem.Separator(),
                                        MenuItem.Default("Updates").Tag(SettingsMenuAction.Updates),
                                        MenuItem.Default("Support").Tag(SettingsMenuAction.Support))
                                        .Header(Text.Muted("Application Settings"));
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.NavigationApp), typeof(Onboarding.Concepts.ClientsApp), typeof(Onboarding.Concepts.EventHandlersApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Hooks.Core.UseServiceApp)]; 
        return article;
    }
}


public class DefaultMenuItemsDemo : ViewBase
{
    private enum MenuAction { Save, Export, Import }

    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        return Layout.Horizontal().Gap(2).Center()
            | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value), 
                new Button("Default Items"),
                MenuItem.Default("Copy"),
                MenuItem.Default("Paste"),
                MenuItem.Default("Cut"),
                MenuItem.Default("Delete"))
            | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value), 
                new Button("With Tags"),
                MenuItem.Default("Save").Tag(MenuAction.Save),
                MenuItem.Default("Export").Tag(MenuAction.Export),
                MenuItem.Default("Import").Tag(MenuAction.Import));
    }
}

public class CustomActionsDropDownDemo : ViewBase
{
    private enum ItemAction { View, Edit, Delete, Export, Share }

    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        return new DropDownMenu(@evt => {
            if (Enum.TryParse<ItemAction>(@evt.Value?.ToString(), ignoreCase: true, out var action))
            {
                switch (action)
                {
                    case ItemAction.Delete: client.Toast("Deleting item..."); break;
                    case ItemAction.Export: client.Toast("Exporting data..."); break;
                    default: client.Toast($"Selected: {action}"); break;
                }
            }
        }, 
        new Button("Custom Actions"))
            | MenuItem.Default("View").Tag(ItemAction.View)
            | MenuItem.Default("Edit").Tag(ItemAction.Edit)
            | MenuItem.Default("Delete").Tag(ItemAction.Delete)
            | MenuItem.Separator()
            | MenuItem.Default("Export").Tag(ItemAction.Export)
            | MenuItem.Default("Share").Tag(ItemAction.Share);
    }
}

public class ComplexDropDownDemo : ViewBase
{
    private enum UserMenuAction { Profile, Settings, Preferences, ThemeLight, ThemeDark, ThemeSystem, NotifyEmail, NotifyPush, NotifySms, Help, About, Logout }
    private enum SettingsMenuAction { General, Appearance, Privacy, Security, Updates, Support }

    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        return Layout.Horizontal().Gap(2).Center()
            | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value), 
                new Button("User Menu"),
                MenuItem.Separator(),
                MenuItem.Default("View Profile").Tag(UserMenuAction.Profile),
                MenuItem.Default("Account Settings").Tag(UserMenuAction.Settings),
                MenuItem.Default("Preferences").Tag(UserMenuAction.Preferences),
                MenuItem.Separator(),
                MenuItem.Default("Theme")
                    .Children(
                        MenuItem.Checkbox("Light").Tag(UserMenuAction.ThemeLight),
                        MenuItem.Checkbox("Dark").Checked().Tag(UserMenuAction.ThemeDark),
                        MenuItem.Checkbox("System").Tag(UserMenuAction.ThemeSystem)
                    ),
                MenuItem.Default("Notifications")
                    .Children(
                        MenuItem.Checkbox("Email").Checked().Tag(UserMenuAction.NotifyEmail),
                        MenuItem.Checkbox("Push").Checked().Tag(UserMenuAction.NotifyPush),
                        MenuItem.Checkbox("SMS").Tag(UserMenuAction.NotifySms)
                    ),
                MenuItem.Separator(),
                MenuItem.Default("Help & Support").Tag(UserMenuAction.Help),
                MenuItem.Default("About").Tag(UserMenuAction.About),
                MenuItem.Separator(),
                MenuItem.Default("Logout").Tag(UserMenuAction.Logout))
                .Header(Text.Muted("Signed in as john.doe@company.com"))
                .Top()
                .Align(DropDownMenu.AlignOptions.End)
            | new DropDownMenu(@evt => client.Toast("Selected: " + @evt.Value), 
                new Button("Settings Menu"),
                MenuItem.Default("General").Tag(SettingsMenuAction.General),
                MenuItem.Default("Appearance").Tag(SettingsMenuAction.Appearance),
                MenuItem.Default("Privacy").Tag(SettingsMenuAction.Privacy),
                MenuItem.Default("Security").Tag(SettingsMenuAction.Security),
                MenuItem.Separator(),
                MenuItem.Default("Updates").Tag(SettingsMenuAction.Updates),
                MenuItem.Default("Support").Tag(SettingsMenuAction.Support))
                .Header(Text.Muted("Application Settings"));
    }
}
