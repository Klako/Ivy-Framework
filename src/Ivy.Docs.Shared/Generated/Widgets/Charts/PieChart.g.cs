using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Charts;

[App(order:4, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/06_Charts/04_PieChart.md", searchHints: ["visualization", "graph", "analytics", "data", "donut", "statistics"])]
public class PieChartApp(bool onlyBody = false) : ViewBase
{
    public PieChartApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("piechart", "PieChart", 1), new ArticleHeading("creating-a-pie-or-a-donut-chart", "Creating a Pie or a Donut chart", 2), new ArticleHeading("donut-chart-with-custom-labels", "Donut Chart with Custom Labels", 3), new ArticleHeading("drill-down-chart", "Drill down chart", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# PieChart").OnLinkClick(onLinkClick)
            | Lead("Represent parts of a whole with pie and donut charts, supporting custom labels and drill-down interactions.")
            | new Markdown(
                """"
                `PieChart`s represent parts of a whole. Build chart [views](app://onboarding/concepts/views) inside [layouts](app://onboarding/concepts/layout) and use [state](app://hooks/core/use-state) for dynamic data. See [Charts](app://onboarding/concepts/charts) for an overview of Ivy chart widgets. Each slice is drawn from the provided data.
                
                The following example showcases a sample case where possible sale data from a store is listed.
                The pie chart shows these data.
                
                ## Creating a Pie or a Donut chart
                
                To create a pie chart or a donut chart easily the function `ToPieChart` should be used. It takes two
                lambda expressions to project the keys of the Pie and the values. The type of the pie can be
                altered via the `PieChartStyles` enum.
                
                The following example shows how to create a Pie and a Donut chart easily.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new PieChartDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class PieChartDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            //Sales data
                            var data = new[]
                            {
                                new { Month = "January", Desktop = 186, Mobile = 100 },
                                new { Month = "February", Desktop = 305, Mobile = 200 },
                                new { Month = "March", Desktop = 237, Mobile = 300 },
                            };
                             //Showing default placement of the legend at the bottom of the chart
                             return Layout.Vertical()
                                | Text.P("Mobile sales over Q1(January-March)").Large()
                               | data.ToPieChart
                                     (
                                        e => e.Month,
                                        e => e.Sum(f => f.Mobile),
                                        PieChartStyles.Dashboard
                                    )
                                    .Toolbox(new Toolbox())
                               | Text.P("Desktop sales over Q1(January-March)").Large()
                               // Showing custom placement of the legend at the right bottom of the chart
                               | data.ToPieChart
                                     (
                                        e => e.Month,
                                        e => e.Sum(f => f.Desktop),
                                        PieChartStyles.Donut
                                    );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Donut Chart with Custom Labels
                
                The following example demonstrates the enhanced pie chart label customization feature with multiple label layers.
                This donut chart shows budget allocation data with currency-formatted values outside the chart and department names inside.
                The example showcases independent positioning, styling, and formatting for each label layer, making complex data presentations more readable and professional.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new DonutChartWithCustomLabelsView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class DonutChartWithCustomLabelsView : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = new[]
                            {
                                new PieChartData("Revenue", 1250000),
                                new PieChartData("Marketing", 450000),
                                new PieChartData("Operations", 320000),
                                new PieChartData("R&D", 280000),
                                new PieChartData("Admin", 150000),
                                new PieChartData("Sales", 380000),
                                new PieChartData("Customer Support", 220000),
                                new PieChartData("IT Infrastructure", 180000),
                                new PieChartData("Legal", 95000),
                                new PieChartData("HR", 120000),
                                new PieChartData("Finance", 160000),
                                new PieChartData("Quality Assurance", 140000)
                            };
                    
                            var totalValue = data.Sum(d => d.Measure);
                    
                            return new PieChart(data)
                                .Pie(new Pie(nameof(PieChartData.Measure), nameof(PieChartData.Dimension))
                                    .InnerRadius("40%")
                                    .OuterRadius("90%")
                                    .Animated(true)
                                    .LabelList(new LabelList(nameof(PieChartData.Measure))
                                        .Position(Positions.Outside)
                                        .Fill(Colors.Blue)
                                        .FontSize(11)
                                        .NumberFormat("$0,0"))
                                    .LabelList(new LabelList(nameof(PieChartData.Dimension))
                                        .Position(Positions.Inside)
                                        .Fill(Colors.White)
                                        .FontSize(9)
                                        .FontFamily("Arial"))
                                )
                                .ColorScheme(ColorScheme.Default)
                                .Tooltip(new ChartTooltip().Animated(true))
                                .Legend(new Legend().IconType(Legend.IconTypes.Rect))
                                .Total(totalValue, "Total Budget");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                The legend can be placed in any of the nine positions by altering the values of the [Align](app://api-reference/ivy/align) enums.
                Also, by default, the inner and outer radius is same resulting in a circle. However, as
                can be seen in this example, these values can be altered to create a custom donut. The function
                `Tooltip` makes sure that the labels show up on mouse hover. The `Animated` function makes a nice animation
                when users hover on that specific part of the pie chart.
                
                ### Drill down chart
                
                The following example shows how these combinations of charts can be used in a realistic example
                for showing how populated some countries are. It uses [UseState](app://hooks/core/use-state) and [ToSelectInput](app://onboarding/concepts/forms) to let users pick a country and see the corresponding drill-down pie.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new DrillDownDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    
                    public class DrillDownDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            // Country population summary (in millions)
                            var countryData = new []
                            {
                                new PieChartData("United States", 333),
                                new PieChartData("Sweden", 10),
                                new PieChartData("China", 1412),
                                new PieChartData("Brazil", 215),
                                new PieChartData("Canada", 38),
                                new PieChartData("Germany", 83)
                            };
                    
                            // Population data for drill-down chart
                            var populationData = new []
                            {
                                // United States - Total: 333 million
                                new { State = "California", Country = "United States", Population = 39 },
                                new { State = "Texas", Country = "United States", Population = 30 },
                                new { State = "Florida", Country = "United States", Population = 23 },
                                new { State = "New York", Country = "United States", Population = 19 },
                                new { State = "Pennsylvania", Country = "United States", Population = 13 },
                                new { State = "Illinois", Country = "United States", Population = 13 },
                                new { State = "Ohio", Country = "United States", Population = 12 },
                                new { State = "Georgia", Country = "United States", Population = 11 },
                                new { State = "North Carolina", Country = "United States", Population = 11 },
                                new { State = "Michigan", Country = "United States", Population = 10 },
                                new { State = "Other States", Country = "United States", Population = 152 },
                    
                                // Sweden - Total: 10 million
                                new { State = "Stockholm", Country = "Sweden", Population = 2 },
                                new { State = "Västra Götaland", Country = "Sweden", Population = 2 },
                                new { State = "Skåne", Country = "Sweden", Population = 1 },
                                new { State = "Uppsala", Country = "Sweden", Population = 1 },
                                new { State = "Östergötland", Country = "Sweden", Population = 1 },
                                new { State = "Other Counties", Country = "Sweden", Population = 3 },
                    
                                // China - Total: 1412 million
                                new { State = "Guangdong", Country = "China", Population = 127 },
                                new { State = "Shandong", Country = "China", Population = 102 },
                                new { State = "Henan", Country = "China", Population = 99 },
                                new { State = "Jiangsu", Country = "China", Population = 85 },
                                new { State = "Sichuan", Country = "China", Population = 84 },
                                new { State = "Hebei", Country = "China", Population = 75 },
                                new { State = "Hunan", Country = "China", Population = 66 },
                                new { State = "Anhui", Country = "China", Population = 61 },
                                new { State = "Hubei", Country = "China", Population = 58 },
                                new { State = "Zhejiang", Country = "China", Population = 57 },
                                new { State = "Other Provinces", Country = "China", Population = 598 },
                    
                                // Brazil - Total: 215 million
                                new { State = "São Paulo", Country = "Brazil", Population = 46 },
                                new { State = "Minas Gerais", Country = "Brazil", Population = 21 },
                                new { State = "Rio de Janeiro", Country = "Brazil", Population = 17 },
                                new { State = "Bahia", Country = "Brazil", Population = 15 },
                                new { State = "Paraná", Country = "Brazil", Population = 12 },
                                new { State = "Rio Grande do Sul", Country = "Brazil", Population = 11 },
                                new { State = "Pernambuco", Country = "Brazil", Population = 10 },
                                new { State = "Ceará", Country = "Brazil", Population = 9 },
                                new { State = "Pará", Country = "Brazil", Population = 9 },
                                new { State = "Santa Catarina", Country = "Brazil", Population = 7 },
                                new { State = "Other States", Country = "Brazil", Population = 58 },
                    
                                // Canada - Total: 38 million
                                new { State = "Ontario", Country = "Canada", Population = 15 },
                                new { State = "Quebec", Country = "Canada", Population = 9 },
                                new { State = "British Columbia", Country = "Canada", Population = 5 },
                                new { State = "Alberta", Country = "Canada", Population = 4 },
                                new { State = "Manitoba", Country = "Canada", Population = 1 },
                                new { State = "Saskatchewan", Country = "Canada", Population = 1 },
                                new { State = "Nova Scotia", Country = "Canada", Population = 1 },
                                new { State = "New Brunswick", Country = "Canada", Population = 1 },
                                new { State = "Newfoundland and Labrador", Country = "Canada", Population = 1 },
                    
                                // Germany - Total: 83 million
                                new { State = "North Rhine-Westphalia", Country = "Germany", Population = 18 },
                                new { State = "Bavaria", Country = "Germany", Population = 13 },
                                new { State = "Baden-Württemberg", Country = "Germany", Population = 11 },
                                new { State = "Lower Saxony", Country = "Germany", Population = 8 },
                                new { State = "Hesse", Country = "Germany", Population = 6 },
                                new { State = "Rhineland-Palatinate", Country = "Germany", Population = 4 },
                                new { State = "Saxony", Country = "Germany", Population = 4 },
                                new { State = "Berlin", Country = "Germany", Population = 4 },
                                new { State = "Schleswig-Holstein", Country = "Germany", Population = 3 },
                                new { State = "Brandenburg", Country = "Germany", Population = 3 },
                                new { State = "Thuringia", Country = "Germany", Population = 2 },
                                new { State = "Saxony-Anhalt", Country = "Germany", Population = 2 },
                                new { State = "Mecklenburg-Vorpommern", Country = "Germany", Population = 2 },
                                new { State = "Hamburg", Country = "Germany", Population = 2 },
                                new { State = "Bremen", Country = "Germany", Population = 1 }
                            };
                    
                            var countries = populationData
                                                .Select(t => t.Country)
                                                .Distinct()
                                                .ToArray();
                    
                            var country = UseState(countries[0]);
                    
                            var countryInput = country.ToSelectInput(countries.ToOptions());
                    
                            // Get states for selected country and convert to PieChartData format
                            var selectedCountryStates = populationData
                                .Where(t => t.Country.Equals(country.Value))
                                .Select(t => new PieChartData(t.State, t.Population))
                                .ToArray();
                    
                            return Layout.Vertical()
                                    | (Layout.Horizontal()
                                    | (Layout.Vertical()
                                       | Text.P("Countries Population").Small()
                                       | new PieChart(countryData)
                                            .Pie("Measure", "Dimension")
                                            .Tooltip())
                                    | (Layout.Vertical()
                                        | Text.P($"{country.Value} - States Population").Small()
                                        | new PieChart(selectedCountryStates)
                                                .Pie("Measure", "Dimension")
                                                .Tooltip()))
                                    | countryInput;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.PieChart", "Ivy.PieChartExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Charts/PieChart.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.LayoutApp), typeof(Hooks.Core.UseStateApp), typeof(Onboarding.Concepts.ChartsApp), typeof(ApiReference.Ivy.AlignApp), typeof(Onboarding.Concepts.FormsApp)]; 
        return article;
    }
}


public class PieChartDemo : ViewBase
{
    public override object? Build()
    {
        //Sales data
        var data = new[]
        {
            new { Month = "January", Desktop = 186, Mobile = 100 },
            new { Month = "February", Desktop = 305, Mobile = 200 },
            new { Month = "March", Desktop = 237, Mobile = 300 },
        };
         //Showing default placement of the legend at the bottom of the chart
         return Layout.Vertical()
            | Text.P("Mobile sales over Q1(January-March)").Large()
           | data.ToPieChart
                 (
                    e => e.Month,
                    e => e.Sum(f => f.Mobile),
                    PieChartStyles.Dashboard
                )
                .Toolbox(new Toolbox())
           | Text.P("Desktop sales over Q1(January-March)").Large()
           // Showing custom placement of the legend at the right bottom of the chart
           | data.ToPieChart
                 (
                    e => e.Month,
                    e => e.Sum(f => f.Desktop),
                    PieChartStyles.Donut
                );
    }
}

public class DonutChartWithCustomLabelsView : ViewBase
{
    public override object? Build()
    {
        var data = new[]
        {
            new PieChartData("Revenue", 1250000),
            new PieChartData("Marketing", 450000),
            new PieChartData("Operations", 320000),
            new PieChartData("R&D", 280000),
            new PieChartData("Admin", 150000),
            new PieChartData("Sales", 380000),
            new PieChartData("Customer Support", 220000),
            new PieChartData("IT Infrastructure", 180000),
            new PieChartData("Legal", 95000),
            new PieChartData("HR", 120000),
            new PieChartData("Finance", 160000),
            new PieChartData("Quality Assurance", 140000)
        };

        var totalValue = data.Sum(d => d.Measure);

        return new PieChart(data)
            .Pie(new Pie(nameof(PieChartData.Measure), nameof(PieChartData.Dimension))
                .InnerRadius("40%")
                .OuterRadius("90%")
                .Animated(true)
                .LabelList(new LabelList(nameof(PieChartData.Measure))
                    .Position(Positions.Outside)
                    .Fill(Colors.Blue)
                    .FontSize(11)
                    .NumberFormat("$0,0"))
                .LabelList(new LabelList(nameof(PieChartData.Dimension))
                    .Position(Positions.Inside)
                    .Fill(Colors.White)
                    .FontSize(9)
                    .FontFamily("Arial"))
            )
            .ColorScheme(ColorScheme.Default)
            .Tooltip(new ChartTooltip().Animated(true))
            .Legend(new Legend().IconType(Legend.IconTypes.Rect))
            .Total(totalValue, "Total Budget");
    }
}


public class DrillDownDemo : ViewBase
{
    public override object? Build()
    {
        // Country population summary (in millions)
        var countryData = new []
        {
            new PieChartData("United States", 333),
            new PieChartData("Sweden", 10),
            new PieChartData("China", 1412),
            new PieChartData("Brazil", 215),
            new PieChartData("Canada", 38),
            new PieChartData("Germany", 83)
        };

        // Population data for drill-down chart
        var populationData = new []
        {
            // United States - Total: 333 million
            new { State = "California", Country = "United States", Population = 39 },
            new { State = "Texas", Country = "United States", Population = 30 },
            new { State = "Florida", Country = "United States", Population = 23 },
            new { State = "New York", Country = "United States", Population = 19 },
            new { State = "Pennsylvania", Country = "United States", Population = 13 },
            new { State = "Illinois", Country = "United States", Population = 13 },
            new { State = "Ohio", Country = "United States", Population = 12 },
            new { State = "Georgia", Country = "United States", Population = 11 },
            new { State = "North Carolina", Country = "United States", Population = 11 },
            new { State = "Michigan", Country = "United States", Population = 10 },
            new { State = "Other States", Country = "United States", Population = 152 },

            // Sweden - Total: 10 million
            new { State = "Stockholm", Country = "Sweden", Population = 2 },
            new { State = "Västra Götaland", Country = "Sweden", Population = 2 },
            new { State = "Skåne", Country = "Sweden", Population = 1 },
            new { State = "Uppsala", Country = "Sweden", Population = 1 },
            new { State = "Östergötland", Country = "Sweden", Population = 1 },
            new { State = "Other Counties", Country = "Sweden", Population = 3 },

            // China - Total: 1412 million
            new { State = "Guangdong", Country = "China", Population = 127 },
            new { State = "Shandong", Country = "China", Population = 102 },
            new { State = "Henan", Country = "China", Population = 99 },
            new { State = "Jiangsu", Country = "China", Population = 85 },
            new { State = "Sichuan", Country = "China", Population = 84 },
            new { State = "Hebei", Country = "China", Population = 75 },
            new { State = "Hunan", Country = "China", Population = 66 },
            new { State = "Anhui", Country = "China", Population = 61 },
            new { State = "Hubei", Country = "China", Population = 58 },
            new { State = "Zhejiang", Country = "China", Population = 57 },
            new { State = "Other Provinces", Country = "China", Population = 598 },

            // Brazil - Total: 215 million
            new { State = "São Paulo", Country = "Brazil", Population = 46 },
            new { State = "Minas Gerais", Country = "Brazil", Population = 21 },
            new { State = "Rio de Janeiro", Country = "Brazil", Population = 17 },
            new { State = "Bahia", Country = "Brazil", Population = 15 },
            new { State = "Paraná", Country = "Brazil", Population = 12 },
            new { State = "Rio Grande do Sul", Country = "Brazil", Population = 11 },
            new { State = "Pernambuco", Country = "Brazil", Population = 10 },
            new { State = "Ceará", Country = "Brazil", Population = 9 },
            new { State = "Pará", Country = "Brazil", Population = 9 },
            new { State = "Santa Catarina", Country = "Brazil", Population = 7 },
            new { State = "Other States", Country = "Brazil", Population = 58 },

            // Canada - Total: 38 million
            new { State = "Ontario", Country = "Canada", Population = 15 },
            new { State = "Quebec", Country = "Canada", Population = 9 },
            new { State = "British Columbia", Country = "Canada", Population = 5 },
            new { State = "Alberta", Country = "Canada", Population = 4 },
            new { State = "Manitoba", Country = "Canada", Population = 1 },
            new { State = "Saskatchewan", Country = "Canada", Population = 1 },
            new { State = "Nova Scotia", Country = "Canada", Population = 1 },
            new { State = "New Brunswick", Country = "Canada", Population = 1 },
            new { State = "Newfoundland and Labrador", Country = "Canada", Population = 1 },

            // Germany - Total: 83 million
            new { State = "North Rhine-Westphalia", Country = "Germany", Population = 18 },
            new { State = "Bavaria", Country = "Germany", Population = 13 },
            new { State = "Baden-Württemberg", Country = "Germany", Population = 11 },
            new { State = "Lower Saxony", Country = "Germany", Population = 8 },
            new { State = "Hesse", Country = "Germany", Population = 6 },
            new { State = "Rhineland-Palatinate", Country = "Germany", Population = 4 },
            new { State = "Saxony", Country = "Germany", Population = 4 },
            new { State = "Berlin", Country = "Germany", Population = 4 },
            new { State = "Schleswig-Holstein", Country = "Germany", Population = 3 },
            new { State = "Brandenburg", Country = "Germany", Population = 3 },
            new { State = "Thuringia", Country = "Germany", Population = 2 },
            new { State = "Saxony-Anhalt", Country = "Germany", Population = 2 },
            new { State = "Mecklenburg-Vorpommern", Country = "Germany", Population = 2 },
            new { State = "Hamburg", Country = "Germany", Population = 2 },
            new { State = "Bremen", Country = "Germany", Population = 1 }
        };

        var countries = populationData
                            .Select(t => t.Country)
                            .Distinct()
                            .ToArray();

        var country = UseState(countries[0]);

        var countryInput = country.ToSelectInput(countries.ToOptions());

        // Get states for selected country and convert to PieChartData format
        var selectedCountryStates = populationData
            .Where(t => t.Country.Equals(country.Value))
            .Select(t => new PieChartData(t.State, t.Population))
            .ToArray();

        return Layout.Vertical()
                | (Layout.Horizontal()
                | (Layout.Vertical()
                   | Text.P("Countries Population").Small()
                   | new PieChart(countryData)
                        .Pie("Measure", "Dimension")
                        .Tooltip())
                | (Layout.Vertical()
                    | Text.P($"{country.Value} - States Population").Small()
                    | new PieChart(selectedCountryStates)
                            .Pie("Measure", "Dimension")
                            .Tooltip()))
                | countryInput;
    }
}
