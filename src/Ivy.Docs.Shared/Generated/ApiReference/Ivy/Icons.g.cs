using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.ApiReference.Ivy;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/04_ApiReference/Ivy/Icons.md", searchHints: ["icons", "lucide", "symbols", "graphics", "glyphs", "svg"])]
public class IconsApp(bool onlyBody = false) : ViewBase
{
    public IconsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("icons", "Icons", 1), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # Icons
                
                Ivy uses the [Lucide](https://lucide.dev/icons/) icon library. Icons are used in [views](app://onboarding/concepts/views) with [state](app://hooks/core/use-state) and [effects](app://hooks/core/use-effect) for search; use [UseService](app://hooks/core/use-service) for clipboard and toasts.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SearchIconsView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SearchIconsView : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            var searchState = UseState("code");
                            var iconsState = UseState<Icons[]>(Array.Empty<Icons>());
                    
                            UseEffect(() =>
                            {
                                var allIcons = Enum.GetValues<Icons>().Where(e => e != Icons.None);
                                iconsState.Set(string.IsNullOrEmpty(searchState.Value)
                                    ? []
                                    : allIcons.Where(e => e.ToString().Contains(searchState.Value, StringComparison.OrdinalIgnoreCase)).Take(10).ToArray());
                            }, [ EffectTrigger.OnMount(), searchState.Throttle(TimeSpan.FromMilliseconds(500)).ToTrigger() ]);
                    
                            var searchInput = searchState.ToSearchInput().Placeholder("Type a icon name");
                    
                            var icons  = iconsState.Value.Select(e => Layout.Horizontal().Gap(2)
                                | new Button(null, @event =>
                                {
                                    var iconCode = "Icons." + e.ToString();
                                    client.CopyToClipboard(iconCode);
                                    client.Toast($"Copied '{iconCode}' to clipboard", "Icon Code Copied");
                                }, ButtonVariant.Ghost, e).Small().WithTooltip($"Click to copy {e.ToString()}")
                                | Text.Label("Icons." + e.ToString())
                                );
                    
                            return Layout.Vertical()
                                   | searchInput
                                   | icons
                                ;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Hooks.Core.UseStateApp), typeof(Hooks.Core.UseEffectApp), typeof(Hooks.Core.UseServiceApp)]; 
        return article;
    }
}


public class SearchIconsView : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var searchState = UseState("code");
        var iconsState = UseState<Icons[]>(Array.Empty<Icons>());
        
        UseEffect(() =>
        {
            var allIcons = Enum.GetValues<Icons>().Where(e => e != Icons.None);
            iconsState.Set(string.IsNullOrEmpty(searchState.Value)
                ? []
                : allIcons.Where(e => e.ToString().Contains(searchState.Value, StringComparison.OrdinalIgnoreCase)).Take(10).ToArray());
        }, [ EffectTrigger.OnMount(), searchState.Throttle(TimeSpan.FromMilliseconds(500)).ToTrigger() ]);
        
        var searchInput = searchState.ToSearchInput().Placeholder("Type a icon name");
        
        var icons  = iconsState.Value.Select(e => Layout.Horizontal().Gap(2)
            | new Button(null, @event =>
            {
                var iconCode = "Icons." + e.ToString();
                client.CopyToClipboard(iconCode);
                client.Toast($"Copied '{iconCode}' to clipboard", "Icon Code Copied");
            }, ButtonVariant.Ghost, e).Small().WithTooltip($"Click to copy {e.ToString()}")
            | Text.Label("Icons." + e.ToString())
            );

        return Layout.Vertical()
               | searchInput
               | icons
            ; 
    }
}
