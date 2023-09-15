using Microsoft.Extensions.Logging;
using MN_Logs.SourceGeneratorMethods;

namespace MN_Logs;

public class Examples
{
    private ILogger<Examples> _logger;
    
    public Examples(ILogger<Examples> logger)
    {
        _logger = logger;
    }
    
    private void ExampleWayToLogFromArticle()
    {
        var s1 = "test";
        var s2 = "test";
        _logger.LogInformation("przykład porównania stringów: {1}", s1 == s2);
            
        var o1 = (object)1;
        var o2 = (object)1;
        _logger.LogInformation("przykład porównania obiektów: {0}", o1 == o2);
            
        var example = new { Amount = 108, Message = "Hello" };
        
        _logger.LogInformation("example => " + example.Amount + " " + example.Message);
        _logger.LogInformation($"example => {example.Amount}, {example.Message}");
        _logger.LogInformation(string.Format("example => {0}, {1}", example.Amount, example.Message));
        _logger.LogInformation("example => {example}", example);
        _logger.DisplayExampleData(example.Amount, example.Message);
    }
}