/**
 * Converts an Arrow Decimal128 raw unscaled value to a JavaScript number.
 * Arrow stores decimals as integer strings; apply `scale` to place the decimal point.
 */
export function convertUnscaledDecimalRawToNumber(rawValue: unknown, scale: number): number {
  const str = String(rawValue);
  if (scale <= 0 || str === "0") return Number(str);

  const isNeg = str.startsWith("-");
  const digits = isNeg ? str.slice(1) : str;
  const padded = digits.padStart(scale + 1, "0");
  const intPart = padded.slice(0, padded.length - scale);
  const fracPart = padded.slice(padded.length - scale).replace(/0+$/, "");
  const result = fracPart ? `${intPart}.${fracPart}` : intPart;
  return parseFloat(isNeg ? `-${result}` : result);
}
