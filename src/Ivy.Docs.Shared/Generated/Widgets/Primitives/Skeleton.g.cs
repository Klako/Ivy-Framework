using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:9, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/09_Skeleton.md", searchHints: ["loading", "placeholder", "shimmer", "skeleton", "loading-state", "pending"])]
public class SkeletonApp(bool onlyBody = false) : ViewBase
{
    public SkeletonApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("skeleton", "Skeleton", 1), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Skeleton").OnLinkClick(onLinkClick)
            | Lead("Create elegant loading placeholders that mimic your content structure to improve perceived performance during data loading.")
            | new Markdown("The `Skeleton` [widget](app://onboarding/concepts/widgets) creates placeholder loading indicators that mimic the shape of your content. It improves perceived performance by showing users the layout of the page while data is loading. Use [Size](app://api-reference/ivy/size) for `.Height()` and `.Width()` to match your content layout.").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ProductCardView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ProductCardView : ViewBase
                    {
                        public record ProductData(
                            string Name,
                            string Description,
                            decimal Price,
                            string ImageUrl,
                            double Rating,
                            int Stock);
                    
                        public override object? Build()
                        {
                            var isLoading = UseState(false);
                            var product = UseState<ProductData?>();
                            var loadTrigger = UseState(0);
                    
                            // Handle loading with proper timer cleanup
                            UseEffect(() => {
                                if (loadTrigger.Value > 0)
                                {
                                    isLoading.Set(true);
                                    product.Set((ProductData?)null);
                    
                                    // Create timer with proper disposal
                                    var timer = new System.Threading.Timer(_ => {
                                        product.Set(new ProductData(
                                            "Premium Wireless Headphones",
                                            "Experience crystal-clear sound with our premium noise-cancelling wireless headphones. Features 30-hour battery life and memory foam ear cushions for all-day comfort.",
                                            149.99m,
                                            "https://png.pngtree.com/png-vector/20250703/ourmid/pngtree-black-headphones-sleek-3d-render-png-image_16600605.webp",
                                            4.7,
                                            12
                                        ));
                                        isLoading.Set(false);
                                    }, null, 2000, Timeout.Infinite);
                    
                                    // Return cleanup function to dispose timer
                                    return timer;
                                }
                                return null;
                            }, [loadTrigger]);
                    
                            void LoadProduct()
                            {
                                loadTrigger.Set(t => t + 1);
                            }
                    
                            return Layout.Vertical().Gap(4).Padding(4)
                                | new Button(
                                    isLoading.Value ? "Loading..." : "Load Product Data",
                                    onClick: _ => LoadProduct()
                                ).Disabled(isLoading.Value)
                                | (product.Value != null || isLoading.Value
                                    ? (isLoading.Value
                                        ? Layout.Vertical().Gap(3)
                                            | new Skeleton().Height(Size.Units(40)).Width(Size.Full())
                                            | new Skeleton().Height(Size.Units(24)).Width(Size.Units(40))
                                            | new Skeleton().Height(Size.Units(16)).Width(Size.Full())
                                            | new Skeleton().Height(Size.Units(16)).Width(Size.Full())
                                            | new Skeleton().Height(Size.Units(16)).Width(Size.Units(40))
                                            | (Layout.Horizontal().Gap(2)
                                                | new Skeleton().Height(Size.Units(24)).Width(Size.Units(40))
                                                | new Skeleton().Height(Size.Units(36)).Width(Size.Units(40)))
                                        : Layout.Vertical().Gap(3)
                                            | new Image("https://png.pngtree.com/png-vector/20250703/ourmid/pngtree-black-headphones-sleek-3d-render-png-image_16600605.webp")
                                                .Height(Size.Units(40))
                                            | Text.H3(product.Value?.Name)
                                            | Text.P(product.Value?.Description)
                                            | Text.Strong($"Rating: {product.Value?.Rating}/5")
                                            | (Layout.Horizontal().Gap(2)
                                                | Text.H4($"${product.Value?.Price}")
                                                | new Spacer().Width(Size.Grow())
                                                | new Button("Add to Cart")))
                                    : Text.P("Click the button above to load product data and see the skeleton loading state."));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Skeleton", "Ivy.SkeletonExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Skeleton.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(ApiReference.Ivy.SizeApp)]; 
        return article;
    }
}


public class ProductCardView : ViewBase
{
    public record ProductData(
        string Name, 
        string Description, 
        decimal Price, 
        string ImageUrl, 
        double Rating, 
        int Stock);
    
    public override object? Build()
    {
        var isLoading = UseState(false);
        var product = UseState<ProductData?>();
        var loadTrigger = UseState(0);
        
        // Handle loading with proper timer cleanup
        UseEffect(() => {
            if (loadTrigger.Value > 0)
            {
                isLoading.Set(true);
                product.Set((ProductData?)null);
                
                // Create timer with proper disposal
                var timer = new System.Threading.Timer(_ => {
                    product.Set(new ProductData(
                        "Premium Wireless Headphones",
                        "Experience crystal-clear sound with our premium noise-cancelling wireless headphones. Features 30-hour battery life and memory foam ear cushions for all-day comfort.",
                        149.99m,
                        "https://png.pngtree.com/png-vector/20250703/ourmid/pngtree-black-headphones-sleek-3d-render-png-image_16600605.webp",
                        4.7,
                        12
                    ));
                    isLoading.Set(false);
                }, null, 2000, Timeout.Infinite);
                
                // Return cleanup function to dispose timer
                return timer;
            }
            return null;
        }, [loadTrigger]);
        
        void LoadProduct()
        {
            loadTrigger.Set(t => t + 1);
        }
        
        return Layout.Vertical().Gap(4).Padding(4)
            | new Button(
                isLoading.Value ? "Loading..." : "Load Product Data", 
                onClick: _ => LoadProduct()
            ).Disabled(isLoading.Value)
            | (product.Value != null || isLoading.Value
                ? (isLoading.Value
                    ? Layout.Vertical().Gap(3)
                        | new Skeleton().Height(Size.Units(40)).Width(Size.Full())
                        | new Skeleton().Height(Size.Units(24)).Width(Size.Units(40))
                        | new Skeleton().Height(Size.Units(16)).Width(Size.Full())
                        | new Skeleton().Height(Size.Units(16)).Width(Size.Full())
                        | new Skeleton().Height(Size.Units(16)).Width(Size.Units(40))
                        | (Layout.Horizontal().Gap(2)
                            | new Skeleton().Height(Size.Units(24)).Width(Size.Units(40))
                            | new Skeleton().Height(Size.Units(36)).Width(Size.Units(40)))
                    : Layout.Vertical().Gap(3)
                        | new Image("https://png.pngtree.com/png-vector/20250703/ourmid/pngtree-black-headphones-sleek-3d-render-png-image_16600605.webp")
                            .Height(Size.Units(40))
                        | Text.H3(product.Value?.Name)
                        | Text.P(product.Value?.Description)
                        | Text.Strong($"Rating: {product.Value?.Rating}/5")
                        | (Layout.Horizontal().Gap(2)
                            | Text.H4($"${product.Value?.Price}")
                            | new Spacer().Width(Size.Grow())
                            | new Button("Add to Cart")))
                : Text.P("Click the button above to load product data and see the skeleton loading state."));
    }
}
