import { cva } from "class-variance-authority";
import { densityHeight, densityText } from "../density-scale";

// Size variants for TableHead padding
export const tableHeadSizeVariant = cva("w-full caption-bottom", {
  variants: {
    density: {
      Small: `${densityHeight.Small} px-1 ${densityText.Small}`,
      Medium: `${densityHeight.Medium} px-2 ${densityText.Medium}`,
      Large: `${densityHeight.Large} px-3 ${densityText.Large}`,
    },
  },
  defaultVariants: {
    density: "Medium",
  },
});

// Size variants for TableCell padding
export const tableCellSizeVariant = cva("align-middle", {
  variants: {
    density: {
      Small: "p-1 text-xs",
      Medium: "p-2 text-sm",
      Large: "p-3 text-base",
    },
  },
  defaultVariants: {
    density: "Medium",
  },
});

export const tableSizeVariant = cva("", {
  variants: {
    density: {
      Small: "text-xs",
      Medium: "text-sm",
      Large: "text-base",
    },
  },
  defaultVariants: {
    density: "Medium",
  },
});
