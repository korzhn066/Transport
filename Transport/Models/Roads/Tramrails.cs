using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Enums;
using Transport.Interface;

namespace Transport.Models.Roads
{
    internal class Tramrails : IRoad
    {
        public RoadEnum Name => RoadEnum.Tramrails;

        public List<RectangleProps> GetRoadImage(double width, double height, double x, double y)
        {
            var roadImage = new List<RectangleProps>
            {
                new RectangleProps("Yellow", x, y, width, height, "Tramrails"),
            };

            return roadImage;
        }
    }
}
