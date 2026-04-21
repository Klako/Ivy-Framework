import type { SpriteMap, SpriteProps } from "@glideapps/glide-data-grid";
import { createElement } from "react";
import { renderToStaticMarkup } from "react-dom/server";
import { icons } from "lucide-react";

function lowerFirst(value: string): string {
  if (!value) return value;
  return value.charAt(0).toLowerCase() + value.slice(1);
}

function upperFirst(value: string): string {
  if (!value) return value;
  return value.charAt(0).toUpperCase() + value.slice(1);
}

/** Keys the grid may use for one logical icon (column metadata vs JSON camelCase keys). */
function iconKeyVariants(key: string): string[] {
  const lf = lowerFirst(key);
  const uf = upperFirst(key);
  return [...new Set([key, lf, uf])];
}

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

function createCustomIconGenerator(svgTemplate: string) {
  return (props: SpriteProps): string => {
    return svgTemplate
      .replace(/\{fgColor\}/g, props.fgColor)
      .replace(/\{bgColor\}/g, props.bgColor);
  };
}

/**
 * Generates header icons SpriteMap from column icon names.
 * Icons are resolved from the generated Lucide icon map.
 */
export function generateHeaderIcons(
  columns: Array<{ icon?: string | null }>,
  customHeaderIcons?: Record<string, string> | null,
): SpriteMap {
  const iconMap: SpriteMap = {};
  const processedIcons = new Set<string>();

  columns.forEach((col) => {
    if (col.icon && !processedIcons.has(col.icon)) {
      processedIcons.add(col.icon);
      const generator = createIconGenerator(col.icon);
      // Same Lucide lookup for all spellings (e.g. "User" vs "user").
      for (const variant of iconKeyVariants(col.icon)) {
        iconMap[variant] = generator;
      }
    }
  });

  if (customHeaderIcons) {
    for (const [name, svgTemplate] of Object.entries(customHeaderIcons)) {
      const generator = createCustomIconGenerator(svgTemplate);
      // Backend JSON uses DictionaryKeyPolicy.CamelCase ("user"), while column.Icon is often
      // PascalCase enum names ("User"). Register all variants so Glide's lookup matches.
      for (const variant of iconKeyVariants(name)) {
        iconMap[variant] = generator;
      }
    }
  }

  return iconMap;
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
