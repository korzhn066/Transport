using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Models
{
    internal class Response
    {
        public bool Result { get; set; } = true;
        public string? Message { get; set; }
    }
}
