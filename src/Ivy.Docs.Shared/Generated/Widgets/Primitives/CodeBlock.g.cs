using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:10, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/10_CodeBlock.md", searchHints: ["syntax", "highlighting", "code-block", "snippet", "pre", "programming"])]
public class CodeBlockApp(bool onlyBody = false) : ViewBase
{
    public CodeBlockApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("codeblock", "CodeBlock", 1), new ArticleHeading("starting-line-number", "Starting Line Number", 2), new ArticleHeading("wrap-lines", "Wrap Lines", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# CodeBlock").OnLinkClick(onLinkClick)
            | Lead("Display beautifully formatted code snippets with syntax highlighting, line numbers, and copy functionality for multiple programming languages.")
            | new Markdown("The `CodeBlock` [widget](app://onboarding/concepts/widgets) displays formatted code snippets with syntax highlighting. It supports multiple programming languages and features line numbers and copy buttons for better user experience. Use [Size](app://api-reference/ivy/size) for `.Width()` and `.Height()` to control dimensions.").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical()
    | new CodeBlock(@"function fibonacci(n) {
  if (n <= 1) return n;
  return fibonacci(n-1) + fibonacci(n-2);
}

// Print first 10 Fibonacci numbers
for (let i = 0; i < 10; i++) {
  console.log(fibonacci(i));
  }
  ")
      .ShowLineNumbers()
      .ShowCopyButton()
      .Language(Languages.Javascript)
      .Width(Size.Full())
      .Height(Size.Auto()))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical()
                        | new CodeBlock(@"function fibonacci(n) {
                      if (n <= 1) return n;
                      return fibonacci(n-1) + fibonacci(n-2);
                    }
                    
                    // Print first 10 Fibonacci numbers
                    for (let i = 0; i < 10; i++) {
                      console.log(fibonacci(i));
                      }
                      ")
                          .ShowLineNumbers()
                          .ShowCopyButton()
                          .Language(Languages.Javascript)
                          .Width(Size.Full())
                          .Height(Size.Auto())
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Starting Line Number
                
                Use `StartingLineNumber` to offset line numbering when displaying code excerpts. This is useful when showing a snippet from a larger file where you want to preserve the original line numbers.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical()
    | new CodeBlock(@"    private static int Calculate(int input)
    {
        return input * 2 + 1;
    }
}")
      .ShowLineNumbers()
      .StartingLineNumber(18)
      .Language(Languages.Csharp))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical()
                        | new CodeBlock(@"    private static int Calculate(int input)
                        {
                            return input * 2 + 1;
                        }
                    }")
                          .ShowLineNumbers()
                          .StartingLineNumber(18)
                          .Language(Languages.Csharp)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Wrap Lines
                
                Use `WrapLines` to enable wrapping of long lines within the code block. This improves readability for code with long lines, especially in constrained layouts. By default, long lines require horizontal scrolling.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical()
    | new CodeBlock(@"public class Example { public void VeryLongMethodName(string parameter1, int parameter2, bool parameter3) { Console.WriteLine(""This is a very long line that will wrap instead of requiring horizontal scrolling.""); } }")
      .WrapLines()
      .Language(Languages.Csharp))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical()
                        | new CodeBlock(@"public class Example { public void VeryLongMethodName(string parameter1, int parameter2, bool parameter3) { Console.WriteLine(""This is a very long line that will wrap instead of requiring horizontal scrolling.""); } }")
                          .WrapLines()
                          .Language(Languages.Csharp)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.CodeBlock", "Ivy.CodeBlockExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/CodeBlock.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(ApiReference.Ivy.SizeApp)]; 
        return article;
    }
}

