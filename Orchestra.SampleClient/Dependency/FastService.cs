namespace Orchestra.SampleClient.Dependency;

public interface IFastService
{
    Task<int> CallAsync(int parameter);
}

public class FastService : IFastService
{
    public Task<int> CallAsync(int parameter)
    {
        return Task.FromResult(parameter + 1);
    }
}