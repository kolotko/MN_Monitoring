using System.Diagnostics.Metrics;

namespace Metrics.Diagnostics;

public class DiagnosticsConfig
{
    public const string ServiceName = "custom-metrics";
    private readonly Meter _meter = new(ServiceName);
    private readonly Histogram<double> _customHistogram;
    private readonly Counter<int> _customCounter;
    private int _customObservableCounter_data;
    private readonly ObservableCounter<int> _customObservableCounter;
    private readonly UpDownCounter<int> _customUpDownCounter;
    private int _customObservableUpDown_data;
    private readonly ObservableUpDownCounter<int> _customObservableUpDownCounter;
    private int _customObservableGauge_data;
    private readonly ObservableGauge<int> _customObservableGauge;
    private readonly Random _random;
    
    public DiagnosticsConfig()
    {
        _random = new Random();
        _customHistogram = _meter.CreateHistogram<double>("custom-histogram");
        _customCounter = _meter.CreateCounter<int>("custom-counter");
        _customObservableCounter = _meter.CreateObservableCounter<int>("custom-observable-counter", ()=> _customObservableCounter_data);
        _customUpDownCounter = _meter.CreateUpDownCounter<int>("custom-up-down-counter");
        _customObservableUpDownCounter = _meter.CreateObservableUpDownCounter<int>("custom-observable-up-down-counte", () => _customObservableUpDown_data);
        _customObservableGauge = _meter.CreateObservableGauge<int>("custom-observable-gauge", () => _customObservableGauge_data);
    }

    public void AddCustomCounter() => _customCounter.Add(1);
    public void AddObservableCustomCounter() => _customObservableCounter_data+=2;
    public void AddCustomUpDownCounter() => _customUpDownCounter.Add(1);
    public void AddObservableCustomUpDownCounter() => _customObservableUpDown_data+=2;
    public void SubtractCustomUpDownCounter() => _customUpDownCounter.Add(-1);
    public void SubtractObservableCustomUpDownCounter() => _customObservableUpDown_data-=2;
    public void RecordCustomHistogram() => _customHistogram.Record(_random.Next(0, 50));
    public void RecordObservableGauge() => _customObservableGauge_data = _random.Next(0, 20);
    
}