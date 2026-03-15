using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:3, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/03_Image.md", searchHints: ["picture", "photo", "img", "graphics", "media", "visual"])]
public class ImageApp(bool onlyBody = false) : ViewBase
{
    public ImageApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("image", "Image", 1), new ArticleHeading("supported-image-sources", "Supported Image Sources", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Image").OnLinkClick(onLinkClick)
            | Lead("Display images with automatic loading, responsive sizing, and proper accessibility features for rich visual content.")
            | new Markdown("The `Image` [widget](app://onboarding/concepts/widgets) displays images in your [app](app://onboarding/concepts/apps).").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Image("https://api.images.cat/150/150")
                    """",Languages.Csharp)
                | new Box().Content(new Image("https://api.images.cat/150/150"))
            )
            | new Markdown(
                """"
                ### Supported Image Sources
                
                The Image widget works with multiple source types:
                
                - **External URLs**: Images hosted on external servers
                - **Local files**: Images stored in your application's file system
                - **Data URIs**: Base64-encoded images embedded directly in the code
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var dataUri = "data:image/png;base64,iVBORw0KGgoAAAANS...";
                new Image(dataUri);
                
                new Image("https://example.com/image.jpg");  // External URL
                new Image("/ivy/images/logo.png");           // Local file
                """",Languages.Csharp)
            | new WidgetDocsView("Ivy.Image", "Ivy.ImageExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Image.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.AppsApp)]; 
        return article;
    }
}

