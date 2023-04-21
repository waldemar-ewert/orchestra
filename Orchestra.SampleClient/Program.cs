
using Orchestra.SampleClient.Dependency;
using Orchestra.SampleClient.Process;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IFastService, FastService>();
builder.Services.AddTransient<ISlowService, SlowService>();
builder.Services.AddTransient<ProcessDefinition, ProcessDefinition>();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/sample", () =>
{
    var process = app.Services.GetRequiredService<ProcessDefinition>();
    Task.WaitAll(process.ExecuteAsync());
    return "OK";
});

app.Run();
