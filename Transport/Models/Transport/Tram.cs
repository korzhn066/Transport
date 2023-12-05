using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Enums;
using Transport.Interface;
using Transport.Models.Transport.Base;

namespace Transport.Models.Transport
{
    internal class Tram : TransportBase, ITransport
    {
        public FuelEnum Fuel => FuelEnum.Electricity;
        public string Name => "Tram";
        public double Acceleration { get; set; }
        public double MaxSpeed { get; set; }
        public double StartSpeed { get; set; }
        public double FuelConsumption { get; set; }
        public double FuelCount { get; set; }

        public bool CheckRoad(RoadEnum road)
        {
            if (road == RoadEnum.Tramrails)
                return true;

            return false;
        }

        public double WastedFuel(int time)
        {
            return MaxSpeed / 10 * time;
        }

        public List<RectangleProps> GetTransportImage(int roadIndex, int roadCount, double screenWidth, double screenHeight, double x)
        {

            var list = new List<RectangleProps>();

            var width = screenWidth / 5;
            var height = screenHeight / 5;
            var y = screenHeight / (roadCount * 2) * ((roadIndex + 1) * 2 - 1) - height / 2;

            list.Add(new RectangleProps("White", x, y, width, height, Name));

            return list;
        }

        private Random _random = new();
        private double lastOffsetX = 0;
        public TransportResponse GetTransportInfoInTime(double screenWidth, int time)
        {
            if (IsWorking && _random.Next(0, 100) < 1)
                IsWorking = false;

            double offsetX;

            if (IsWorking)
            {
                offsetX = GetX(screenWidth, Acceleration, StartSpeed, MaxSpeed, time);
                lastOffsetX = offsetX;
            }
            else
            {
                offsetX = lastOffsetX;
            }



            return new TransportResponse()
            {
                RemainingFuel = 0,
                OffsetX = offsetX,
                IsWorking = IsWorking
            };
        }
    }
}

