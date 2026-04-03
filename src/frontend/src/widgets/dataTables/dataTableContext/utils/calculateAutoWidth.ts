import { DataColumn } from "../../types/types";

/**
 * Character width estimation for width calculation
 * Based on typical proportional font metrics
 */
const AVG_CHAR_WIDTH = 8;
const HEADER_PADDING = 48; // Icon + padding + sort indicator space
const MIN_WIDTH = 60;
const MAX_AUTO_WIDTH = 400;

/**
 * Default widths by column type when auto-sizing
 */
const TYPE_DEFAULTS: Record<string, number> = {
  boolean: 80,
  icon: 60,
  number: 100,
  date: 120,
  datetime: 160,
  text: 180,
  labels: 200,
  link: 200,
};

/**
 * Calculate optimal column width based on header and type
 */
export function calculateAutoWidth(column: DataColumn): number {
  const normalizedType = (column.type ?? "text").toLowerCase();
  const typeDefaultWidth = TYPE_DEFAULTS[normalizedType] || TYPE_DEFAULTS.text;

  const headerText = column.header || column.name;
  const headerWidth = Math.ceil(headerText.length * AVG_CHAR_WIDTH + HEADER_PADDING);

  const calculatedWidth = Math.max(typeDefaultWidth, headerWidth);

  return Math.max(MIN_WIDTH, Math.min(calculatedWidth, MAX_AUTO_WIDTH));
}
