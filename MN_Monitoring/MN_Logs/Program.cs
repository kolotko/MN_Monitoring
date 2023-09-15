using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MN_Logs;
using Serilog;


var host = CreateHostBuilder(args).Build();
var workerInstance = host.Services.GetRequiredService<Worker>();
workerInstance.Execute();
host.RunAsync();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((builder, services) =>
        {
            services.AddTransient<Worker>();
        }).UseSerilog((context, services, configuration) => {
            configuration.ReadFrom.Configuration(context.Configuration);
        });

// static IConfiguration LoadConfiguration()
// {
//     var builder = new ConfigurationBuilder()
//         .SetBasePath(Directory.GetCurrentDirectory())
//         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
//
//     return builder.Build();
// }