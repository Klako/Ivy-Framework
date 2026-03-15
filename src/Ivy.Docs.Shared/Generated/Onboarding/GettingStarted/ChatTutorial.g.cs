using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.GettingStarted;

[App(order:6, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/01_GettingStarted/06_ChatTutorial.md", searchHints: ["tutorial", "ai", "chat", "openai", "example"])]
public class ChatTutorialApp(bool onlyBody = false) : ViewBase
{
    public ChatTutorialApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("chat-tutorial", "Chat Tutorial", 1), new ArticleHeading("prerequisites", "Prerequisites", 2), new ArticleHeading("create-the-chat-application", "Create the Chat Application", 2), new ArticleHeading("add-message-handling", "Add Message Handling", 2), new ArticleHeading("create-the-icon-agent", "Create the Icon Agent", 2), new ArticleHeading("how-it-works", "How It Works", 2), new ArticleHeading("try-it-out", "Try It Out", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Chat Tutorial").OnLinkClick(onLinkClick)
            | Lead("Create an AI-powered chat application that suggests [Lucide icons](app://widgets/primitives/icon) based on application descriptions using modern chat patterns.")
            | new Markdown(
                """"
                ## Prerequisites
                
                Before starting this tutorial, make sure you have:
                
                1. [Installed](app://onboarding/getting-started/installation) Ivy on your development machine
                2. An OpenAI API key set in your environment variables as `OPENAI_API_KEY`
                
                ## Create the Chat Application
                
                Let's create a new chat application that helps users find appropriate Lucide icons for their applications. We'll create a new file called `LucideIconAgentApp.cs` in your `Apps` folder.
                
                First, let's create the basic structure:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                [App(icon: Icons.Sparkles)]
                public class LucideIconAgentApp : SampleBase
                {
                    public LucideIconAgentApp() : base(Align.TopRight)
                    {
                    }
                
                    protected override object? BuildSample()
                    {
                        var client = UseService<IClientProvider>();
                
                        var messages = UseState(ImmutableArray.Create<ChatMessage>(new ChatMessage(ChatSender.Assistant,
                            "Hello! I'm the Lucide Icon Agent. I can help you find icons for your app. Please describe your application.")));
                
                        return new Chat(messages.Value.ToArray(), OnSend);
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Add Message Handling
                
                Now let's implement the message handling logic. We'll add the `OnSend` method that processes user input and generates icon suggestions:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                async ValueTask OnSend(Event<Chat, string> @event)
                {
                    messages.Set(messages.Value.Add(new ChatMessage(ChatSender.User, @event.Value)));
                    var currentMessages = messages.Value;
                    messages.Set(messages.Value.Add(new ChatMessage(ChatSender.Assistant, new ChatStatus("Thinking..."))));
                
                    var agent = new LucideIconAgent();
                    var suggestion = await agent.SuggestIconAsync(@event.Value);
                    if(suggestion != null)
                    {
                        var icons = suggestion.Split(';');
                        Icons[] iconEnums = icons.Select(icon => Enum.TryParse<Icons>(icon, out var result) ? result : Icons.None)
                            .Where(e => e != Icons.None).ToArray();
                
                        Action<Event<Button>> onIconClick = e =>
                        {
                            client.CopyToClipboard(e.Sender.Icon?.ToString() ?? "");
                            client.Toast($"Copied '{e.Sender.Icon?.ToString()}' to clipboard", "Icon Copied");
                        };
                
                        var content = Layout.Horizontal().Gap(1)
                            | iconEnums.Select(e => e.ToButton(onIconClick).WithTooltip(e.ToString()));
                
                        messages.Set(currentMessages.Add(new ChatMessage(ChatSender.Assistant, content)));
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Create the Icon Agent
                
                The `LucideIconAgent` class handles the AI-powered icon suggestions. Create this class in the same file:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class LucideIconAgent
                {
                    private readonly Kernel _kernel;
                
                    public LucideIconAgent()
                    {
                        var openAiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")!;
                        _kernel = Kernel.CreateBuilder().AddOpenAIChatCompletion("gpt-4o-2024-11-20", openAiKey)
                            .Build();
                    }
                
                    public async Task<string?> SuggestIconAsync(string appDescription)
                    {
                        var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
                        var history = new ChatHistory();
                        var allIcons = Enum.GetValues<Icons>().Where(e => e != Icons.None);
                
                        history.AddSystemMessage(
                            $"""
                            You are an expert on Lucide React.
                            User will submit a description of an application that is being built and you will suggest 7 icons from the Lucide React
                            library that are good idiomatic alternatives to recommend.
                            Answer with ; separated list of icon names.
                
                            Available icons in the Lucide React library:
                            ```
                            {string.Join("\n", allIcons.Select(e => e.ToString()).ToArray())}
                            ```
                
                            Do not use code blocks or any other markdown formatting. No explanation is needed.
                            """
                        );
                        history.AddUserMessage(appDescription);
                
                        var result = await chatCompletionService.GetChatMessageContentAsync(
                            history,
                            kernel: _kernel);
                
                        return result.Content;
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## How It Works
                
                1. The app starts with a welcome message asking users to describe their application
                2. When a user sends a message, it's added to the chat history
                3. The `LucideIconAgent` uses OpenAI's GPT-4 to analyze the description and suggest relevant Lucide icons
                4. The suggested icons are displayed as [clickable buttons](app://widgets/common/button)
                5. Clicking an icon copies its name to the clipboard and shows a [toast notification](app://onboarding/concepts/alerts)
                
                ## Try It Out
                
                You can now run the project and try it out! Describe your application, and the AI will suggest appropriate Lucide icons that you can use in your project.
                """").OnLinkClick(onLinkClick)
            | new Callout("Make sure you have set your OpenAI API key in the environment variables before running the project.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown("You can find the full source code for the project at [GitHub](https://github.com/Ivy-Interactive/Ivy-Framework/tree/main/Ivy.Samples/Apps/Demos/LucideIconAgentApp.cs).").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Widgets.Primitives.IconApp), typeof(Onboarding.GettingStarted.InstallationApp), typeof(Widgets.Common.ButtonApp), typeof(Onboarding.Concepts.AlertsApp)]; 
        return article;
    }
}

