using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Enums;
using Transport.Interface;

namespace Transport.Models.Roads
{
    internal class Highway : IRoad
    {
        public RoadEnum Name => RoadEnum.Highway;

        public List<RectangleProps> GetRoadImage(double width, double height, double x, double y)
        {
            var roadImage = new List<RectangleProps>
            {
                new RectangleProps("Black", x, y, width, height)
            };

            var lineCount = 10;
            var lineWidth = width / lineCount / 2;
            var lineHeight = height * 0.04;
            var lineY = y + height * 0.48;

            for (int i = 0; i < lineCount; i++)
            {
                roadImage.Add(new RectangleProps("White", lineWidth * 2 * i, lineY, lineWidth, lineHeight));
            }

            return roadImage;
        }
    }
}
