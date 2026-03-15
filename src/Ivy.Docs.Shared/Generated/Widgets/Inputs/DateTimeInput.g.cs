using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Inputs;

[App(order:7, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/04_Inputs/07_DateTimeInput.md", searchHints: ["calendar", "date", "time", "picker", "datetime", "timestamp", "month", "week", "year"])]
public class DateTimeInputApp(bool onlyBody = false) : ViewBase
{
    public DateTimeInputApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("datetimeinput", "DateTimeInput", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("variants", "Variants", 2), new ArticleHeading("supported-state-types", "Supported State Types", 2), new ArticleHeading("event-handling", "Event Handling", 2), new ArticleHeading("format", "Format", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# DateTimeInput").OnLinkClick(onLinkClick)
            | Lead("Capture dates and times with intuitive picker [interfaces](app://onboarding/concepts/views) supporting calendar selection, time input, and combined date-time entry.")
            | new Markdown(
                """"
                The `DateTimeInput` [widget](app://onboarding/concepts/widgets) provides a comprehensive date and time picker interface with support for different variants. It allows users to select dates, months, weeks, years from a calendar, times from a time selector, or both date and time together, making it ideal for scheduling, event creation, reporting periods, and [form](app://onboarding/concepts/forms) inputs.
                
                ## Basic Usage
                
                Here's a simple example of a DateTimeInput that allows users to select a date:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicDateUsageDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var dateState = UseState(DateTime.Today);
                            var daysBetween = dateState.Value.Subtract(DateTime.Today).Days;
                            return Layout.Vertical()
                                    | dateState.ToDateInput()
                                               .WithField()
                                               .Label("When is your birthday?")
                                    | Text.Html($"<i>That's <b>{daysBetween}</b> days from now!");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicDateUsageDemo())
            )
            | new Markdown(
                """"
                ## Variants
                
                `DateTimeInput` supports six variants: Date, DateTime, Time, Month, Week, and Year. The following extension methods are available:
                
                - `ToDateInput()`: Calendar picker for dates only.
                - `ToDateTimeInput()`: Calendar picker with time input.
                - `ToTimeInput()`: Time picker only.
                - `ToMonthInput()`: Month picker with year navigation; selects the 1st of the chosen month.
                - `ToWeekInput()`: Calendar with week numbers; selects the Monday of the chosen week.
                - `ToYearInput()`: Year picker with decade navigation; selects January 1st of the chosen year.
                
                The following demo shows the core variants in action:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class DateTimeVariantsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var dateState = UseState(DateTime.Today.Date);
                            var timeState = UseState(DateTime.Now);
                            var dateTimeState = UseState(DateTime.Today);
                            var monthState = UseState(DateTime.Today);
                            var weekState = UseState(DateTime.Today);
                            var yearState = UseState(DateTime.Today);
                    
                            return Layout.Vertical()
                                    | dateState.ToDateInput()
                                           .Format("dd/MM/yyyy")
                                           .WithField()
                                           .Label("Date")
                                    | dateTimeState.ToDateTimeInput()
                                           .Format("dd/MM/yyyy HH:mm:ss")
                                           .WithField()
                                           .Label("DateTime")
                                    | timeState.ToTimeInput()
                                           .WithField()
                                           .Label("Time")
                                    | monthState.ToMonthInput()
                                           .WithField()
                                           .Label("Month")
                                    | weekState.ToWeekInput()
                                           .WithField()
                                           .Label("Week")
                                    | yearState.ToYearInput()
                                           .WithField()
                                           .Label("Year");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new DateTimeVariantsDemo())
            )
            | new Markdown(
                """"
                ## Supported State Types
                
                `DateTimeInput` supports various date and time types:
                
                - `DateTime` and `DateTime?`
                - `DateTimeOffset` and `DateTimeOffset?`
                - `DateOnly` and `DateOnly?`
                - `TimeOnly` and `TimeOnly?`
                - `string` (for ISO format)
                
                ## Event Handling
                
                `DateTimeInput` can handle change and blur events:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var dateState = UseState(DateTime.Now);
                var onChangeHandler = (Event<IInput<DateTime>, DateTime> e) =>
                {
                    dateState.Set(e.Value);
                };
                return dateState.ToDateTimeInput().OnChange(onChangeHandler);
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Format
                
                `DateTimeInput` can be customized with various formats. So the captured value can be
                expressed in any format as supported by .NET.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class FormatDemo : ViewBase
                    {
                         public override object? Build()
                         {
                             var monthDateYear = UseState(DateTime.Today.Date);
                             var yearMonthDate = UseState(DateTime.Today.Date);
                    
                             return Layout.Vertical()
                                    | monthDateYear.ToDateInput()
                                                    .Format("MM/dd/yyyy")
                                                    .WithField()
                                                    .Label("MM/dd/yyyy")
                                    | yearMonthDate.ToDateInput()
                                                    .Placeholder("yyyy/MMM/dd")
                                                    .Format("yyyy/MMM/dd")
                                                    .WithField()
                                                    .Label("yyyy/MMM/dd");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new FormatDemo())
            )
            | new WidgetDocsView("Ivy.DateTimeInput", "Ivy.DateTimeInputExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Inputs/DateTimeInput.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.FormsApp)]; 
        return article;
    }
}


public class BasicDateUsageDemo : ViewBase
{
    public override object? Build()
    {
        var dateState = UseState(DateTime.Today);
        var daysBetween = dateState.Value.Subtract(DateTime.Today).Days;
        return Layout.Vertical() 
                | dateState.ToDateInput()
                           .WithField()
                           .Label("When is your birthday?")
                | Text.Html($"<i>That's <b>{daysBetween}</b> days from now!");
    }
}    

public class DateTimeVariantsDemo : ViewBase
{    
    public override object? Build()
    {    
        var dateState = UseState(DateTime.Today.Date);
        var timeState = UseState(DateTime.Now);
        var dateTimeState = UseState(DateTime.Today);
        var monthState = UseState(DateTime.Today);
        var weekState = UseState(DateTime.Today);
        var yearState = UseState(DateTime.Today);
        
        return Layout.Vertical()
                | dateState.ToDateInput()
                       .Format("dd/MM/yyyy")
                       .WithField()
                       .Label("Date")
                | dateTimeState.ToDateTimeInput()
                       .Format("dd/MM/yyyy HH:mm:ss")
                       .WithField()
                       .Label("DateTime")
                | timeState.ToTimeInput()
                       .WithField()
                       .Label("Time")
                | monthState.ToMonthInput()
                       .WithField()
                       .Label("Month")
                | weekState.ToWeekInput()
                       .WithField()
                       .Label("Week")
                | yearState.ToYearInput()
                       .WithField()
                       .Label("Year");
    }    
}                

public class FormatDemo : ViewBase
{
     public override object? Build()
     {    
         var monthDateYear = UseState(DateTime.Today.Date);
         var yearMonthDate = UseState(DateTime.Today.Date);
         
         return Layout.Vertical()
                | monthDateYear.ToDateInput()
                                .Format("MM/dd/yyyy")
                                .WithField()
                                .Label("MM/dd/yyyy")
                | yearMonthDate.ToDateInput()
                                .Placeholder("yyyy/MMM/dd")
                                .Format("yyyy/MMM/dd")
                                .WithField()
                                .Label("yyyy/MMM/dd");
    }
}    
