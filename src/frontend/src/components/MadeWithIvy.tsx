"use client";
import { useState, useEffect } from "react";
import IvyLogo from "./IvyLogo";

function GitHubMark({ className }: { className?: string }) {
  return (
    <svg className={className} viewBox="0 0 16 16" fill="currentColor">
      <path d="M8 0C3.58 0 0 3.58 0 8c0 3.54 2.29 6.53 5.47 7.59.4.07.55-.17.55-.38 0-.19-.01-.82-.01-1.49-2.01.37-2.53-.49-2.69-.94-.09-.23-.48-.94-.82-1.13-.28-.15-.68-.52-.01-.53.63-.01 1.08.58 1.23.82.72 1.21 1.87.87 2.33.66.07-.52.28-.87.51-1.07-1.78-.2-3.64-.89-3.64-3.95 0-.87.31-1.59.82-2.15-.08-.2-.36-1.02.08-2.12 0 0 .67-.21 2.2.82.64-.18 1.32-.27 2-.27.68 0 1.36.09 2 .27 1.53-1.04 2.2-.82 2.2-.82.44 1.1.16 1.92.08 2.12.51.56.82 1.27.82 2.15 0 3.07-1.87 3.75-3.65 3.95.29.25.54.73.54 1.48 0 1.07-.01 1.93-.01 2.2 0 .21.15.46.55.38A8.013 8.013 0 0016 8c0-4.42-3.58-8-8-8z" />
    </svg>
  );
}

function parseOwnerRepo(value: string): string | null {
  const urlMatch = value.match(/github\.com\/([^/]+\/[^/]+)/);
  if (urlMatch) return urlMatch[1];
  const shortMatch = value.match(/^([^/]+\/[^/]+)$/);
  if (shortMatch) return shortMatch[1];
  return null;
}

function toGitHubUrl(value: string): string {
  if (value.startsWith('http')) return value;
  return `https://github.com/${value}`;
}

function formatStarCount(count: number): string {
  if (count >= 1000) return `${(count / 1000).toFixed(1)}k`;
  return count.toString();
}

export default function MadeWithIvy() {
  const [isHovered, setIsHovered] = useState(false);
  const [shouldShow, setShouldShow] = useState(false);
  const [gitHubUrl] = useState<string | null>(() => {
    if (typeof document === 'undefined') return null;
    const meta = document.querySelector('meta[name="github-url"]');
    return meta?.getAttribute('content') || null;
  });
  const [starCount, setStarCount] = useState<number | null>(null);

  useEffect(() => {
    const checkWindowSize = () => {
      setShouldShow(window.innerWidth >= 600 && window.innerHeight >= 600);
    };

    checkWindowSize();
    window.addEventListener("resize", checkWindowSize);

    return () => window.removeEventListener("resize", checkWindowSize);
  }, []);

  useEffect(() => {
    if (!gitHubUrl) return;
    const ownerRepo = parseOwnerRepo(gitHubUrl);
    if (!ownerRepo) return;
    fetch(`https://api.github.com/repos/${ownerRepo}`)
      .then(res => res.json())
      .then(data => {
        if (data.stargazers_count != null) {
          setStarCount(data.stargazers_count);
        }
      })
      .catch(() => {});
  }, [gitHubUrl]);

  if (!shouldShow) return null;

  const linkUrl = gitHubUrl
    ? toGitHubUrl(gitHubUrl)
    : 'https://github.com/Ivy-Interactive/Ivy-Framework';

  const handleClick = () => {
    window.open(linkUrl, '_blank');
  };

  const handleKeyDown = (e: React.KeyboardEvent) => {
    if (e.key === 'Enter' || e.key === ' ') {
      e.preventDefault();
      window.open(linkUrl, '_blank');
    }
  };

  return (
    <div
      className="bg-primary fixed bottom-0 right-0 z-100 overflow-hidden rounded-tl-full "
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
    >
      <div
        className={`
          rounded-tl-full
          flex
          items-end
          justify-end
          transition-all
          duration-300
          ease-in-out
          origin-bottom-right
          cursor-pointer
          ${isHovered ? "w-48 h-48" : "w-16 h-16"}
          ${isHovered ? "bg-primary-foreground" : "bg-primary"}
        `}
        onClick={handleClick}
        role="button"
        tabIndex={0}
        onKeyDown={handleKeyDown}
      >
        {gitHubUrl ? (
          <>
            <div
              className={`
                absolute bottom-2 right-2
                flex items-center justify-center
                transition-opacity duration-300
                text-primary-foreground
                ${isHovered ? "opacity-0" : "opacity-100"}
              `}
            >
              <GitHubMark className="w-5 h-5" />
            </div>
            <div
              style={{ color: "var(--primary)" }}
              className={`
                flex
                flex-col
                items-end
                gap-1.5
                transition-opacity
                duration-300
                m-4
                ${isHovered ? "opacity-100" : "opacity-0"}
              `}
            >
              <div className="flex items-center gap-1.5">
                <GitHubMark className="w-5 h-5" />
                {starCount != null && (
                  <span className="font-mono font-bold text-lg">
                    {formatStarCount(starCount)}
                  </span>
                )}
              </div>
              <span className="font-mono font-bold text-gray-400 text-xs whitespace-nowrap">
                STAR ON GITHUB
              </span>
            </div>
          </>
        ) : (
          <div
            style={{ color: "var(--primary)" }}
            className={`
              flex
              flex-col
              items-right
              gap-1.5
              transition-opacity
              duration-300
              m-4
              ${isHovered ? "opacity-100" : "opacity-0"}
            `}
          >
            <span className="font-mono font-bold text-gray-400">MADE WITH</span>
            <IvyLogo className="w-24" />
          </div>
        )}
      </div>
    </div>
  );
}
