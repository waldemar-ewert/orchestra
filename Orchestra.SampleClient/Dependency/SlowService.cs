namespace Orchestra.SampleClient.Dependency;

public interface ISlowService
{
    Task<int> StartAsync(int parameter);

    Task<int> GetResultAsync();
}

public class SlowService : ISlowService
{
    private int _state;

    public Task<int> StartAsync(int parameter)
    {
        _state = parameter;
        return Task.FromResult(0);
    }

    public Task<int> GetResultAsync()
    {
        return Task.FromResult(_state + 1);
    }
}
