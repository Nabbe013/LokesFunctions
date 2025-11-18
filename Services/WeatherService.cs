using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WeatherFunctionTest.Models;

namespace WeatherFunctionTest.Services
{
    public class WeatherService
    {

            private readonly HttpClient _httpClient;

            public WeatherService(HttpClient httpClient)
            {

                _httpClient = httpClient;

            }

            public async Task<WeatherDTO> GetPostAsync()
            {
                var url = "https://weatherapi.dreammaker-it.se/Forecast?city=Linkoping&lang=se";

                var response = await _httpClient.GetFromJsonAsync<WeatherDTO>(url);

                if (response == null)
                {
                    return new WeatherDTO();
                }

                return response;
            }
        }

}

