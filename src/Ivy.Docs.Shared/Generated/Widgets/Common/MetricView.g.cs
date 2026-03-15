using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Common;

[App(order:13, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/03_Common/13_MetricView.md", searchHints: ["kpi", "metrics", "dashboard", "statistics", "analytics", "performance"])]
public class MetricViewApp(bool onlyBody = false) : ViewBase
{
    public MetricViewApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("metricview", "MetricView", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("negative-trends", "Negative Trends", 3), new ArticleHeading("using-metricview-in-layouts", "Using MetricView in Layouts", 3), new ArticleHeading("async-data-loading", "Async Data Loading", 3), new ArticleHeading("error-handling", "Error Handling", 3), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# MetricView").OnLinkClick(onLinkClick)
            | Lead("Display key performance indicators (KPIs) and metrics with trend indicators, goal progress tracking, and data loading via UseQuery hooks for dashboard [applications](app://onboarding/concepts/apps).")
            | new Markdown(
                """"
                The `MetricView` [widget](app://onboarding/concepts/widgets) is a specialized dashboard component built on top of [Card](app://widgets/common/card) that displays business metrics with visual indicators for performance trends and goal achievement. It uses [UseQuery](app://hooks/core/use-query) hooks for data fetching and automatically handles loading states, error handling, and provides a consistent layout for KPI dashboards.
                
                ## Basic Usage
                
                Here's a simple example of a metric view showing total sales with a trend indicator and goal progress. The second parameter is an optional icon, and the third parameter is a hook function that receives an `IViewContext` and returns a `QueryResult<MetricRecord>`.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new MetricView(
                        "Total Sales",
                        Icons.DollarSign,
                        ctx => ctx.UseQuery(
                            key: "total-sales",
                            fetcher: () => Task.FromResult(new MetricRecord(
                                "$84,250",      // Current metric value
                                0.21,           // 21% increase from previous period
                                0.21,           // 21% of goal achieved
                                "$800,000"      // Goal target
                            ))
                        )
                    )
                    """",Languages.Csharp)
                | new Box().Content(new MetricView(
    "Total Sales",
    Icons.DollarSign,
    ctx => ctx.UseQuery(
        key: "total-sales",
        fetcher: () => Task.FromResult(new MetricRecord(
            "$84,250",      // Current metric value
            0.21,           // 21% increase from previous period
            0.21,           // 21% of goal achieved
            "$800,000"      // Goal target
        ))
    )
))
            )
            | new Markdown(
                """"
                ### Negative Trends
                
                Negative trend values automatically display with a downward arrow and destructive color styling.
                """").OnLinkClick(onLinkClick)
            | new Callout("Trend Arrows: Green up arrow for positive trends, red down arrow for negative trends", icon:Icons.Info).OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new MetricView(
    "Stock Price",
    Icons.CircleDollarSign,
    ctx => ctx.UseQuery(
        key: "stock-price",
        fetcher: () => Task.FromResult(new MetricRecord(
            "$42.30",
            -0.15,          // 15% decrease (negative trend)
            0.45,
            "$95.00 target"
        ))
    )
))),
                new Tab("Code", new CodeBlock(
                    """"
                    new MetricView(
                        "Stock Price",
                        Icons.CircleDollarSign,
                        ctx => ctx.UseQuery(
                            key: "stock-price",
                            fetcher: () => Task.FromResult(new MetricRecord(
                                "$42.30",
                                -0.15,          // 15% decrease (negative trend)
                                0.45,
                                "$95.00 target"
                            ))
                        )
                    )
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Using MetricView in Layouts
                
                Combine multiple MetricViews in grid [layouts](app://onboarding/concepts/views) to create comprehensive dashboards.
                """").OnLinkClick(onLinkClick)
            | new Callout("MetricRecord takes four parameters: MetricFormatted (string) for the value, TrendComparedToPreviousPeriod (decimal, e.g. 0.21 for 21%) for trend arrows, GoalAchieved (0 to 1) for progress bars, and GoalFormatted (string) for goal text. All except MetricFormatted are optional.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Grid().Columns(2)
    | new MetricView("Total Sales", Icons.DollarSign,
        ctx => ctx.UseQuery(key: "sales", fetcher: () => Task.FromResult(new MetricRecord("$84,250", 0.21, 0.21, "$800,000"))))
    | new MetricView("Post Engagement", Icons.Heart,
        ctx => ctx.UseQuery(key: "engagement", fetcher: () => Task.FromResult(new MetricRecord("1,012.50%", 0.381, 1.25, "806.67%"))))
    | new MetricView("User Comments", Icons.UserCheck,
        ctx => ctx.UseQuery(key: "comments", fetcher: () => Task.FromResult(new MetricRecord("2.25", 0.381, 0.90, "2.50"))))
    | new MetricView("System Health", Icons.Activity,
        ctx => ctx.UseQuery(key: "health", fetcher: () => Task.FromResult(new MetricRecord("99.9%", null, 0.99, "100% uptime")))))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Grid().Columns(2)
                        | new MetricView("Total Sales", Icons.DollarSign,
                            ctx => ctx.UseQuery(key: "sales", fetcher: () => Task.FromResult(new MetricRecord("$84,250", 0.21, 0.21, "$800,000"))))
                        | new MetricView("Post Engagement", Icons.Heart,
                            ctx => ctx.UseQuery(key: "engagement", fetcher: () => Task.FromResult(new MetricRecord("1,012.50%", 0.381, 1.25, "806.67%"))))
                        | new MetricView("User Comments", Icons.UserCheck,
                            ctx => ctx.UseQuery(key: "comments", fetcher: () => Task.FromResult(new MetricRecord("2.25", 0.381, 0.90, "2.50"))))
                        | new MetricView("System Health", Icons.Activity,
                            ctx => ctx.UseQuery(key: "health", fetcher: () => Task.FromResult(new MetricRecord("99.9%", null, 0.99, "100% uptime"))))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Async Data Loading
                
                The MetricView uses [UseQuery](app://hooks/core/use-query) hooks for data fetching, which automatically handle loading states with a skeleton loader. This is useful when fetching metrics from [databases](app://onboarding/concepts/program) or APIs.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new MetricView(
    "Database Query",
    Icons.Database,
    ctx => ctx.UseQuery(
        key: "db-query",
        fetcher: async ct => {
            await Task.Delay(1000, ct); // Simulate API call
            return new MetricRecord("1,247 records", 0.125, 0.75, "1,500 records");
        }
    )
))),
                new Tab("Code", new CodeBlock(
                    """"
                    new MetricView(
                        "Database Query",
                        Icons.Database,
                        ctx => ctx.UseQuery(
                            key: "db-query",
                            fetcher: async ct => {
                                await Task.Delay(1000, ct); // Simulate API call
                                return new MetricRecord("1,247 records", 0.125, 0.75, "1,500 records");
                            }
                        )
                    )
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Error Handling
                
                When the data fetching fails, the MetricView automatically displays an error state from the QueryResult.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new MetricView(
    "Failed Metric",
    Icons.TriangleAlert,
    ctx => ctx.UseQuery<MetricRecord, string>(
        key: "failing-metric",
        fetcher: async ct => {
            await Task.Delay(500, ct);
            throw new Exception("Failed to load metric data");
        }
    )
))),
                new Tab("Code", new CodeBlock(
                    """"
                    new MetricView(
                        "Failed Metric",
                        Icons.TriangleAlert,
                        ctx => ctx.UseQuery<MetricRecord, string>(
                            key: "failing-metric",
                            fetcher: async ct => {
                                await Task.Delay(500, ct);
                                throw new Exception("Failed to load metric data");
                            }
                        )
                    )
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.MetricView", null, "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Views/Dashboards/MetricView.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("E-Commerce Analytics Dashboard",
                Vertical().Gap(4)
                | new Markdown("A complete e-commerce dashboard showing sales metrics, customer engagement, and inventory status with async data loading from a [database](app://onboarding/concepts/program).").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new ECommerceDashboard())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class ECommerceDashboard : ViewBase
                        {
                            public record SalesData(decimal Revenue, decimal PreviousRevenue, int Orders, int PreviousOrders, decimal ConversionRate, decimal PreviousConversionRate);
                        
                            private QueryResult<MetricRecord> UseRevenueMetric(IViewContext context)
                            {
                                return context.UseQuery(
                                    key: "revenue-metric",
                                    fetcher: async ct =>
                                    {
                                        await Task.Delay(800, ct); // Simulate database query
                                        var data = new SalesData(
                                            Revenue: 284750.50m,
                                            PreviousRevenue: 235000m,
                                            Orders: 1247,
                                            PreviousOrders: 1089,
                                            ConversionRate: 3.45m,
                                            PreviousConversionRate: 2.87m
                                        );
                        
                                        var trend = (double)((data.Revenue - data.PreviousRevenue) / data.PreviousRevenue);
                                        var goalAchieved = (double)(data.Revenue / 400000m); // Monthly goal: $400k
                        
                                        return new MetricRecord(
                                            data.Revenue.ToString("C0"),
                                            trend,
                                            goalAchieved,
                                            "$400,000 target"
                                        );
                                    }
                                );
                            }
                        
                            private QueryResult<MetricRecord> UseOrdersMetric(IViewContext context)
                            {
                                return context.UseQuery(
                                    key: "orders-metric",
                                    fetcher: async ct =>
                                    {
                                        await Task.Delay(600, ct);
                                        var orders = 1247;
                                        var previousOrders = 1089;
                                        var trend = (double)(orders - previousOrders) / previousOrders;
                        
                                        return new MetricRecord(
                                            orders.ToString("N0"),
                                            trend,
                                            (double)orders / 1500, // Goal: 1500 orders
                                            "1,500 orders target"
                                        );
                                    }
                                );
                            }
                        
                            private QueryResult<MetricRecord> UseConversionMetric(IViewContext context)
                            {
                                return context.UseQuery(
                                    key: "conversion-metric",
                                    fetcher: async ct =>
                                    {
                                        await Task.Delay(700, ct);
                                        var rate = 3.45;
                                        var previous = 2.87;
                                        var trend = (rate - previous) / previous;
                        
                                        return new MetricRecord(
                                            rate.ToString("F2") + "%",
                                            trend,
                                            rate / 5.0, // Target: 5% conversion
                                            "5% target"
                                        );
                                    }
                                );
                            }
                        
                            private QueryResult<MetricRecord> UseAverageOrderValue(IViewContext context)
                            {
                                return context.UseQuery(
                                    key: "aov-metric",
                                    fetcher: async ct =>
                                    {
                                        await Task.Delay(500, ct);
                                        var aov = 228.45m;
                                        var previous = 215.80m;
                        
                                        return new MetricRecord(
                                            aov.ToString("C2"),
                                            (double)((aov - previous) / previous),
                                            null,
                                            null
                                        );
                                    }
                                );
                            }
                        
                            public override object? Build()
                            {
                                return Layout.Vertical().Gap(4)
                                    | Text.H2("E-Commerce Dashboard")
                                    | (Layout.Grid().Columns(2).Gap(3)
                                        | new MetricView("Total Revenue", Icons.DollarSign, UseRevenueMetric)
                                        | new MetricView("Total Orders", Icons.ShoppingCart, UseOrdersMetric)
                                        | new MetricView("Conversion Rate", Icons.TrendingUp, UseConversionMetric)
                                        | new MetricView("Avg Order Value", Icons.CreditCard, UseAverageOrderValue)
                                    );
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("SaaS Metrics Dashboard",
                Vertical().Gap(4)
                | new Markdown("Track key SaaS metrics including MRR, churn rate, active users, and customer lifetime value with [UseQuery](app://hooks/core/use-query) hooks for data caching and automatic revalidation.").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new SaaSDashboard())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class SaaSDashboard : ViewBase
                        {
                            private QueryResult<MetricRecord> UseMrrMetric(IViewContext context)
                            {
                                return context.UseQuery(
                                    key: "mrr-metric",
                                    fetcher: async ct =>
                                    {
                                        await Task.Delay(800, ct); // Simulate API call
                                        var mrr = 125430m;
                                        var previousMrr = 108750m;
                        
                                        return new MetricRecord(
                                            mrr.ToString("C0"),
                                            (double)((mrr - previousMrr) / previousMrr),
                                            (double)(mrr / 150000m),
                                            "$150K target"
                                        );
                                    }
                                );
                            }
                        
                            private QueryResult<MetricRecord> UseActiveUsersMetric(IViewContext context)
                            {
                                return context.UseQuery(
                                    key: "active-users-metric",
                                    fetcher: async ct =>
                                    {
                                        await Task.Delay(600, ct);
                                        var activeUsers = 3847;
                                        var previousActiveUsers = 3520;
                        
                                        return new MetricRecord(
                                            activeUsers.ToString("N0"),
                                            (double)(activeUsers - previousActiveUsers) / previousActiveUsers,
                                            (double)activeUsers / 5000,
                                            "5,000 users goal"
                                        );
                                    }
                                );
                            }
                        
                            private QueryResult<MetricRecord> UseChurnRateMetric(IViewContext context)
                            {
                                return context.UseQuery(
                                    key: "churn-rate-metric",
                                    fetcher: async ct =>
                                    {
                                        await Task.Delay(700, ct);
                                        var churnRate = 2.3;
                                        var previousChurnRate = 3.1;
                        
                                        return new MetricRecord(
                                            churnRate.ToString("F1") + "%",
                                            -(churnRate - previousChurnRate) / previousChurnRate, // Negative is good for churn
                                            1 - (churnRate / 5.0), // Lower is better
                                            "Target: <2%"
                                        );
                                    }
                                );
                            }
                        
                            private QueryResult<MetricRecord> UseLtvMetric(IViewContext context)
                            {
                                return context.UseQuery(
                                    key: "ltv-metric",
                                    fetcher: async ct =>
                                    {
                                        await Task.Delay(500, ct);
                                        var ltv = 8450m;
                                        var previousLtv = 7890m;
                        
                                        return new MetricRecord(
                                            ltv.ToString("C0"),
                                            (double)((ltv - previousLtv) / previousLtv),
                                            null,
                                            null
                                        );
                                    }
                                );
                            }
                        
                            public override object? Build()
                            {
                                return Layout.Vertical().Gap(4)
                                    | Text.H2("SaaS Metrics Dashboard")
                                    | Text.Muted("Real-time business metrics and KPIs")
                                    | (Layout.Grid().Columns(2).Gap(3)
                                        | new MetricView("Monthly Recurring Revenue", Icons.DollarSign, UseMrrMetric)
                                        | new MetricView("Active Users", Icons.Users, UseActiveUsersMetric)
                                        | new MetricView("Churn Rate", Icons.UserMinus, UseChurnRateMetric)
                                        | new MetricView("Customer LTV", Icons.Gem, UseLtvMetric)
                                    );
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.AppsApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Widgets.Common.CardApp), typeof(Hooks.Core.UseQueryApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.ProgramApp)]; 
        return article;
    }
}


public class ECommerceDashboard : ViewBase
{
    public record SalesData(decimal Revenue, decimal PreviousRevenue, int Orders, int PreviousOrders, decimal ConversionRate, decimal PreviousConversionRate);

    private QueryResult<MetricRecord> UseRevenueMetric(IViewContext context)
    {
        return context.UseQuery(
            key: "revenue-metric",
            fetcher: async ct =>
            {
                await Task.Delay(800, ct); // Simulate database query
                var data = new SalesData(
                    Revenue: 284750.50m,
                    PreviousRevenue: 235000m,
                    Orders: 1247,
                    PreviousOrders: 1089,
                    ConversionRate: 3.45m,
                    PreviousConversionRate: 2.87m
                );

                var trend = (double)((data.Revenue - data.PreviousRevenue) / data.PreviousRevenue);
                var goalAchieved = (double)(data.Revenue / 400000m); // Monthly goal: $400k

                return new MetricRecord(
                    data.Revenue.ToString("C0"),
                    trend,
                    goalAchieved,
                    "$400,000 target"
                );
            }
        );
    }

    private QueryResult<MetricRecord> UseOrdersMetric(IViewContext context)
    {
        return context.UseQuery(
            key: "orders-metric",
            fetcher: async ct =>
            {
                await Task.Delay(600, ct);
                var orders = 1247;
                var previousOrders = 1089;
                var trend = (double)(orders - previousOrders) / previousOrders;

                return new MetricRecord(
                    orders.ToString("N0"),
                    trend,
                    (double)orders / 1500, // Goal: 1500 orders
                    "1,500 orders target"
                );
            }
        );
    }

    private QueryResult<MetricRecord> UseConversionMetric(IViewContext context)
    {
        return context.UseQuery(
            key: "conversion-metric",
            fetcher: async ct =>
            {
                await Task.Delay(700, ct);
                var rate = 3.45;
                var previous = 2.87;
                var trend = (rate - previous) / previous;

                return new MetricRecord(
                    rate.ToString("F2") + "%",
                    trend,
                    rate / 5.0, // Target: 5% conversion
                    "5% target"
                );
            }
        );
    }

    private QueryResult<MetricRecord> UseAverageOrderValue(IViewContext context)
    {
        return context.UseQuery(
            key: "aov-metric",
            fetcher: async ct =>
            {
                await Task.Delay(500, ct);
                var aov = 228.45m;
                var previous = 215.80m;

                return new MetricRecord(
                    aov.ToString("C2"),
                    (double)((aov - previous) / previous),
                    null,
                    null
                );
            }
        );
    }

    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | Text.H2("E-Commerce Dashboard")
            | (Layout.Grid().Columns(2).Gap(3)
                | new MetricView("Total Revenue", Icons.DollarSign, UseRevenueMetric)
                | new MetricView("Total Orders", Icons.ShoppingCart, UseOrdersMetric)
                | new MetricView("Conversion Rate", Icons.TrendingUp, UseConversionMetric)
                | new MetricView("Avg Order Value", Icons.CreditCard, UseAverageOrderValue)
            );
    }
}

public class SaaSDashboard : ViewBase
{
    private QueryResult<MetricRecord> UseMrrMetric(IViewContext context)
    {
        return context.UseQuery(
            key: "mrr-metric",
            fetcher: async ct =>
            {
                await Task.Delay(800, ct); // Simulate API call
                var mrr = 125430m;
                var previousMrr = 108750m;

                return new MetricRecord(
                    mrr.ToString("C0"),
                    (double)((mrr - previousMrr) / previousMrr),
                    (double)(mrr / 150000m),
                    "$150K target"
                );
            }
        );
    }

    private QueryResult<MetricRecord> UseActiveUsersMetric(IViewContext context)
    {
        return context.UseQuery(
            key: "active-users-metric",
            fetcher: async ct =>
            {
                await Task.Delay(600, ct);
                var activeUsers = 3847;
                var previousActiveUsers = 3520;

                return new MetricRecord(
                    activeUsers.ToString("N0"),
                    (double)(activeUsers - previousActiveUsers) / previousActiveUsers,
                    (double)activeUsers / 5000,
                    "5,000 users goal"
                );
            }
        );
    }

    private QueryResult<MetricRecord> UseChurnRateMetric(IViewContext context)
    {
        return context.UseQuery(
            key: "churn-rate-metric",
            fetcher: async ct =>
            {
                await Task.Delay(700, ct);
                var churnRate = 2.3;
                var previousChurnRate = 3.1;

                return new MetricRecord(
                    churnRate.ToString("F1") + "%",
                    -(churnRate - previousChurnRate) / previousChurnRate, // Negative is good for churn
                    1 - (churnRate / 5.0), // Lower is better
                    "Target: <2%"
                );
            }
        );
    }

    private QueryResult<MetricRecord> UseLtvMetric(IViewContext context)
    {
        return context.UseQuery(
            key: "ltv-metric",
            fetcher: async ct =>
            {
                await Task.Delay(500, ct);
                var ltv = 8450m;
                var previousLtv = 7890m;

                return new MetricRecord(
                    ltv.ToString("C0"),
                    (double)((ltv - previousLtv) / previousLtv),
                    null,
                    null
                );
            }
        );
    }

    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | Text.H2("SaaS Metrics Dashboard")
            | Text.Muted("Real-time business metrics and KPIs")
            | (Layout.Grid().Columns(2).Gap(3)
                | new MetricView("Monthly Recurring Revenue", Icons.DollarSign, UseMrrMetric)
                | new MetricView("Active Users", Icons.Users, UseActiveUsersMetric)
                | new MetricView("Churn Rate", Icons.UserMinus, UseChurnRateMetric)
                | new MetricView("Customer LTV", Icons.Gem, UseLtvMetric)
            );
    }
}
