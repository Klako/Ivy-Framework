import React from "react";
import {
  Align,
  BorderRadius,
  BorderStyle,
  getAspectRatio,
  getRowGap,
  getColumnGap,
  getHeight,
  getAlign,
  getPadding,
  getWidth,
  Orientation,
  getColor,
  getMargin,
  getAlignSelf,
  getBorderStyle,
  getBorderThickness,
  getBoxRadius,
} from "@/lib/styles";
import { ScrollArea } from "@/components/ui/scroll-area";
import { ImageOverlayProvider } from "@/components/markdown/ImageOverlayContext";
import { type Responsive, useBreakpoint, resolveResponsive } from "@/hooks/use-responsive";

const EMPTY_ARRAY: never[] = [];

interface StackLayoutWidgetProps {
  children: React.ReactNode;
  orientation: Orientation;
  rowGap?: number;
  columnGap?: number;
  padding?: string;
  margin?: string;
  width?: string;
  height?: string;
  background?: string;
  alignContent?: Align;
  scroll?: "None" | "Auto" | "Vertical" | "Horizontal" | "Both";
  removeParentPadding?: boolean;
  visible?: boolean;
  wrap?: boolean;
  childAlignSelf?: (Align | undefined)[];
  borderColor?: string;
  borderRadius?: BorderRadius;
  borderStyle?: BorderStyle;
  borderThickness?: string;
  aspectRatio?: number;
  responsiveOrientation?: Responsive<Orientation>;
  responsiveRowGap?: Responsive<number>;
  responsiveColumnGap?: Responsive<number>;
  responsivePadding?: Responsive<string>;
  responsiveWidth?: string | Responsive<string>;
  responsiveHeight?: string | Responsive<string>;
  responsiveVisible?: boolean | Responsive<boolean>;
  responsiveDensity?: Responsive<string>;
}

export const StackLayoutWidget: React.FC<StackLayoutWidgetProps> = ({
  orientation = "Vertical",
  children,
  rowGap = 4,
  columnGap = 4,
  padding,
  margin,
  width,
  height,
  background,
  alignContent,
  scroll,
  removeParentPadding = false,
  visible = true,
  wrap = false,
  childAlignSelf = EMPTY_ARRAY,
  borderColor,
  borderRadius = "None",
  borderStyle = "None",
  borderThickness,
  aspectRatio,
  responsiveOrientation,
  responsiveRowGap,
  responsiveColumnGap,
  responsivePadding,
  responsiveWidth,
  responsiveHeight,
  responsiveVisible,
}) => {
  const bp = useBreakpoint();

  // Resolve responsive values with mobile-first cascading
  const resolvedOrientation = responsiveOrientation
    ? resolveResponsive(responsiveOrientation, bp, orientation)
    : orientation;
  const resolvedRowGap = responsiveRowGap
    ? resolveResponsive(responsiveRowGap, bp, rowGap)
    : rowGap;
  const resolvedColumnGap = responsiveColumnGap
    ? resolveResponsive(responsiveColumnGap, bp, columnGap)
    : columnGap;
  const resolvedPadding = responsivePadding
    ? resolveResponsive(responsivePadding, bp, padding as string)
    : padding;
  const resolvedWidth = responsiveWidth
    ? resolveResponsive(responsiveWidth as string | Responsive<string>, bp, width as string)
    : width;
  const resolvedHeight = responsiveHeight
    ? resolveResponsive(responsiveHeight as string | Responsive<string>, bp, height as string)
    : height;
  const resolvedVisible =
    responsiveVisible !== undefined
      ? resolveResponsive(responsiveVisible as boolean | Responsive<boolean>, bp, visible)
      : visible;

  const baseStyles: React.CSSProperties = {
    ...getPadding(resolvedPadding),
    ...getMargin(margin),
    ...getRowGap(resolvedRowGap),
    ...getColumnGap(resolvedColumnGap),
    ...getAlign(resolvedOrientation, alignContent),
    ...getWidth(resolvedWidth),
    ...getHeight(resolvedHeight),
    ...getAspectRatio(aspectRatio),
    ...getColor(background, "backgroundColor", "background"),
    ...(borderStyle !== "None" ? getBorderStyle(borderStyle) : {}),
    ...(borderThickness ? getBorderThickness(borderThickness) : {}),
    ...(borderColor ? getColor(borderColor, "borderColor", "background") : {}),
    ...(borderRadius === "Rounded"
      ? getBoxRadius()
      : borderRadius === "Full"
        ? { borderRadius: "9999px" }
        : {}),
  };

  // Override flexWrap if wrap is enabled
  if (wrap) {
    baseStyles.flexWrap = "wrap";
  }

  if (!resolvedVisible) {
    return null;
  }

  // Wrap children with alignSelf styles if needed
  const wrappedChildren = React.Children.map(children, (child, index) => {
    const alignSelf = childAlignSelf[index];
    if (alignSelf && React.isValidElement(child)) {
      const alignSelfStyles = getAlignSelf(alignSelf);
      return <div style={alignSelfStyles}>{child}</div>;
    }
    return child;
  });

  const hasScroll = scroll && scroll !== "None";

  if (hasScroll) {
    const flexStyles = { ...baseStyles };
    delete flexStyles.width;
    delete flexStyles.height;
    const outerStyles: React.CSSProperties = {
      ...getWidth(resolvedWidth),
      ...getHeight(resolvedHeight),
    };

    return (
      <ImageOverlayProvider>
        <ScrollArea
          className={removeParentPadding ? "remove-parent-padding" : ""}
          style={outerStyles}
          type="scroll"
          scrollHideDelay={600}
        >
          <div style={flexStyles}>{wrappedChildren}</div>
        </ScrollArea>
      </ImageOverlayProvider>
    );
  }

  return (
    <ImageOverlayProvider>
      <div style={baseStyles} className={removeParentPadding ? "remove-parent-padding" : ""}>
        {wrappedChildren}
      </div>
    </ImageOverlayProvider>
  );
};
