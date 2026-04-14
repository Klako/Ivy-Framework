using Ivy.Tendril.Hooks;
using Ivy.Tendril.Services;

namespace Ivy.Tendril.Test.Hooks;

public class UseStartJobTests
{
    private class MockState<T> : IState<T>
    {
        public T Value { get; private set; }

        public MockState(T initialValue)
        {
            Value = initialValue;
        }

        public void Set(T value)
        {
            Value = value;
        }
    }

    private class MockJobService : IJobService
    {
        public List<(string Type, string[] Args)> StartedJobs { get; } = new();
        public event Action? JobsChanged;
        public event Action<JobNotification>? NotificationReady;

        public string StartJob(string type, string[] args, string? inboxFilePath)
        {
            StartedJobs.Add((type, args));
            return Guid.NewGuid().ToString();
        }

        public string StartJob(string type, params string[] args)
        {
            StartedJobs.Add((type, args));
            return Guid.NewGuid().ToString();
        }

        public void CompleteJob(string id, int? exitCode, bool timedOut = false, bool staleOutput = false) { }
        public void StopJob(string id) { }
        public void DeleteJob(string id) { }
        public void ClearCompletedJobs() { }
        public void ClearFailedJobs() { }
        public Job? GetJob(string id) => null;
        public List<Job> GetJobs() => new();
        public List<Job> GetRecentlyCompletedJobs(int count = 10) => new();
        public void Dispose() { }
    }

    private class MockViewContext : IViewContext
    {
        private readonly Dictionary<Type, object> _services = new();
        private int _stateIndex = 0;
        private readonly List<object> _states = new();

        public void RegisterService<T>(T service) where T : notnull
        {
            _services[typeof(T)] = service;
        }

        public T UseService<T>()
        {
            if (_services.TryGetValue(typeof(T), out var service))
                return (T)service;
            throw new InvalidOperationException($"Service {typeof(T).Name} not registered");
        }

        public IState<T> UseState<T>(T? initialValue = default, bool buildOnChange = true)
        {
            if (_stateIndex < _states.Count)
            {
                return (IState<T>)_states[_stateIndex++];
            }

            var state = new MockState<T>(initialValue!);
            _states.Add(state);
            _stateIndex++;
            return state;
        }

        public void Reset()
        {
            _stateIndex = 0;
        }

        // Minimal implementations for interface compliance
        public void TrackDisposable(IDisposable disposable) { }
        public void TrackDisposable(IEnumerable<IDisposable> disposables) { }
        public IState<T> UseState<T>(Func<T> buildInitialValue, bool buildOnChange = true) =>
            UseState(buildInitialValue(), buildOnChange);
        public void UseEffect(Func<Task> handler, params IEffectTriggerConvertible[] triggers) { }
        public void UseEffect(Func<Task<IDisposable?>> handler, params IEffectTriggerConvertible[] triggers) { }
        public void UseEffect(Func<Task<IAsyncDisposable?>> handler, params IEffectTriggerConvertible[] triggers) { }
        public void UseEffect(Func<IDisposable?> handler, params IEffectTriggerConvertible[] triggers) { }
        public void UseEffect(Func<IAsyncDisposable?> handler, params IEffectTriggerConvertible[] triggers) { }
        public void UseEffect(Action handler, params IEffectTriggerConvertible[] triggers) { }
        public T CreateContext<T>(Func<T> factory) => factory();
        public T UseContext<T>() => throw new NotImplementedException();
        public object UseContext(Type type) => throw new NotImplementedException();
        public object UseService(Type type) => throw new NotImplementedException();
        public IReadOnlyList<object> UseStream<T>() => throw new NotImplementedException();
        public IWriteStream<T> UseWriteStream<T>() => throw new NotImplementedException();
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    [Fact]
    public void UseStartJob_IsStarting_StartsAsFalse()
    {
        // Arrange
        var context = new MockViewContext();
        var mockJobService = new MockJobService();
        context.RegisterService<IJobService>(mockJobService);

        // Act
        var (_, isStarting) = context.UseStartJob();

        // Assert
        Assert.False(isStarting);
    }

    [Fact]
    public void UseStartJob_IsStarting_BecomesTrueAfterStartJob()
    {
        // Arrange
        var context = new MockViewContext();
        var mockJobService = new MockJobService();
        context.RegisterService<IJobService>(mockJobService);

        // Act
        var (startJob, _) = context.UseStartJob();
        startJob("TestJob", new[] { "arg1", "arg2" });

        // Refresh the hook to get updated state
        context.Reset();
        var (_, isStartingAfter) = context.UseStartJob();

        // Assert
        Assert.True(isStartingAfter);
    }

    [Fact]
    public void UseStartJob_SubsequentCalls_IgnoredWhenIsStartingIsTrue()
    {
        // Arrange
        var context = new MockViewContext();
        var mockJobService = new MockJobService();
        context.RegisterService<IJobService>(mockJobService);

        // Act
        var (startJob, _) = context.UseStartJob();
        startJob("TestJob1", new[] { "arg1" });
        startJob("TestJob2", new[] { "arg2" });
        startJob("TestJob3", new[] { "arg3" });

        // Assert - only the first call should have triggered StartJob
        Assert.Single(mockJobService.StartedJobs);
        Assert.Equal("TestJob1", mockJobService.StartedJobs[0].Type);
    }

    [Fact]
    public void UseStartJob_CallsUnderlyingJobService()
    {
        // Arrange
        var context = new MockViewContext();
        var mockJobService = new MockJobService();
        context.RegisterService<IJobService>(mockJobService);

        // Act
        var (startJob, _) = context.UseStartJob();
        startJob("MakePlan", new[] { "-Description", "Test Plan", "-Project", "TestProject" });

        // Assert
        Assert.Single(mockJobService.StartedJobs);
        var (type, args) = mockJobService.StartedJobs[0];
        Assert.Equal("MakePlan", type);
        Assert.Equal(new[] { "-Description", "Test Plan", "-Project", "TestProject" }, args);
    }
}
