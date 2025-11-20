using Azure;
using Azure.Data.Tables;
using LokesFunctions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Timer;
//using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WeatherFunctionTest.Models;
using WeatherFunctionTest.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Extensions.Configuration;


namespace WeatherFunctionTest
{
    public class Functions
    {
        private readonly ILogger _logger;
        private readonly WeatherService _weatherService;        
        private readonly QuoteService _quoteService;
        private readonly TableServiceClient _tableServiceClient;

        public Functions(ILoggerFactory loggerFactory, WeatherService weatherService,
             QuoteService quoteService, TableServiceClient tableServiceClient)
        {
            _logger = loggerFactory.CreateLogger<Functions>();
            _weatherService = weatherService;          
            _quoteService = quoteService;
            _tableServiceClient = tableServiceClient;
                    
        }

   
        [Function("SaveWeatherTimer")] // Saving weather history to Azure Table Storage every 12 hours
        public async Task SaveWeatherTimer([TimerTrigger("0 0 */12 * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"Timer triggered at {DateTime.UtcNow}");

            var weatherDto = await _weatherService.GetPostAsync() ?? new WeatherDTO();

            weatherDto.PartitionKey = "Weather";
            weatherDto.RowKey = Guid.NewGuid().ToString();
            weatherDto.date = DateTime.UtcNow;

            var tableClient = _tableServiceClient.GetTableClient("WeatherHistory");
            await tableClient.CreateIfNotExistsAsync(); 
            await tableClient.AddEntityAsync(weatherDto);
                 
        }

        [Function("GetQuote")] // HTTP request to get a quote
        public async Task<HttpResponseData> GetQuoteAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var quoteDto = await _quoteService.GetPostAsync();
            
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(quoteDto);
            return response;
        }
    }
}
