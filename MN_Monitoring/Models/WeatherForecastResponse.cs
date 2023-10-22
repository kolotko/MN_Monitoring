namespace Models;

public class WeatherForecastResponse
{
    public string City { get; set; }
    public WeatherForecastData[] Forecast { get; set; }
}