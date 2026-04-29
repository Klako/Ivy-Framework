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

function getScrollParent(el: HTMLElement | null): HTMLElement | Window {
  if (!el) return window;
  let parent: HTMLElement | null = el.parentElement;
  while (parent) {
    const { overflowY } = getComputedStyle(parent);
    if (overflowY === "auto" || overflowY === "scroll" || overflowY === "overlay") return parent;
    parent = parent.parentElement;
  }
  return window;
}

function scrollHeadingIntoView(
  el: HTMLElement,
  articleRoot: HTMLElement | null,
  behavior: ScrollBehavior,
): void {
  const scrollParent = getScrollParent(articleRoot ?? el);
  if (scrollParent === window) {
    el.scrollIntoView({ behavior, block: "start" });
    return;
  }
  const parent = scrollParent as HTMLElement;
  const delta = el.getBoundingClientRect().top - parent.getBoundingClientRect().top;
  const margin = parseFloat(getComputedStyle(el).scrollMarginTop) || 0;
  parent.scrollTo({ top: Math.max(0, parent.scrollTop + delta - margin), behavior });
}

function scrollToHashFragment(
  articleRef: React.RefObject<HTMLElement | null>,
  behavior: ScrollBehavior,
  maxFrames: number,
): () => void {
  let frame = 0;
  let cancelled = false;
  let rafId = 0;

  const tick = () => {
    if (cancelled) return;
    const targetId = getHashTargetId();
    if (!targetId) return;
    const el = document.getElementById(targetId);
    if (el) {
      scrollHeadingIntoView(el, articleRef.current, behavior);
      return;
    }
    frame++;
    if (frame < maxFrames) rafId = requestAnimationFrame(tick);
  };

  rafId = requestAnimationFrame(tick);
  return () => {
    cancelled = true;
    cancelAnimationFrame(rafId);
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

  useLayoutEffect(() => {
    let cancel = scrollToHashFragment(articleRef, "auto", 300);
    const onHashChange = () => {
      cancel();
      cancel = scrollToHashFragment(articleRef, "smooth", 120);
    };
    window.addEventListener("hashchange", onHashChange);
    return () => {
      cancel();
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
          articleId={id}
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
