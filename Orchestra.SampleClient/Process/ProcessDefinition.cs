using Orchestra.Engine;
using Orchestra.SampleClient.Dependency;

namespace Orchestra.SampleClient.Process;

public class ProcessDefinition : BaseProcessDefinition<ProcessContext>
{
    private readonly IFastService _fastService;
    private readonly ISlowService _slowService;

    public ProcessDefinition(IFastService fastService, ISlowService slowService)
    {
        _fastService = fastService;
        _slowService = slowService;
    }

    public Task<ProcessStep>[] ExecuteAsync() => new[]
    {
        Do (async () =>
        {
            Context.Number = 1;
            var result1 = await _fastService.CallAsync(10);
            Context.Number += result1;
        }),
        Do (async () =>
        {
            await _slowService.StartAsync(Context.Id);
            var result2 = await Wait(_slowService.GetResultAsync);
            Context.Number += result2;
        }),
        If (() => Context.Number > 10, new[]
        {
            Do (async () =>
            {
                var result3 = await _fastService.CallAsync(20);
                Context.Number += result3;
            }),
            Do (async () =>
            {
                await _slowService.StartAsync(Context.Id);
                var result4 = await Wait(_slowService.GetResultAsync);
                Context.Number += result4;
            })
        },
        Else: new []
        {
            Do (async () =>
            {
                var result3 = await _fastService.CallAsync(30);
                Context.Number += result3;
            })
        }),
        While (() => Context.Number > 3, new[]
        {
            Do (async () =>
            {
                await _slowService.StartAsync(Context.Id);
                var result4 = await Wait(_slowService.GetResultAsync);
                Context.Number += result4;
            })
        })
    };
}