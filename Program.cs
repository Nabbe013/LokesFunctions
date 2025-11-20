using Azure.Data.Tables;
using LokesFunctions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
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

        services.AddSingleton<TableServiceClient>( sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            string connString = configuration["AzureWebJobsStorage"];
            return new TableServiceClient(connString);
        });

        services.AddHttpClient<WeatherService>();
        services.AddHttpClient<QuoteService>();
       

    })
    .Build();


host.Run();
