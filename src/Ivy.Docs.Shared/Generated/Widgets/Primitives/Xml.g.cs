using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:16, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/16_Xml.md", searchHints: ["markup", "xml", "data", "format", "structure", "syntax"])]
public class XmlApp(bool onlyBody = false) : ViewBase
{
    public XmlApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("xml", "Xml", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("using-text-helper", "Using Text Helper", 3), new ArticleHeading("using-xobject", "Using XObject", 3), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Xml").OnLinkClick(onLinkClick)
            | Lead("Display and interact with XML data in your Ivy applications with syntax highlighting, collapsible nodes, and real-time validation. Perfect for configuration files, API responses, and data feeds.")
            | new Markdown(
                """"
                The `Xml` [widget](app://onboarding/concepts/widgets) displays XML data in a formatted, syntax-highlighted view. It's useful for displaying configuration files, data feeds, and other XML-structured content.
                
                ## Basic Usage
                
                The simplest way to display XML data is by passing a string directly to the Xml widget.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicXmlExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicXmlExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var simpleXml = """
                                <person id="1">
                                    <name>John Doe</name>
                                    <age>30</age>
                                    <email>john.doe@example.com</email>
                                </person>
                                """;
                    
                            return Layout.Vertical().Gap(4)
                                | new Xml(simpleXml);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Using Text Helper
                
                You can also use the `Text.Xml()` helper method for displaying XML content inline with other text elements.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TextHelperExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class TextHelperExample : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical().Gap(4)
                                | Text.P("Here's an example XML configuration:")
                                | Text.Xml("<config><setting>value</setting></config>")
                                | Text.P("You can also use it with state variables:")
                                | Text.Xml(UseState("<root><item>dynamic</item></root>"));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Using XObject
                
                You can also pass `XObject` instances directly to the Xml widget, which will automatically convert them to their string representation.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new XObjectXmlExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class XObjectXmlExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var xml = new System.Xml.Linq.XElement("person",
                                new System.Xml.Linq.XComment("This is a comment"),
                                new System.Xml.Linq.XAttribute("id", 1),
                                new System.Xml.Linq.XAttribute("source", "web"),
                                new System.Xml.Linq.XElement("name", "John Doe"),
                                new System.Xml.Linq.XElement("age", 30),
                                new System.Xml.Linq.XElement("isStudent", false),
                                new System.Xml.Linq.XElement("address",
                                    new System.Xml.Linq.XElement("street", "123 Main St"),
                                    new System.Xml.Linq.XElement("city", "Anytown"),
                                    new System.Xml.Linq.XElement("state", "NY"),
                                    new System.Xml.Linq.XElement("zip", "12345")
                                ),
                                new System.Xml.Linq.XElement("phoneNumbers",
                                    new System.Xml.Linq.XElement("phoneNumber", "555-1234"),
                                    new System.Xml.Linq.XElement("phoneNumber", "555-5678")
                                )
                            );
                    
                            return Layout.Vertical().Gap(4)
                                | new Xml(xml);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Xml", "Ivy.XmlExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Xml.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Interactive XML Editor",
                Vertical().Gap(4)
                | new Markdown("This example shows how to create an interactive XML editor with real-time preview.").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new InteractiveXmlEditor())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class InteractiveXmlEditor : ViewBase
                        {
                            public override object? Build()
                            {
                                var xmlContent = UseState("""
                                    <person>
                                        <name>Jane Doe</name>
                                        <age>25</age>
                                        <skills>
                                            <skill>C#</skill>
                                            <skill>JavaScript</skill>
                                            <skill>Python</skill>
                                        </skills>
                                    </person>
                                    """);
                        
                                var isValid = UseState(true);
                                var errorMessage = UseState("");
                        
                                void ValidateXml()
                                {
                                    try
                                    {
                                        var doc = System.Xml.Linq.XDocument.Parse(xmlContent.Value);
                                        isValid.Value = true;
                                        errorMessage.Value = "";
                                    }
                                    catch (Exception ex)
                                    {
                                        isValid.Value = false;
                                        errorMessage.Value = ex.Message;
                                    }
                                }
                        
                                // Validate on content change
                                UseEffect(() => {
                                    ValidateXml();
                                });
                        
                                return Layout.Vertical().Gap(4)
                                    | Text.Label("XML Editor")
                                    | xmlContent.ToTextareaInput(placeholder: "Enter XML content here...")
                                        .Height(Size.Units(50))
                                    | (isValid.Value
                                        ? new Xml(xmlContent.Value)
                                        : Text.Danger($"Invalid XML: {errorMessage.Value}"))
                                    | new Button("Validate XML", onClick: _ => ValidateXml());
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp)]; 
        return article;
    }
}


public class BasicXmlExample : ViewBase
{
    public override object? Build()
    {
        var simpleXml = """
            <person id="1">
                <name>John Doe</name>
                <age>30</age>
                <email>john.doe@example.com</email>
            </person>
            """;
        
        return Layout.Vertical().Gap(4)
            | new Xml(simpleXml);
    }
}

public class TextHelperExample : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | Text.P("Here's an example XML configuration:")
            | Text.Xml("<config><setting>value</setting></config>")
            | Text.P("You can also use it with state variables:")
            | Text.Xml(UseState("<root><item>dynamic</item></root>"));
    }
}

public class XObjectXmlExample : ViewBase
{
    public override object? Build()
    {
        var xml = new System.Xml.Linq.XElement("person",
            new System.Xml.Linq.XComment("This is a comment"),
            new System.Xml.Linq.XAttribute("id", 1),
            new System.Xml.Linq.XAttribute("source", "web"),
            new System.Xml.Linq.XElement("name", "John Doe"),
            new System.Xml.Linq.XElement("age", 30),
            new System.Xml.Linq.XElement("isStudent", false),
            new System.Xml.Linq.XElement("address",
                new System.Xml.Linq.XElement("street", "123 Main St"),
                new System.Xml.Linq.XElement("city", "Anytown"),
                new System.Xml.Linq.XElement("state", "NY"),
                new System.Xml.Linq.XElement("zip", "12345")
            ),
            new System.Xml.Linq.XElement("phoneNumbers",
                new System.Xml.Linq.XElement("phoneNumber", "555-1234"),
                new System.Xml.Linq.XElement("phoneNumber", "555-5678")
            )
        );
        
        return Layout.Vertical().Gap(4)
            | new Xml(xml);
    }
}

public class InteractiveXmlEditor : ViewBase
{
    public override object? Build()
    {
        var xmlContent = UseState("""
            <person>
                <name>Jane Doe</name>
                <age>25</age>
                <skills>
                    <skill>C#</skill>
                    <skill>JavaScript</skill>
                    <skill>Python</skill>
                </skills>
            </person>
            """);
        
        var isValid = UseState(true);
        var errorMessage = UseState("");
        
        void ValidateXml()
        {
            try
            {
                var doc = System.Xml.Linq.XDocument.Parse(xmlContent.Value);
                isValid.Value = true;
                errorMessage.Value = "";
            }
            catch (Exception ex)
            {
                isValid.Value = false;
                errorMessage.Value = ex.Message;
            }
        }
        
        // Validate on content change
        UseEffect(() => {
            ValidateXml();
        });
        
        return Layout.Vertical().Gap(4)
            | Text.Label("XML Editor")
            | xmlContent.ToTextareaInput(placeholder: "Enter XML content here...")
                .Height(Size.Units(50))
            | (isValid.Value 
                ? new Xml(xmlContent.Value)
                : Text.Danger($"Invalid XML: {errorMessage.Value}"))
            | new Button("Validate XML", onClick: _ => ValidateXml());
    }
}
