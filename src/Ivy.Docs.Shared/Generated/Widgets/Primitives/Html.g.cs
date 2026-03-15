using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:15, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/15_Html.md", searchHints: ["markup", "html", "custom", "raw", "embedded", "content"])]
public class HtmlApp(bool onlyBody = false) : ViewBase
{
    public HtmlApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("html", "Html", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("content-examples", "Content Examples", 2), new ArticleHeading("text-formatting", "Text Formatting", 3), new ArticleHeading("lists-and-structure", "Lists and Structure", 3), new ArticleHeading("links-and-navigation", "Links and Navigation", 3), new ArticleHeading("tables", "Tables", 3), new ArticleHeading("complex-layout-example", "Complex Layout Example", 2), new ArticleHeading("security-features", "Security Features", 2), new ArticleHeading("allowed-html-tags", "Allowed HTML Tags", 3), new ArticleHeading("security-measures", "Security Measures", 3), new ArticleHeading("by-passing-security-dangerouslyallowscripts", "By-passing Security (DangerouslyAllowScripts)", 3), new ArticleHeading("example-of-security-in-action", "Example of Security in Action", 3), new ArticleHeading("best-practices--use-cases", "Best Practices & Use Cases", 2), new ArticleHeading("best-practices", "Best Practices", 3), new ArticleHeading("keep-it-simple", "Keep It Simple", 4), new ArticleHeading("validate-external-content", "Validate External Content", 4), new ArticleHeading("handle-long-content", "Handle Long Content", 4), new ArticleHeading("common-use-cases", "Common Use Cases", 3), new ArticleHeading("rich-text-from-cms", "Rich Text from CMS", 4), new ArticleHeading("documentation-and-help-text", "Documentation and Help Text", 4), new ArticleHeading("formatted-user-content", "Formatted User Content", 4), new ArticleHeading("limitations", "Limitations", 2), new ArticleHeading("when-to-use-html-widget", "When to Use Html Widget", 2), new ArticleHeading("faq", "Faq", 2), new ArticleHeading("how-to-apply-background-styling-with-css-gradients", "How to apply background styling with CSS gradients?", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Html").OnLinkClick(onLinkClick)
            | Lead("Render raw HTML content directly in your Ivy application for external content integration, formatted text, and custom markup control.")
            | new Markdown(
                """"
                The `Html` [widget](app://onboarding/concepts/widgets) allows you to render raw HTML content in your [Ivy app](app://onboarding/concepts/apps). This is useful when you need to include content from external sources, display formatted text, or when you want direct control over the markup.
                
                ## Basic Usage
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicHtmlView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicHtmlView : ViewBase
                    {
                        public override object? Build()
                        {
                            var simpleHtml = "<p>Hello, <strong>World</strong>!</p>";
                    
                            return new Html(simpleHtml);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Content Examples
                
                ### Text Formatting
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TextFormattingView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class TextFormattingView : ViewBase
                    {
                        public override object? Build()
                        {
                            var formattedText =
                                """
                                <h1>Main Heading</h1>
                                <h2>Subheading</h2>
                                <p>This paragraph contains <strong>bold text</strong>, <em>italic text</em>,
                                   and <b>bold using b tag</b>, plus <i>italic using i tag</i>.</p>
                                <p>You can also use <span style="color: blue;">colored text</span>
                                   and <span style="text-decoration: underline;">underlined text</span>.</p>
                                """;
                    
                            return new Html(formattedText);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Lists and Structure").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ListsView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ListsView : ViewBase
                    {
                        public override object? Build()
                        {
                            var listsHtml =
                                """
                                <h3>Unordered List</h3>
                                <ul>
                                    <li>First item</li>
                                    <li>Second item with <strong>bold text</strong></li>
                                    <li>Third item with <em>italic text</em></li>
                                </ul>
                    
                                <h3>Ordered List</h3>
                                <ol>
                                    <li>Step one</li>
                                    <li>Step two</li>
                                    <li>Step three</li>
                                </ol>
                    
                                <h3>Nested Lists</h3>
                                <ul>
                                    <li>Parent item
                                        <ul>
                                            <li>Child item 1</li>
                                            <li>Child item 2</li>
                                        </ul>
                                    </li>
                                    <li>Another parent item</li>
                                </ul>
                                """;
                    
                            return new Html(listsHtml);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Links and Navigation").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new LinksView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class LinksView : ViewBase
                    {
                        public override object? Build()
                        {
                            var linksHtml =
                                """
                                <p>Visit <a href="https://github.com/Ivy-Interactive/Ivy-Framework">Ivy Framework</a> on GitHub.</p>
                                <p>You can also link to <a href="#section1">internal sections</a> or
                                   <a href="mailto:example@example.com">email addresses</a>.</p>
                                """;
                    
                            return new Html(linksHtml);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Tables").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TablesView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class TablesView : ViewBase
                    {
                        public override object? Build()
                        {
                            var tableHtml =
                                """
                                <table border="1" style="width: 100%; border-collapse: collapse; margin: 10px 0;">
                                    <tr style="background-color: #f0f0f0;">
                                        <th style="padding: 12px; text-align: left; border: 1px solid #ddd;">Feature</th>
                                        <th style="padding: 12px; text-align: left; border: 1px solid #ddd;">Status</th>
                                        <th style="padding: 12px; text-align: left; border: 1px solid #ddd;">Description</th>
                                    </tr>
                                    <tr>
                                        <td style="padding: 8px; border: 1px solid #ddd;">HTML Rendering</td>
                                        <td style="padding: 8px; border: 1px solid #ddd; color: green;">✓ Supported</td>
                                        <td style="padding: 8px; border: 1px solid #ddd;">Safe HTML rendering with sanitization</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 8px; border: 1px solid #ddd;">Custom Styling</td>
                                        <td style="padding: 8px; border: 1px solid #ddd; color: green;">✓ Supported</td>
                                        <td style="padding: 8px; border: 1px solid #ddd;">Inline styles are preserved</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 8px; border: 1px solid #ddd;">JavaScript</td>
                                        <td style="padding: 8px; border: 1px solid #ddd; color: red;">✗ Blocked</td>
                                        <td style="padding: 8px; border: 1px solid #ddd;">Scripts are removed for security</td>
                                    </tr>
                                </table>
                                """;
                    
                            return new Html(tableHtml);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("## Complex Layout Example").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ComplexLayoutView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ComplexLayoutView : ViewBase
                    {
                        public override object? Build()
                        {
                            var complexHtml =
                                """
                                <div style="background-color: var(--muted); padding: 20px; border-radius: 8px; border-left: 4px solid var(--primary);">
                                    <h2 style="color: var(--primary); margin-top: 0;">Product Features</h2>
                                    <p style="font-size: 16px; color: var(--muted-foreground);">Discover what makes our product special:</p>
                    
                                    <div style="display: flex; gap: 20px; margin: 20px 0;">
                                        <div style="flex: 1; background: var(--background); padding: 15px; border-radius: 5px; box-shadow: 0 2px 4px rgba(0,0,0,0.1);">
                                            <h3 style="color: var(--primary); margin-top: 0;">Performance</h3>
                                            <p>Lightning-fast rendering with optimized algorithms.</p>
                                        </div>
                                        <div style="flex: 1; background: var(--background); padding: 15px; border-radius: 5px; box-shadow: 0 2px 4px rgba(0,0,0,0.1);">
                                            <h3 style="color: var(--destructive); margin-top: 0;">Security</h3>
                                            <p>Built-in HTML sanitization protects against XSS attacks.</p>
                                        </div>
                                    </div>
                    
                                    <div style="background: var(--accent); padding: 15px; border-radius: 5px; margin-top: 20px;">
                                        <h4 style="margin-top: 0;">Quick Stats</h4>
                                        <ul style="margin: 0;">
                                            <li><strong>Rendering Speed:</strong> <span style="color: var(--primary);">99.9% faster</span></li>
                                            <li><strong>Memory Usage:</strong> <span style="color: var(--accent-foreground);">50% less</span></li>
                                            <li><strong>Security Score:</strong> <span style="color: var(--primary);">A+</span></li>
                                        </ul>
                                    </div>
                                </div>
                                """;
                    
                            return Layout.Vertical().Gap(4)
                                | Text.H1("HTML Widget Demo")
                                | new Html(complexHtml);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Security Features
                
                The Html widget includes robust security measures to protect against malicious content:
                
                ### Allowed HTML Tags
                
                Only these HTML tags are permitted:
                
                - **Text formatting:** `p`, `div`, `span`, `strong`, `em`, `b`, `i`, `br`
                - **Headings:** `h1`, `h2`, `h3`, `h4`, `h5`, `h6`
                - **Lists:** `ul`, `ol`, `li`
                - **Links:** `a`
                
                ### Security Measures
                
                - **Script removal:** All `<script>` tags are completely removed
                - **Event handler blocking:** All `on*` event handlers (onclick, onload, etc.) are stripped
                - **JavaScript URL blocking:** `javascript:` URLs in href attributes are removed
                - **Tag whitelisting:** Only approved HTML tags are allowed
                
                ### By-passing Security (DangerouslyAllowScripts)
                """").OnLinkClick(onLinkClick)
            | new Callout("**Security Risk:** Enabling `DangerouslyAllowScripts` bypasses the built-in HTML sanitization and executes any JavaScript contained within the HTML string. Only use this feature if you absolutely trust the source of the HTML content. Rendering user-generated content with this flag enabled exposes your application to Cross-Site Scripting (XSS) attacks.", icon:Icons.CircleAlert).OnLinkClick(onLinkClick)
            | new Markdown("If you need to render raw HTML that includes `<script>` tags and you trust the source completely, you can bypass the sanitization by setting `DangerouslyAllowScripts(true)`.").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ScriptHtmlView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ScriptHtmlView : ViewBase
                    {
                        public override object? Build()
                        {
                            var htmlWithScript =
                                """
                                <div id="target-div">Loading...</div>
                                <script>
                                    document.getElementById('target-div').innerText = 'Script executed successfully!';
                                </script>
                                """;
                    
                            // Use the fluent method to enable scripts execution
                            return new Html(htmlWithScript).DangerouslyAllowScripts();
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Example of Security in Action").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SecurityDemoView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SecurityDemoView : ViewBase
                    {
                        public override object? Build()
                        {
                            // This potentially dangerous HTML...
                            var unsafeHtml =
                                """
                                <p>Safe content</p>
                                <script>alert('This will be removed!');</script>
                                <div onclick="alert('This will be removed!')">Click me</div>
                                <a href="javascript:alert('Blocked!')">Blocked link</a>
                                <iframe src="https://evil.com">This tag will be removed</iframe>
                                """;
                    
                            // ...becomes safe when rendered
                            return new Html(unsafeHtml);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Best Practices & Use Cases
                
                ### Best Practices
                
                #### Keep It Simple
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Good: Simple, clear HTML
                var goodHtml = "<p>Welcome to our <strong>project</strong>!</p>";
                
                // Avoid: Overly complex nested structures
                var complexHtml = "<div><div><div><p>Deep nesting</p></div></div></div>";
                """",Languages.Csharp)
            | new Markdown("#### Validate External Content").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class ExternalContentView : ViewBase
                {
                    public override object? Build()
                    {
                        var externalHtml = GetContentFromExternalSource();
                
                        // Always validate external content
                        if (string.IsNullOrEmpty(externalHtml))
                        {
                            return new Html("<p>No content available</p>");
                        }
                
                        return new Html(externalHtml);
                    }
                
                    private string GetContentFromExternalSource()
                    {
                        // Your external content logic here
                        return "<p>External content</p>";
                    }
                }
                """",Languages.Csharp)
            | new Markdown("#### Handle Long Content").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class LongContentView : ViewBase
                {
                    public override object? Build()
                    {
                        var longContent = GetLongHtmlContent();
                
                        return Layout.Vertical().Gap(4)
                            | Text.H2("Article")
                            | new Html($"<div style='max-height: 400px; overflow-y: auto;'>{longContent}</div>");
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Common Use Cases
                
                #### Rich Text from CMS
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class CMSContentView : ViewBase
                {
                    public override object? Build()
                    {
                        var cmsContent =
                            """
                            <h2>Latest News</h2>
                            <p><em>Published: January 15, 2024</em></p>
                            <p>We're excited to announce new features in our latest release...</p>
                            """;
                
                        return new Html(cmsContent);
                    }
                }
                """",Languages.Csharp)
            | new Markdown("#### Documentation and Help Text").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class HelpContentView : ViewBase
                {
                    public override object? Build()
                    {
                        var helpHtml =
                            """
                            <h3>How to Use This Feature</h3>
                            <ol>
                                <li>Click the <strong>Start</strong> button</li>
                                <li>Select your preferences</li>
                                <li>Review the results</li>
                            </ol>
                            <p><em>Need more help? <a href='mailto:support@example.com'>Contact support</a></em></p>
                            """;
                
                        return new Html(helpHtml);
                    }
                }
                """",Languages.Csharp)
            | new Markdown("#### Formatted User Content").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class UserContentView : ViewBase
                {
                    public override object? Build()
                    {
                        var userComment =
                            """
                            <div style='background: #f9f9f9; padding: 15px; border-radius: 5px;'>
                                <p><strong>User123:</strong> This is a great feature! I especially like the <em>ease of use</em>.</p>
                                <p><small>Posted 2 hours ago</small></p>
                            </div>
                            """;
                
                        return new Html(userComment);
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Limitations
                
                - **No JavaScript:** All JavaScript code is removed for security
                - **Limited HTML tags:** Only a subset of HTML tags are supported
                - **No form elements:** Input fields, buttons, and forms are not supported
                - **No embedded content:** iframes, objects, and embeds are blocked
                - **No CSS imports:** External stylesheets cannot be imported
                
                ## When to Use Html Widget
                
                **Use Html widget when:**
                
                - Displaying content from external sources (CMS, APIs)
                - Showing rich text with formatting
                - Rendering documentation or help content
                - Displaying user-generated content (with proper sanitization)
                - Creating complex [layouts](app://onboarding/concepts/views) with custom styling
                
                **Don't use Html widget when:**
                
                - You need interactive elements (use [Button](app://widgets/common/button), Input, etc.)
                - You want to embed external content (use [Iframe widget](app://widgets/primitives/iframe))
                - You need JavaScript functionality
                - Simple text formatting would suffice (use [Text widget](app://widgets/primitives/text-block))
                
                ## Faq
                
                ### How to apply background styling with CSS gradients?
                
                The `Html` widget in safe mode strips inline `style` attributes. To render custom CSS (including gradients, box-shadows, or positioned elements), you must chain `.DangerouslyAllowScripts()`:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                return new Html("""
                    <div style="position: fixed; inset: 0; z-index: -1; background: linear-gradient(135deg, #667eea, #764ba2, #f093fb);"></div>
                """).DangerouslyAllowScripts()
                | Layout.Center()
                    | myContent;
                """",Languages.Csharp)
            | new Markdown("Alternatively, use native Ivy styling with `Layout.Background(Colors.X)` — but this only supports solid `Colors` enum values, not CSS gradients.").OnLinkClick(onLinkClick)
            | new WidgetDocsView("Ivy.Html", null, "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Html.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.AppsApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Widgets.Common.ButtonApp), typeof(Widgets.Primitives.IframeApp), typeof(Widgets.Primitives.TextBlockApp)]; 
        return article;
    }
}


public class BasicHtmlView : ViewBase
{
    public override object? Build()
    {
        var simpleHtml = "<p>Hello, <strong>World</strong>!</p>";
        
        return new Html(simpleHtml);
    }
}

public class TextFormattingView : ViewBase
{
    public override object? Build()
    {
        var formattedText = 
            """
            <h1>Main Heading</h1>
            <h2>Subheading</h2>
            <p>This paragraph contains <strong>bold text</strong>, <em>italic text</em>, 
               and <b>bold using b tag</b>, plus <i>italic using i tag</i>.</p>
            <p>You can also use <span style="color: blue;">colored text</span> 
               and <span style="text-decoration: underline;">underlined text</span>.</p>
            """;
        
        return new Html(formattedText);
    }
}

public class ListsView : ViewBase
{
    public override object? Build()
    {
        var listsHtml = 
            """
            <h3>Unordered List</h3>
            <ul>
                <li>First item</li>
                <li>Second item with <strong>bold text</strong></li>
                <li>Third item with <em>italic text</em></li>
            </ul>
            
            <h3>Ordered List</h3>
            <ol>
                <li>Step one</li>
                <li>Step two</li>
                <li>Step three</li>
            </ol>
            
            <h3>Nested Lists</h3>
            <ul>
                <li>Parent item
                    <ul>
                        <li>Child item 1</li>
                        <li>Child item 2</li>
                    </ul>
                </li>
                <li>Another parent item</li>
            </ul>
            """;
        
        return new Html(listsHtml);
    }
}

public class LinksView : ViewBase
{
    public override object? Build()
    {
        var linksHtml = 
            """
            <p>Visit <a href="https://github.com/Ivy-Interactive/Ivy-Framework">Ivy Framework</a> on GitHub.</p>
            <p>You can also link to <a href="#section1">internal sections</a> or 
               <a href="mailto:example@example.com">email addresses</a>.</p>
            """;
        
        return new Html(linksHtml);
    }
}

public class TablesView : ViewBase
{
    public override object? Build()
    {
        var tableHtml = 
            """
            <table border="1" style="width: 100%; border-collapse: collapse; margin: 10px 0;">
                <tr style="background-color: #f0f0f0;">
                    <th style="padding: 12px; text-align: left; border: 1px solid #ddd;">Feature</th>
                    <th style="padding: 12px; text-align: left; border: 1px solid #ddd;">Status</th>
                    <th style="padding: 12px; text-align: left; border: 1px solid #ddd;">Description</th>
                </tr>
                <tr>
                    <td style="padding: 8px; border: 1px solid #ddd;">HTML Rendering</td>
                    <td style="padding: 8px; border: 1px solid #ddd; color: green;">✓ Supported</td>
                    <td style="padding: 8px; border: 1px solid #ddd;">Safe HTML rendering with sanitization</td>
                </tr>
                <tr>
                    <td style="padding: 8px; border: 1px solid #ddd;">Custom Styling</td>
                    <td style="padding: 8px; border: 1px solid #ddd; color: green;">✓ Supported</td>
                    <td style="padding: 8px; border: 1px solid #ddd;">Inline styles are preserved</td>
                </tr>
                <tr>
                    <td style="padding: 8px; border: 1px solid #ddd;">JavaScript</td>
                    <td style="padding: 8px; border: 1px solid #ddd; color: red;">✗ Blocked</td>
                    <td style="padding: 8px; border: 1px solid #ddd;">Scripts are removed for security</td>
                </tr>
            </table>
            """;
        
        return new Html(tableHtml);
    }
}

public class ComplexLayoutView : ViewBase
{
    public override object? Build()
    {
        var complexHtml = 
            """
            <div style="background-color: var(--muted); padding: 20px; border-radius: 8px; border-left: 4px solid var(--primary);">
                <h2 style="color: var(--primary); margin-top: 0;">Product Features</h2>
                <p style="font-size: 16px; color: var(--muted-foreground);">Discover what makes our product special:</p>
                
                <div style="display: flex; gap: 20px; margin: 20px 0;">
                    <div style="flex: 1; background: var(--background); padding: 15px; border-radius: 5px; box-shadow: 0 2px 4px rgba(0,0,0,0.1);">
                        <h3 style="color: var(--primary); margin-top: 0;">Performance</h3>
                        <p>Lightning-fast rendering with optimized algorithms.</p>
                    </div>
                    <div style="flex: 1; background: var(--background); padding: 15px; border-radius: 5px; box-shadow: 0 2px 4px rgba(0,0,0,0.1);">
                        <h3 style="color: var(--destructive); margin-top: 0;">Security</h3>
                        <p>Built-in HTML sanitization protects against XSS attacks.</p>
                    </div>
                </div>
                
                <div style="background: var(--accent); padding: 15px; border-radius: 5px; margin-top: 20px;">
                    <h4 style="margin-top: 0;">Quick Stats</h4>
                    <ul style="margin: 0;">
                        <li><strong>Rendering Speed:</strong> <span style="color: var(--primary);">99.9% faster</span></li>
                        <li><strong>Memory Usage:</strong> <span style="color: var(--accent-foreground);">50% less</span></li>
                        <li><strong>Security Score:</strong> <span style="color: var(--primary);">A+</span></li>
                    </ul>
                </div>
            </div>
            """;
        
        return Layout.Vertical().Gap(4)
            | Text.H1("HTML Widget Demo")
            | new Html(complexHtml);
    }
}

public class ScriptHtmlView : ViewBase
{
    public override object? Build()
    {
        var htmlWithScript = 
            """
            <div id="target-div">Loading...</div>
            <script>
                document.getElementById('target-div').innerText = 'Script executed successfully!';
            </script>
            """;
        
        // Use the fluent method to enable scripts execution
        return new Html(htmlWithScript).DangerouslyAllowScripts();
    }
}

public class SecurityDemoView : ViewBase
{
    public override object? Build()
    {
        // This potentially dangerous HTML...
        var unsafeHtml = 
            """
            <p>Safe content</p>
            <script>alert('This will be removed!');</script>
            <div onclick="alert('This will be removed!')">Click me</div>
            <a href="javascript:alert('Blocked!')">Blocked link</a>
            <iframe src="https://evil.com">This tag will be removed</iframe>
            """;
        
        // ...becomes safe when rendered
        return new Html(unsafeHtml);
    }
}
