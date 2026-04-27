using System.Runtime.InteropServices;

namespace Ivy.Desktop;

public static class DpiDetector
{
    /// <summary>
    /// Gets the system scaling factor for high-DPI displays (1.0 normal, 2.0 typical Retina, etc.).
    /// </summary>
    public static double GetSystemScalingFactor()
    {
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return GetWindowsScalingFactor();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return GetMacOSScalingFactor();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return GetLinuxScalingFactor();
        }
        catch
        {
        }

        return 1.0;
    }

    #region Windows

    [DllImport("user32.dll")]
    private static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("gdi32.dll")]
    private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

    [DllImport("user32.dll")]
    private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    private static double GetWindowsScalingFactor()
    {
        var hdc = GetDC(IntPtr.Zero);
        var dpi = GetDeviceCaps(hdc, 88 /* LOGPIXELSX */);
        ReleaseDC(IntPtr.Zero, hdc);
        return dpi / 96.0;
    }

    #endregion

    #region macOS

    [StructLayout(LayoutKind.Sequential)]
    private struct CGRect
    {
        public double X, Y, Width, Height;
    }

    [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
    private static extern uint CGMainDisplayID();

    [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
    private static extern IntPtr CGDisplayCopyDisplayMode(uint display, IntPtr options);

    [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
    private static extern void CGDisplayModeRelease(IntPtr mode);

    [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
    private static extern nuint CGDisplayModeGetWidth(IntPtr mode);

    [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
    private static extern nuint CGDisplayModeGetPixelWidth(IntPtr mode);

    [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
    private static extern CGRect CGDisplayBounds(uint display);

    [DllImport("/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics")]
    private static extern nuint CGDisplayPixelsWide(uint display);

    private static double GetMacOSScalingFactor()
    {
        try
        {
            var id = CGMainDisplayID();
            double ratio = 1.0;
            var have = false;

            var mode = CGDisplayCopyDisplayMode(id, IntPtr.Zero);
            if (mode != IntPtr.Zero)
            {
                try
                {
                    var lw = CGDisplayModeGetWidth(mode);
                    var pw = CGDisplayModeGetPixelWidth(mode);
                    if (lw != 0)
                    {
                        ratio = pw / (double)lw;
                        have = true;
                    }
                }
                finally
                {
                    CGDisplayModeRelease(mode);
                }
            }

            // Bounds vs pixels can both equal "native" width; mode ratio often still exposes 2×.
            if (!have || ratio < 1.001)
            {
                var b = CGDisplayBounds(id);
                var px = CGDisplayPixelsWide(id);
                if (b.Width > 0.5 && px > 0)
                {
                    var r2 = px / b.Width;
                    if (r2 > ratio + 0.01 || !have)
                        ratio = r2;
                    have = true;
                }
            }

            if (!have || ratio < 1.001)
                return 1.0;

            var n = Math.Round(ratio);
            return Math.Abs(ratio - n) < 0.06 ? Math.Clamp(n, 1.0, 4.0) : Math.Clamp(ratio, 1.0, 4.0);
        }
        catch
        {
            return 1.0;
        }
    }

    #endregion

    #region Linux

    [DllImport("libX11", EntryPoint = "XOpenDisplay")]
    private static extern IntPtr XOpenDisplay(string? display_name);

    [DllImport("libX11", EntryPoint = "XDisplayWidth")]
    private static extern int XDisplayWidth(IntPtr display, int screen_number);

    [DllImport("libX11", EntryPoint = "XDisplayWidthMM")]
    private static extern int XDisplayWidthMM(IntPtr display, int screen_number);

    [DllImport("libX11", EntryPoint = "XCloseDisplay")]
    private static extern int XCloseDisplay(IntPtr display);

    private static double GetLinuxScalingFactor()
    {
        foreach (var name in new[] { "GDK_SCALE", "GDK_DPI_SCALE", "QT_SCALE_FACTOR" })
        {
            var v = Environment.GetEnvironmentVariable(name);
            if (!string.IsNullOrEmpty(v) && double.TryParse(v, out var s) && s > 0)
                return s;
        }

        try
        {
            var d = XOpenDisplay(null);
            if (d == IntPtr.Zero)
                return 1.0;

            var px = XDisplayWidth(d, 0);
            var mm = XDisplayWidthMM(d, 0);
            XCloseDisplay(d);
            if (mm <= 0)
                return 1.0;

            return (px * 25.4 / mm) / 96.0;
        }
        catch
        {
            return 1.0;
        }
    }

    #endregion
}
