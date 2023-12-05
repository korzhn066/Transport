
using System.Collections.Generic;
using Transport.Enums;
using Transport.Models;

namespace Transport.Interface
{
    internal interface ITransport
    {
        bool CheckRoad(RoadEnum road);
        public List<RectangleProps> GetTransportImage(int roadIndex, int roadCount, double screenWidth, double screenHeight, double x);
        public TransportResponse GetTransportInfoInTime(double screenWidth, int time);

        //public void SetParameters();
        public double FuelConsumption { get; set; }
        public double FuelCount { get; set; }
        string Name { get; }
        double Acceleration { get; set; }
        double MaxSpeed { get; set; }
        double StartSpeed { get; set; }
    }
}