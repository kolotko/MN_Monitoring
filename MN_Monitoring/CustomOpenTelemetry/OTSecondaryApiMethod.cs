﻿using System.Diagnostics;
using CustomOpenTelemetry.ConstsClass;
using OpenTelemetry.Trace;

namespace CustomOpenTelemetry;

public static class OTSecondaryApiMethod
{
    public static void SecondaryApiDisplayParameters(this Activity activity, string city, int numberOfDays)
    {
        activity.AddEvent(new ActivityEvent(SecondApiConsts.DisplayParametersMessage, 
            tags: new ActivityTagsCollection(new 
                List<KeyValuePair<string, object?>>
                {
                    new (SecondApiConsts.PropertyCity, city),
                    new (SecondApiConsts.PropertyNumberOfDays, numberOfDays),
                }
            )
        ));
    }
    public static void SecondaryApiSaveExceptionInTracing(this Activity activity, Exception exception)
    {
        activity.RecordException(exception, new TagList()
        {
            {SecondApiConsts.PropertyExampleParameter, 1}
        });
    }

    public static Activity SecondaryApiGetActivityForGeneratingData(this ActivitySource source)
    {
        var activity = source.StartActivity(SecondApiConsts.GeneratingDataMessage);
        activity.SetTag(SecondApiConsts.PropertyDelay, 1000);
        return activity;
    }
}