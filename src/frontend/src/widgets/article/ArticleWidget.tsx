import { useEventHandler } from "@/components/event-handler";
import { TypographyContext } from "@/contexts/TypographyContext";
import { articleTypography } from "@/lib/styles";
import { ArticleFooter } from "@/widgets/article/ArticleFooter";
import { ArticleSidebar } from "@/widgets/article/ArticleSidebar";
import { InternalLink } from "@/types/widgets";
import React, { useLayoutEffect, useRef } from "react";

interface ArticleWidgetProps {
  id: string;
  children: React.ReactNode[];
  showToc?: boolean;
  showFooter?: boolean;
  previous: InternalLink;
  next: InternalLink;
  documentSource?: string;
  title?: string;
  headings?: { id: string; text: string; level: number }[];
  gap?: number;
}

const EMPTY_ARRAY: never[] = [];

function getHashTargetId(): string | null {
  const raw = window.location.hash;
  if (!raw || raw === "#") return null;
  const withoutHash = raw.slice(1);
  try {
    return decodeURIComponent(withoutHash.replace(/\+/g, " "));
  } catch {
    return withoutHash;
  }
}

function scrollElementWithIdIntoView(id: string, behavior: ScrollBehavior): boolean {
  const el = document.getElementById(id);
  if (!el) return false;
  el.scrollIntoView({ behavior, block: "start" });
  return true;
}

/** Scroll to the URL hash, retrying until the heading exists in the DOM. */
function scrollToHashFragment(options: {
  behavior: ScrollBehavior;
  maxAttempts: number;
  intervalMs: number;
}): { cancel: () => void } {
  const { behavior, maxAttempts, intervalMs } = options;
  let attempt = 0;
  let cancelled = false;
  let timeoutId: ReturnType<typeof setTimeout> | null = null;

  const step = () => {
    if (cancelled) return;
    const targetId = getHashTargetId();
    if (!targetId) return;
    if (scrollElementWithIdIntoView(targetId, behavior)) return;
    attempt++;
    if (attempt < maxAttempts) {
      timeoutId = setTimeout(step, intervalMs);
    }
  };

  // Defer until after layout so markdown headings (rehype-slug ids) exist in the DOM.
  timeoutId = setTimeout(step, 0);

  return {
    cancel: () => {
      cancelled = true;
      if (timeoutId !== null) clearTimeout(timeoutId);
    },
  };
}

export const ArticleWidget: React.FC<ArticleWidgetProps> = ({
  id,
  children,
  previous,
  next,
  documentSource,
  showFooter = true,
  showToc = true,
  title,
  headings = EMPTY_ARRAY,
  gap = 4,
}) => {
  const eventHandler = useEventHandler();
  const articleRef = useRef<HTMLElement>(null);

  // Deep links like /path/to/article#section-id — scroll after headings exist in the DOM.
  useLayoutEffect(() => {
    let cancelScroll: (() => void) | null = null;

    const run = (behavior: ScrollBehavior, maxAttempts: number) => {
      cancelScroll?.();
      cancelScroll = scrollToHashFragment({ behavior, maxAttempts, intervalMs: 50 }).cancel;
    };

    run("auto", 60);

    const onHashChange = () => run("smooth", 30);

    window.addEventListener("hashchange", onHashChange);
    return () => {
      cancelScroll?.();
      window.removeEventListener("hashchange", onHashChange);
    };
  }, [id]);

  return (
    <div className="flex flex-col gap-2 max-w-6xl mx-auto px-4 sm:px-6 lg:px-8 relative mt-8">
      <div className="flex flex-grow gap-8">
        <article ref={articleRef} className="w-full max-w-[48rem]">
          <TypographyContext.Provider value={articleTypography}>
            <div
              className="flex flex-col flex-grow min-h-[calc(100vh+8rem)]"
              style={{ gap: `${gap * 0.25}rem` }}
            >
              {children}
            </div>
          </TypographyContext.Provider>
          {showFooter && (
            <ArticleFooter
              id={id}
              previous={previous}
              next={next}
              documentSource={documentSource}
              onLinkClick={eventHandler}
            />
          )}
        </article>
        <ArticleSidebar
          articleRef={articleRef}
          showToc={showToc}
          documentSource={documentSource}
          title={title}
          headings={headings}
        />
      </div>
    </div>
  );
};
