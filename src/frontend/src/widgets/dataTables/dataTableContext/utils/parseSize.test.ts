import { describe, it, expect } from "vitest";
import { parseSize, parseSizeGrow, parseSizeMin } from "./parseSize";

describe("parseSize", () => {
  // Fixed size types
  it("should parse Px size", () => {
    expect(parseSize("Px:200")).toBe(200);
  });

  it("should parse Rem size", () => {
    expect(parseSize("Rem:10")).toBe(160);
  });

  it("should parse Units size", () => {
    expect(parseSize("Units:20")).toBe(80);
  });

  it("should parse Units:1", () => {
    expect(parseSize("Units:1")).toBe(4);
  });

  // Grow/proportional types return 0
  it("should return 0 for Fraction", () => {
    expect(parseSize("Fraction:0.5")).toBe(0);
  });

  it("should return 0 for Fraction:1", () => {
    expect(parseSize("Fraction:1")).toBe(0);
  });

  it("should return 0 for Auto", () => {
    expect(parseSize("Auto")).toBe(0);
  });

  it("should return 0 for Grow:2", () => {
    expect(parseSize("Grow:2")).toBe(0);
  });

  it("should return 0 for Grow without value", () => {
    expect(parseSize("Grow")).toBe(0);
  });

  it("should return 0 for Full", () => {
    expect(parseSize("Full")).toBe(0);
  });

  it("should return 0 for Fit", () => {
    expect(parseSize("Fit")).toBe(0);
  });

  it("should return 0 for Screen", () => {
    expect(parseSize("Screen")).toBe(0);
  });

  it("should return 0 for MinContent", () => {
    expect(parseSize("MinContent")).toBe(0);
  });

  it("should return 0 for MaxContent", () => {
    expect(parseSize("MaxContent")).toBe(0);
  });

  it("should return 0 for Shrink:1", () => {
    expect(parseSize("Shrink:1")).toBe(0);
  });

  it("should return 0 for Shrink without value", () => {
    expect(parseSize("Shrink")).toBe(0);
  });

  // Edge cases
  it("should return 150 for undefined", () => {
    expect(parseSize(undefined)).toBe(150);
  });

  it("should return number as-is", () => {
    expect(parseSize(42)).toBe(42);
  });

  it("should return 150 for empty string", () => {
    expect(parseSize("")).toBe(150);
  });

  it("should return 150 for unknown type", () => {
    expect(parseSize("Unknown:5")).toBe(150);
  });

  // Min/max in primary parsing (ignored)
  it("should parse only primary part from min/max string (Fraction)", () => {
    expect(parseSize("Fraction:0.5,Px:100,Px:500")).toBe(0);
  });

  it("should parse only primary part from min/max string (Px)", () => {
    expect(parseSize("Px:200,Px:100,Px:500")).toBe(200);
  });
});

describe("parseSizeGrow", () => {
  // Fraction types
  it("should return 0.5 for Fraction:0.5", () => {
    expect(parseSizeGrow("Fraction:0.5")).toBe(0.5);
  });

  it("should return 0.333 for Fraction:0.333", () => {
    expect(parseSizeGrow("Fraction:0.333")).toBe(0.333);
  });

  it("should return 1 for Fraction:1", () => {
    expect(parseSizeGrow("Fraction:1")).toBe(1);
  });

  // Grow types
  it("should return 2 for Grow:2", () => {
    expect(parseSizeGrow("Grow:2")).toBe(2);
  });

  it("should return 1 for Grow without value", () => {
    expect(parseSizeGrow("Grow")).toBe(1);
  });

  it("should return 1 for Grow:1", () => {
    expect(parseSizeGrow("Grow:1")).toBe(1);
  });

  // Other grow types
  it("should return 1 for Auto", () => {
    expect(parseSizeGrow("Auto")).toBe(1);
  });

  it("should return 1 for Full", () => {
    expect(parseSizeGrow("Full")).toBe(1);
  });

  it("should return 1 for Screen", () => {
    expect(parseSizeGrow("Screen")).toBe(1);
  });

  // Non-grow types
  it("should return undefined for Px", () => {
    expect(parseSizeGrow("Px:200")).toBeUndefined();
  });

  it("should return undefined for Rem", () => {
    expect(parseSizeGrow("Rem:10")).toBeUndefined();
  });

  it("should return undefined for Units", () => {
    expect(parseSizeGrow("Units:20")).toBeUndefined();
  });

  it("should return undefined for Fit", () => {
    expect(parseSizeGrow("Fit")).toBeUndefined();
  });

  it("should return undefined for MinContent", () => {
    expect(parseSizeGrow("MinContent")).toBeUndefined();
  });

  it("should return undefined for MaxContent", () => {
    expect(parseSizeGrow("MaxContent")).toBeUndefined();
  });

  it("should return undefined for Shrink", () => {
    expect(parseSizeGrow("Shrink:1")).toBeUndefined();
  });

  // Edge cases
  it("should return undefined for undefined", () => {
    expect(parseSizeGrow(undefined)).toBeUndefined();
  });

  it("should return undefined for number", () => {
    expect(parseSizeGrow(42)).toBeUndefined();
  });

  it("should extract grow from min/max string", () => {
    expect(parseSizeGrow("Fraction:0.5,Px:100,Px:500")).toBe(0.5);
  });
});

describe("parseSizeMin", () => {
  it("should extract Px min from Fraction with min/max", () => {
    expect(parseSizeMin("Fraction:0.5,Px:100,Px:500")).toBe(100);
  });

  it("should extract Units min from Auto with min", () => {
    expect(parseSizeMin("Auto,Units:20")).toBe(80);
  });

  it("should extract Rem min from Grow with min", () => {
    expect(parseSizeMin("Grow:1,Rem:5")).toBe(80);
  });

  it("should return undefined for Px without min", () => {
    expect(parseSizeMin("Px:200")).toBeUndefined();
  });

  it("should return undefined for Fraction without min", () => {
    expect(parseSizeMin("Fraction:0.5")).toBeUndefined();
  });

  it("should return undefined for undefined", () => {
    expect(parseSizeMin(undefined)).toBeUndefined();
  });

  it("should return undefined for number", () => {
    expect(parseSizeMin(42)).toBeUndefined();
  });
});
