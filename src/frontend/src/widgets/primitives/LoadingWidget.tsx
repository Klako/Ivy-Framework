import { Loading } from "@/components/Loading";
import React from "react";

interface LoadingWidgetProps {
  type?: "Spinner" | "Skeleton";
}

export const LoadingWidget: React.FC<LoadingWidgetProps> = ({ type = "Spinner" }) => (
  <Loading type={type} />
);
