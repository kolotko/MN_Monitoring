using System.Diagnostics;
using CustomOpenTelemetry;
using Microsoft.AspNetCore.Mvc;

namespace ThirdApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet(Name = "GetWeatherForecast")]
    public void Get(string traceId, string spanId)
    {
        Activity.Current?.ThirdApiCreateSpanLinkForOtherContext(traceId, spanId);
    }
}