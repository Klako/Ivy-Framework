import { GridColumn, GridColumnIcon } from '@glideapps/glide-data-grid';
import type { DataColumn } from '../types/types';
import { calculateAutoWidth } from '../dataTableContext/utils/calculateAutoWidth';

/**
 * Maps column icon names or types to appropriate icons.
 * Columns with an explicit `.Icon()` return the icon name as-is
 * (looked up in the SpriteMap). Type-based icons use built-in GridColumnIcon values.
 */
export function mapColumnIcon(
  col: DataColumn
): GridColumnIcon | string | undefined {
  // Explicit icon — return as-is for SpriteMap lookup
  if (col.icon) {
    return col.icon;
  }

  // Auto-detect from column type
  const normalizedType = (col.type ?? 'text').toLowerCase();

  if (normalizedType === 'number') {
    return GridColumnIcon.HeaderNumber;
  }
  if (normalizedType === 'text') {
    return GridColumnIcon.HeaderString;
  }
  if (normalizedType === 'date' || normalizedType === 'datetime') {
    return GridColumnIcon.HeaderDate;
  }
  if (normalizedType === 'boolean') {
    return GridColumnIcon.HeaderBoolean;
  }
  if (normalizedType === 'icon') {
    return GridColumnIcon.HeaderImage;
  }

  return GridColumnIcon.HeaderString;
}

/**
 * Reorders columns array by moving a column from startIndex to endIndex
 * Returns a new array without modifying the original
 */
export function reorderColumns(
  columns: DataColumn[],
  startIndex: number,
  endIndex: number
): DataColumn[] {
  const result = [...columns];
  const [removed] = result.splice(startIndex, 1);
  result.splice(endIndex, 0, removed);
  return result;
}

/**
 * Converts data columns to GridColumn format with proper widths and groups
 * Filters out hidden columns and applies column ordering
 */
export function convertToGridColumns(
  columns: DataColumn[],
  columnOrder: number[],
  columnWidths: Record<string, number>,
  containerWidth: number,
  showGroups: boolean,
  showColumnTypeIcons: boolean = true
): GridColumn[] {
  // Filter out hidden columns first
  const visibleColumns = columns.filter(col => !col.hidden);

  // Apply column order if available
  let orderedColumns = visibleColumns;

  // User reordering (columnOrder array) takes precedence over backend order property
  if (columnOrder.length === columns.length) {
    // Use the columnOrder array (from user reordering)
    orderedColumns = columnOrder
      .map(idx => columns[idx])
      .filter(col => !col.hidden);
  } else {
    // Fall back to explicit order property if no user reordering has happened
    const hasOrderProperty = visibleColumns.some(
      col => col.order !== undefined
    );
    if (hasOrderProperty) {
      orderedColumns = [...visibleColumns].sort((a, b) => {
        const orderA = a.order ?? Number.MAX_SAFE_INTEGER;
        const orderB = b.order ?? Number.MAX_SAFE_INTEGER;
        return orderA - orderB;
      });
    }
  }

  return orderedColumns.map((col, index) => {
    const originalIndex = columns.indexOf(col);
    const baseWidth = columnWidths[originalIndex.toString()] || col.width;
    // Ensure width is always a number
    let numericBaseWidth =
      typeof baseWidth === 'string' ? parseFloat(baseWidth) : baseWidth;

    // If width is NaN, undefined, or 0, use auto-calculated width
    if (!numericBaseWidth || isNaN(numericBaseWidth)) {
      numericBaseWidth = calculateAutoWidth(col);
    }

    // Make the last column fill the remaining space using grow (avoids gap from
    // manual width calc; grid handles scrollbar space internally)
    // Explicit icons always show; type-inferred icons respect showColumnTypeIcons
    const icon =
      (col.icon || showColumnTypeIcons) ? mapColumnIcon(col) : undefined;

    if (index === orderedColumns.length - 1 && containerWidth > 0) {
      return {
        title: col.header || col.name,
        width: numericBaseWidth,
        grow: 1,
        group: showGroups ? col.group : undefined,
        icon,
      };
    }

    return {
      title: col.header || col.name,
      width: numericBaseWidth,
      group: showGroups ? col.group : undefined,
      icon,
    };
  });
}
