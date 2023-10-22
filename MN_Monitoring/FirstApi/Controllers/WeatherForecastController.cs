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
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly HttpClient _client;
    private readonly AppSettings _appSettings;
    private readonly OTFirstApiMethod _otFirstApiMethod;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory httpClientFactory, IOptions<AppSettings> appSettings, OTFirstApiMethod otFirstApiMethod)
    {
        _logger = logger;
        _client = httpClientFactory.CreateClient();
        _appSettings = appSettings.Value;
        _otFirstApiMethod = otFirstApiMethod;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get(string city, int numberOfDays)
    {
        _otFirstApiMethod.DisplayParameters(city, numberOfDays);
        
        var forecasts = await _client.GetFromJsonAsync<WeatherForecastResponse>($"{_appSettings.SecondApiUrl}/WeatherForecast?city={city}&numberOfDays={numberOfDays}");

        return Ok(forecasts);
    }
}