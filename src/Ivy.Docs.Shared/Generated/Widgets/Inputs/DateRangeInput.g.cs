using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Inputs;

[App(order:8, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/04_Inputs/08_DateRangeInput.md", searchHints: ["calendar", "date", "picker", "range", "period", "dates"])]
public class DateRangeInputApp(bool onlyBody = false) : ViewBase
{
    public DateRangeInputApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("daterangeinput", "DateRangeInput", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("supported-types", "Supported Types", 2), new ArticleHeading("variants", "Variants", 2), new ArticleHeading("format", "Format", 2), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# DateRangeInput").OnLinkClick(onLinkClick)
            | Lead("Select date ranges with an intuitive calendar [interface](app://onboarding/concepts/views) for start and end dates, perfect for filtering and event scheduling.")
            | new Markdown(
                """"
                The `DateRangeInput` [widget](app://onboarding/concepts/widgets) allows users to select a range of dates. It provides a calendar interface for both start and end date selection, making it ideal for filtering data by date ranges or scheduling events.
                
                ## Basic Usage
                
                Here's a simple example of a `DateRangeInput` that allows users to select a date range:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicDateRangeDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var dateRangeState = UseState(() => (from: DateTime.Today.AddDays(-7), to: DateTime.Today));
                            var start = dateRangeState.Value.Item1;
                            var end = dateRangeState.Value.Item2;
                            var span = $"That's {(end-start).Days} days";
                            return Layout.Vertical()
                                    | dateRangeState.ToDateRangeInput()
                                    | Text.P(span).Large();
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicDateRangeDemo())
            )
            | new Markdown(
                """"
                As can be seen, the starting and ending date of the date range can be extracted using the
                `DateTimeRange.Value.Item1` and `DateTimeRange.Value.Item2`
                
                ## Supported Types
                
                DateRangeInput supports DateOnly tuple types:
                
                - `(DateOnly, DateOnly)` - Date-only range
                - `(DateOnly?, DateOnly?)` - Nullable date-only range
                
                ## Variants
                
                The `DateRangeInput` can be customized with various states. The following example demonstrates **Disabled**, **Invalid**, and **Nullable** states:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class DateRangeVariantsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var dateRange = UseState(() =>
                                (from: DateTime.Today.AddDays(-7), to: DateTime.Today));
                    
                            var nullableRange = UseState<(DateOnly?, DateOnly?)>(() =>
                                (DateOnly.FromDateTime(DateTime.Today.AddDays(-7)),
                                 DateOnly.FromDateTime(DateTime.Today)));
                    
                            return Layout.Vertical().Gap(4)
                                | Text.P("Disabled State").Small()
                                | dateRange.ToDateRangeInput().Disabled()
                                | Text.P("Invalid State").Small()
                                | dateRange.ToDateRangeInput().Invalid("Invalid date range")
                                | Text.P("Nullable State").Small()
                                | nullableRange.ToDateRangeInput();
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new DateRangeVariantsDemo())
            )
            | new Markdown(
                """"
                ## Format
                
                To change the format of selected dates the `Format` function needs to be used.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class FormatDateRangeDemo : ViewBase
                    {
                        public override object? Build()
                        {
                             var dateRangeState = UseState(() =>
                                (from: DateTime.Today.AddDays(-7), to: DateTime.Today));
                             return Layout.Vertical()
                                     | dateRangeState.ToDateRangeInput()
                                                      .Format("yyyy-MM-dd");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new FormatDateRangeDemo())
            )
            | new WidgetDocsView("Ivy.DateRangeInput", "Ivy.DateRangeInputExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Inputs/DateRangeInput.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Hotel Booking Example",
                Vertical().Gap(4)
                | new Markdown("A realistic example demonstrating a hotel booking form with date range selection, validation, and price calculation:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new HotelBookingDemo())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class HotelBookingDemo : ViewBase
                        {
                            private const decimal PricePerNight = 120m;
                        
                            public override object? Build()
                            {
                                var bookingRange = UseState<(DateOnly?, DateOnly?)>(() => (null, null));
                        
                                var from = bookingRange.Value.Item1;
                                var to = bookingRange.Value.Item2;
                        
                                var nights = from.HasValue && to.HasValue
                                    ? (to.Value.ToDateTime(TimeOnly.MinValue) - from.Value.ToDateTime(TimeOnly.MinValue)).Days
                                    : 0;
                        
                                var isValid = nights >= 1;
                                var errorMessage = !isValid && from.HasValue ? "Minimum stay is 1 night" : "";
                                var totalPrice = nights * PricePerNight;
                        
                                return Layout.Vertical().Gap(4)
                                    | Text.P("Book Your Stay").Large().Bold()
                                    | bookingRange.ToDateRangeInput()
                                        .Placeholder("Select check-in and check-out dates")
                                        .Format("MMM dd, yyyy")
                                        .Invalid(errorMessage)
                                    | (nights > 0
                                        ? Layout.Horizontal().Gap(2)
                                            | Text.P($"{nights} night(s)").Small().Muted()
                                            | Text.P($"Total: ${totalPrice}").Small().Bold()
                                        : null);
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.WidgetsApp)]; 
        return article;
    }
}


public class BasicDateRangeDemo : ViewBase
{
    public override object? Build()
    {    
        var dateRangeState = UseState(() => (from: DateTime.Today.AddDays(-7), to: DateTime.Today));
        var start = dateRangeState.Value.Item1;
        var end = dateRangeState.Value.Item2;
        var span = $"That's {(end-start).Days} days";
        return Layout.Vertical()
                | dateRangeState.ToDateRangeInput()
                | Text.P(span).Large();
    }    
}        

public class DateRangeVariantsDemo : ViewBase
{
    public override object? Build()
    {
        var dateRange = UseState(() => 
            (from: DateTime.Today.AddDays(-7), to: DateTime.Today));
        
        var nullableRange = UseState<(DateOnly?, DateOnly?)>(() => 
            (DateOnly.FromDateTime(DateTime.Today.AddDays(-7)), 
             DateOnly.FromDateTime(DateTime.Today)));

        return Layout.Vertical().Gap(4)
            | Text.P("Disabled State").Small()
            | dateRange.ToDateRangeInput().Disabled()
            | Text.P("Invalid State").Small()
            | dateRange.ToDateRangeInput().Invalid("Invalid date range")
            | Text.P("Nullable State").Small()
            | nullableRange.ToDateRangeInput();
    }
}

public class FormatDateRangeDemo : ViewBase
{
    public override object? Build()
    {   
         var dateRangeState = UseState(() => 
            (from: DateTime.Today.AddDays(-7), to: DateTime.Today));
         return Layout.Vertical()
                 | dateRangeState.ToDateRangeInput()
                                  .Format("yyyy-MM-dd");
    }    
}        

public class HotelBookingDemo : ViewBase
{
    private const decimal PricePerNight = 120m;
    
    public override object? Build()
    {
        var bookingRange = UseState<(DateOnly?, DateOnly?)>(() => (null, null));
        
        var from = bookingRange.Value.Item1;
        var to = bookingRange.Value.Item2;
        
        var nights = from.HasValue && to.HasValue 
            ? (to.Value.ToDateTime(TimeOnly.MinValue) - from.Value.ToDateTime(TimeOnly.MinValue)).Days 
            : 0;
        
        var isValid = nights >= 1;
        var errorMessage = !isValid && from.HasValue ? "Minimum stay is 1 night" : "";
        var totalPrice = nights * PricePerNight;

        return Layout.Vertical().Gap(4)
            | Text.P("Book Your Stay").Large().Bold()
            | bookingRange.ToDateRangeInput()
                .Placeholder("Select check-in and check-out dates")
                .Format("MMM dd, yyyy")
                .Invalid(errorMessage)
            | (nights > 0 
                ? Layout.Horizontal().Gap(2)
                    | Text.P($"{nights} night(s)").Small().Muted()
                    | Text.P($"Total: ${totalPrice}").Small().Bold()
                : null);
    }
}
