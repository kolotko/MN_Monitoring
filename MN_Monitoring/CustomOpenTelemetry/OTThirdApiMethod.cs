using System.Diagnostics;
using CustomOpenTelemetry.ConstsClass;

namespace CustomOpenTelemetry;

public static class OTThirdApiMethod
{
    public static void ThirdApiCreateSpanLinkForOtherContext(this Activity activity, string traceId, string spanId)
    {
        var currentContext = new ActivityContext();
        var traceIdObject = ActivityTraceId.CreateFromString(traceId);
        var spanIdObject = ActivitySpanId.CreateFromString(spanId);
        var activityContext = new ActivityContext(traceIdObject, spanIdObject, ActivityTraceFlags.None);
        
        using var newActivity = Activity.Current?.Source.StartActivity(ThirdApiConsts.SpanLinkForOtherContextMessage, 
            ActivityKind.Internal, 
            currentContext,
            links: new List<ActivityLink>()
            {
                new ActivityLink(activityContext)
            });
    }
}