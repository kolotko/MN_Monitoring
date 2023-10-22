using System.Diagnostics;
using CustomOpenTelemetry;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace SecondApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly OTSecondaryApiMethod _oTSecondaryApiMethod;

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, OTSecondaryApiMethod oTSecondaryApiMethod)
    {
        _logger = logger;
        _oTSecondaryApiMethod = oTSecondaryApiMethod;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get(string city, int numberOfDays)
    {
        
        _oTSecondaryApiMethod.DisplayParameters(city, numberOfDays);
        WeatherForecastData[] forecasts;
        using (var activity = _oTSecondaryApiMethod.GetActivityForGeneratingData())
        {
            await Task.Delay(Random.Shared.Next(5, 100));
            forecasts = Enumerable.Range(1, numberOfDays).Select(index => new WeatherForecastData
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
        }
        
        return Ok(new WeatherForecastResponse()
        {
            City = city,
            Forecast = forecasts
        });
    }
}