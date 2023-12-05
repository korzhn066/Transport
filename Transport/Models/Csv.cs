using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Models
{
    internal class Csv
    {
        public int Time { get; set; }
        public string FuelCount { get; set; } = null!;

        public string IsWorking { get; set; } = null!;
    }
}
