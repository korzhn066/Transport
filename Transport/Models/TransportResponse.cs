using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Models
{
    internal class TransportResponse
    {
        public double RemainingFuel { get; set; }
        public double OffsetX { get; set; }
        public bool IsWorking { get; set; } = true;
    }
}
