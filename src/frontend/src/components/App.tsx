import { useEffect } from "react";
import { renderWidgetTree, loadingState } from "@/widgets/widgetRenderer";
import { useBackend } from "@/hooks/use-backend";
import { Toaster } from "@/components/ui/toaster";
import { ErrorSheet } from "@/components/ErrorSheet";
import ErrorBoundary from "./ErrorBoundary";
// import MadeWithIvy from "./MadeWithIvy"; // TODO: Branding feature commented out - can be re-enabled in the future
import { DevTools } from "./DevTools";
import {
  getAppArgs,
  getAppId,
  getShellParam,
  getParentId,
  wrapAppContent,
  isDevToolsEnabled,
} from "@/lib/utils";
// import { hasLicensedFeature } from "@/lib/license"; // TODO: Branding feature commented out - can be re-enabled in the future
import { ConnectionModal } from "./ConnectionModal";
import { ThemeProvider } from "./theme-provider";
import { EventHandlerProvider } from "./event-handler";
import { StreamHandlerProvider } from "./stream-handler";
import { BreakpointProvider } from "@/hooks/use-breakpoint-context";

export function App() {
  sessionStorage.removeItem("vite-chunk-reload");

  const appId = getAppId();
  const appArgs = getAppArgs();
  const parentId = getParentId();
  const appShell = getShellParam();

  const { connection, widgetTree, eventHandler, subscribeToStream, disconnected, connectionState } =
    useBackend(appId, appArgs, parentId, appShell);
  // const [removeBranding, setRemoveBranding] = useState(true); // TODO: Branding feature commented out - can be re-enabled in the future
  //
  // useEffect(() => {
  //   hasLicensedFeature("RemoveBranding").then(setRemoveBranding);
  // }, []);

  useEffect(() => {
    const handlePopState = (event: PopStateEvent) => {
      const appShell = getShellParam();
      if (appShell) {
        const newAppId = getAppId();
        connection?.invoke("Navigate", newAppId, event.state).catch((err) => {
          console.error("SignalR Error when sending Navigate:", err);
        });
      }
    };

    window.addEventListener("popstate", handlePopState);
    return () => {
      window.removeEventListener("popstate", handlePopState);
    };
  }, [connection]);

  return (
    <ThemeProvider defaultTheme="system" storageKey="ivy-ui-theme">
      <ErrorBoundary>
        <BreakpointProvider>
          <EventHandlerProvider eventHandler={eventHandler}>
            <StreamHandlerProvider subscribeToStream={subscribeToStream}>
              <>
                {/* {!removeBranding && <MadeWithIvy />} */}
                {/* TODO: Branding feature commented out - can be re-enabled in the future */}
                {isDevToolsEnabled() && <DevTools />}
                {wrapAppContent(renderWidgetTree(widgetTree || loadingState()))}
                <ErrorSheet />
                <Toaster />
                {(disconnected || connectionState === "reconnecting") && <ConnectionModal />}
              </>
            </StreamHandlerProvider>
          </EventHandlerProvider>
        </BreakpointProvider>
      </ErrorBoundary>
    </ThemeProvider>
  );
}
