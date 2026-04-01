using Ivy.Core.Apps;
using Microsoft.AspNetCore.Http;

namespace Ivy.Tests.Core.Apps;

public class AppRouterTests
{
    private static HttpContext CreateHttpContext(string queryString)
    {
        var context = new DefaultHttpContext();
        context.Request.QueryString = new QueryString(queryString);
        return context;
    }

    [Fact]
    public void GetAppShell_ShellFalse_ReturnsFalse()
    {
        var context = CreateHttpContext("?shell=false");
        Assert.False(AppRouter.GetAppShell(context));
    }

    [Fact]
    public void GetAppShell_ShellTrue_ReturnsTrue()
    {
        var context = CreateHttpContext("?shell=true");
        Assert.True(AppRouter.GetAppShell(context));
    }

    [Fact]
    public void GetAppShell_ChromeFalse_ReturnsFalse()
    {
        var context = CreateHttpContext("?chrome=false");
        Assert.False(AppRouter.GetAppShell(context));
    }

    [Fact]
    public void GetAppShell_ChromeTrue_ReturnsTrue()
    {
        var context = CreateHttpContext("?chrome=true");
        Assert.True(AppRouter.GetAppShell(context));
    }

    [Fact]
    public void GetAppShell_NoParameter_ReturnsTrue()
    {
        var context = CreateHttpContext("");
        Assert.True(AppRouter.GetAppShell(context));
    }

    [Fact]
    public void GetAppShell_ShellTakePrecedenceOverChrome()
    {
        // ?shell=true&chrome=false → shell wins, returns true
        var context = CreateHttpContext("?shell=true&chrome=false");
        Assert.True(AppRouter.GetAppShell(context));
    }

    [Fact]
    public void GetAppShell_ShellFalseTakesPrecedenceOverChromeTrue()
    {
        // ?shell=false&chrome=true → shell wins, returns false
        var context = CreateHttpContext("?shell=false&chrome=true");
        Assert.False(AppRouter.GetAppShell(context));
    }

    [Fact]
    public void GetAppShell_CaseInsensitive()
    {
        var context = CreateHttpContext("?shell=FALSE");
        Assert.False(AppRouter.GetAppShell(context));

        var context2 = CreateHttpContext("?chrome=False");
        Assert.False(AppRouter.GetAppShell(context2));
    }
}
