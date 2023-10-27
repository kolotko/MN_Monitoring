using System.Diagnostics;
using CustomOpenTelemetry;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;

namespace FirstApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly HttpClient _client;
    private readonly AppSettings _appSettings;
    
    public WeatherForecastController(IHttpClientFactory httpClientFactory, IOptions<AppSettings> appSettings)
    {
        _client = httpClientFactory.CreateClient();
        _appSettings = appSettings.Value;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get(string city, int numberOfDays)
    {
        if (city.Length < 3)
        {
            Activity.Current?.FirstApiValidateCityLength();
            return BadRequest();
        }
        Activity.Current?.FirstApiDisplayParameters(city, numberOfDays);

        var forecasts = await _client.GetFromJsonAsync<WeatherForecastResponse>($"{_appSettings.SecondApiUrl}/WeatherForecast?city={city}&numberOfDays={numberOfDays}");

        return Ok(forecasts);
    }
}