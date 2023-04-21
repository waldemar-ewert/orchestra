using System.Diagnostics;

namespace Orchestra.Engine;

public class BaseProcessDefinition<TContext> where TContext : new()
{
    public TContext Context { get; set; } = new TContext();

    [DebuggerNonUserCode]
    public async Task<ProcessStep> Do(Func<Task> action)
    {
        await action();
        return new ProcessStep();
    }

    [DebuggerNonUserCode]
    public async Task<ProcessStep> If(Func<bool> condition, Task<ProcessStep>[] Then, Task<ProcessStep>[] Else)
    {
        foreach (var step in Then) { await step; }
        foreach (var step in Else) { await step; }
        return new ProcessStep();
    }

    [DebuggerNonUserCode]
    public async Task<ProcessStep> While(Func<bool> condition, Task<ProcessStep>[] steps)
    {
        foreach (var step in steps) { await step; }
        return new ProcessStep();
    }


    [DebuggerNonUserCode]
    public Task<T> Wait<T>(Func<Task<T>> callback) where T : new()
    {
        return Task.FromResult(new T());
    }
}