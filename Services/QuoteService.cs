using LokesFunctions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LokesFunctions.Services
{
    public class QuoteService
    {

        private readonly HttpClient _httpClient;
        public QuoteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", "pZi1C6JpFpuqUFRph9DXZg==zHHkOMOVul1zzaFJ");
        }

        public async Task<QuoteDTO> GetPostAsync()
        {
            var url = "https://api.api-ninjas.com/v1/quotes";
            var response = await _httpClient.GetFromJsonAsync<List<QuoteDTO>>(url);

            var quote = response?.FirstOrDefault() ?? new QuoteDTO();

            return quote;
        }


    }
}
