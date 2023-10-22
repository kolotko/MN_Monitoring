using System.Diagnostics;
using Microsoft.Extensions.Options;
using Models;

namespace CustomOpenTelemetry;

public class OTFirstApiMethod : CustomOpenTelemetryAbstraction
{
    public OTFirstApiMethod(IOptions<AppSettings> appSettings) : base(appSettings) {}
    
    public void DisplayParameters(string city, int numberOfDays)
    {
        if (!IsEnable())
            return;
        
        using var activity = Activity.Current?.Source.StartActivity($"Request data for city: {city} and number of days: {numberOfDays}");
        activity.SetTag("city", city);
        activity.SetTag("numberOfDays", numberOfDays);
    }
}