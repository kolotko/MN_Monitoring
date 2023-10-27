using System.Diagnostics;
using CustomOpenTelemetry.ConstsClass;
namespace CustomOpenTelemetry;

public static class OTFirstApiMethod
{
    public static void FirstApiDisplayParameters(this Activity activity, string city, int numberOfDays)
    {
        // TODO zmien to na event
        // using var activity2 = activity.AddEvent(FirstApiConsts.DisplayParametersMessage);
        // activity.SetTag("city", city);
        // activity.SetTag("numberOfDays", numberOfDays);
        //
        
        activity.AddEvent(new ActivityEvent(FirstApiConsts.DisplayParametersMessage, 
            tags: new ActivityTagsCollection(new 
                List<KeyValuePair<string, object?>>
                {
                    new ("city", city),
                    new ("numberOfDays", numberOfDays),
                }
            )
        ));
    }
}