using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:11, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/11_Chrome.md", searchHints: ["chrome", "sidebar", "header", "footer", "navigation", "tabs", "pages", "wallpaper", "background", "transformer", "menu"])]
public class ChromeApp(bool onlyBody = false) : ViewBase
{
    public ChromeApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("chrome-configuration", "Chrome Configuration", 1), new ArticleHeading("chromesettings-options", "ChromeSettings Options", 2), new ArticleHeading("wallpaper", "Wallpaper", 2), new ArticleHeading("configuration", "Configuration", 3), new ArticleHeading("full-example", "Full Example", 3), new ArticleHeading("footer-transformer", "Footer Transformer", 2), new ArticleHeading("usage-example", "Usage Example", 3), new ArticleHeading("role-based-filtering", "Role-Based Filtering", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Chrome Configuration").OnLinkClick(onLinkClick)
            | Lead("Configure the application chrome ([sidebar](app://widgets/layouts/sidebar-layout), header, footer) using ChromeSettings to customize [navigation](app://onboarding/concepts/navigation), branding, and layout behavior.")
            | new Markdown("You can add custom elements to both the header and footer sections of the sidebar using `ChromeSettings`. The example uses [Layout](app://onboarding/concepts/layout), [Button](app://widgets/common/button), and [Text](app://widgets/primitives/text-block) widgets:").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var chromeSettings = new ChromeSettings()
                    .Header(
                        Layout.Vertical().Gap(2)
                        | new IvyLogo()
                        | Text.Lead("Enterprise Management System")
                        | Text.Muted("Comprehensive business application suite")
                    )
                    .Footer(
                        Layout.Vertical().Gap(2)
                        | new Button("Support")
                            .OnClick(_ => { })
                        | Text.P("Enterprise Application Framework").Small()
                    )
                    .DefaultApp<MyApp>()
                    .UseTabs(preventDuplicates: true)
                    .SidebarOpen(false); // Start with sidebar collapsed
                
                server.UseChrome(() => new DefaultSidebarChrome(chromeSettings));
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## ChromeSettings Options
                
                - **DefaultAppId(string? appId)** - Sets the default app to load by ID.
                
                - **DefaultApp<T>()** - Sets the default app using a type (recommended for compile-time safety).
                
                - **UseTabs(bool preventDuplicates)** - Enables tab navigation. When `preventDuplicates` is `true`, prevents duplicate tabs.
                
                - **UsePages()** - Switches to page navigation (replaces content instead of opening tabs).
                
                - **UseFooterMenuItemsTransformer(`Func<IEnumerable<MenuItem>, INavigator, IEnumerable<MenuItem>>` transformer)** - Provides a way to dynamically transform the footer menu items. Useful for adding, removing, or re-ordering links based on runtime context such as user roles or navigation state.
                
                - **WallpaperAppId(string? appId)** / **WallpaperApp<T>()** - Sets a dedicated *wallpaper* app that is shown whenever the tab list is empty. Handy for welcome screens or branded backgrounds.
                
                - **SidebarOpen(bool open)** - Controls whether the sidebar is initially expanded or collapsed. Defaults to `true`.
                """").OnLinkClick(onLinkClick)
            | new Callout("Use `server.UseDefaultApp(typeof(AppName))` instead of `UseChrome()` for single-purpose applications, embedded views, or minimal interfaces where sidebar navigation isn't needed.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Wallpaper
                
                Configure a dedicated background *app* that appears when no other tabs are open. Perfect for welcome screens, dashboards or branded imagery.
                
                The **Wallpaper** is just another [app](app://onboarding/concepts/apps) rendered full-screen by the Chrome host whenever the tab area is empty. This keeps your UI visually engaging instead of showing an empty canvas.
                
                ### Configuration
                
                The wallpaper is selected through `ChromeSettings.WallpaperAppId`. Two helper extensions make this convenient:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Explicit id
                var chromeSettings = ChromeSettings.Default()
                    .WallpaperAppId("welcome-screen");
                
                // Or using a type – compile-time safety
                chromeSettings = chromeSettings.WallpaperApp<WelcomeScreenApp>();
                """",Languages.Csharp)
            | new Markdown(
                """"
                1. Implement a normal Ivy app (derive from [ViewBase](app://onboarding/concepts/views)).
                2. Register it like any other [app](app://onboarding/concepts/apps) (`server.AddApp<WelcomeScreenApp>()`).
                3. Reference it in `ChromeSettings` with one of the helpers above.
                
                ### Full Example
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class WelcomeScreenApp : ViewBase
                {
                    public override object? Build()
                        => Layout.Center(
                            new Image("/ivy/img/brand-logo.svg").AltText("My Brand"),
                            Text.H1("Welcome to My System")
                        );
                }
                
                var server = new Server();
                server.AddAppsFromAssembly();
                
                var chromeSettings = ChromeSettings.Default()
                    .WallpaperApp<WelcomeScreenApp>()
                    .UseTabs();
                
                server.UseChrome(() => new DefaultSidebarChrome(chromeSettings));
                await server.RunAsync();
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Footer Transformer
                
                Dynamically customize the list of links shown at the very bottom of the sidebar by providing a transformation function with `UseFooterMenuItemsTransformer`.
                
                `ChromeSettings.UseFooterMenuItemsTransformer` accepts a delegate with the following signature:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("Func<IEnumerable<MenuItem>, INavigator, IEnumerable<MenuItem>>",Languages.Csharp)
            | new Markdown(
                """"
                - **items** – the menu items produced by Ivy (from discovered apps).
                - **navigator** – helper you can use to build `MenuItem` actions that navigate to a URI or app.
                - **return value** – the new collection that will be rendered. You can re-order, filter, or append items freely.
                
                ### Usage Example
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var chromeSettings = ChromeSettings.Default()
                    .UseFooterMenuItemsTransformer((items, navigator) =>
                    {
                        // Convert to list for easier manipulation
                        var list = items.ToList();
                
                        // Append a static link at the end
                        list.Add(new MenuItem("Logout", _ => navigator.Navigate("app://logout"), Icons.Logout));
                
                        // Move "Settings" to the top of the footer
                        var settings = list.FirstOrDefault(i => i.Id == "app://settings");
                        if (settings != null)
                        {
                            list.Remove(settings);
                            list.Insert(0, settings);
                        }
                
                        return list;
                    });
                """",Languages.Csharp)
            | new Markdown(
                """"
                You can leverage a footer-menu items transformer to conditionally show or hide links, inject additional ones like "Docs", "Logout", or "Change theme", and rearrange or group items without having to update each individual app.
                
                ### Role-Based Filtering
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var chromeSettings = ChromeSettings.Default()
                    .UseFooterMenuItemsTransformer((items, navigator) =>
                    {
                        var user = AuthContext.CurrentUser;
                
                        // Hide admin-only links for non-admins
                        var filtered = items.Where(i =>
                            !i.Tags.Contains("admin") || user?.IsInRole("admin") == true);
                
                        return filtered;
                    });
                """",Languages.Csharp)
            | new Markdown(
                """"
                In this example we tag certain `MenuItem`s with the custom tag `admin` when generating them elsewhere. The transformer then checks the current user's roles (via your auth system) and removes admin-only links for non-admins.
                
                For more information about SideBar, check its [documentation](app://widgets/layouts/sidebar-layout)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Widgets.Layouts.SidebarLayoutApp), typeof(Onboarding.Concepts.NavigationApp), typeof(Onboarding.Concepts.LayoutApp), typeof(Widgets.Common.ButtonApp), typeof(Widgets.Primitives.TextBlockApp), typeof(Onboarding.Concepts.AppsApp), typeof(Onboarding.Concepts.ViewsApp)]; 
        return article;
    }
}

