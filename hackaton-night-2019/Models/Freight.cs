using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hackaton_night_2019.Models
{
    public class Freight
    {
        public int FreightId { get; set; }
        public string FreightDescriptions { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public bool HasSupport { get; set; }
    }
}
