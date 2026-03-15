using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Inputs;

[App(order:11, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/04_Inputs/11_CodeInput.md", searchHints: ["editor", "syntax", "programming", "monaco", "highlighting", "code"])]
public class CodeInputApp(bool onlyBody = false) : ViewBase
{
    public CodeInputApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("codeinput", "CodeInput", 1), new ArticleHeading("supported-languages", "Supported Languages", 2), new ArticleHeading("c", "C", 3), new ArticleHeading("javascript", "Javascript", 3), new ArticleHeading("python", "Python", 3), new ArticleHeading("sql", "SQL", 3), new ArticleHeading("html", "HTML", 3), new ArticleHeading("css", "CSS", 3), new ArticleHeading("json", "Json", 3), new ArticleHeading("dbml", "DBML", 3), new ArticleHeading("typescript", "Typescript", 3), new ArticleHeading("yaml", "YAML", 3), new ArticleHeading("plain-text", "Plain Text", 3), new ArticleHeading("styling-options", "Styling Options", 2), new ArticleHeading("invalid-state", "Invalid State", 3), new ArticleHeading("disabled-state", "Disabled State", 3), new ArticleHeading("event-handling", "Event Handling", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# CodeInput").OnLinkClick(onLinkClick)
            | Lead("Edit code with syntax highlighting, line numbers, and formatting support for multiple programming languages in a specialized input [field](app://widgets/inputs/field). Use [Size](app://api-reference/ivy/size) for `.Width()` and `.Height()` to control dimensions.")
            | new Markdown(
                """"
                The `CodeInput` [widget](app://onboarding/concepts/widgets) provides a specialized text input field for entering and editing code with syntax highlighting.
                It supports various programming languages and offers features like line numbers and code formatting.
                
                ## Supported Languages
                
                ### C
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(UseState("using System;\n\npublic class Program\n{\n    static void Main()\n    {\n        Console.WriteLine(\"Hello, World!\");\n    }\n}")
    .ToCodeInput()
    .Width(Size.Full())
    .Height(Size.Auto())
    .Language(Languages.Csharp))),
                new Tab("Code", new CodeBlock(
                    """"
                    UseState("using System;\n\npublic class Program\n{\n    static void Main()\n    {\n        Console.WriteLine(\"Hello, World!\");\n    }\n}")
                        .ToCodeInput()
                        .Width(Size.Full())
                        .Height(Size.Auto())
                        .Language(Languages.Csharp)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Javascript").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(UseState("function greet(name) {\n  console.log(`Hello, ${name}!`);\n}\ngreet('World');")
    .ToCodeInput()
    .Width(Size.Full())
    .Height(Size.Auto())
    .Language(Languages.Javascript))),
                new Tab("Code", new CodeBlock(
                    """"
                    UseState("function greet(name) {\n  console.log(`Hello, ${name}!`);\n}\ngreet('World');")
                        .ToCodeInput()
                        .Width(Size.Full())
                        .Height(Size.Auto())
                        .Language(Languages.Javascript)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Python").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(UseState("def greet(name):\n    print(f'Hello, {name}!')\n\ngreet('World')")
    .ToCodeInput()
    .Width(Size.Full())
    .Height(Size.Auto())
    .Language(Languages.Python))),
                new Tab("Code", new CodeBlock(
                    """"
                    UseState("def greet(name):\n    print(f'Hello, {name}!')\n\ngreet('World')")
                        .ToCodeInput()
                        .Width(Size.Full())
                        .Height(Size.Auto())
                        .Language(Languages.Python)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### SQL").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(UseState("SELECT u.name, u.email, p.title\nFROM users u\nJOIN posts p ON u.id = p.user_id\nWHERE u.active = true\nORDER BY p.created_at DESC;")
    .ToCodeInput()
    .Width(Size.Full())
    .Height(Size.Auto())
    .Language(Languages.Sql))),
                new Tab("Code", new CodeBlock(
                    """"
                    UseState("SELECT u.name, u.email, p.title\nFROM users u\nJOIN posts p ON u.id = p.user_id\nWHERE u.active = true\nORDER BY p.created_at DESC;")
                        .ToCodeInput()
                        .Width(Size.Full())
                        .Height(Size.Auto())
                        .Language(Languages.Sql)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### HTML").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(UseState("<html>\n<body>\n  <h1>Hello World!</h1>\n</body>\n</html>")
    .ToCodeInput()
    .Width(Size.Full())
    .Height(Size.Auto())
    .Language(Languages.Html))),
                new Tab("Code", new CodeBlock(
                    """"
                    UseState("<html>\n<body>\n  <h1>Hello World!</h1>\n</body>\n</html>")
                        .ToCodeInput()
                        .Width(Size.Full())
                        .Height(Size.Auto())
                        .Language(Languages.Html)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### CSS").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(UseState("body {\n  font-family: Arial, sans-serif;\n  color: #333;\n}")
    .ToCodeInput()
    .Width(Size.Full())
    .Height(Size.Auto())
    .Language(Languages.Css))),
                new Tab("Code", new CodeBlock(
                    """"
                    UseState("body {\n  font-family: Arial, sans-serif;\n  color: #333;\n}")
                        .ToCodeInput()
                        .Width(Size.Full())
                        .Height(Size.Auto())
                        .Language(Languages.Css)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Json").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(UseState("{\n  \"name\": \"Ivy\",\n  \"version\": \"1.0.0\",\n  \"features\": [\"syntax highlighting\", \"auto-complete\"],\n  \"config\": {\n    \"theme\": \"dark\",\n    \"fontSize\": 14\n  }\n}")
    .ToCodeInput()
    .Width(Size.Full())
    .Height(Size.Auto())
    .Language(Languages.Json))),
                new Tab("Code", new CodeBlock(
                    """"
                    UseState("{\n  \"name\": \"Ivy\",\n  \"version\": \"1.0.0\",\n  \"features\": [\"syntax highlighting\", \"auto-complete\"],\n  \"config\": {\n    \"theme\": \"dark\",\n    \"fontSize\": 14\n  }\n}")
                        .ToCodeInput()
                        .Width(Size.Full())
                        .Height(Size.Auto())
                        .Language(Languages.Json)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### DBML").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(UseState("Table users {\n  id integer [primary key]\n  username varchar\n  role varchar\n  created_at timestamp\n}")
    .ToCodeInput()
    .Width(Size.Full())
    .Height(Size.Auto())
    .Language(Languages.Dbml))),
                new Tab("Code", new CodeBlock(
                    """"
                    UseState("Table users {\n  id integer [primary key]\n  username varchar\n  role varchar\n  created_at timestamp\n}")
                        .ToCodeInput()
                        .Width(Size.Full())
                        .Height(Size.Auto())
                        .Language(Languages.Dbml)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Typescript").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(UseState("interface User {\n  name: string;\n  age: number;\n}\n\nconst user: User = { name: 'John', age: 30 };")
    .ToCodeInput()
    .Width(Size.Full())
    .Height(Size.Auto())
    .Language(Languages.Typescript))),
                new Tab("Code", new CodeBlock(
                    """"
                    UseState("interface User {\n  name: string;\n  age: number;\n}\n\nconst user: User = { name: 'John', age: 30 };")
                        .ToCodeInput()
                        .Width(Size.Full())
                        .Height(Size.Auto())
                        .Language(Languages.Typescript)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### YAML").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(UseState("name: my-app\nversion: 1.0.0\nservices:\n  web:\n    image: nginx:latest\n    ports:\n      - \"80:80\"")
    .ToCodeInput()
    .Width(Size.Full())
    .Height(Size.Auto())
    .Language(Languages.Yaml))),
                new Tab("Code", new CodeBlock(
                    """"
                    UseState("name: my-app\nversion: 1.0.0\nservices:\n  web:\n    image: nginx:latest\n    ports:\n      - \"80:80\"")
                        .ToCodeInput()
                        .Width(Size.Full())
                        .Height(Size.Auto())
                        .Language(Languages.Yaml)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Plain Text").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(UseState("Here is some plain text, with no syntax highlighting whatsoever.\nUnlike the TextInput widget, this uses a monospaced font, which\nmakes some types of text easier to read. For example:\n\n  +----------------------------+\n  |                            |\n  |       ASCII Diagrams       |\n  |                            |\n  +----------------------------+")
    .ToCodeInput()
    .Width(Size.Full())
    .Height(Size.Auto())
    .Language(Languages.Text))),
                new Tab("Code", new CodeBlock(
                    """"
                    UseState("Here is some plain text, with no syntax highlighting whatsoever.\nUnlike the TextInput widget, this uses a monospaced font, which\nmakes some types of text easier to read. For example:\n\n  +----------------------------+\n  |                            |\n  |       ASCII Diagrams       |\n  |                            |\n  +----------------------------+")
                        .ToCodeInput()
                        .Width(Size.Full())
                        .Height(Size.Auto())
                        .Language(Languages.Text)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Styling Options
                
                ### Invalid State
                
                The `Invalid` state provides visual feedback when code contains syntax errors or validation issues. It displays an error message and typically shows a red border to indicate problems.
                
                Mark a `CodeInput` as invalid when content has syntax errors:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(UseState("function greet(name) {\n    console.log('Hello, ' + name);\n    return 'Welcome ' + name;\n}")
    .ToCodeInput()
    .Language(Languages.Javascript)
    .Invalid("Missing closing parenthesis!"))),
                new Tab("Code", new CodeBlock(
                    """"
                    UseState("function greet(name) {\n    console.log('Hello, ' + name);\n    return 'Welcome ' + name;\n}")
                        .ToCodeInput()
                        .Language(Languages.Javascript)
                        .Invalid("Missing closing parenthesis!")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Disabled State
                
                The `Disabled` state prevents editing while allowing users to view the code. It's useful for displaying read-only examples or temporarily preventing modifications.
                
                Disable a `CodeInput` when needed:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(UseState("def calculate_fibonacci(n):\n    if n <= 1:\n        return n\n    return calculate_fibonacci(n-1) + calculate_fibonacci(n-2)\n\nresult = calculate_fibonacci(10)")
    .ToCodeInput()
    .Language(Languages.Python)
    .Disabled())),
                new Tab("Code", new CodeBlock(
                    """"
                    UseState("def calculate_fibonacci(n):\n    if n <= 1:\n        return n\n    return calculate_fibonacci(n-1) + calculate_fibonacci(n-2)\n\nresult = calculate_fibonacci(10)")
                        .ToCodeInput()
                        .Language(Languages.Python)
                        .Disabled()
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Event Handling
                
                Event handling enables you to respond to code changes and validate input in real-time. This allows for dynamic behavior like live validation and [conditional UI updates](app://onboarding/concepts/views).
                
                Handle code changes and validation:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new CodeInputWithValidation())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class CodeInputWithValidation : ViewBase
                    {
                        public override object? Build()
                        {
                            var codeState = UseState("");
                            var isValid = !string.IsNullOrWhiteSpace(codeState.Value);
                    
                            return Layout.Vertical()
                                | codeState.ToCodeInput()
                                        .Width(Size.Auto())
                                        .Height(Size.Auto())
                                        .Placeholder("Enter your code here...")
                                        .Language(Languages.Javascript)
                                        .WithField()
                                        .Label("Enter Code:")
                                | Text.P(isValid
                                    ? "Entered code is valid ✅"
                                    : "Enter some code to validate").Small();
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.CodeInput", "Ivy.CodeInputExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Inputs/CodeInput.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Widgets.Inputs.FieldApp), typeof(ApiReference.Ivy.SizeApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.ViewsApp)]; 
        return article;
    }
}


public class CodeInputWithValidation : ViewBase 
{
    public override object? Build()
    {        
        var codeState = UseState("");
        var isValid = !string.IsNullOrWhiteSpace(codeState.Value);
        
        return Layout.Vertical()
            | codeState.ToCodeInput()
                    .Width(Size.Auto())
                    .Height(Size.Auto())
                    .Placeholder("Enter your code here...")
                    .Language(Languages.Javascript)
                    .WithField()
                    .Label("Enter Code:")
            | Text.P(isValid 
                ? "Entered code is valid ✅" 
                : "Enter some code to validate").Small();
    }
}
