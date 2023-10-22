using System.Diagnostics;
using Microsoft.Extensions.Options;
using Models;

namespace CustomOpenTelemetry;

public class OTSecondaryApiMethod : CustomOpenTelemetryAbstraction
{
    public OTSecondaryApiMethod(IOptions<AppSettings> appSettings) : base(appSettings) {}
    
    public void DisplayParameters(string city, int numberOfDays)
    {
        if (!IsEnable())
            return;
        
        using var activity = Activity.Current?.Source.StartActivity($"Receive parameters. City: {city} and number of days: {numberOfDays}");
        activity.SetTag("city", city);
        activity.SetTag("numberOfDays", numberOfDays);
    }

    public Activity GetActivityForGeneratingData()
    {
        return Activity.Current?.Source.StartActivity("Generating data");
    }
}