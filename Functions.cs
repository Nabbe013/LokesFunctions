using Azure;
using Azure.Data.Tables;
using LokesFunctions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WeatherFunctionTest.Models;
using WeatherFunctionTest.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WeatherFunctionTest
{
    public class Functions
    {
        private readonly ILogger _logger;
        private readonly WeatherService _weatherService;
        private readonly FakeTableClient _tableClient;
        private readonly QuoteService _quoteService;

        public Functions(ILoggerFactory loggerFactory, WeatherService weatherService,
            FakeTableClient tableClient, QuoteService quoteService)
        {
            _logger = loggerFactory.CreateLogger<Functions>();
            _weatherService = weatherService;
            _tableClient = tableClient;
            _quoteService = quoteService;
        }

        [Function("SaveWeather")]
        public async Task<HttpResponseData> SaveWeatherAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var weatherDto = await _weatherService.GetPostAsync();

            weatherDto.PartitionKey = "Weather";
            weatherDto.RowKey = Guid.NewGuid().ToString();
            weatherDto.date = DateTime.UtcNow;
            

            await _tableClient.AddEntityAsync(weatherDto);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(weatherDto);
            return response;

        }

        [Function("GetQuote")]
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
