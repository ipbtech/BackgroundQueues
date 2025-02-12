using BackgroundQueues.Events;
using BackgroundQueues.Extensions;
using BackgroundQueues.Services;
using Coravel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<OperationExecutor>();
builder.Services.AddSingleton<OperationQueue>();
builder.Services.AddHostedService<BackgroundExecutionService>();

builder.Services.AddEvents();
builder.Services.AddScoped<OperationStartedEventListener>();
builder.Services.AddScoped<OperationValidatedEventListener>();
builder.Services.AddScoped<OperationExecutedEventListener>();
builder.Services.AddScoped<OperationFinishedEventListener>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCoravelEvents();

app.MapControllers();
app.Run();
