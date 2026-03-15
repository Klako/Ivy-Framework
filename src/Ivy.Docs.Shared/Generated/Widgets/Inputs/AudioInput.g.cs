using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Inputs;

[App(order:12, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/04_Inputs/12_AudioInput.md", searchHints: ["microphone", "recording", "voice", "audio", "capture", "sound"])]
public class AudioInputApp(bool onlyBody = false) : ViewBase
{
    public AudioInputApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("audio-input", "Audio Input", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("chunked-upload-streaming", "Chunked Upload (Streaming)", 3), new ArticleHeading("audio-format", "Audio Format", 2), new ArticleHeading("styling", "Styling", 2), new ArticleHeading("custom-labels", "Custom Labels", 3), new ArticleHeading("disabled-state", "Disabled State", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Audio Input").OnLinkClick(onLinkClick)
            | Lead("Enable audio recording with a flexible [interface](app://onboarding/concepts/views) for capturing user audio input with automatic upload support.")
            | new Markdown(
                """"
                The `AudioInput` [widget](app://onboarding/concepts/widgets) allows users to record audio using their microphone. It provides an audio recording interface with options for audio formats, automatic uploads, and chunked streaming. This widget is for recording audio, not playing it.
                
                ## Basic Usage
                
                Here's a simple example of an `AudioInput` that uploads audio to the server and stores it in [state](app://hooks/core/use-state):
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicAudioInputDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var audioFile = UseState<FileUpload<byte[]>?>();
                            var upload = UseUpload(
                                MemoryStreamUploadHandler.Create(audioFile),
                                defaultContentType: "audio/webm"
                            );
                    
                            return Layout.Vertical()
                                   | new AudioInput(upload.Value, "Start recording", "Recording audio...")
                                   | (audioFile.Value != null
                                       ? Text.P($"Recorded: {audioFile.Value.FileName} ({StringHelper.FormatBytes(audioFile.Value.Length)})")
                                       : null);
                       }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicAudioInputDemo())
            )
            | new Markdown(
                """"
                ### Chunked Upload (Streaming)
                
                Upload audio in chunks while recording. Use `ChunkedMemoryStreamUploadHandler` to accumulate chunks into a single file:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class ChunkedUploadDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            var audioFile = UseState<FileUpload<byte[]>?>();
                            var chunkCount = UseState(0);
                    
                            // Use ChunkedMemoryStreamUploadHandler to accumulate chunks into a single file
                            var upload = UseUpload(
                                ChunkedMemoryStreamUploadHandler.Create(audioFile),
                                defaultContentType: "audio/webm"
                            );
                    
                            return Layout.Vertical().Gap(4)
                                   | Text.P("Records audio and uploads in 2-second chunks while recording. Each chunk is accumulated into a single file.")
                                   | new AudioInput(upload.Value, "Start chunked recording", "Recording (uploading every 2s)...")
                                       .ChunkInterval(2000)
                                   | Text.P($"Chunks received: {chunkCount.Value}").Small()
                                   | (audioFile.Value != null
                                       ? Text.P($"Total accumulated: {StringHelper.FormatBytes(audioFile.Value.Length)}").Small()
                                       : null);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new ChunkedUploadDemo())
            )
            | new Callout("Use `MemoryStreamUploadHandler` for complete file uploads (uploads when recording stops) and `ChunkedMemoryStreamUploadHandler` for streaming uploads (uploads chunks during recording).", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Audio Format
                
                Specify the audio format using MIME type:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class AudioFormatDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var audioFile = UseState<FileUpload<byte[]>?>();
                    
                            // Use webm format (most compatible)
                            var upload = UseUpload(
                                MemoryStreamUploadHandler.Create(audioFile),
                                defaultContentType: "audio/webm"
                            );
                    
                            return Layout.Vertical()
                                   | new AudioInput(upload.Value, "Record WebM", "Recording WebM...")
                                       .MimeType("audio/webm")
                                   | (audioFile.Value != null
                                       ? Text.P($"Format: {audioFile.Value.ContentType}, Size: {StringHelper.FormatBytes(audioFile.Value.Length)}").Small()
                                       : null);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new AudioFormatDemo())
            )
            | new Callout("Use `audio/webm` for best browser compatibility. Other formats like `audio/mp4` or `audio/wav` may work depending on the browser.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Styling
                
                ### Custom Labels
                
                Customize the labels shown when idle and recording:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class CustomLabelsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var audioFile = UseState<FileUpload<byte[]>?>();
                            var upload = UseUpload(
                                MemoryStreamUploadHandler.Create(audioFile),
                                defaultContentType: "audio/webm"
                            );
                    
                            return Layout.Vertical()
                                   | new AudioInput(upload.Value)
                                       .Label("Click to start voice memo")
                                       .RecordingLabel("Recording your voice...")
                                   | (audioFile.Value != null
                                       ? audioFile.Value.ToDetails()
                                       : null);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new CustomLabelsDemo())
            )
            | new Markdown(
                """"
                ### Disabled State
                
                Disable the audio recorder:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class AudioInputDisabledDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var audioFile = UseState<FileUpload<byte[]>?>();
                            var upload = UseUpload(
                                MemoryStreamUploadHandler.Create(audioFile),
                                defaultContentType: "audio/webm"
                            );
                    
                            return new AudioInput(upload.Value, "Recording disabled", disabled: true);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new AudioInputDisabledDemo())
            )
            | new WidgetDocsView("Ivy.AudioInput", "Ivy.AudioInputExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/AudioInput.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Hooks.Core.UseStateApp)]; 
        return article;
    }
}


public class BasicAudioInputDemo : ViewBase
{
    public override object? Build()
    {
        var audioFile = UseState<FileUpload<byte[]>?>();
        var upload = UseUpload(
            MemoryStreamUploadHandler.Create(audioFile),
            defaultContentType: "audio/webm"
        );

        return Layout.Vertical()
               | new AudioInput(upload.Value, "Start recording", "Recording audio...")
               | (audioFile.Value != null
                   ? Text.P($"Recorded: {audioFile.Value.FileName} ({StringHelper.FormatBytes(audioFile.Value.Length)})")
                   : null);
   }
}

public class ChunkedUploadDemo : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var audioFile = UseState<FileUpload<byte[]>?>();
        var chunkCount = UseState(0);

        // Use ChunkedMemoryStreamUploadHandler to accumulate chunks into a single file
        var upload = UseUpload(
            ChunkedMemoryStreamUploadHandler.Create(audioFile),
            defaultContentType: "audio/webm"
        );

        return Layout.Vertical().Gap(4)
               | Text.P("Records audio and uploads in 2-second chunks while recording. Each chunk is accumulated into a single file.")
               | new AudioInput(upload.Value, "Start chunked recording", "Recording (uploading every 2s)...")
                   .ChunkInterval(2000)
               | Text.P($"Chunks received: {chunkCount.Value}").Small()
               | (audioFile.Value != null
                   ? Text.P($"Total accumulated: {StringHelper.FormatBytes(audioFile.Value.Length)}").Small()
                   : null);
    }
}

public class AudioFormatDemo : ViewBase
{
    public override object? Build()
    {
        var audioFile = UseState<FileUpload<byte[]>?>();

        // Use webm format (most compatible)
        var upload = UseUpload(
            MemoryStreamUploadHandler.Create(audioFile),
            defaultContentType: "audio/webm"
        );

        return Layout.Vertical()
               | new AudioInput(upload.Value, "Record WebM", "Recording WebM...")
                   .MimeType("audio/webm")
               | (audioFile.Value != null
                   ? Text.P($"Format: {audioFile.Value.ContentType}, Size: {StringHelper.FormatBytes(audioFile.Value.Length)}").Small()
                   : null);
    }
}

public class CustomLabelsDemo : ViewBase
{
    public override object? Build()
    {
        var audioFile = UseState<FileUpload<byte[]>?>();
        var upload = UseUpload(
            MemoryStreamUploadHandler.Create(audioFile),
            defaultContentType: "audio/webm"
        );

        return Layout.Vertical()
               | new AudioInput(upload.Value)
                   .Label("Click to start voice memo")
                   .RecordingLabel("Recording your voice...")
               | (audioFile.Value != null
                   ? audioFile.Value.ToDetails()
                   : null);
    }
}

public class AudioInputDisabledDemo : ViewBase
{
    public override object? Build()
    {
        var audioFile = UseState<FileUpload<byte[]>?>();
        var upload = UseUpload(
            MemoryStreamUploadHandler.Create(audioFile),
            defaultContentType: "audio/webm"
        );

        return new AudioInput(upload.Value, "Recording disabled", disabled: true);
    }
}
