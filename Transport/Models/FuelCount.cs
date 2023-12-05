using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Enums;

namespace Transport.Models
{
    internal class FuelCount
    {
        public FuelEnum Name { get; set; }
        public double Count { get; set; }

        public override string ToString()
        {
            return Name.ToString() + " - " + Count.ToString();
        }
    }
}
