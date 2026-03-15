using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.GettingStarted;

[App(order:3, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/01_GettingStarted/03_Basics.md", searchHints: ["fundamentals", "views", "widgets", "state", "beginner", "basics"])]
public class BasicsApp(bool onlyBody = false) : ViewBase
{
    public BasicsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = this.UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("basics", "Basics", 1), new ArticleHeading("create-a-new-project", "Create a new project", 2), new ArticleHeading("views-and-widgets", "Views and Widgets", 2), new ArticleHeading("state-management", "State Management", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Basics").OnLinkClick(onLinkClick)
            | Lead("Learn the essential concepts of Ivy development including [Views](app://onboarding/concepts/views), [Widgets](app://onboarding/concepts/widgets), [state management](app://hooks/core/use-state), and how to build your first interactive application.")
            | new Markdown(
                """"
                First, make sure you have [installed Ivy](app://onboarding/getting-started/installation) on your development machine.
                
                ## Create a new project
                
                Using the [CLI](app://onboarding/cli/cli-overview) we can easily create a new project.
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --namespace YourProjectNamespace")
                
            | new Markdown(
                """"
                ## Views and Widgets
                
                Now let's add our first Ivy app. In the folder `Apps` create a new file `CounterApp.cs` that inherits from [ViewBase](app://onboarding/concepts/views).
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                [App(icon: Icons.Box)]
                public class CounterApp : ViewBase
                {
                    public override object? Build()
                    {
                        return "HelloWorld";
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                Ivy is heavily inspired by React. A view is similar to a component in React and needs to implement a `Build` function that can return any `object` and Ivy will figure out how to render it (see [ContentBuilders](app://onboarding/concepts/content-builders)).
                
                The result from `Build` is usually another view or a widget. Widgets are the smallest building blocks in Ivy and are rendered on the client as a React component.
                
                Now let's make it a little more interesting by returning a button widget that shows a toast when clicked.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class CounterApp : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            return new Button("Click me", onClick: _ => client.Toast("Hello!"));
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new CounterApp())
            )
            | new Callout(
                """"
                These pages are implemented in Ivy so try to click on the button above. You should get a toast with the text "Hello!"
                """", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                In this example, you can see how to use an Ivy Button. For more details, check out the [Button implementation](app://widgets/common/button).
                
                ## State Management
                
                Ivy has a built-in state management system through the [UseState](app://hooks/core/use-state) hook (similar to React).
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                [App(icon: Icons.Box)]
                public class CounterApp : ViewBase
                {
                    public override object? Build()
                    {
                        var counter = UseState(0);
                        return Layout.Horizontal().Align(Align.Left)
                          | new Button("+1", onClick: _ => counter.Set(counter.Value+1))
                          | new Button("-1", onClick: _ => counter.Set(counter.Value-1))
                          | counter;
                    }
                }
                """",Languages.Csharp)
            | new Markdown("---").OnLinkClick(onLinkClick)
            | new Box().Content(new CounterApp1())
            | new Markdown("You can learn more about layouts [here](app://widgets/layouts/sidebar-layout). For more information about widgets, see [Widgets](app://onboarding/concepts/widgets).").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Hooks.Core.UseStateApp), typeof(Onboarding.GettingStarted.InstallationApp), typeof(Onboarding.CLI.CLIOverviewApp), typeof(Onboarding.Concepts.ContentBuildersApp), typeof(Widgets.Common.ButtonApp), typeof(Widgets.Layouts.SidebarLayoutApp)]; 
        return article;
    }
}


public class CounterApp : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        return new Button("Click me", onClick: _ => client.Toast("Hello!"));
    }
}

public class CounterApp1 : ViewBase
{
    public override object? Build()
    {
        var counter = UseState(0);
        return Layout.Horizontal().Align(Align.Left) | new Button("+1", onClick: _ => counter.Set(counter.Value + 1)) | new Button("-1", onClick: _ => counter.Set(counter.Value - 1)) | counter;
    }
}
