using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Inputs;

[App(order:13, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/04_Inputs/13_FeedbackInput.md", searchHints: ["rating", "stars", "review", "feedback", "score", "evaluation"])]
public class FeedbackInputApp(bool onlyBody = false) : ViewBase
{
    public FeedbackInputApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("feedbackinput", "FeedbackInput", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("variants", "Variants", 2), new ArticleHeading("event-handling", "Event Handling", 2), new ArticleHeading("styling-and-customization", "Styling and Customization", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# FeedbackInput").OnLinkClick(onLinkClick)
            | Lead("Collect user feedback with combined rating and comment inputs, perfect for surveys, reviews, and [feedback forms](app://onboarding/concepts/forms).")
            | new Markdown(
                """"
                The `FeedbackInput` [widget](app://onboarding/concepts/widgets) provides a specialized input for collecting user feedback. It typically includes rating options and a text field for comments, making it ideal for surveys, reviews, and feedback forms.
                
                ## Basic Usage
                
                Here's a simple example of a `FeedbackInput` with the default `Stars` variant:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicFeedbackDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var rating = UseState(3);
                            return new FeedbackInput<int>(rating);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicFeedbackDemo())
            )
            | new Markdown(
                """"
                ## Variants
                
                `FeedbackInput`s come in several variants to suit different use cases:
                For star style feedback ( 1 star to 5 stars) the variant `FeedbackInputVariant.Stars` should be used.
                For binary style feedback ( yes, no, liked/disliked, recommended/not-recommended) type feedback
                the variant `FeedbackInputVariant.Thumbs` should be used. `FeedbackInputVariant.Emojis` should be used
                for collecting sentiment analysis feedbacks about anything.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class FeedbackDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            // Initial guess feedbacks
                            var starFeedback = UseState(3);
                            var thumbsFeedback = UseState(true);
                            var emojiFeedback = UseState(4);
                            return Layout.Vertical()
                                    | H3 ("Simple movie review")
                                    | new FeedbackInput<bool>(thumbsFeedback)
                                          .Variant(FeedbackInputVariant.Thumbs)
                                          .WithField()
                                          .Label("Did you like the movie ?")
                                    | new FeedbackInput<int>(starFeedback)
                                          .Variant(FeedbackInputVariant.Stars)
                                          .WithField()
                                          .Label("How would you like to rate the movie ?")
                                    | new FeedbackInput<int>(emojiFeedback)
                                          .Variant(FeedbackInputVariant.Emojis)
                                          .WithField()
                                          .Label("How do you feel after seeing the movie ?");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new FeedbackDemo())
            )
            | new Markdown(
                """"
                ## Event Handling
                
                The following example shows how change events can be handled for `FeedbackInput`s.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class FeedbackHandling: ViewBase
                    {
                        public override object? Build()
                        {
                            var feedbackState = UseState(1);
                            var exclamation = UseState("");
                            exclamation.Set(feedbackState.Value switch
                            {
                                0 => "No rating yet",
                                1 => "Seriously?",
                                2 => "Oh! is it that bad?",
                                3 => "Ah! you almost liked it!",
                                4 => "Cool! Tell me more!",
                                5 => "WOW! Would you recommend it?",
                                _ => "Invalid rating"
                            });
                            return Layout.Vertical()
                                    | new FeedbackInput<int>(feedbackState)
                                    | Text.Block(exclamation);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new FeedbackHandling())
            )
            | new Markdown(
                """"
                ## Styling and Customization
                
                `FeedbackInput`s can be customized with various styling options, including `Disabled` and `Invalid` states:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class StyledFeedbackDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var state = UseState(3);
                            return Layout.Vertical()
                                    | new FeedbackInput<int>(state)
                                          .Disabled()
                                          .WithField().Label("Disabled")
                                    | new FeedbackInput<int>(state)
                                          .Invalid("Validation error")
                                          .WithField().Label("Invalid");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new StyledFeedbackDemo())
            )
            | new WidgetDocsView("Ivy.FeedbackInput", "Ivy.FeedbackInputExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Inputs/FeedbackInput.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.FormsApp), typeof(Onboarding.Concepts.WidgetsApp)]; 
        return article;
    }
}


public class BasicFeedbackDemo : ViewBase
{
    public override object? Build()
    {
        var rating = UseState(3);
        return new FeedbackInput<int>(rating);
    }
}

public class FeedbackDemo : ViewBase
{
    public override object? Build()
    {
        // Initial guess feedbacks
        var starFeedback = UseState(3);
        var thumbsFeedback = UseState(true);
        var emojiFeedback = UseState(4);
        return Layout.Vertical()
                | H3 ("Simple movie review")
                | new FeedbackInput<bool>(thumbsFeedback)
                      .Variant(FeedbackInputVariant.Thumbs)
                      .WithField()
                      .Label("Did you like the movie ?")
                | new FeedbackInput<int>(starFeedback)
                      .Variant(FeedbackInputVariant.Stars)
                      .WithField()
                      .Label("How would you like to rate the movie ?")
                | new FeedbackInput<int>(emojiFeedback)
                      .Variant(FeedbackInputVariant.Emojis)
                      .WithField()
                      .Label("How do you feel after seeing the movie ?");
    }
}

public class FeedbackHandling: ViewBase
{
    public override object? Build()
    {
        var feedbackState = UseState(1);
        var exclamation = UseState("");
        exclamation.Set(feedbackState.Value switch
        {
            0 => "No rating yet",
            1 => "Seriously?",
            2 => "Oh! is it that bad?",
            3 => "Ah! you almost liked it!",
            4 => "Cool! Tell me more!",
            5 => "WOW! Would you recommend it?",
            _ => "Invalid rating"
        });
        return Layout.Vertical()
                | new FeedbackInput<int>(feedbackState)
                | Text.Block(exclamation);
    }
}

public class StyledFeedbackDemo : ViewBase
{
    public override object? Build()
    {
        var state = UseState(3);
        return Layout.Vertical()
                | new FeedbackInput<int>(state)
                      .Disabled()
                      .WithField().Label("Disabled")
                | new FeedbackInput<int>(state)
                      .Invalid("Validation error")
                      .WithField().Label("Invalid");
    }
}
