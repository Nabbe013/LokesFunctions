using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherFunctionTest.Models;

namespace WeatherFunctionTest
{
    public class FakeTableClient
    {
        private readonly List<WeatherDTO> _storage = new();

        public Task AddEntityAsync(WeatherDTO weather)
        {
            _storage.Add(weather);
            Console.WriteLine($"Weather saved locally: {weather.city}, {weather.date}");
            return Task.CompletedTask;
        }
        public IEnumerable<WeatherDTO> GetAll() => _storage;
    }
}
