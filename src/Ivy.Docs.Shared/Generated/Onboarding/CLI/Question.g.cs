using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI;

[App(order:9, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/09_Question.md", searchHints: ["mcp", "question"])]
public class QuestionApp(bool onlyBody = false) : ViewBase
{
    public QuestionApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivy-question", "ivy question", 1), new ArticleHeading("usage", "Usage", 2), new ArticleHeading("arguments", "Arguments", 3), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# ivy question").OnLinkClick(onLinkClick)
            | Lead("Query the local context dynamically using integrated Local RAG features specifically tailored to your semantic `ivyVersion`.")
            | new Markdown(
                """"
                The `ivy question` command executes semantic queries across the comprehensive framework knowledge base. When asked "how" to do something or for code examples regarding Ivy internals, the underlying engine cross-references the latest indexed state of `Ivy.Docs.Shared`.
                
                ## Usage
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy question <QUESTION>")
                
            | new Markdown(
                """"
                ### Arguments
                
                - `<QUESTION>`: The natural language string prompt or instruction to run against the knowledge base. Wrap the query in double quotes.
                
                ## Examples
                
                Ask a standard architectural question:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy question \"How do I implement a new Application Shell in Ivy?\"")
                
            | new Markdown("Ask for specific CLI advice:").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy question \"What is the command to create an auto-incrementing migration in Ivy?\"")
                
            ;
        return article;
    }
}

