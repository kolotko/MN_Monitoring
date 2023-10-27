using System.Diagnostics;
using CustomOpenTelemetry.ConstsClass;
namespace CustomOpenTelemetry;

public static class OTFirstApiMethod
{
    
    public static void FirstApiValidateCityLength(this Activity activity)
    {
        activity.AddEvent(new ActivityEvent(FirstApiConsts.ValidateCityLengthMessage, 
            tags: new ActivityTagsCollection(new 
                List<KeyValuePair<string, object?>>
                {
                    new (FirstApiConsts.PropertyReason, "city length is too short"),
                }
            )
        )).SetStatus(ActivityStatusCode.Error);
    }
    public static void FirstApiDisplayParameters(this Activity activity, string city, int numberOfDays)
    {
        activity.AddEvent(new ActivityEvent(FirstApiConsts.DisplayParametersMessage, 
            tags: new ActivityTagsCollection(new 
                List<KeyValuePair<string, object?>>
                {
                    new (FirstApiConsts.PropertyCity, city),
                    new (FirstApiConsts.PropertyNumberOfDays, numberOfDays),
                }
            )
        ));
    }
}