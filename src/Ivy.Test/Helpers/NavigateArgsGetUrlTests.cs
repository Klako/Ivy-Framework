namespace Ivy.Test.Helpers;

public class NavigateArgsGetUrlTests
{
    [Fact]
    public void GetUrl_WithSafeUrlFragment_AppendsFragment()
    {
        var args = new NavigateArgs("onboarding-docs", UrlFragment: "introduction");
        var url = args.GetUrl();

        Assert.StartsWith("/onboarding-docs", url);
        Assert.EndsWith("#introduction", url);
    }

    [Fact]
    public void GetUrl_WithUrlFragment_AfterQueryString()
    {
        var args = new NavigateArgs("app", AppArgs: new { x = 1 }, UrlFragment: "section-a");
        var url = args.GetUrl();

        Assert.Contains("?", url);
        Assert.EndsWith("#section-a", url);
        Assert.True(url.IndexOf('?') < url.IndexOf('#', StringComparison.Ordinal));
    }

    [Fact]
    public void GetUrl_WithoutUrlFragment_NoHash()
    {
        var args = new NavigateArgs("dashboard");
        var url = args.GetUrl();

        Assert.DoesNotContain('#', url);
    }

    [Fact]
    public void GetUrl_WithNullUrlFragment_NoHash()
    {
        var args = new NavigateArgs("dashboard", UrlFragment: null);
        Assert.DoesNotContain('#', args.GetUrl());
    }

    [Fact]
    public void GetUrl_WithEmptyUrlFragment_NoHash()
    {
        var args = new NavigateArgs("dashboard", UrlFragment: "");
        Assert.DoesNotContain('#', args.GetUrl());
    }

    [Theory]
    [InlineData("bad fragment")]
    [InlineData("intro.dots")]
    [InlineData("has:colon")]
    [InlineData("unicode-я")]
    public void GetUrl_WithInvalidUrlFragment_Throws(string fragment)
    {
        var args = new NavigateArgs("app", UrlFragment: fragment);
        Assert.Throws<InvalidOperationException>(() => args.GetUrl());
    }

    [Fact]
    public void GetUrl_WithInvalidAppId_ThrowsBeforeFragment()
    {
        var args = new NavigateArgs("bad:id", UrlFragment: "valid");
        Assert.Throws<InvalidOperationException>(() => args.GetUrl());
    }
}
