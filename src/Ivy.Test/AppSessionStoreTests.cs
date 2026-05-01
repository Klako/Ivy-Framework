using Ivy.Core.Server;

namespace Ivy.Test;

public class AppSessionStoreTests
{
    [Fact]
    public async Task ScheduleDeferredRemoval_RemovesAfterDelay()
    {
        using var store = new AppSessionStore();
        var removed = false;

        store.ScheduleDeferredRemoval("conn-1", TimeSpan.FromMilliseconds(50), async connId =>
        {
            removed = true;
            await Task.CompletedTask;
        });

        Assert.True(store.HasDeferredRemoval("conn-1"));
        Assert.False(removed);

        await Task.Delay(200);

        Assert.True(removed);
        Assert.False(store.HasDeferredRemoval("conn-1"));
    }

    [Fact]
    public async Task CancelDeferredRemoval_PreventsRemoval()
    {
        using var store = new AppSessionStore();
        var removed = false;

        store.ScheduleDeferredRemoval("conn-1", TimeSpan.FromMilliseconds(200), async connId =>
        {
            removed = true;
            await Task.CompletedTask;
        });

        Assert.True(store.HasDeferredRemoval("conn-1"));

        var cancelled = store.CancelDeferredRemoval("conn-1");
        Assert.True(cancelled);
        Assert.False(store.HasDeferredRemoval("conn-1"));

        await Task.Delay(400);

        Assert.False(removed);
    }

    [Fact]
    public void CancelDeferredRemoval_NoExistingDeferral_ReturnsFalse()
    {
        using var store = new AppSessionStore();

        Assert.False(store.CancelDeferredRemoval("conn-1"));
    }

    [Fact]
    public void HasDeferredRemoval_NoDeferral_ReturnsFalse()
    {
        using var store = new AppSessionStore();

        Assert.False(store.HasDeferredRemoval("conn-1"));
    }

    [Fact]
    public async Task ScheduleDeferredRemoval_ReplacesExisting()
    {
        using var store = new AppSessionStore();
        var firstCalled = false;
        var secondCalled = false;

        store.ScheduleDeferredRemoval("conn-1", TimeSpan.FromMilliseconds(200), async connId =>
        {
            firstCalled = true;
            await Task.CompletedTask;
        });

        // Replace with a new deferred removal
        store.ScheduleDeferredRemoval("conn-1", TimeSpan.FromMilliseconds(50), async connId =>
        {
            secondCalled = true;
            await Task.CompletedTask;
        });

        await Task.Delay(300);

        Assert.False(firstCalled);
        Assert.True(secondCalled);
    }
}
