using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Models
{
    internal class Magazine
    {
        public List<double> RemainingFuel { get; } = new();
        public List<double> OffsetsX { get; } = new();
        public List<bool> IsWorking { get; } = new();
    }
}
