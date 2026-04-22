import { useCallback } from "react";
import * as arrow from "apache-arrow";
import { convertUnscaledDecimalRawToNumber } from "../../utils/arrowDecimal";
import { DataRow } from "../../types/types";

/**
 * Hook for accessing row data from Arrow table
 */
export const useRowData = (arrowTableRef: React.RefObject<arrow.Table | null>) => {
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
            value = convertUnscaledDecimalRawToNumber(value, field.type.scale);
          }
          values.push(value);
        }
      }
      return { values };
    },
    [arrowTableRef],
  );

  return { getRowData };
};
