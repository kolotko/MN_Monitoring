using Microsoft.Extensions.Options;
using Models;

namespace CustomOpenTelemetry;

public class CustomOpenTelemetryAbstraction
{
    protected bool _isEnable { get; set; }

    public CustomOpenTelemetryAbstraction(IOptions<AppSettings> appSettings)
    {
        _isEnable = appSettings.Value.OTEnable;
    }

    protected bool IsEnable()
    {
        return _isEnable;
    }
}