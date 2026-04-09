using Ivy.Tendril.Apps.Jobs;
using Ivy.Tendril.Services;

namespace Ivy.Tendril.Test;

public class JobServiceCompletionGuardTests
{
    private static JobService CreateService()
    {
        SynchronizationContext.SetSynchronizationContext(null);
        return new JobService(
            TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(10));
    }

    [Fact]
    public void CompleteJob_ConcurrentCalls_OnlyFirstCompletes()
    {
        var service = CreateService();
        var id = service.CreateTestJob("ExecutePlan", "test-plan");

        var notificationCount = 0;
        service.NotificationReady += _ => Interlocked.Increment(ref notificationCount);

        using var barrier = new Barrier(2);
        var statuses = new JobStatus?[2];

        var t1 = new Thread(() =>
        {
            barrier.SignalAndWait();
            service.CompleteJob(id, 0);
            statuses[0] = service.GetJob(id)?.Status;
        });

        var t2 = new Thread(() =>
        {
            barrier.SignalAndWait();
            service.CompleteJob(id, 1);
            statuses[1] = service.GetJob(id)?.Status;
        });

        t1.Start();
        t2.Start();
        t1.Join(TimeSpan.FromSeconds(5));
        t2.Join(TimeSpan.FromSeconds(5));

        var job = service.GetJob(id);
        Assert.NotNull(job);
        // Only one thread should have completed the job — status should be either Completed or Failed, not both
        Assert.True(job.Status is JobStatus.Completed or JobStatus.Failed);
        // Only one notification should have fired
        Assert.Equal(1, notificationCount);
    }

    [Fact]
    public void StopJob_RacingWithCompleteJob_OnlyOneWins()
    {
        var service = CreateService();
        var id = service.CreateTestJob("ExecutePlan", "test-plan");

        var notificationCount = 0;
        service.NotificationReady += _ => Interlocked.Increment(ref notificationCount);

        using var barrier = new Barrier(2);

        var t1 = new Thread(() =>
        {
            barrier.SignalAndWait();
            service.StopJob(id);
        });

        var t2 = new Thread(() =>
        {
            barrier.SignalAndWait();
            service.CompleteJob(id, 0);
        });

        t1.Start();
        t2.Start();
        t1.Join(TimeSpan.FromSeconds(5));
        t2.Join(TimeSpan.FromSeconds(5));

        var job = service.GetJob(id);
        Assert.NotNull(job);
        // Status should be one terminal state — not corrupted
        Assert.True(job.Status is JobStatus.Stopped or JobStatus.Completed);
    }

    [Fact]
    public void CompleteJob_AfterStopJob_IsNoOp()
    {
        var service = CreateService();
        var id = service.CreateTestJob("ExecutePlan", "test-plan");

        service.StopJob(id);
        var job = service.GetJob(id);
        Assert.NotNull(job);
        Assert.Equal(JobStatus.Stopped, job.Status);

        // CompleteJob after StopJob should be a no-op
        service.CompleteJob(id, 0);

        job = service.GetJob(id);
        Assert.Equal(JobStatus.Stopped, job!.Status);
    }

    [Fact]
    public void CompleteJob_StaleOutputDetected_SetsTimeoutWithStaleReason()
    {
        var service = CreateService();
        var id = service.CreateTestJob("ExecutePlan", "test-plan");

        var job = service.GetJob(id);
        Assert.NotNull(job);
        job.StaleOutputDetected = true;

        service.CompleteJob(id, null, timedOut: true, staleOutput: true);

        job = service.GetJob(id);
        Assert.NotNull(job);
        Assert.Equal(JobStatus.Timeout, job.Status);
        Assert.Contains("No output for 10 minutes", job.StatusMessage);
    }
}
