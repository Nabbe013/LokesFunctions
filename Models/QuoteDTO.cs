using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LokesFunctions.Models
{
    public class QuoteDTO
    {
        public string quote { get; set; }
        public string author { get; set; }
        public string work { get; set; }
        public List<string> categories { get; set; }
    }
}
