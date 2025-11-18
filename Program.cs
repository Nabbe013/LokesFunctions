using LokesFunctions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherFunctionTest;
using WeatherFunctionTest.Services;


var host = new HostBuilder()
    
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddSingleton<FakeTableClient>();
        services.AddHttpClient<WeatherService>();
        services.AddHttpClient<QuoteService>();
        services.AddSingleton<Functions>();

    })
    .Build();


host.Run();
