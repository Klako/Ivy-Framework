using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:15, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/15_Volume.md", searchHints: ["storage", "files", "filesystem", "data", "paths", "volume"])]
public class VolumeApp(bool onlyBody = false) : ViewBase
{
    public VolumeApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("volume", "Volume", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("ivolume-interface", "IVolume Interface", 3), new ArticleHeading("overview", "Overview", 3), new ArticleHeading("foldervolume-implementation", "FolderVolume Implementation", 2), new ArticleHeading("constructor", "Constructor", 3), new ArticleHeading("path-structure", "Path Structure", 3), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # Volume
                
                Ivy provides a standardized way to manage [application](app://onboarding/concepts/apps) data storage through the `IVolume` interface and `FolderVolume` implementation. This ensures consistent file path handling and proper directory structure for your application's data files.
                
                ## Basic Usage
                
                Configure a volume for your application during [server](app://onboarding/concepts/program) startup:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                
                var server = new Server();
                
                // Configure a volume for your application
                var volume = new FolderVolume("/data/myapp");
                server.UseVolume(volume);
                
                await server.RunAsync();
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### IVolume Interface
                
                The `IVolume` interface defines the contract for volume implementations:
                
                The `GetAbsolutePath` method combines the volume root with the specified path parts and returns the absolute path. It automatically creates parent directories if they don't exist.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public interface IVolume
                {
                    public string GetAbsolutePath(params string[] parts);
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Overview
                
                The Volume Management system in Ivy provides:
                
                - **Automatic directory creation**: Parent directories are created automatically when you request a path
                - **Namespace isolation**: Files are organized under `Ivy/{YourAppName}/` to prevent conflicts between [applications](app://onboarding/concepts/apps)
                - **Fallback to local app data**: If the configured root directory doesn't exist, it falls back to the system's local application data folder
                - **Clean path composition**: Use params array for path parts instead of manual string concatenation
                - **Dependency injection support**: Volumes are registered as [services](app://hooks/core/use-service) and can be injected into your [views](app://onboarding/concepts/views)
                
                ## FolderVolume Implementation
                
                The `FolderVolume` class provides the standard implementation for file system volumes:
                
                ### Constructor
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("public class FolderVolume(string root) : IVolume",Languages.Csharp)
            | new Markdown(
                """"
                - **root**: The root directory path for the volume. If this directory doesn't exist, the volume will fall back to the system's local application data folder.
                
                ### Path Structure
                
                The `FolderVolume` creates paths in the following structure:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("{root}/Ivy/{YourAppName}/{pathParts}",Languages.Text)
            | new Markdown(
                """"
                Where:
                
                - `{root}` is the configured root directory or fallback location
                - `Ivy` is a fixed namespace prefix
                - `{YourAppName}` is automatically derived from your application's assembly name
                - `{pathParts}` are the path parts you provide to `GetAbsolutePath`
                
                ## Examples
                """").OnLinkClick(onLinkClick)
            | new Expandable("Using Volumes in Services",
                Vertical().Gap(4)
                | new Markdown("Inject and use the volume in your services:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    
                    public class FileService(IVolume volume)
                    {
                        public void SaveUserData(string userId, byte[] data)
                        {
                            // Automatically creates the full path: /data/myapp/Ivy/YourAppName/users/123/profile.json
                            var path = volume.GetAbsolutePath("users", userId, "profile.json");
                            File.WriteAllBytes(path, data);
                        }
                    
                        public byte[] LoadUserData(string userId)
                        {
                            var path = volume.GetAbsolutePath("users", userId, "profile.json");
                            return File.ReadAllBytes(path);
                        }
                    }
                    """",Languages.Csharp)
            )
            | new Expandable("Using Volumes in Views",
                Vertical().Gap(4)
                | new Markdown("Access volumes through [dependency injection](app://hooks/core/use-service) in your [views](app://views):").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    public class DataManagementView : ViewBase
                    {
                        public override object? Build()
                        {
                            var volume = UseService<IVolume>();
                    
                            return new Column
                            {
                                Children = [
                                    new Button("Save Data")
                                    {
                                        OnClick = () => SaveData(volume)
                                    },
                                    new Button("Load Data")
                                    {
                                        OnClick = () => LoadData(volume)
                                    }
                                ]
                            };
                        }
                    
                        private void SaveData(IVolume volume)
                        {
                            var data = Encoding.UTF8.GetBytes("Sample data");
                            var path = volume.GetAbsolutePath("cache", "sample.txt");
                            File.WriteAllBytes(path, data);
                        }
                    
                        private void LoadData(IVolume volume)
                        {
                            var path = volume.GetAbsolutePath("cache", "sample.txt");
                            if (File.Exists(path))
                            {
                                var data = File.ReadAllBytes(path);
                                // Process data...
                            }
                        }
                    }
                    """",Languages.Csharp)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.AppsApp), typeof(Onboarding.Concepts.ProgramApp), typeof(Hooks.Core.UseServiceApp), typeof(Onboarding.Concepts.ViewsApp)]; 
        return article;
    }
}

