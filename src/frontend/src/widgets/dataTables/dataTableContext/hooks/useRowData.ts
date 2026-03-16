import { useCallback } from 'react';
import * as arrow from 'apache-arrow';
import { DataRow } from '../../types/types';

/**
 * Converts an Arrow Decimal128 raw value to a JavaScript number.
 * Arrow Decimal128 stores values as unscaled integers — the scale factor
 * from the field type must be applied to get the real value.
 */
function convertDecimalValue(rawValue: unknown, scale: number): number {
  const str = String(rawValue);
  if (scale <= 0 || str === '0') return Number(str);

  const isNeg = str.startsWith('-');
  const digits = isNeg ? str.slice(1) : str;
  const padded = digits.padStart(scale + 1, '0');
  const intPart = padded.slice(0, padded.length - scale);
  const fracPart = padded.slice(padded.length - scale).replace(/0+$/, '');
  const result = fracPart ? `${intPart}.${fracPart}` : intPart;
  return parseFloat(isNeg ? `-${result}` : result);
}

/**
 * Hook for accessing row data from Arrow table
 */
export const useRowData = (
  arrowTableRef: React.RefObject<arrow.Table | null>
) => {
  const getRowData = useCallback(
    (rowIndex: number): DataRow | null => {
      const table = arrowTableRef.current;
      if (!table || rowIndex < 0 || rowIndex >= table.numRows) {
        return null;
      }

      const values: (string | number | boolean | Date | string[] | null)[] = [];
      for (let j = 0; j < table.numCols; j++) {
        const column = table.getChildAt(j);
        if (column) {
          let value = column.get(rowIndex);
          // Arrow Decimal128 values are raw unscaled integers — apply scale
          const field = table.schema.fields[j];
          if (field && arrow.DataType.isDecimal(field.type) && value != null) {
            value = convertDecimalValue(value, field.type.scale);
          }
          values.push(value);
        }
      }
      return { values };
    },
    [arrowTableRef]
  );

  return { getRowData };
};
