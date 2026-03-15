using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Advanced;

[App(order:6, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/07_Advanced/06_Calendar.md", searchHints: ["calendar", "events", "schedule", "agenda", "date", "time", "meeting", "appointment", "planner"])]
public class CalendarApp(bool onlyBody = false) : ViewBase
{
    public CalendarApp() : this(false)
    {
    }
    public override object? Build()
    {
        var events = new[]
        {
        new { Id = "1", Title = "Team Standup", Start = DateTime.Today.AddHours(9), End = DateTime.Today.AddHours(9).AddMinutes(30), IsAllDay = false, Category = "Work" },
        new { Id = "2", Title = "Sprint Planning", Start = DateTime.Today.AddDays(1).AddHours(10), End = DateTime.Today.AddDays(1).AddHours(12), IsAllDay = false, Category = "Work" },
        new { Id = "3", Title = "Design Review", Start = DateTime.Today.AddDays(2).AddHours(14), End = DateTime.Today.AddDays(2).AddHours(15), IsAllDay = false, Category = "Design" },
        new { Id = "4", Title = "Company Retreat", Start = DateTime.Today.AddDays(3), End = DateTime.Today.AddDays(5), IsAllDay = true, Category = "Personal" },
        new { Id = "5", Title = "Code Review", Start = DateTime.Today.AddHours(14), End = DateTime.Today.AddHours(15), IsAllDay = false, Category = "Work" },
        new { Id = "6", Title = "Gym", Start = DateTime.Today.AddDays(1).AddHours(7), End = DateTime.Today.AddDays(1).AddHours(8), IsAllDay = false, Category = "Health" },
        };
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("calendar", "Calendar", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("builder-pattern", "Builder Pattern", 2), new ArticleHeading("drag-and-drop", "Drag and Drop", 2), new ArticleHeading("views", "Views", 2), new ArticleHeading("event-handling", "Event Handling", 2), new ArticleHeading("configuration", "Configuration", 2), new ArticleHeading("calendarevent-properties", "CalendarEvent Properties", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Calendar").OnLinkClick(onLinkClick)
            | Lead("Display and manage events on an interactive calendar with month, week, day, and agenda views, drag-and-drop support, and customizable event rendering.")
            | new Markdown(
                """"
                The `Calendar` [widget](app://onboarding/concepts/widgets) provides a full-featured calendar for displaying events across multiple views. Build calendars inside [views](app://onboarding/concepts/views) and [layouts](app://onboarding/concepts/layout), and use [state](app://hooks/core/use-state) to manage event data. It supports month, week, day, and agenda views with built-in navigation, drag-and-drop event moving, and click interactions.
                
                ## Basic Usage
                
                Create a calendar by adding `CalendarEvent` items directly:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Calendar(
                        new CalendarEvent()
                            .EventId("1").Title("Team Standup")
                            .Start(DateTime.Today.AddHours(9))
                            .End(DateTime.Today.AddHours(9).AddMinutes(30)),
                        new CalendarEvent()
                            .EventId("2").Title("Sprint Planning")
                            .Start(DateTime.Today.AddDays(1).AddHours(10))
                            .End(DateTime.Today.AddDays(1).AddHours(12))
                            .Color("Blue"),
                        new CalendarEvent()
                            .EventId("3").Title("Company Retreat")
                            .Start(DateTime.Today.AddDays(3))
                            .End(DateTime.Today.AddDays(5))
                            .AllDay()
                    )
                    """",Languages.Csharp)
                | new Box().Content(new Calendar(
    new CalendarEvent()
        .EventId("1").Title("Team Standup")
        .Start(DateTime.Today.AddHours(9))
        .End(DateTime.Today.AddHours(9).AddMinutes(30)),
    new CalendarEvent()
        .EventId("2").Title("Sprint Planning")
        .Start(DateTime.Today.AddDays(1).AddHours(10))
        .End(DateTime.Today.AddDays(1).AddHours(12))
        .Color("Blue"),
    new CalendarEvent()
        .EventId("3").Title("Company Retreat")
        .Start(DateTime.Today.AddDays(3))
        .End(DateTime.Today.AddDays(5))
        .AllDay()
))
            )
            | new Markdown(
                """"
                ## Builder Pattern
                
                Use `.ToCalendar()` to create a calendar from any collection, similar to `.ToKanban()`:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    events.ToCalendar(
                        startSelector: e => e.Start,
                        endSelector: e => e.End,
                        eventIdSelector: e => e.Id
                    )
                    .Title(e => e.Title)
                    .AllDay(e => e.IsAllDay)
                    """",Languages.Csharp)
                | new Box().Content(events.ToCalendar(
    startSelector: e => e.Start,
    endSelector: e => e.End,
    eventIdSelector: e => e.Id
)
.Title(e => e.Title)
.AllDay(e => e.IsAllDay))
            )
            | new Markdown(
                """"
                ## Drag and Drop
                
                Enable drag-and-drop with `.EnableDragDrop()` and handle moves with `.OnMove()`:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new CalendarDragDropExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class CalendarDragDropExample : ViewBase
                    {
                        record Meeting(string Id, string Title, DateTime Start, DateTime End, string Color);
                    
                        public override object? Build()
                        {
                            var meetings = UseState(new[]
                            {
                                new Meeting("1", "Team Sync", DateTime.Today.AddHours(9), DateTime.Today.AddHours(10), "Blue"),
                                new Meeting("2", "Design Review", DateTime.Today.AddHours(14), DateTime.Today.AddHours(15), "Green"),
                                new Meeting("3", "Standup", DateTime.Today.AddDays(1).AddHours(9), DateTime.Today.AddDays(1).AddHours(9).AddMinutes(15), "Purple"),
                            });
                    
                            return meetings.Value
                                .ToCalendar(
                                    startSelector: m => m.Start,
                                    endSelector: m => m.End,
                                    eventIdSelector: m => m.Id)
                                .Title(m => m.Title)
                                .Color(m => m.Color)
                                .DefaultView(CalendarDisplayMode.Week)
                                .EnableDragDrop()
                                .OnMove(move =>
                                {
                                    var updated = meetings.Value.Select(m =>
                                    {
                                        if (m.Id == move.EventId?.ToString())
                                            return m with { Start = move.Start, End = move.End };
                                        return m;
                                    }).ToArray();
                                    meetings.Set(updated);
                                });
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Views
                
                The calendar supports four display modes controlled by `.DefaultView()`:
                
                - `CalendarDisplayMode.Month` — Monthly grid with event pills (default)
                - `CalendarDisplayMode.Week` — Weekly time grid with positioned event blocks
                - `CalendarDisplayMode.Day` — Single day time grid
                - `CalendarDisplayMode.Agenda` — List of upcoming events grouped by date
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Calendar(
                        new CalendarEvent()
                            .EventId("1").Title("Morning Meeting")
                            .Start(DateTime.Today.AddHours(9))
                            .End(DateTime.Today.AddHours(10)),
                        new CalendarEvent()
                            .EventId("2").Title("Lunch")
                            .Start(DateTime.Today.AddHours(12))
                            .End(DateTime.Today.AddHours(13))
                            .Color("Orange")
                    ).DefaultView(CalendarDisplayMode.Week)
                    """",Languages.Csharp)
                | new Box().Content(new Calendar(
    new CalendarEvent()
        .EventId("1").Title("Morning Meeting")
        .Start(DateTime.Today.AddHours(9))
        .End(DateTime.Today.AddHours(10)),
    new CalendarEvent()
        .EventId("2").Title("Lunch")
        .Start(DateTime.Today.AddHours(12))
        .End(DateTime.Today.AddHours(13))
        .Color("Orange")
).DefaultView(CalendarDisplayMode.Week))
            )
            | new Markdown(
                """"
                ## Event Handling
                
                Handle event clicks and slot selections:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new CalendarEventsExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class CalendarEventsExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var lastAction = UseState("No interaction yet");
                    
                            var calendar = new Calendar(
                                new CalendarEvent()
                                    .EventId("1").Title("Click Me")
                                    .Start(DateTime.Today.AddHours(10))
                                    .End(DateTime.Today.AddHours(11))
                                    .Color("Blue"),
                                new CalendarEvent()
                                    .EventId("2").Title("Or Me")
                                    .Start(DateTime.Today.AddDays(1).AddHours(14))
                                    .End(DateTime.Today.AddDays(1).AddHours(15))
                                    .Color("Green")
                            )
                            .OnEventClick(eventId => lastAction.Set($"Clicked: {eventId}"))
                            .OnSelectSlot(slot => lastAction.Set($"Selected: {slot.Start:MMM dd HH:mm} - {slot.End:HH:mm}"));
                    
                            return Layout.Vertical()
                                | new Badge(lastAction.Value)
                                | calendar;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Configuration
                
                | Method | Description |
                |--------|-------------|
                | `.DefaultView(CalendarDisplayMode)` | Set initial view (Month, Week, Day, Agenda) |
                | `.Date(DateTime)` | Set the initial display date |
                | `.ShowToolbar(bool)` | Show or hide the navigation toolbar |
                | `.EnableDragDrop(bool)` | Enable drag-and-drop event moving |
                
                ## CalendarEvent Properties
                
                | Method | Description |
                |--------|-------------|
                | `.EventId(object)` | Unique identifier for the event |
                | `.Title(string)` | Display title |
                | `.Start(DateTime)` | Event start time |
                | `.End(DateTime)` | Event end time |
                | `.AllDay(bool)` | Mark as all-day event |
                | `.Color(string)` | Event color (e.g. "Blue", "Green", "Red") |
                """").OnLinkClick(onLinkClick)
            | new WidgetDocsView("Ivy.Calendar", "Ivy.CalendarExtensions,Ivy.CalendarEventExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Calendar/Calendar.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.LayoutApp), typeof(Hooks.Core.UseStateApp)]; 
        return article;
    }
}


public class CalendarDragDropExample : ViewBase
{
    record Meeting(string Id, string Title, DateTime Start, DateTime End, string Color);

    public override object? Build()
    {
        var meetings = UseState(new[]
        {
            new Meeting("1", "Team Sync", DateTime.Today.AddHours(9), DateTime.Today.AddHours(10), "Blue"),
            new Meeting("2", "Design Review", DateTime.Today.AddHours(14), DateTime.Today.AddHours(15), "Green"),
            new Meeting("3", "Standup", DateTime.Today.AddDays(1).AddHours(9), DateTime.Today.AddDays(1).AddHours(9).AddMinutes(15), "Purple"),
        });

        return meetings.Value
            .ToCalendar(
                startSelector: m => m.Start,
                endSelector: m => m.End,
                eventIdSelector: m => m.Id)
            .Title(m => m.Title)
            .Color(m => m.Color)
            .DefaultView(CalendarDisplayMode.Week)
            .EnableDragDrop()
            .OnMove(move =>
            {
                var updated = meetings.Value.Select(m =>
                {
                    if (m.Id == move.EventId?.ToString())
                        return m with { Start = move.Start, End = move.End };
                    return m;
                }).ToArray();
                meetings.Set(updated);
            });
    }
}

public class CalendarEventsExample : ViewBase
{
    public override object? Build()
    {
        var lastAction = UseState("No interaction yet");

        var calendar = new Calendar(
            new CalendarEvent()
                .EventId("1").Title("Click Me")
                .Start(DateTime.Today.AddHours(10))
                .End(DateTime.Today.AddHours(11))
                .Color("Blue"),
            new CalendarEvent()
                .EventId("2").Title("Or Me")
                .Start(DateTime.Today.AddDays(1).AddHours(14))
                .End(DateTime.Today.AddDays(1).AddHours(15))
                .Color("Green")
        )
        .OnEventClick(eventId => lastAction.Set($"Clicked: {eventId}"))
        .OnSelectSlot(slot => lastAction.Set($"Selected: {slot.Start:MMM dd HH:mm} - {slot.End:HH:mm}"));

        return Layout.Vertical()
            | new Badge(lastAction.Value)
            | calendar;
    }
}
