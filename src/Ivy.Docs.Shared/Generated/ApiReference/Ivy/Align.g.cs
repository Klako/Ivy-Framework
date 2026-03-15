using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.ApiReference.Ivy;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/04_ApiReference/Ivy/Align.md", searchHints: ["alignment", "positioning", "center", "justify", "layout", "vertical"])]
public class AlignApp(bool onlyBody = false) : ViewBase
{
    public AlignApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("align", "Align", 1), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # Align
                
                `Align` specifies alignment options for UI elements across the Ivy framework. It is commonly used for aligning content within [layouts](app://onboarding/concepts/layout) such as `Layout.Horizontal` or `Layout.Vertical`, and in [widgets](app://onboarding/concepts/widgets) like [Box](app://widgets/primitives/box) via `ContentAlign`.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new AlignView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class AlignView : ViewBase
                    {
                        public override object? Build()
                        {
                            var squareBox = new Box().Width(Size.Units(5)).Height(Size.Units(5));
                            var tallBox = new Box().Width(Size.Units(5)).Height(Size.Units(7));
                            var tallestBox = new Box().Width(Size.Units(5)).Height(Size.Units(10));
                            var wideBox = new Box().Width(Size.Units(7)).Height(Size.Units(5));
                            var widestBox = new Box().Width(Size.Units(10)).Height(Size.Units(5));
                    
                            var container = new Box().Width(Size.Units(32)).Height(Size.Units(32)).Background(Colors.Pink).Padding(0).ContentAlign(null);
                    
                            object AlignHorizontalTest(Align align) =>
                                container.Content(
                                    Layout.Horizontal().Align(align) | squareBox | tallBox | tallestBox
                                );
                    
                            object AlignVerticalTest(Align align) =>
                                container.Content(
                                    Layout.Vertical().Align(align).Height(Size.Full()) | squareBox | wideBox | widestBox
                                );
                    
                            var alignValues = (Align[])Enum.GetValues(typeof(Align));
                    
                            var header = new object[] { null!, Text.Monospaced("Layout.Vertical()"), Text.Monospaced("Layout.Horizontal()") };
                    
                            var values = alignValues.Select(e => new[]
                            {
                                Text.Monospaced("Align." + e),
                                AlignVerticalTest(e),
                                AlignHorizontalTest(e)
                            }).SelectMany(e => e).ToArray();
                    
                            return Layout.Grid().Columns(3)
                                   | (object[])[..header, ..values];
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.LayoutApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Widgets.Primitives.BoxApp)]; 
        return article;
    }
}


public class AlignView : ViewBase
{
    public override object? Build()
    {
        var squareBox = new Box().Width(Size.Units(5)).Height(Size.Units(5));
        var tallBox = new Box().Width(Size.Units(5)).Height(Size.Units(7));
        var tallestBox = new Box().Width(Size.Units(5)).Height(Size.Units(10));
        var wideBox = new Box().Width(Size.Units(7)).Height(Size.Units(5));
        var widestBox = new Box().Width(Size.Units(10)).Height(Size.Units(5));

        var container = new Box().Width(Size.Units(32)).Height(Size.Units(32)).Background(Colors.Pink).Padding(0).ContentAlign(null);

        object AlignHorizontalTest(Align align) =>
            container.Content(
                Layout.Horizontal().Align(align) | squareBox | tallBox | tallestBox
            );

        object AlignVerticalTest(Align align) =>
            container.Content(
                Layout.Vertical().Align(align).Height(Size.Full()) | squareBox | wideBox | widestBox
            );

        var alignValues = (Align[])Enum.GetValues(typeof(Align));

        var header = new object[] { null!, Text.Monospaced("Layout.Vertical()"), Text.Monospaced("Layout.Horizontal()") };

        var values = alignValues.Select(e => new[]
        {
            Text.Monospaced("Align." + e),
            AlignVerticalTest(e),
            AlignHorizontalTest(e)
        }).SelectMany(e => e).ToArray();

        return Layout.Grid().Columns(3)
               | (object[])[..header, ..values];
    }
}
