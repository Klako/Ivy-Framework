import type { SpriteMap, SpriteProps } from "@glideapps/glide-data-grid";
import { createElement } from "react";
import { renderToStaticMarkup } from "react-dom/server";
import { icons } from "lucide-react";

/**
 * Creates a Glide Data Grid sprite generator for a Lucide icon.
 */
function createIconGenerator(iconName: string) {
  const IconComponent = icons[iconName as keyof typeof icons];

  return (props: SpriteProps): string => {
    if (!IconComponent) {
      console.warn(`Icon "${iconName}" not found in Lucide icon map`);
      return `<svg width="24" height="24" fill="none" xmlns="http://www.w3.org/2000/svg"></svg>`;
    }

    const element = createElement(IconComponent, {
      size: 24,
      color: props.fgColor,
      strokeWidth: 2,
    });
    return renderToStaticMarkup(element);
  };
}

/**
 * Generates header icons SpriteMap from column icon names.
 * Icons are resolved from the generated Lucide icon map.
 */
export function generateHeaderIcons(columns: Array<{ icon?: string | null }>): SpriteMap {
  const icons: SpriteMap = {};
  const processedIcons = new Set<string>();

  columns.forEach((col) => {
    if (col.icon && !processedIcons.has(col.icon)) {
      processedIcons.add(col.icon);
      icons[col.icon] = createIconGenerator(col.icon);
    }
  });

  return icons;
}

/**
 * Adds standard UI icons to the header icons map.
 */
export function addStandardIcons(baseIcons: SpriteMap): SpriteMap {
  const standardIcons = [
    "ChevronUp",
    "ChevronDown",
    "Filter",
    "Search",
    "Settings",
    "MoreVertical",
    "Info",
    "HelpCircle",
  ];

  const extendedIcons = { ...baseIcons };

  standardIcons.forEach((iconName) => {
    if (!extendedIcons[iconName]) {
      extendedIcons[iconName] = createIconGenerator(iconName);
    }
  });

  return extendedIcons;
}
