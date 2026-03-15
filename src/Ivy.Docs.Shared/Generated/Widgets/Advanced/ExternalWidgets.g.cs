using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Advanced;

[App(order:5, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/07_Advanced/05_ExternalWidgets.md", searchHints: ["external widget", "custom widget", "React component", "Vite", "IIFE", "embedded resource", "multiple widgets", "shared frontend", "same bundle"])]
public class ExternalWidgetsApp(bool onlyBody = false) : ViewBase
{
    public ExternalWidgetsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("external-widgets", "External Widgets", 1), new ArticleHeading("architecture-overview", "Architecture Overview", 2), new ArticleHeading("scaffolding-with-the-cli", "Scaffolding with the CLI", 2), new ArticleHeading("c-backend", "C# Backend", 2), new ArticleHeading("frontend-vite-library", "Frontend (Vite Library)", 2), new ArticleHeading("vite-configuration", "Vite configuration", 3), new ArticleHeading("entry-point", "Entry point", 3), new ArticleHeading("react-component", "React component", 3), new ArticleHeading("project-structure-and-build", "Project structure and build", 2), new ArticleHeading("standalone-widget-project", "Standalone widget project", 3), new ArticleHeading("integrated-pattern-inside-host-app", "Integrated pattern (inside host app)", 3), new ArticleHeading("multiple-widgets-in-one-bundle-same-frontend", "Multiple widgets in one bundle (same frontend)", 3), new ArticleHeading("host-requirements", "Host requirements", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# External Widgets").OnLinkClick(onLinkClick)
            | Lead("External [widgets](app://onboarding/concepts/widgets) let you extend the Ivy Framework with custom React components built and bundled separately from the core framework. Use them for domain-specific UI (e.g. diagrams, charts, or rich editors) without coupling that code to the framework backend.")
            | new Markdown(
                """"
                ## Architecture Overview
                
                1. **C# proxy** — A record inheriting from `WidgetBase<T>` with `[ExternalWidget]`, defining [props](app://onboarding/concepts/widgets) and [events](app://onboarding/concepts/event-handlers).
                2. **React component** — The actual UI, built with standard React and tooling (e.g. Vite).
                3. **Build pipeline** — MSBuild runs the frontend build and embeds the output (JS/CSS) as resources in the widget assembly.
                
                The host [app](app://onboarding/concepts/apps) loads the script and CSS from embedded resources and renders your component, passing props and wiring events back to C#.
                
                ## Scaffolding with the CLI
                
                You can generate a new external widget with the Ivy [CLI](app://onboarding/cli/cli-overview) so namespace, names, and build match the framework:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddOutput("ivy widget")
                .AddOutput("Namespace: ExternalWidget")
                .AddOutput("Widget: MyWidget")
                
            | new Markdown(
                """"
                ## C# Backend
                
                Create a record that inherits from `WidgetBase<T>` and mark it with `[ExternalWidget]`. The attribute tells the framework where to find the bundled script and (optionally) CSS, and which export/global name to use. See Widgets and Event handlers for the basics.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                using Ivy;
                using Ivy.Core;
                
                namespace MyProject.Widgets;
                
                [ExternalWidget(
                    "frontend/dist/ExternalWidget.js",
                    StylePath = "frontend/dist/style.css",
                    ExportName = "MyWidget",
                    GlobalName = "MyProject_Widgets_MyWidget")]
                public record MyWidget : WidgetBase<MyWidget>
                {
                    public MyWidget(string? label = null)
                    {
                        Label = label;
                    }
                
                    internal MyWidget() { }
                
                    [Prop] public string? Label { get; set; }
                
                    [Event] public Func<Event<MyWidget>, ValueTask>? OnClick { get; set; }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                - **Script path** — Path to the JS file relative to the project (and to embedded resources). Often `frontend/dist/...`.
                - **StylePath** — Optional path to a CSS file. If omitted, include styles in the JS bundle.
                - **ExportName** — Name of the React component export the loader should use.
                - **GlobalName** — Must match the Vite library `name` (and the global variable the IIFE assigns). Use the full namespace with dots replaced by underscores (e.g. `MyProject_Widgets_MyWidget`).
                """").OnLinkClick(onLinkClick)
            | new Callout(
                """"
                Use `[Prop]` for data and `[Event]` for callbacks. You can add extension methods for a fluent API (e.g. `.Label("...")`, `.OnClick(...)`).
                """", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Frontend (Vite Library)
                
                The frontend should be a separate project (e.g. a `frontend/` folder) set up as a **library** build.
                
                ### Vite configuration
                
                Build an IIFE so the host can load one script and get a global. The `name` in `build.lib` must match `GlobalName` in C#.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // vite.config.ts
                import { defineConfig } from 'vite';
                import react from '@vitejs/plugin-react';
                import { resolve } from 'path';
                
                export default defineConfig({
                  plugins: [
                    react({
                      jsxRuntime: 'classic', // Use global React; required for Ivy host
                    }),
                  ],
                  build: {
                    lib: {
                      entry: resolve(__dirname, 'src/index.ts'),
                      name: 'MyProject_Widgets_MyWidget',
                      fileName: () => 'ExternalWidget.js',
                      formats: ['iife'],
                    },
                    rollupOptions: {
                      external: ['react', 'react-dom'],
                      output: {
                        globals: {
                          react: 'React',
                          'react-dom': 'ReactDOM',
                        },
                        extend: false,
                      },
                    },
                  },
                });
                """",Languages.Typescript)
            | new Callout("Using `fileName: () => 'ExternalWidget.js'` avoids Vite adding suffixes like `.iife.js`, so the path matches what you put in `[ExternalWidget]`.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Entry point
                
                Export your widget component and assign it to `window` under the same name as `build.lib.name` so the IIFE loader can find it.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // src/index.ts
                import './style.css';
                import { MyWidget } from './MyWidget';
                
                if (typeof window !== 'undefined') {
                  (window as unknown as Record<string, unknown>).MyProject_Widgets_MyWidget = {
                    MyWidget,
                  };
                }
                
                export { MyWidget };
                """",Languages.Typescript)
            | new Markdown(
                """"
                ### React component
                
                Ivy passes props (including `id`, `width`, `height`, `onIvyEvent`, `events`) and optional custom props (e.g. `label`). Use `onIvyEvent(eventName, widgetId, args)` to fire events back to C#.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // src/MyWidget.tsx
                import React from "react";
                import { IvyEventHandler } from "./types";
                import { getWidth, getHeight } from "./styles";
                
                interface MyWidgetProps {
                  id: string;
                  width?: string;
                  height?: string;
                  onIvyEvent: IvyEventHandler;
                  events?: string[];
                  label?: string;
                }
                
                export const MyWidget: React.FC<MyWidgetProps> = ({
                  id,
                  width = "Full",
                  height = "Full",
                  onIvyEvent,
                  events = [],
                  label,
                }) => {
                  const handleClick = () => {
                    if (events.includes("OnClick")) {
                      onIvyEvent("OnClick", id, []);
                    }
                  };
                
                  const style: React.CSSProperties = {
                    ...getWidth(width),
                    ...getHeight(height),
                  };
                
                  return (
                    <div
                      style={style}
                      className="p-4 border rounded-lg bg-[var(--background)] text-[var(--foreground)] border-[var(--border)]"
                    >
                      <button
                        onClick={handleClick}
                        className="px-4 py-2 rounded transition-colors bg-[var(--primary)] text-white hover:opacity-90"
                      >
                        {label ?? "Click me"}
                      </button>
                    </div>
                  );
                };
                """",Languages.Typescript)
            | new Markdown(
                """"
                Use Ivy theme variables ([Colors](app://api-reference/ivy/colors): `--primary`, `--background`, `--foreground`, `--border`, etc.) so the widget matches the host app ([Theming](app://onboarding/concepts/theming)). Size props use Ivy’s [Size](app://api-reference/ivy/size) format (e.g. `Full`, `Units:80`); you can parse them in the component or in a small helper.
                
                ## Project structure and build
                
                ### Standalone widget project
                
                For a reusable widget (e.g. NuGet or shared repo), use a dedicated project and folder **outside** any host app directory so the host does not compile the widget’s sources.
                
                Typical layout:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                MyWidget/
                ├── MyWidget.cs
                ├── MyWidget.csproj
                └── frontend/
                    ├── package.json
                    ├── vite.config.ts
                    ├── tsconfig.json
                    └── src/
                        ├── index.ts
                        ├── MyWidget.tsx
                        └── style.css
                """",Languages.Text)
            | new Markdown(
                """"
                In the `.csproj`:
                
                - Embed the built assets.
                - Run the frontend build before the C# build.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                <ItemGroup>
                  <EmbeddedResource Include="frontend/dist/**/*" />
                </ItemGroup>
                
                <Target Name="BuildFrontend" BeforeTargets="Build" Condition="Exists('frontend/package.json')">
                  <Exec Command="npm install" WorkingDirectory="frontend" />
                  <Exec Command="npm run build" WorkingDirectory="frontend" />
                </Target>
                """",Languages.Xml)
            | new Callout("Use forward slashes in paths (`frontend/dist/**/*`) for cross-platform builds.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Integrated pattern (inside host app)
                
                When the widget lives inside the host app (e.g. `HostApp/Widgets/MyWidget/`), add a `<ProjectReference>` to the widget project.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                <ItemGroup>
                  <ProjectReference Include="Widgets/MyWidget/MyWidget.csproj" />
                </ItemGroup>
                """",Languages.Xml)
            | new Markdown(
                """"
                ### Multiple widgets in one bundle (same frontend)
                
                You can ship **several widgets from one frontend project**: one Vite build produces a single JS bundle, and multiple C# widget records point to that same script. Each widget type is resolved by `ExportName` from the same global object. The backend serves the same embedded file for all of them; the browser may cache it per URL.
                
                - **One frontend** — one `frontend/` project, one `npm run build`, one output file (e.g. `ExternalWidgets.js`).
                - **Same script path and GlobalName** — every C# widget uses the same `[ExternalWidget("frontend/dist/ExternalWidgets.js", ..., GlobalName = "MyProject_Widgets")]` so they all use the same global object.
                - **Different ExportName** — each C# record must specify the React component name explicitly: `ExportName = "MyWidget"`, `ExportName = "AnotherWidget"`, etc. Do not rely on `"default"` for multi-widget bundles.
                
                **Vite and entry point:** Use one library name for the whole bundle and assign all components to that global:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // vite.config.ts — one name for the whole bundle
                name: 'MyProject_Widgets',
                fileName: () => 'ExternalWidgets.js',
                
                // src/index.ts
                (window as any).MyProject_Widgets = {
                  MyWidget,
                  AnotherWidget,
                };
                """",Languages.Typescript)
            | new Markdown("**C#:** Use the same `Script path` and `GlobalName` for each widget, and different `ExportName` values so the loader picks the right component from the same global:").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                [ExternalWidget("frontend/dist/ExternalWidgets.js", ExportName = "MyWidget", GlobalName = "MyProject_Widgets")]
                public record MyWidget : WidgetBase<MyWidget> { ... }
                
                [ExternalWidget("frontend/dist/ExternalWidgets.js", ExportName = "AnotherWidget", GlobalName = "MyProject_Widgets")]
                public record AnotherWidget : WidgetBase<AnotherWidget> { ... }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Host requirements
                
                External widgets that externalize React expect the host app to provide React (and ReactDOM) on the global object.
                
                The host’s entry point should set:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                (window as any).React = React;
                (window as any).ReactDOM = ReactDOM; // or createRoot etc.
                """",Languages.Typescript)
            | new Markdown(
                """"
                Ivy’s standard host (e.g. [Chrome](app://onboarding/concepts/chrome)) does this. If you see “Global not found” or React-related errors, ensure the host app exposes these globals before any external widget script runs.
                
                ## Troubleshooting
                
                | Issue | What to check |
                | ----- | ------------- |
                | **Script resource not found** | Path in `[ExternalWidget]` must match the embedded path (project-relative, e.g. `frontend/dist/ExternalWidget.js`). Resource name is assembly name + path with `/` → `.`. After changing `fileName` in Vite, run `dotnet clean` then `dotnet build`. |
                | **Global not found** | `GlobalName` in C# must equal Vite `build.lib.name`. In `src/index.ts`, assign the export to `window[GlobalName]`. Use `jsxRuntime: 'classic'` so the bundle uses the global React. |
                | **Duplicate type / CS0579** | Widget project is under the host app and the host is compiling its files. Exclude the widget directory in the host's `.csproj` so only the widget project builds it (see below). |
                | **Invalid hook call / multiple React** | Widget must not bundle React when the host provides it. Keep `react` and `react-dom` in `external` and `globals` in Vite, and use `jsxRuntime: 'classic'`. |
                | **Wrong filename (.iife.js)** | Set `fileName: () => 'ExternalWidget.js'` in `vite.config.ts` so the output name has no extra suffix. |
                
                **Excluding the widget folder (integrated pattern):** When the widget project lives inside the host app directory, exclude it from the host’s compilation so the host does not compile the widget’s sources. In the host’s `.csproj`:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                <PropertyGroup>
                  <DefaultItemExcludes>$(DefaultItemExcludes);Widgets/MyWidget/**</DefaultItemExcludes>
                </PropertyGroup>
                
                <ItemGroup>
                  <ProjectReference Include="Widgets/MyWidget/MyWidget.csproj" />
                </ItemGroup>
                """",Languages.Xml)
            | new Markdown(
                """"
                Alternatively use explicit removes: `<Compile Remove="Widgets/MyWidget/**/*.cs" />` and `<None Remove="Widgets/MyWidget/**/*" />`.
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.EventHandlersApp), typeof(Onboarding.Concepts.AppsApp), typeof(Onboarding.CLI.CLIOverviewApp), typeof(ApiReference.Ivy.ColorsApp), typeof(Onboarding.Concepts.ThemingApp), typeof(ApiReference.Ivy.SizeApp), typeof(Onboarding.Concepts.ChromeApp)]; 
        return article;
    }
}

