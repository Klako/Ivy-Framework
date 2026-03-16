import type { SpriteMap, SpriteProps } from '@glideapps/glide-data-grid';
import { LUCIDE_ICONS, type IconNode } from './lucideIconNodes.generated';

/**
 * Builds an SVG string from Lucide icon node descriptors.
 */
function iconNodeToSvg(nodes: IconNode[], color: string): string {
  const elements = nodes
    .map(([tag, attrs]) => {
      const attrStr = Object.entries(attrs)
        .map(([k, v]) => `${k}="${v === 'currentColor' ? color : v}"`)
        .join(' ');
      return `<${tag} ${attrStr}/>`;
    })
    .join('');

  return `<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="${color}" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">${elements}</svg>`;
}

/**
 * Creates a Glide Data Grid sprite generator for a Lucide icon.
 */
function createIconGenerator(iconName: string) {
  const nodes = LUCIDE_ICONS[iconName];

  return (props: SpriteProps): string => {
    if (!nodes) {
      console.warn(`Icon "${iconName}" not found in Lucide icon map`);
      return `<svg width="24" height="24" fill="none" xmlns="http://www.w3.org/2000/svg"></svg>`;
    }
    return iconNodeToSvg(nodes, props.fgColor);
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
  customHeaderIcons?: Record<string, string>,
): SpriteMap {
  const icons: SpriteMap = {};
  const processedIcons = new Set<string>();

  columns.forEach(col => {
    if (col.icon && !processedIcons.has(col.icon)) {
      processedIcons.add(col.icon);
      icons[col.icon] = createIconGenerator(col.icon);
    }
  });

  if (customHeaderIcons) {
    for (const [name, svgTemplate] of Object.entries(customHeaderIcons)) {
      icons[name] = createCustomIconGenerator(svgTemplate);
    }
  }

  return icons;
}

/**
 * Adds standard UI icons to the header icons map.
 */
export function addStandardIcons(baseIcons: SpriteMap): SpriteMap {
  const standardIcons = [
    'ChevronUp',
    'ChevronDown',
    'Filter',
    'Search',
    'Settings',
    'MoreVertical',
    'Info',
    'HelpCircle',
  ];

  const extendedIcons = { ...baseIcons };

  standardIcons.forEach(iconName => {
    if (!extendedIcons[iconName]) {
      extendedIcons[iconName] = createIconGenerator(iconName);
    }
  });

  return extendedIcons;
}
