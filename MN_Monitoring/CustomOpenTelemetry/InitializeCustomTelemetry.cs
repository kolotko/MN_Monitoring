using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace CustomOpenTelemetry;

public static class InitializeCustomTelemetry
{
    public static void CustomTelemetryInitialize(this IServiceCollection services,  IConfiguration configuration)
    {
        var jaegerUrl = configuration.GetSection("AppSettings:JaegerUrl").Value;
        var oTServiceName = configuration.GetSection("AppSettings:OTServiceName").Value;
        var oTSourceName = configuration.GetSection("AppSettings:OTSourceName").Value;
        var oTProjectVersion = configuration.GetSection("AppSettings:OTProjectVersion").Value;
        
        services.AddOpenTelemetry()
            .ConfigureResource(resourceBuilder => 
                resourceBuilder.AddService(oTServiceName)
                     .AddAttributes(new List<KeyValuePair<string, object>>()
                     {
                         new ("Version", oTProjectVersion)
                     }))
            .WithTracing(
                builder => builder.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddConsoleExporter()
                    .AddSource(oTSourceName)
                    .AddOtlpExporter(opt =>
                    {
                        opt.Endpoint = new Uri(jaegerUrl);
                    })
            );
    }
}