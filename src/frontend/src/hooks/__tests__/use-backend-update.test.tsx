import { describe, it, expect } from "vitest";
import { _private, CompactWidgetNode, WidgetUpdate } from "../use-backend";
//import { WidgetNode } from "@/types/widgets";
//import { widgetMap } from "@/widgets/widgetMap";
import fs from "fs";

type TestData = {
  Source: CompactWidgetNode;
  Target: CompactWidgetNode;
  Updates: [Algorithm: string, Update: WidgetUpdate][];
};

describe("use-backend:applyWidgetUpdate", () => {
  it("correctly updates with linear+nopropdiff generated test data", () => {
    let tests: TestData[] = JSON.parse(
      fs.readFileSync("src/hooks/__tests__/tests.json", { encoding: "utf-8" }),
    );
    for (let test of tests) {
      let decompactedSource = _private.decompactWidgetNode(test.Source);
      let decompactedTarget = _private.decompactWidgetNode(test.Target);
      for (let [algorithm, update] of test.Updates) {
        if (algorithm == "linear+nopropdiff") {
          let updatedSource = _private.applyWidgetUpdate(decompactedSource, update);
          expect(updatedSource).toStrictEqual(decompactedTarget);
        }
      }
    }
  });
  it("correctly updates with linear+propdiff generated test data", () => {
    let tests: TestData[] = JSON.parse(
      fs.readFileSync("src/hooks/__tests__/tests.json", { encoding: "utf-8" }),
    );
    for (let test of tests) {
      let decompactedSource = _private.decompactWidgetNode(test.Source);
      let decompactedTarget = _private.decompactWidgetNode(test.Target);
      for (let [algorithm, update] of test.Updates) {
        if (algorithm == "linear+propdiff") {
          let updatedSource = _private.applyWidgetUpdate(decompactedSource, update);
          expect(updatedSource).toStrictEqual(decompactedTarget);
        }
      }
    }
  });
  it("correctly updates with lcs+nopropdiff generated test data", () => {
    let tests: TestData[] = JSON.parse(
      fs.readFileSync("src/hooks/__tests__/tests.json", { encoding: "utf-8" }),
    );
    for (let test of tests) {
      let decompactedSource = _private.decompactWidgetNode(test.Source);
      let decompactedTarget = _private.decompactWidgetNode(test.Target);
      for (let [algorithm, update] of test.Updates) {
        if (algorithm == "lcs+nopropdiff") {
          let updatedSource = _private.applyWidgetUpdate(decompactedSource, update);
          expect(updatedSource).toStrictEqual(decompactedTarget);
        }
      }
    }
  });
  it("correctly updates with lcs+propdiff generated test data", () => {
    let tests: TestData[] = JSON.parse(
      fs.readFileSync("src/hooks/__tests__/tests.json", { encoding: "utf-8" }),
    );
    for (let test of tests) {
      let decompactedSource = _private.decompactWidgetNode(test.Source);
      let decompactedTarget = _private.decompactWidgetNode(test.Target);
      for (let [algorithm, update] of test.Updates) {
        if (algorithm == "lcs+propdiff") {
          let updatedSource = _private.applyWidgetUpdate(decompactedSource, update);
          expect(updatedSource).toStrictEqual(decompactedTarget);
        }
      }
    }
  });
});
