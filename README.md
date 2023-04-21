# Orchestra project

Light workflow engine in C#

## Main goals

- The business processes expressed in plain old C#
- The engine embedded into consumer's host (console, WebAPI, etc.)
- The engine SQL database embedded into consumer's SQL database

## Process expressed in C#

The business process looks like following C# code:
```
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

```

## Database

The engine schema consist of 3 types of tables:
- one table for each process type
  - the table keeps instance contexts of certain process type
  - the table's scheme is unique for each process type
- one system table for maintenance of all process instances
  - like what state the process instance is dehydrated in, etc.
- one system table for needs of the engine
  - like version of engine, etc.
