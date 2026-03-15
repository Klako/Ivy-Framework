using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.ApiReference.Ivy;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/04_ApiReference/Ivy/Colors.md", searchHints: ["colors", "palette", "theme", "styling", "appearance", "design"])]
public class ColorsApp(bool onlyBody = false) : ViewBase
{
    public ColorsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("colors", "Colors", 1), new ArticleHeading("all-colors", "All Colors", 3), new ArticleHeading("neutral-colors", "Neutral Colors", 3), new ArticleHeading("chromatic-colors", "Chromatic Colors", 3), new ArticleHeading("semantic-colors", "Semantic Colors", 3), new ArticleHeading("practical-examples", "Practical Examples", 2), new ArticleHeading("colors-on-different-backgrounds", "Colors on Different Backgrounds", 3), new ArticleHeading("common-usage-patterns", "Common Usage Patterns", 3), new ArticleHeading("status-indicators", "Status Indicators", 4), new ArticleHeading("button-colors", "Button Colors", 4), new ArticleHeading("best-practices", "Best Practices", 2), new ArticleHeading("technical-implementation", "Technical Implementation", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # Colors
                
                Ivy provides predefined colors with light/dark [theme](app://onboarding/concepts/theming) support.
                
                The system includes neutral (Black, White, grayscale), chromatic (Red to Rose spectrum), and semantic (Primary, Secondary, Destructive, Success, Warning, Info) colors.
                
                All colors meet WCAG accessibility standards and automatically adapt to light/dark themes. Use them with [widgets](app://onboarding/concepts/widgets) such as [Box](app://widgets/primitives/box) (`.Background()`, `.Background()`) and [Button](app://widgets/common/button) variants.
                
                ### All Colors
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new AllColorsView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class AllColorsView : ViewBase
                    {
                        public override object? Build()
                        {
                            Colors[] colors = Enum.GetValues<Colors>();
                    
                            return Layout.Vertical(
                                colors.Select(color =>
                                    new Box(color.ToString())
                                        .Width(Size.Auto())
                                        .Height(Size.Units(10))
                                        .Background(color).BorderRadius(BorderRadius.Rounded)
                                        .Padding(3)
                                        .ContentAlign(Align.Center)
                                )
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Neutral Colors").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new NeutralColorsView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class NeutralColorsView : ViewBase
                    {
                        public override object? Build()
                        {
                            var neutralColors = new Colors[] { Colors.Black, Colors.White, Colors.Slate, Colors.Gray, Colors.Zinc, Colors.Neutral, Colors.Stone };
                    
                            return Layout.Vertical(
                                neutralColors.Select(color =>
                                    new Box(color.ToString())
                                        .Width(Size.Auto())
                                        .Height(Size.Units(10))
                                        .Background(color).BorderRadius(BorderRadius.Rounded)
                                        .Padding(3)
                                        .ContentAlign(Align.Center)
                                )
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Chromatic Colors").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ChromaticColorsView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ChromaticColorsView : ViewBase
                    {
                        public override object? Build()
                        {
                            var chromaticColors = new Colors[] {
                                Colors.Red, Colors.Orange, Colors.Amber, Colors.Yellow, Colors.Lime, Colors.Green,
                                Colors.Emerald, Colors.Teal, Colors.Cyan, Colors.Sky, Colors.Blue, Colors.Indigo,
                                Colors.Violet, Colors.Purple, Colors.Fuchsia, Colors.Pink, Colors.Rose
                            };
                    
                            return Layout.Vertical(
                                chromaticColors.Select(color =>
                                    new Box(color.ToString())
                                        .Width(Size.Auto())
                                        .Height(Size.Units(10))
                                        .Background(color).BorderRadius(BorderRadius.Rounded)
                                        .Padding(3)
                                        .ContentAlign(Align.Center)
                                )
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Semantic Colors").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SemanticColorsView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SemanticColorsView : ViewBase
                    {
                        public override object? Build()
                        {
                            var semanticColors = new Colors[] { Colors.Primary, Colors.Secondary, Colors.Destructive, Colors.Success, Colors.Warning, Colors.Info };
                    
                            return Layout.Vertical(
                                semanticColors.Select(color =>
                                    new Box(color.ToString())
                                        .Width(Size.Auto())
                                        .Height(Size.Units(10))
                                        .Background(color).BorderRadius(BorderRadius.Rounded)
                                        .Padding(3)
                                        .ContentAlign(Align.Center)
                                )
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Practical Examples
                
                ### Colors on Different Backgrounds
                
                This example demonstrates how colors appear on both light and dark backgrounds:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ColorsOnBackgroundsView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ColorsOnBackgroundsView : ViewBase
                    {
                        public override object? Build()
                        {
                            Colors[] colors = Enum.GetValues<Colors>();
                    
                            object GenerateColors()
                            {
                                return Layout.Vertical(
                                    colors.Select(color =>
                                        new Box(color.ToString())
                                            .Width(Size.Auto())
                                            .Height(Size.Units(10))
                                            .Background(color).BorderRadius(BorderRadius.Rounded)
                                            .Padding(3)
                                            .ContentAlign(Align.Center)
                                    )
                                );
                            }
                    
                            var lightBackground = Layout.Vertical(
                                Text.Block("Light Background").Color(Colors.Black),
                                GenerateColors()
                            ).Padding(10);
                    
                            var darkBackground = Layout.Vertical(
                                Text.Block("Dark Background").Color(Colors.White),
                                GenerateColors()
                            ).Padding(10).Background(Colors.Black);
                    
                            return Layout.Grid().Columns(2)
                                | lightBackground
                                | darkBackground;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Common Usage Patterns
                
                #### Status Indicators
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new StatusIndicatorsView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class StatusIndicatorsView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical(
                                new Box("Success").Background(Colors.Success).Padding(5).BorderRadius(BorderRadius.Rounded).ContentAlign(Align.Center),
                                new Box("Warning").Background(Colors.Warning).Padding(5).BorderRadius(BorderRadius.Rounded).ContentAlign(Align.Center),
                                new Box("Error").Background(Colors.Destructive).Padding(5).BorderRadius(BorderRadius.Rounded).ContentAlign(Align.Center),
                                new Box("Info").Background(Colors.Info).Padding(5).BorderRadius(BorderRadius.Rounded).ContentAlign(Align.Center)
                            ).Gap(5);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("#### Button Colors").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ButtonColorsView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ButtonColorsView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical(
                                new Button("Primary Action").Variant(ButtonVariant.Primary),
                                new Button("Secondary Action").Variant(ButtonVariant.Secondary),
                                new Button("Destructive Action").Variant(ButtonVariant.Destructive)
                            ).Gap(10);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Best Practices
                
                1. **Use semantic colors** for consistent UI patterns (Primary, Secondary, Destructive, Success, Warning, Info)
                2. **Test on both backgrounds** to ensure proper contrast and readability
                3. **Consider color meaning** - use red/destructive for errors, green for success
                4. **Maintain consistency** - stick to a chosen color scheme throughout your project
                5. **Accessibility first** - ensure proper contrast ratios for text and backgrounds
                
                ## Technical Implementation
                
                Colors are defined as an enum in `Colors` and map to CSS custom properties that automatically adapt to the current theme. Use [Align](app://api-reference/ivy/align) with `.ContentAlign()` when centering content inside colored elements. Each color includes variants for different states and theme modes.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Get all available colors dynamically
                Colors[] colors = Enum.GetValues<Colors>();
                
                // Using colors with widgets
                new Box("Content")
                    .Background(Colors.Primary)
                    .Background(Colors.Secondary)
                    .ContentAlign(Align.Center);
                """",Languages.Csharp)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ThemingApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Widgets.Primitives.BoxApp), typeof(Widgets.Common.ButtonApp), typeof(ApiReference.Ivy.AlignApp)]; 
        return article;
    }
}


public class AllColorsView : ViewBase
{
    public override object? Build()
    {
        Colors[] colors = Enum.GetValues<Colors>();
        
        return Layout.Vertical(
            colors.Select(color =>
                new Box(color.ToString())
                    .Width(Size.Auto())
                    .Height(Size.Units(10))
                    .Background(color).BorderRadius(BorderRadius.Rounded)
                    .Padding(3)
                    .ContentAlign(Align.Center)
            )
        );
    }
}

public class NeutralColorsView : ViewBase
{
    public override object? Build()
    {
        var neutralColors = new Colors[] { Colors.Black, Colors.White, Colors.Slate, Colors.Gray, Colors.Zinc, Colors.Neutral, Colors.Stone };
        
        return Layout.Vertical(
            neutralColors.Select(color =>
                new Box(color.ToString())
                    .Width(Size.Auto())
                    .Height(Size.Units(10))
                    .Background(color).BorderRadius(BorderRadius.Rounded)
                    .Padding(3)
                    .ContentAlign(Align.Center)
            )
        );
    }
}

public class ChromaticColorsView : ViewBase
{
    public override object? Build()
    {
        var chromaticColors = new Colors[] { 
            Colors.Red, Colors.Orange, Colors.Amber, Colors.Yellow, Colors.Lime, Colors.Green, 
            Colors.Emerald, Colors.Teal, Colors.Cyan, Colors.Sky, Colors.Blue, Colors.Indigo, 
            Colors.Violet, Colors.Purple, Colors.Fuchsia, Colors.Pink, Colors.Rose 
        };
        
        return Layout.Vertical(
            chromaticColors.Select(color =>
                new Box(color.ToString())
                    .Width(Size.Auto())
                    .Height(Size.Units(10))
                    .Background(color).BorderRadius(BorderRadius.Rounded)
                    .Padding(3)
                    .ContentAlign(Align.Center)
            )
        );
    }
}

public class SemanticColorsView : ViewBase
{
    public override object? Build()
    {
        var semanticColors = new Colors[] { Colors.Primary, Colors.Secondary, Colors.Destructive, Colors.Success, Colors.Warning, Colors.Info };
        
        return Layout.Vertical(
            semanticColors.Select(color =>
                new Box(color.ToString())
                    .Width(Size.Auto())
                    .Height(Size.Units(10))
                    .Background(color).BorderRadius(BorderRadius.Rounded)
                    .Padding(3)
                    .ContentAlign(Align.Center)
            )
        );
    }
}

public class ColorsOnBackgroundsView : ViewBase
{
    public override object? Build()
    {
        Colors[] colors = Enum.GetValues<Colors>();

        object GenerateColors()
        {
            return Layout.Vertical(
                colors.Select(color =>
                    new Box(color.ToString())
                        .Width(Size.Auto())
                        .Height(Size.Units(10))
                        .Background(color).BorderRadius(BorderRadius.Rounded)
                        .Padding(3)
                        .ContentAlign(Align.Center)
                )
            );
        }

        var lightBackground = Layout.Vertical(
            Text.Block("Light Background").Color(Colors.Black),
            GenerateColors()
        ).Padding(10);

        var darkBackground = Layout.Vertical(
            Text.Block("Dark Background").Color(Colors.White),
            GenerateColors()
        ).Padding(10).Background(Colors.Black);

        return Layout.Grid().Columns(2)
            | lightBackground
            | darkBackground;
    }
}

public class StatusIndicatorsView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical(
            new Box("Success").Background(Colors.Success).Padding(5).BorderRadius(BorderRadius.Rounded).ContentAlign(Align.Center),
            new Box("Warning").Background(Colors.Warning).Padding(5).BorderRadius(BorderRadius.Rounded).ContentAlign(Align.Center),
            new Box("Error").Background(Colors.Destructive).Padding(5).BorderRadius(BorderRadius.Rounded).ContentAlign(Align.Center),
            new Box("Info").Background(Colors.Info).Padding(5).BorderRadius(BorderRadius.Rounded).ContentAlign(Align.Center)
        ).Gap(5);
    }
}

public class ButtonColorsView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical(
            new Button("Primary Action").Variant(ButtonVariant.Primary),
            new Button("Secondary Action").Variant(ButtonVariant.Secondary),
            new Button("Destructive Action").Variant(ButtonVariant.Destructive)
        ).Gap(10);
    }
}
