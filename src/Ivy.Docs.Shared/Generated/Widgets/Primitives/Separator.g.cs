using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:7, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/07_Separator.md", searchHints: ["divider", "line", "horizontal", "vertical", "separator", "hr"])]
public class SeparatorApp(bool onlyBody = false) : ViewBase
{
    public SeparatorApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("separator", "Separator", 1), new ArticleHeading("text-alignment", "Text Alignment", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Separator").OnLinkClick(onLinkClick)
            | Lead("Create visual dividers between content sections to organize information and improve interface readability with clear content demarcation.")
            | new Markdown("The `Separator` [widget](app://onboarding/concepts/widgets) creates a visual divider between content sections. It helps organize information and improve readability by clearly demarcating different parts of your interface.").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ProfileDetailView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ProfileDetailView : ViewBase
                    {
                        public override object? Build()
                        {
                            // Sample user data - in a real app, this would come from a data source
                            var user = new
                            {
                                Name = "Alex Johnson",
                                Email = "alex.johnson@example.com",
                                Role = "Senior Developer",
                                Department = "Engineering",
                                JoinDate = new DateTime(2020, 3, 15),
                                Skills = new[] { "C#", "React", "Azure", "SQL", "TypeScript" },
                                Projects = new[]
                                {
                                    "Customer Portal Redesign",
                                    "Inventory Management System",
                                    "API Gateway Implementation"
                                }
                            };
                    
                            return Layout.Vertical().Gap(4)
                                | Text.H1("User Profile")
                    
                                | Layout.Vertical().Gap(2)
                                    | Text.H2("Personal Information")
                                    | (Layout.Horizontal().Gap(2)
                                        | Text.Strong("Name:")
                                        | Text.Inline(user.Name))
                                    | (Layout.Horizontal().Gap(2)
                                        | Text.Strong("Email:")
                                        | Text.Inline(user.Email))
                    
                                | new Separator()
                    
                                | Layout.Vertical().Gap(2)
                                    | Text.H2("Work Information")
                                    | (Layout.Horizontal().Gap(2)
                                        | Text.Strong("Role:")
                                        | Text.Inline(user.Role))
                                    | (Layout.Horizontal().Gap(2)
                                        | Text.Strong("Department:")
                                        | Text.Inline(user.Department))
                                    | (Layout.Horizontal().Gap(2)
                                        | Text.Strong("Join Date:")
                                        | Text.Inline(user.JoinDate.ToShortDateString()))
                    
                                | new Separator()
                    
                                | Layout.Vertical().Gap(2)
                                    | Text.H2("Skills")
                                    | (Layout.Horizontal().Gap(2).Wrap(true)
                                        | user.Skills.Select(skill =>
                                            new Badge(skill).Variant(BadgeVariant.Secondary)))
                    
                                | new Separator()
                    
                                | Layout.Vertical().Gap(2)
                                    | Text.H2("Projects")
                                    | Layout.Vertical().Gap(1)
                                        | user.Projects.Select(project => Text.P(project));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Text Alignment
                
                When using a label with the separator, you can control its alignment using the `TextAlign` method:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SeparatorTextAlignView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SeparatorTextAlignView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical().Gap(4)
                                | new Separator("Left Aligned").TextAlign(TextAlignment.Left)
                                | new Separator("Center Aligned").TextAlign(TextAlignment.Center)
                                | new Separator("Right Aligned").TextAlign(TextAlignment.Right);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Separator", "Ivy.SeparatorExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Separator.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp)]; 
        return article;
    }
}


public class ProfileDetailView : ViewBase
{
    public override object? Build()
    {
        // Sample user data - in a real app, this would come from a data source
        var user = new
        {
            Name = "Alex Johnson",
            Email = "alex.johnson@example.com",
            Role = "Senior Developer",
            Department = "Engineering",
            JoinDate = new DateTime(2020, 3, 15),
            Skills = new[] { "C#", "React", "Azure", "SQL", "TypeScript" },
            Projects = new[] 
            { 
                "Customer Portal Redesign", 
                "Inventory Management System", 
                "API Gateway Implementation" 
            }
        };
        
        return Layout.Vertical().Gap(4)
            | Text.H1("User Profile")
            
            | Layout.Vertical().Gap(2)
                | Text.H2("Personal Information")
                | (Layout.Horizontal().Gap(2)
                    | Text.Strong("Name:")
                    | Text.Inline(user.Name))
                | (Layout.Horizontal().Gap(2)
                    | Text.Strong("Email:")
                    | Text.Inline(user.Email))
            
            | new Separator()
            
            | Layout.Vertical().Gap(2)
                | Text.H2("Work Information")
                | (Layout.Horizontal().Gap(2)
                    | Text.Strong("Role:")
                    | Text.Inline(user.Role))
                | (Layout.Horizontal().Gap(2)
                    | Text.Strong("Department:")
                    | Text.Inline(user.Department))
                | (Layout.Horizontal().Gap(2)
                    | Text.Strong("Join Date:")
                    | Text.Inline(user.JoinDate.ToShortDateString()))
            
            | new Separator()
            
            | Layout.Vertical().Gap(2)
                | Text.H2("Skills")
                | (Layout.Horizontal().Gap(2).Wrap(true)
                    | user.Skills.Select(skill => 
                        new Badge(skill).Variant(BadgeVariant.Secondary)))
            
            | new Separator()
            
            | Layout.Vertical().Gap(2)
                | Text.H2("Projects")
                | Layout.Vertical().Gap(1)
                    | user.Projects.Select(project => Text.P(project));
    }
}

public class SeparatorTextAlignView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | new Separator("Left Aligned").TextAlign(TextAlignment.Left)
            | new Separator("Center Aligned").TextAlign(TextAlignment.Center)
            | new Separator("Right Aligned").TextAlign(TextAlignment.Right);
    }
}
