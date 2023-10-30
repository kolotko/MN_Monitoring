using Metrics.Diagnostics;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DiagnosticsConfig>();



builder.Services.AddOpenTelemetry().WithMetrics(opts => opts
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Metrics"))
    .AddMeter(DiagnosticsConfig.ServiceName)
    .AddView(
        instrumentName: "custom-histogram",
        new ExplicitBucketHistogramConfiguration { Boundaries = new double[] { 0, 10, 20, 30, 40, 50 } })
    .AddAspNetCoreInstrumentation()
    .AddRuntimeInstrumentation()
    .AddProcessInstrumentation()
    .AddPrometheusExporter()
);


var app = builder.Build();

app.UseOpenTelemetryPrometheusScrapingEndpoint();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();