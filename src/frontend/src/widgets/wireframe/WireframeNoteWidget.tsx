import { getHeight, getWidth } from "@/lib/styles";
import React from "react";

interface WireframeNoteWidgetProps {
  id: string;
  text?: string;
  color?: "Yellow" | "Blue" | "Green" | "Pink" | "Orange" | "Purple";
  width?: string;
  height?: string;
}

const NOTE_COLORS = {
  Yellow: { bg: "#f5d949", fold: "#d4b82e", shadow: "rgba(180,150,30,0.25)", text: "#3a3000" },
  Blue: { bg: "#87ceeb", fold: "#6ab0d0", shadow: "rgba(80,140,180,0.25)", text: "#1a2e40" },
  Green: { bg: "#90d890", fold: "#70b870", shadow: "rgba(80,160,80,0.25)", text: "#1a3a1a" },
  Pink: { bg: "#f5a0c0", fold: "#d880a0", shadow: "rgba(180,80,120,0.25)", text: "#3a1020" },
  Orange: { bg: "#f5b870", fold: "#d89850", shadow: "rgba(180,130,50,0.25)", text: "#3a2000" },
  Purple: { bg: "#c8a8e8", fold: "#a888c8", shadow: "rgba(140,100,180,0.25)", text: "#2a1040" },
};

const FOLD_SIZE = 24;

export const WireframeNoteWidget: React.FC<WireframeNoteWidgetProps> = ({
  text,
  color = "Yellow",
  width,
  height,
}) => {
  const palette = NOTE_COLORS[color] || NOTE_COLORS.Yellow;

  const style: React.CSSProperties = {
    ...getWidth(width),
    ...getHeight(height),
    position: "relative",
    display: "inline-flex",
    flexDirection: "column",
    filter: `drop-shadow(3px 4px 6px ${palette.shadow})`,
    transform: "rotate(-1.2deg)",
  };

  return (
    <div style={style}>
      <svg
        style={{
          position: "absolute",
          inset: 0,
          width: "100%",
          height: "100%",
          pointerEvents: "none",
        }}
        preserveAspectRatio="none"
        viewBox="0 0 200 200"
      >
        {/* Main note body with slightly wobbly edges */}
        <path
          d={`M 2 1
              Q 100 -1, 176 1.5
              L 176 1.5
              L 200 ${FOLD_SIZE}
              Q 201 100, 199 199
              Q 100 201, 1 199
              Q -0.5 100, 2 1 Z`}
          fill={palette.bg}
          stroke={palette.text}
          strokeWidth="1.2"
          strokeOpacity="0.18"
        />
        {/* Folded corner triangle */}
        <path
          d={`M 176 1.5 L 176 ${FOLD_SIZE} L 200 ${FOLD_SIZE} Z`}
          fill={palette.fold}
          stroke={palette.text}
          strokeWidth="0.8"
          strokeOpacity="0.15"
        />
      </svg>
      <div
        style={{
          position: "relative",
          padding: "18px 20px 16px 18px",
          color: palette.text,
          fontFamily: "'Comic Sans MS', 'Segoe Print', 'Bradley Hand', cursive",
          fontSize: "15px",
          lineHeight: "1.5",
          whiteSpace: "pre-wrap",
          wordBreak: "break-word",
          minHeight: "60px",
        }}
      >
        {text}
      </div>
    </div>
  );
};
