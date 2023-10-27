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

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get(string city, int numberOfDays)
    {
        Activity.Current?.SecondaryApiDisplayParameters(city, numberOfDays);
        WeatherForecastData[] forecasts;

        // throw new Exception();
        try
        {
            throw new Exception();
        }
        catch (Exception e)
        {
            Activity.Current?.SecondaryApiSaveExceptionInTracing(e);
        }
        
        using (var activity = Activity.Current?.Source.SecondaryApiGetActivityForGeneratingData())
        {
            await Task.Delay(1000);
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