using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:22, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/22_VideoPlayer.md", searchHints: ["media", "youtube", "playback", "video", "streaming", "embed"])]
public class VideoPlayerApp(bool onlyBody = false) : ViewBase
{
    public VideoPlayerApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("video-player", "Video Player", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("configuration-options", "Configuration Options", 2), new ArticleHeading("autoplay-muted-and-looping", "Autoplay, Muted, and Looping", 3), new ArticleHeading("controls-toggle", "Controls Toggle", 3), new ArticleHeading("custom-sizing", "Custom Sizing", 3), new ArticleHeading("poster-image-preview-frame", "Poster Image (Preview Frame)", 3), new ArticleHeading("large-video-files", "Large Video Files", 3), new ArticleHeading("youtube-video-embed", "YouTube Video Embed", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Video Player").OnLinkClick(onLinkClick)
            | Lead("Play video content with browser controls. Supports common video formats (e.g., MP4, WebM) and provides customizable playback options.")
            | new Markdown(
                """"
                The `VideoPlayer` [widget](app://onboarding/concepts/widgets) displays a video player with browser-native controls in your [app](app://onboarding/concepts/apps). This widget is for playing video files.
                
                ## Basic Usage
                
                Create a simple video player:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
                    """",Languages.Csharp)
                | new Box().Content(new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4"))
            )
            | new Markdown(
                """"
                ## Configuration Options
                
                ### Autoplay, Muted, and Looping
                
                Configure automatic playback and looping (muted autoplay is more likely to be allowed by browsers):
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(4)
| Text.H4("Muted Autoplay Video")
| new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
    .Autoplay(true)
    .Muted(true)
| Text.H4("Looping Video")
| new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
    .Loop(true))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(4)
                    | Text.H4("Muted Autoplay Video")
                    | new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
                        .Autoplay(true)
                        .Muted(true)
                    | Text.H4("Looping Video")
                    | new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
                        .Loop(true)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Controls Toggle
                
                Enable or disable browser playback controls:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(4)
| Text.P("With Controls (default)").Small()
| new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
    .Controls(true)
| Text.P("Without Controls (programmatic control only)").Small()
| new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
    .Controls(false))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(4)
                    | Text.P("With Controls (default)").Small()
                    | new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
                        .Controls(true)
                    | Text.P("Without Controls (programmatic control only)").Small()
                    | new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
                        .Controls(false)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Custom Sizing
                
                Control width and height of the video player:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(4)
| Text.P("50% width, fixed height").Small()
| new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
    .Width(Size.Fraction(0.5f))
    .Height(Size.Units(50)))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(4)
                    | Text.P("50% width, fixed height").Small()
                    | new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
                        .Width(Size.Fraction(0.5f))
                        .Height(Size.Units(50))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Poster Image (Preview Frame)
                
                Display a placeholder image before playback:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
    .Poster("https://www.w3schools.com/html/pic_trulli.jpg"))),
                new Tab("Code", new CodeBlock(
                    """"
                    new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
                        .Poster("https://www.w3schools.com/html/pic_trulli.jpg")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Large Video Files
                
                The VideoPlayer also supports streaming of large video files.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new VideoPlayer("https://archive.org/download/BigBuckBunny_124/Content/big_buck_bunny_720p_surround.mp4")
    .Height(Size.Units(100)))),
                new Tab("Code", new CodeBlock(
                    """"
                    new VideoPlayer("https://archive.org/download/BigBuckBunny_124/Content/big_buck_bunny_720p_surround.mp4")
                        .Height(Size.Units(100))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### YouTube Video Embed
                
                Embed a YouTube video directly by providing its URL:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new VideoPlayer("https://www.youtube.com/watch?v=dQw4w9WgXcQ")
    .Height(Size.Units(100))
    .Controls(false))),
                new Tab("Code", new CodeBlock(
                    """"
                    new VideoPlayer("https://www.youtube.com/watch?v=dQw4w9WgXcQ")
                        .Height(Size.Units(100))
                        .Controls(false)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.VideoPlayer", "Ivy.VideoPlayerExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/VideoPlayer.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.AppsApp)]; 
        return article;
    }
}

