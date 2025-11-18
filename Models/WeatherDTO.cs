using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherFunctionTest.Models

{  
        public class WeatherDTO : ITableEntity
        {
            public string RowKey { get; set; } = string.Empty;
            public string PartitionKey { get; set; } = string.Empty; // Weather
            public ETag ETag { get; set; }
            public DateTimeOffset? Timestamp { get; set; }
            public string summary { get; set; }
            public string city { get; set; }
            public string lang { get; set; }
            public int temperatureC { get; set; }
            public int temperatureF { get; set; }
            public int humidity { get; set; }
            public int windSpeed { get; set; }
            public DateTime date { get; set; }
            public int unixTime { get; set; }
            public Icon icon { get; set; }
        }

        public class Icon
        {
            public string url { get; set; }
            public string code { get; set; }
        }

    
}
