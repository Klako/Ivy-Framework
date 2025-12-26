/**
 * Default values for chart configuration objects.
 * These match the C# backend defaults in Ivy.Widgets.Charts.Shared.
 * When values equal the C# default, they are not serialized, so we must apply them here.
 */

import type {
  LinesProps,
  BarProps,
  PieProps,
  CartesianGridProps,
  LegendProps,
  PieLegendProps,
  ToolTipProps,
  ToolboxProps,
  XAxisProps,
  YAxisProps,
} from './chartTypes';

// Line/Area defaults (Line.cs, Area.cs)
export const LINE_DEFAULTS: Partial<LinesProps> = {
  curveType: 'Natural',
  legendType: 'Line',
  strokeWidth: 1,
  connectNulls: false,
  animated: false,
  scale: 'Linear',
};

// Bar defaults (Bar.cs)
export const BAR_DEFAULTS: Partial<BarProps> = {
  legendType: 'Line',
  strokeWidth: 1,
  animated: false,
  radius: [0, 0, 0, 0],
};

// Pie defaults (Pie.cs)
export const PIE_DEFAULTS: Partial<PieProps> = {
  legendType: 'Line',
  strokeWidth: 1,
  animated: false,
  startAngle: 0,
  endAngle: 360,
};

// CartesianGrid defaults (CartesianGrid.cs)
export const CARTESIAN_GRID_DEFAULTS: Partial<CartesianGridProps> = {
  horizontal: true,
  vertical: true,
};

// Legend defaults (Legend.cs)
export const LEGEND_DEFAULTS: Partial<LegendProps> = {
  layout: 'Horizontal',
  align: 'Center',
  verticalAlign: 'Bottom',
  iconSize: 14,
};

// Pie Legend defaults (Legend.cs with string types for compatibility)
export const PIE_LEGEND_DEFAULTS: Partial<PieLegendProps> = {
  layout: 'Horizontal',
  align: 'Center',
  verticalAlign: 'Bottom',
  iconSize: 14,
};

// Tooltip defaults (Tooltip.cs)
export const TOOLTIP_DEFAULTS: Partial<ToolTipProps> = {
  animated: false,
};

// Toolbox defaults (Toolbox.cs)
export const TOOLBOX_DEFAULTS: Partial<ToolboxProps> = {
  enabled: true,
  orientation: 'Horizontal',
  align: 'Right',
  verticalAlign: 'Top',
  saveAsImage: true,
  restore: true,
  dataView: true,
  magicType: true,
};

// XAxis defaults (Axis.cs)
export const X_AXIS_DEFAULTS: Partial<XAxisProps> = {
  type: 'Category',
  scale: 'Auto',
  allowDecimals: true,
  allowDuplicatedCategory: true,
  allowDataOverflow: false,
  angle: 0,
  tickCount: 5,
  tickSize: 6,
  includeHidden: false,
  reversed: false,
  mirror: false,
  domainStart: 'auto',
  domainEnd: 'auto',
  tickLine: false,
  axisLine: true,
  minTickGap: 5,
  hide: false,
  height: 30,
  orientation: 'Bottom',
};

// YAxis defaults (Axis.cs)
export const Y_AXIS_DEFAULTS: Partial<YAxisProps> = {
  type: 'Number',
  scale: 'Auto',
  allowDecimals: true,
  allowDuplicatedCategory: true,
  allowDataOverflow: false,
  angle: 0,
  tickCount: 5,
  tickSize: 6,
  includeHidden: false,
  reversed: false,
  mirror: false,
  domainStart: 'auto',
  domainEnd: 'auto',
  tickLine: false,
  axisLine: true,
  minTickGap: 5,
  hide: false,
  width: 60,
  orientation: 'Left',
};

// ReferenceLine defaults (ReferenceLine.cs)
export const REFERENCE_LINE_DEFAULTS = {
  strokeWidth: 1,
};

/**
 * Apply defaults to an object, only setting values that are undefined.
 */
export function applyDefaults<T extends object>(
  obj: Partial<T> | undefined,
  defaults: Partial<T>
): Partial<T> {
  if (!obj) return { ...defaults };
  const result = { ...defaults };
  for (const key in obj) {
    if (obj[key] !== undefined) {
      (result as Record<string, unknown>)[key] = obj[key];
    }
  }
  return result;
}

/**
 * Apply defaults to an array of objects.
 */
export function applyDefaultsToArray<T extends object>(
  arr: Partial<T>[] | undefined,
  defaults: Partial<T>
): Partial<T>[] {
  if (!arr || arr.length === 0) return [];
  return arr.map(item => applyDefaults(item, defaults));
}
