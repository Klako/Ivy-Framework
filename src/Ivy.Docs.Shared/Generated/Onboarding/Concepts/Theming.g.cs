using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:12, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/12_Theming.md", searchHints: ["theme", "styling", "colors", "dark-mode", "customization", "branding"])]
public class ThemingApp(bool onlyBody = false) : ViewBase
{
    public ThemingApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("theming", "Theming", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("theme-modes", "Theme Modes", 2), new ArticleHeading("options", "Options", 3), new ArticleHeading("custom-themes", "Custom Themes", 2), new ArticleHeading("server-configuration-experimental", "Server Configuration (Experimental)", 3), new ArticleHeading("runtime-theme-changes", "Runtime Theme Changes", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Theming").OnLinkClick(onLinkClick)
            | Lead("Customize your Ivy [application's](app://onboarding/concepts/apps) visual appearance with flexible theming support including light/dark modes, custom color schemes, typography, and dynamic theme switching.")
            | new Markdown(
                """"
                ## Overview
                
                Ivy's theming system provides:
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                graph TD
                    A["Theming"] --> B["Theme Modes"]
                    A --> C["Custom Themes"]
                    A --> D["Dynamic CSS"]
                
                    B --> B1["Light/Dark/System"]
                    C --> C1["Colors & Typography"]
                    D --> D2["Runtime Updates"]
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Theme Modes
                
                Control basic light/dark appearance:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                [App(icon: Icons.Palette)]
                public class ThemeSwitcher : ViewBase
                {
                    public override object? Build()
                    {
                        var client = UseService<IClientProvider>();
                
                        return Layout.Vertical()
                            | new Button("Light")
                            {
                                OnClick = _ =>
                                {
                                    client.SetThemeMode(ThemeMode.Light);
                                    return ValueTask.CompletedTask;
                                }
                            }
                            | new Button("Dark")
                            {
                                OnClick = _ =>
                                {
                                    client.SetThemeMode(ThemeMode.Dark);
                                    return ValueTask.CompletedTask;
                                }
                            }
                            | new Button("System")
                            {
                                OnClick = _ =>
                                {
                                    client.SetThemeMode(ThemeMode.System);
                                    return ValueTask.CompletedTask;
                                }
                            };
                    }
                }
                """",Languages.Csharp)
            | new Markdown("### Options").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                graph LR
                    A["ThemeMode"] --> B["Light"]
                    A --> C["Dark"]
                    A --> D["System"]
                
                    B --> B1["Bright backgrounds"]
                    C --> C1["Dark backgrounds"]
                    D --> D1["OS preference"]
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Custom Themes
                
                ### Server Configuration (Experimental)
                
                The `UseTheme()` method is available for [server](app://onboarding/concepts/program)-level theme configuration
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var server = new Server()
                    .UseTheme(theme => {
                        theme.Name = "Ocean";
                        theme.Colors = new ThemeColorScheme
                        {
                            Light = new ThemeColors
                            {
                                Primary = "#0077BE",
                                PrimaryForeground = "#FFFFFF",
                                Secondary = "#5B9BD5",
                                SecondaryForeground = "#FFFFFF",
                                Background = "#F0F8FF",
                                Foreground = "#1A1A1A",
                                Destructive = "#DC143C",
                                DestructiveForeground = "#FFFFFF",
                                Success = "#20B2AA",
                                SuccessForeground = "#FFFFFF",
                                Warning = "#FFD700",
                                WarningForeground = "#1A1A1A",
                                Info = "#4682B4",
                                InfoForeground = "#FFFFFF",
                                Border = "#B0C4DE",
                                Input = "#E6F2FF",
                                Ring = "#0077BE",
                                Muted = "#E0E8F0",
                                MutedForeground = "#5A6A7A",
                                Accent = "#87CEEB",
                                AccentForeground = "#1A1A1A",
                                Card = "#FFFFFF",
                                CardForeground = "#1A1A1A",
                                Popover = "#FFFFFF",
                                PopoverForeground = "#1A1A1A"
                            },
                            Dark = new ThemeColors
                            {
                                Primary = "#4A9EFF",
                                PrimaryForeground = "#001122",
                                Secondary = "#2D4F70",
                                SecondaryForeground = "#E8F4FD",
                                Background = "#001122",
                                Foreground = "#E8F4FD",
                                Destructive = "#FF6B7D",
                                DestructiveForeground = "#FFFFFF",
                                Success = "#4ECDC4",
                                SuccessForeground = "#001122",
                                Warning = "#FFE066",
                                WarningForeground = "#001122",
                                Info = "#87CEEB",
                                InfoForeground = "#001122",
                                Border = "#1A3A5C",
                                Input = "#0F2A4A",
                                Ring = "#4A9EFF",
                                Muted = "#0F2A4A",
                                MutedForeground = "#8BB3D9",
                                Accent = "#1A3A5C",
                                AccentForeground = "#E8F4FD",
                                Card = "#0F2A4A",
                                CardForeground = "#E8F4FD",
                                Popover = "#001122",
                                PopoverForeground = "#E8F4FD"
                            }
                        };
                    });
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Runtime Theme Changes
                
                Use [UseService](app://hooks/core/use-service) to get `IThemeService` and modify themes dynamically. The `ApplyTheme()` method applies CSS custom properties generated from the theme:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                [App(icon: Icons.Brush)]
                public class ThemeCustomizer : ViewBase
                {
                    public override object? Build()
                    {
                        var themeService = UseService<IThemeService>();
                        var client = UseService<IClientProvider>();
                
                        void ApplyCustomTheme()
                        {
                            var customTheme = new Theme
                            {
                                Name = "Ocean Blue",
                                Colors = new ThemeColorScheme
                                {
                                    Light = new ThemeColors
                                    {
                                        Primary = "#0077be",
                                        Background = "#ffffff",
                                        Foreground = "#1a1a1a"
                                    },
                                    Dark = new ThemeColors
                                    {
                                        Primary = "#0099ff",
                                        Background = "#0d1117",
                                        Foreground = "#ffffff"
                                    }
                                }
                            };
                
                            // Set the theme and generate CSS custom properties
                            themeService.SetTheme(customTheme);
                            var css = themeService.GenerateThemeCss();
                
                            // Apply the generated CSS variables to the frontend
                            client.ApplyTheme(css);
                        }
                
                        return new Button("Apply Ocean Theme")
                        {
                            OnClick = _ =>
                            {
                                ApplyCustomTheme();
                                return ValueTask.CompletedTask;
                            }
                        };
                    }
                }
                """",Languages.Csharp)
            | new Callout("`ApplyTheme()` only accepts CSS generated by `ThemeService.GenerateThemeCss()`. It does not support arbitrary custom CSS injection.", icon:Icons.Info).OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.AppsApp), typeof(Onboarding.Concepts.ProgramApp), typeof(Hooks.Core.UseServiceApp)]; 
        return article;
    }
}

