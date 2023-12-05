using System.Collections.Generic;
using Transport.Enums;
using Transport.Interface;

namespace Transport.Models.Roads
{
    internal class Sidewalk : IRoad
    {
        public RoadEnum Name => RoadEnum.Sidewalk;

        public List<RectangleProps> GetRoadImage(double width, double height, double x, double y)
        {
            var roadImage = new List<RectangleProps>
            {
                new RectangleProps("Green", x, y, width, height * 0.2),
                new RectangleProps("Green", x, y + height * 0.8, width, height * 0.2)
            };

            var tilesNumberX = 3;
            var tilesNumberY = 3;
            var tilesWidth = width / tilesNumberX;
            var tilesHeight = height * 0.6 / tilesNumberY;

            for (int i = 0; i < tilesNumberX; i++)
            {
                for (int j = 0; j < tilesNumberY; j++)
                {
                    if (i % 2 == 0 && j % 2 == 0 ||
                        i == j)
                    {
                        roadImage.Add(new RectangleProps("white", x + i * tilesWidth, y + height * 0.2 + j * tilesHeight, tilesWidth, tilesHeight));
                    }
                    else
                    {
                        roadImage.Add(new RectangleProps("Black", x + i * tilesWidth, y + height * 0.2 + j * tilesHeight, tilesWidth, tilesHeight));
                    }
                }
            }

            return roadImage;
        }
    }
}
