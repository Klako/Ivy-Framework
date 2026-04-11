import { describe, it, expect } from "vitest";
import * as fs from "fs";
import * as path from "path";

/**
 * Tests for useFileAttachments toast feedback when uploadUrl is missing.
 *
 * Since @testing-library/react is not available in this project,
 * we verify the source code contains the correct patterns.
 */
describe("useFileAttachments - handleDrop uploadUrl guard", () => {
  const hookSource = fs.readFileSync(path.resolve(__dirname, "./useFileAttachments.ts"), "utf-8");

  it("should show a toast when uploadUrl is missing on drop", () => {
    // Find the handleDrop function
    const handleDropIndex = hookSource.indexOf("const handleDrop = useCallback");
    expect(handleDropIndex).toBeGreaterThan(-1);

    const handleDropBlock = hookSource.slice(
      handleDropIndex,
      hookSource.indexOf("[disabled, uploadUrl, uploadFiles],", handleDropIndex) + 50,
    );

    expect(handleDropBlock).toContain("if (!uploadUrl)");
    expect(handleDropBlock).toContain("toast(");
    expect(handleDropBlock).toContain('"Upload not available"');
  });

  it("should not call uploadFiles when uploadUrl is undefined in handleDrop", () => {
    const handleDropIndex = hookSource.indexOf("const handleDrop = useCallback");
    const handleDropBlock = hookSource.slice(
      handleDropIndex,
      hookSource.indexOf("[disabled, uploadUrl, uploadFiles],", handleDropIndex) + 50,
    );

    // The !uploadUrl check should come before uploadFiles call and return early
    const uploadUrlCheckIndex = handleDropBlock.indexOf("if (!uploadUrl)");
    const returnAfterCheck = handleDropBlock.indexOf("return;", uploadUrlCheckIndex);
    const uploadFilesCallIndex = handleDropBlock.indexOf("await uploadFiles(files)");

    expect(uploadUrlCheckIndex).toBeGreaterThan(-1);
    expect(returnAfterCheck).toBeGreaterThan(-1);
    expect(returnAfterCheck).toBeLessThan(uploadFilesCallIndex);
  });

  it("should include uploadUrl in handleDrop dependency array", () => {
    const handleDropIndex = hookSource.indexOf("const handleDrop = useCallback");
    const handleDropBlock = hookSource.slice(
      handleDropIndex,
      hookSource.indexOf(
        ");",
        hookSource.indexOf("[disabled, uploadUrl, uploadFiles],", handleDropIndex),
      ) + 5,
    );

    expect(handleDropBlock).toContain("[disabled, uploadUrl, uploadFiles],");
  });
});

describe("useFileAttachments - handlePaste uploadUrl guard with toast", () => {
  const hookSource = fs.readFileSync(path.resolve(__dirname, "./useFileAttachments.ts"), "utf-8");

  it("should show a toast when uploadUrl is missing and clipboard contains files", () => {
    const handlePasteIndex = hookSource.indexOf("const handlePaste = useCallback");
    expect(handlePasteIndex).toBeGreaterThan(-1);

    const handlePasteBlock = hookSource.slice(
      handlePasteIndex,
      hookSource.indexOf("[disabled, uploadUrl, uploadFiles],", handlePasteIndex) + 50,
    );

    expect(handlePasteBlock).toContain("if (!uploadUrl)");
    expect(handlePasteBlock).toContain("toast(");
    expect(handlePasteBlock).toContain('"Upload not available"');
  });

  it("should only show toast for file pastes, not text-only pastes", () => {
    const handlePasteIndex = hookSource.indexOf("const handlePaste = useCallback");
    const handlePasteBlock = hookSource.slice(
      handlePasteIndex,
      hookSource.indexOf("[disabled, uploadUrl, uploadFiles],", handlePasteIndex) + 50,
    );

    // Should check for file items before showing toast
    expect(handlePasteBlock).toContain('item.kind === "file"');
    expect(handlePasteBlock).toContain("hasFiles");
  });

  it("should check disabled separately from uploadUrl in handlePaste", () => {
    const handlePasteIndex = hookSource.indexOf("const handlePaste = useCallback");
    const handlePasteBlock = hookSource.slice(
      handlePasteIndex,
      hookSource.indexOf("[disabled, uploadUrl, uploadFiles],", handlePasteIndex) + 50,
    );

    // disabled check should be separate from uploadUrl check
    expect(handlePasteBlock).toContain("if (disabled) return;");
    // Should NOT have the combined check anymore
    expect(handlePasteBlock).not.toContain("if (disabled || !uploadUrl) return;");
  });
});
