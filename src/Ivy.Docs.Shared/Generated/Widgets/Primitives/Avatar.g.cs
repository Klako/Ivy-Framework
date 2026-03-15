using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:8, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/08_Avatar.md", searchHints: ["profile", "user", "image", "photo", "picture", "circle"])]
public class AvatarApp(bool onlyBody = false) : ViewBase
{
    public AvatarApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("avatar", "Avatar", 1), new ArticleHeading("practical-usage", "Practical Usage", 2), new ArticleHeading("integration-with-other-widgets", "Integration with Other Widgets", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Avatar").OnLinkClick(onLinkClick)
            | Lead("Display user or entity representations with automatic fallbacks from images to initials to placeholders for consistent visual identity.")
            | new Markdown(
                """"
                `Avatars` are graphical representations of users or entities. They display an image if available or fall back to initials or a placeholder when no image is provided.
                
                To create a new avatar, it is recommended to use a [layout](app://onboarding/concepts/layout).
                
                Make sure to define a name and supply a `url` to fetch the image.
                
                If no image is provided, a default avatar will be used, showing the first letters of the name.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal()
    | new Avatar("Niels Bosma", "https://api.images.cat/150/150?1")
    | new Avatar("Niels Bosma"))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal()
                        | new Avatar("Niels Bosma", "https://api.images.cat/150/150?1")
                        | new Avatar("Niels Bosma")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Practical Usage
                
                It's possible to create a dictionary where each object contains a name and an associated avatar.
                
                `Avatars` can be used to showcase Teams like this.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new IvyTeamDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class IvyTeamDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var team = new Dictionary<string,string>()
                            {
                                 {"Niels Bosma",
                                     "https://api.images.cat/150/150?1"},
                                 {"Mikael Rinne",
                                     "https://api.images.cat/150/150?2"},
                                 {"Renco Smeding",
                                     "https://api.images.cat/150/150?3"},
                                 {"Jesper",
                                     "https://api.images.cat/150/150?4"},
                                 {"Frida Bosma",
                                     "https://api.images.cat/150/150?5"},
                                 {"Viktor Bolin",
                                     "https://api.images.cat/150/150?6"},
                            };
                    
                            var layout = Layout.Grid()
                                               .Columns(3)
                                               .Rows(2);
                    
                    
                            foreach(var key in team.Keys)
                            {
                                layout = layout
                                          |new Card(new Avatar(key, team[key]).Height(Size.Units(200)).Width(Size.Units(100)))
                                                .Title(key);
                            }
                            return Layout.Vertical()
                                          | H3("Ivy Team")
                                          | layout;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Integration with Other Widgets
                
                Avatars can be integrated into other [widgets](app://onboarding/concepts/widgets), including [cards](app://widgets/common/card), add [buttons](app://widgets/common/button), and more. Use [Size](app://api-reference/ivy/size) for `.Width()` and `.Height()` when customizing dimensions.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new AvatarAsFoodIcon())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class AvatarAsFoodIcon : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical()
                                    |
                                    new Card(
                                    new Avatar("Köttbullar","https://api.images.cat/150/150?7"),
                                    new Button("Add to order")).Title("Köttbullar")
                                        .Description("The quintessential Swedish food, these meatballs are more than just a dish; they're a cultural icon.")
                                        .Width(Size.Units(100))
                                    |
                                    new Card(
                                    new Avatar("Pytt i Panna","https://api.images.cat/150/150?8"),
                                    new Button("Add to order")).Title("Pytt i Panna")
                                        .Description("Translating to small pieces in a pan, this hearty hash of potatoes, onions, and meat is a beloved comfort food. It's a brilliant way to use leftovers and is often crowned with a fried egg.")
                                        .Width(Size.Units(100));
                        }
                    }
                    
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Avatar", "Ivy.AvatarExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Avatar.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.LayoutApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Widgets.Common.CardApp), typeof(Widgets.Common.ButtonApp), typeof(ApiReference.Ivy.SizeApp)]; 
        return article;
    }
}


public class IvyTeamDemo : ViewBase
{
    public override object? Build()
    {
        var team = new Dictionary<string,string>()
        {
             {"Niels Bosma",
                 "https://api.images.cat/150/150?1"},
             {"Mikael Rinne",
                 "https://api.images.cat/150/150?2"},
             {"Renco Smeding",
                 "https://api.images.cat/150/150?3"},
             {"Jesper",
                 "https://api.images.cat/150/150?4"},
             {"Frida Bosma",
                 "https://api.images.cat/150/150?5"},
             {"Viktor Bolin",
                 "https://api.images.cat/150/150?6"},
        };

        var layout = Layout.Grid()
                           .Columns(3)
                           .Rows(2);


        foreach(var key in team.Keys)
        {
            layout = layout
                      |new Card(new Avatar(key, team[key]).Height(Size.Units(200)).Width(Size.Units(100)))
                            .Title(key);
        }
        return Layout.Vertical()
                      | H3("Ivy Team")
                      | layout;
    }
}

public class AvatarAsFoodIcon : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical()
                |
                new Card(
                new Avatar("Köttbullar","https://api.images.cat/150/150?7"),
                new Button("Add to order")).Title("Köttbullar")
                    .Description("The quintessential Swedish food, these meatballs are more than just a dish; they're a cultural icon.")
                    .Width(Size.Units(100))
                |
                new Card(
                new Avatar("Pytt i Panna","https://api.images.cat/150/150?8"),
                new Button("Add to order")).Title("Pytt i Panna")
                    .Description("Translating to small pieces in a pan, this hearty hash of potatoes, onions, and meat is a beloved comfort food. It's a brilliant way to use leftovers and is often crowned with a fried egg.")
                    .Width(Size.Units(100));
    }
}

