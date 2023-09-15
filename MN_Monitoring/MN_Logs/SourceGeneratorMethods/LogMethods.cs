using Microsoft.Extensions.Logging;

namespace MN_Logs.SourceGeneratorMethods;

public static partial class LogMethods
{
    [LoggerMessage(
        Level = LogLevel.Warning, 
        Message = "example =>  {Amount}, {Message}")]
    public static partial void DisplayExampleData(this ILogger logger, int Amount, string message);
}