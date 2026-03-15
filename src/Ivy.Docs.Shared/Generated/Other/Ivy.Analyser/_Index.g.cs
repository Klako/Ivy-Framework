using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Other.Ivy.Analyser;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/05_Other/Ivy.Analyser/_Index.md")]
public class _IndexApp(bool onlyBody = false) : ViewBase
{
    public _IndexApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivyanalyser-diagnostics", "Ivy.Analyser Diagnostics", 1), new ArticleHeading("hook-usage", "Hook Usage", 2), new ArticleHeading("app-constructor", "App Constructor", 2), new ArticleHeading("useeffect", "UseEffect", 2), new ArticleHeading("widget-children", "Widget Children", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # Ivy.Analyser Diagnostics
                
                Reference pages for all Ivy.Analyser diagnostic codes.
                
                ## Hook Usage
                
                - [IVYHOOK001](app://other/ivy/analyser/ivyhook001) - Invalid Hook Usage
                - [IVYHOOK001B](app://other/ivy/analyser/ivyhook001-b) - Hook Used in Nested Closure
                - [IVYHOOK002](app://other/ivy/analyser/ivyhook002) - Hook Called Conditionally
                - [IVYHOOK003](app://other/ivy/analyser/ivyhook003) - Hook Called in Loop
                - [IVYHOOK004](app://other/ivy/analyser/ivyhook004) - Hook Called in Switch Statement
                - [IVYHOOK005](app://other/ivy/analyser/ivyhook005) - Hook Not at Top of Build Method
                - [IVYHOOK006](app://other/ivy/analyser/ivyhook006) - Hook Result Stored in Class Member
                
                ## App Constructor
                
                - [IVYAPP001](app://other/ivy/analyser/ivyapp001) - App Must Have Parameterless Constructor
                
                ## UseEffect
                
                - [IVYEFFECT001](app://other/ivy/analyser/ivyeffect001) - Avoid Task.ContinueWith inside UseEffect
                
                ## Widget Children
                
                - [IVYCHILD001](app://other/ivy/analyser/ivychild001) - Adding Children to Leaf Widget
                - [IVYCHILD002](app://other/ivy/analyser/ivychild002) - Adding Multiple Children to Single-Child Widget
                - [IVYCHILD003](app://other/ivy/analyser/ivychild003) - Wrong Child Type for Widget
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Other.Ivy.Analyser.IVYHOOK001App), typeof(Other.Ivy.Analyser.IVYHOOK001BApp), typeof(Other.Ivy.Analyser.IVYHOOK002App), typeof(Other.Ivy.Analyser.IVYHOOK003App), typeof(Other.Ivy.Analyser.IVYHOOK004App), typeof(Other.Ivy.Analyser.IVYHOOK005App), typeof(Other.Ivy.Analyser.IVYHOOK006App), typeof(Other.Ivy.Analyser.IVYAPP001App), typeof(Other.Ivy.Analyser.IVYEFFECT001App), typeof(Other.Ivy.Analyser.IVYCHILD001App), typeof(Other.Ivy.Analyser.IVYCHILD002App), typeof(Other.Ivy.Analyser.IVYCHILD003App)]; 
        return article;
    }
}

