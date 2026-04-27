---
searchHints:
  - loading
  - spinner
  - loader
  - waiting
  - progress
  - skeleton
  - busy
---

# Loading

<Ingress>
Signal that something is happening. The Loading [widget](../../01_Onboarding/02_Concepts/03_Widgets.md) renders a compact spinner by default, or a randomized skeleton placeholder when you want to hint at the shape of the content that's on its way.
</Ingress>

The `Loading` widget has two variants selected via a fluent API:

- `new Loading()` — the default spinner with a "Loading..." label.
- `new Loading().Spinner()` — explicit spinner, equivalent to the default.
- `new Loading().Skeleton()` — a randomized skeleton placeholder layout. The layout is randomized once per mount and stays stable across re-renders.

For a modal loading experience with async work and cancellation, see the [`UseLoading`](../../03_Hooks/02_Core/23_UseLoading.md) hook instead.

## Spinner

The spinner variant is a small animated circle followed by a "Loading..." label. Use it for short waits where you don't need to hint at the shape of the incoming content.

```csharp demo-below
new Loading()
```

```csharp demo-below
new Loading().Spinner()
```

## Skeleton

The skeleton variant renders a title bar and a few placeholder lines of random widths, suggesting that textual content is on its way. Use it when you want the loading state to roughly mirror the shape of the content that will replace it.

```csharp demo-below
new Loading().Skeleton()
```

If you need full control over the placeholder layout, reach for the [`Skeleton`](09_Skeleton.md) widget directly.

<WidgetDocs Type="Ivy.Loading" ExtensionTypes="Ivy.LoadingExtensions" SourceUrl="https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Loading.cs"/>
