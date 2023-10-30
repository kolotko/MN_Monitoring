using Metrics.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Metrics.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly DiagnosticsConfig _diagnosticsConfig;

    public WeatherForecastController(DiagnosticsConfig diagnosticsConfig)
    {
        _diagnosticsConfig = diagnosticsConfig;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _diagnosticsConfig.AddCustomCounter();
        _diagnosticsConfig.AddObservableCustomCounter();
        _diagnosticsConfig.RecordCustomHistogram();
        if (new Random().NextDouble() >= 0.5)
        {
            _diagnosticsConfig.SubtractCustomUpDownCounter();
            _diagnosticsConfig.SubtractObservableCustomUpDownCounter();
        }
        else
        {
            _diagnosticsConfig.AddCustomUpDownCounter();
            _diagnosticsConfig.AddObservableCustomUpDownCounter();
        }
        _diagnosticsConfig.RecordObservableGauge();
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}