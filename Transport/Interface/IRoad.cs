using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Enums;
using Transport.Models;

namespace Transport.Interface
{
    internal interface IRoad
    {
        RoadEnum Name { get; }
        List<RectangleProps> GetRoadImage(double width, double height, double x, double y);  
    }
}
