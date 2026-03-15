using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:23, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/23_Stepper.md", searchHints: ["stepper", "steps", "wizard", "progress", "sequence", "multi-step"])]
public class StepperApp(bool onlyBody = false) : ViewBase
{
    public StepperApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("stepper", "Stepper", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("configuration-options", "Configuration Options", 2), new ArticleHeading("allow-forward-selection", "Allow Forward Selection", 3), new ArticleHeading("dynamic-step-states", "Dynamic Step States", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Stepper").OnLinkClick(onLinkClick)
            | Lead("Display a step-by-step progress indicator with visual feedback. Perfect for wizards, multi-step [forms](app://onboarding/concepts/forms), and sequential workflows.")
            | new Markdown(
                """"
                The `Stepper` [widget](app://onboarding/concepts/widgets) displays a horizontal sequence of steps with visual indicators showing the current position, completed steps, and upcoming steps. Each step can have a symbol, icon, label, and description.
                
                ## Basic Usage
                
                Create a simple stepper with steps:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Stepper(
                        null,
                        1,
                        new StepperItem("1", null, "Step 1", "First step"),
                        new StepperItem("2", null, "Step 2", "Second step"),
                        new StepperItem("3", null, "Step 3", "Third step")
                    )
                    """",Languages.Csharp)
                | new Box().Content(new Stepper(
    null,
    1,
    new StepperItem("1", null, "Step 1", "First step"),
    new StepperItem("2", null, "Step 2", "Second step"),
    new StepperItem("3", null, "Step 3", "Third step")
))
            )
            | new Markdown("The `Stepper` constructor takes three main parameters:").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                graph LR
                    A[Stepper] --> B[onSelect<br/>Event Handler]
                    A --> C[selectedIndex<br/>Active Step Index]
                    A --> D[items<br/>StepperItem Array]
                    B --> B1["null = disabled<br/>handler = enabled"]
                    C --> C1["Zero-based index<br/>Controls highlighting"]
                    D --> D1["Symbol, Icon<br/>Label, Description"]
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Configuration Options
                
                ### Allow Forward Selection
                
                Enable `AllowSelectForward` to allow clicking on future steps:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new StepperForwardSelectionDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class StepperForwardSelectionDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var selectedIndex = UseState(1);
                    
                            var items = new[]
                            {
                                new StepperItem("1", null, "Step 1"),
                                new StepperItem("2", null, "Step 2"),
                                new StepperItem("3", null, "Step 3")
                            };
                    
                            return new Stepper(OnSelect, selectedIndex.Value, items).AllowSelectForward();
                    
                            ValueTask OnSelect(Event<Stepper, int> e)
                            {
                                selectedIndex.Set(e.Value);
                                return ValueTask.CompletedTask;
                            }
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Dynamic Step States
                
                Update step icons and states based on the current selection:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new StepperDynamicStatesDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class StepperDynamicStatesDemo : ViewBase
                    {
                        StepperItem[] GetItems(int selectedIndex) =>
                        [
                            new("1", selectedIndex > 0 ? Icons.Check : null, "Company", "Setup company"),
                            new("2", selectedIndex > 1 ? Icons.Check : null, "Raise", "Raise capital"),
                            new("3", null, "Founders", "Add founders"),
                        ];
                    
                        public override object? Build()
                        {
                            var selectedIndex = UseState(0);
                    
                            var items = GetItems(selectedIndex.Value);
                    
                            return Layout.Vertical()
                                | new Stepper(OnSelect, selectedIndex.Value, items)
                                | (Layout.Horizontal().Gap(0)
                                    | new Button("Previous").Link().OnClick(() =>
                                    {
                                        selectedIndex.Set(Math.Clamp(selectedIndex.Value - 1, 0, items.Length - 1));
                                    })
                                    | new Button("Next").Link().OnClick(() =>
                                    {
                                        selectedIndex.Set(Math.Clamp(selectedIndex.Value + 1, 0, items.Length - 1));
                                    })
                                );
                    
                            ValueTask OnSelect(Event<Stepper, int> e)
                            {
                                selectedIndex.Set(e.Value);
                                return ValueTask.CompletedTask;
                            }
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Stepper", "Ivy.StepperExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Stepper.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.FormsApp), typeof(Onboarding.Concepts.WidgetsApp)]; 
        return article;
    }
}


public class StepperForwardSelectionDemo : ViewBase
{
    public override object? Build()
    {
        var selectedIndex = UseState(1);
        
        var items = new[]
        {
            new StepperItem("1", null, "Step 1"),
            new StepperItem("2", null, "Step 2"),
            new StepperItem("3", null, "Step 3")
        };
        
        return new Stepper(OnSelect, selectedIndex.Value, items).AllowSelectForward();
        
        ValueTask OnSelect(Event<Stepper, int> e)
        {
            selectedIndex.Set(e.Value);
            return ValueTask.CompletedTask;
        }
    }
}

public class StepperDynamicStatesDemo : ViewBase
{
    StepperItem[] GetItems(int selectedIndex) =>
    [
        new("1", selectedIndex > 0 ? Icons.Check : null, "Company", "Setup company"),
        new("2", selectedIndex > 1 ? Icons.Check : null, "Raise", "Raise capital"),
        new("3", null, "Founders", "Add founders"),
    ];
    
    public override object? Build()
    {
        var selectedIndex = UseState(0);
        
        var items = GetItems(selectedIndex.Value);
        
        return Layout.Vertical()
            | new Stepper(OnSelect, selectedIndex.Value, items)
            | (Layout.Horizontal().Gap(0)
                | new Button("Previous").Link().OnClick(() =>
                {
                    selectedIndex.Set(Math.Clamp(selectedIndex.Value - 1, 0, items.Length - 1));
                })
                | new Button("Next").Link().OnClick(() =>
                {
                    selectedIndex.Set(Math.Clamp(selectedIndex.Value + 1, 0, items.Length - 1));
                })
            );
        
        ValueTask OnSelect(Event<Stepper, int> e)
        {
            selectedIndex.Set(e.Value);
            return ValueTask.CompletedTask;
        }
    }
}
