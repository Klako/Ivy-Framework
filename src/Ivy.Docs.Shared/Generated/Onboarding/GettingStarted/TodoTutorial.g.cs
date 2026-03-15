using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.GettingStarted;

[App(order:5, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/01_GettingStarted/05_TodoTutorial.md", searchHints: ["tutorial", "example", "todo", "walkthrough", "guide", "step-by-step"])]
public class TodoTutorialApp(bool onlyBody = false) : ViewBase
{
    public TodoTutorialApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("todo-tutorial", "Todo Tutorial", 1), new ArticleHeading("prerequisites", "Prerequisites", 2), new ArticleHeading("creating-the-todo-app", "Creating the Todo App", 2), new ArticleHeading("1-create-a-new-project", "1. Create a new project", 3), new ArticleHeading("2-create-the-todo-model", "2. Create the Todo Model", 3), new ArticleHeading("3-create-the-main-app-class", "3. Create the Main App Class", 3), new ArticleHeading("4-add-state-management", "4. Add State Management", 3), new ArticleHeading("5-build-the-ui", "5. Build the UI", 3), new ArticleHeading("6-create-the-todoitem-component", "6. Create the TodoItem Component", 3), new ArticleHeading("7-run", "7. Run", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Todo Tutorial").OnLinkClick(onLinkClick)
            | Lead("Build a complete todo application from scratch to learn essential Ivy concepts including [state management](app://hooks/core/use-state), [components](app://onboarding/concepts/widgets), and [event handling](app://onboarding/concepts/event-handlers).")
            | new Markdown(
                """"
                ## Prerequisites
                
                Before starting this tutorial, make sure you have [installed](app://onboarding/getting-started/installation) Ivy.
                
                ## Creating the Todo App
                
                Let's create a new todo app step by step.
                
                ### 1. Create a new project
                
                Using the Ivy [CLI](app://onboarding/cli/cli-overview) we can create a new project.
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --namespace Todos")
                
            | new Markdown(
                """"
                ### 2. Create the Todo Model
                
                Create a new file `TodosApp.cs` in the `Apps` folder.
                Declare a record `Todo.cs` to represent our todo items:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("public record Todo(string Title, bool Done);",Languages.Csharp)
            | new Markdown(
                """"
                ### 3. Create the Main App Class
                
                Create a new class `TodosApp` in the file that inherits from [ViewBase](app://onboarding/concepts/views):
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                [App(icon: Icons.Calendar)]
                public class TodosApp : ViewBase
                {
                    public override object? Build()
                    {
                        // We'll add the implementation here
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### 4. Add State Management
                
                Inside the `Build` method, we'll add state management for our todos and input field:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                //State for the input field where users type new todo titles
                var newTitle = UseState("");
                //State for storing the list of todo items
                var todos = UseState(ImmutableArray.Create<Todo>());
                
                //Service for showing toast notifications
                var client = UseService<IClientProvider>();
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### 5. Build the UI
                
                Now let's create the user interface. We'll use Ivy's [layout system](app://widgets/layouts/_index) and components:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                return new Card().Title("Todos").Description("What do you want to get done today?")
                   | (Layout.Vertical()
                       | (Layout.Horizontal().Width(Size.Full())
                          | newTitle.ToTextInput(placeholder: "New Task...").Width(Size.Grow())
                          | new Button("Add", onClick: _ =>
                              {
                                  var title = newTitle.Value;
                                  todos.Set(todos.Value.Add(new Todo(title, false)));
                                  client.Toast($"New '{title}' todo added.", "Todos");
                                  newTitle.Set("");
                              }
                          ).Icon(Icons.Plus).Variant(ButtonVariant.Primary)
                       )
                       | (Layout.Vertical()
                          | todos.Value.Select(todo => new TodoItem(todo,
                              () =>
                              {
                                  todos.Set(todos.Value.Remove(todo));
                              },
                              () =>
                              {
                                  todos.Set(todos.Value.Replace(todo, todo with
                                  {
                                      Done = !todo.Done
                                  }));
                              }
                          ))
                       ))
                    ;
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### 6. Create the TodoItem Component
                
                Create a new class `TodoItem.cs` for the todo item view:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class TodoItem(Todo todo, Action deleteTodo, Action toggleTodo) : ViewBase
                {
                    public override object? Build()
                    {
                        return Layout.Vertical()
                           | (Layout.Horizontal().Align(Align.Center).Width(Size.Full())
                              | new BoolInput<bool>(todo.Done, _ =>
                              {
                                  toggleTodo();
                              })
                              | (todo.Done
                                  ? Text.Muted(todo.Title).StrikeThrough().Width(Size.Grow())
                                  : Text.Literal(todo.Title).Width(Size.Grow()))
                              | new Button(null, _ =>
                                  {
                                      deleteTodo();
                                  }
                              ).Icon(Icons.Trash).Variant(ButtonVariant.Outline)
                           )
                           | new Separator()
                        ;
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### 7. Run
                
                Now let's run the project.
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy run")
                
            | new Markdown("You can find the source code for the app at [GitHub](https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/Ivy.Samples.Shared/Apps/Demos/TodosApp.cs).").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Hooks.Core.UseStateApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.EventHandlersApp), typeof(Onboarding.GettingStarted.InstallationApp), typeof(Onboarding.CLI.CLIOverviewApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Widgets.Layouts._IndexApp)]; 
        return article;
    }
}

