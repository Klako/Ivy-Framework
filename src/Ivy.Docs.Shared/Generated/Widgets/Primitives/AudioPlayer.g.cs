using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:21, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/21_AudioPlayer.md", searchHints: ["sound", "playback", "media", "mp3", "music", "audio"])]
public class AudioPlayerApp(bool onlyBody = false) : ViewBase
{
    public AudioPlayerApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("audio-player", "Audio Player", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("configuration-options", "Configuration Options", 2), new ArticleHeading("autoplay-and-looping", "Autoplay and Looping", 3), new ArticleHeading("preload-strategy", "Preload Strategy", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Audio Player").OnLinkClick(onLinkClick)
            | Lead("Play audio content with browser controls. Supports common audio formats and provides customizable playback options.")
            | new Markdown(
                """"
                The `AudioPlayer` [widget](app://onboarding/concepts/widgets) displays an audio player with browser controls in your [app](app://onboarding/concepts/apps). This widget is for playing audio files, not recording them.
                
                ## Basic Usage
                
                Create a simple audio player:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3")
                    """",Languages.Csharp)
                | new Box().Content(new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3"))
            )
            | new Markdown(
                """"
                ## Configuration Options
                
                ### Autoplay and Looping
                
                Configure automatic playback and looping:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(4)
| Text.H4("Muted Autoplay (browsers allow this)")
| new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3")
    .Autoplay(true)
    .Muted(true)
| Text.H4("Looping Audio")
| new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3")
    .Loop(true))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(4)
                    | Text.H4("Muted Autoplay (browsers allow this)")
                    | new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3")
                        .Autoplay(true)
                        .Muted(true)
                    | Text.H4("Looping Audio")
                    | new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3")
                        .Loop(true)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Preload Strategy
                
                Control how much audio data is loaded:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(4)
| Text.P("Preload: None (no data loaded)").Small()
| new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3")
    .Preload(AudioPreload.None)
| Text.P("Preload: Metadata (duration and basic info)").Small()
| new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3")
    .Preload(AudioPreload.Metadata)
| Text.P("Preload: Auto (entire file)").Small()
| new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3")
    .Preload(AudioPreload.Auto))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(4)
                    | Text.P("Preload: None (no data loaded)").Small()
                    | new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3")
                        .Preload(AudioPreload.None)
                    | Text.P("Preload: Metadata (duration and basic info)").Small()
                    | new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3")
                        .Preload(AudioPreload.Metadata)
                    | Text.P("Preload: Auto (entire file)").Small()
                    | new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3")
                        .Preload(AudioPreload.Auto)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.AudioPlayer", "Ivy.AudioPlayerExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/AudioPlayer.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.AppsApp)]; 
        return article;
    }
}

