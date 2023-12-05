
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
        public void SetParameters(double startSpeed, double acceleration, double maxSpeed
            , double? fuelCount = null, double? fuelCosumption = null);
        string Name { get; }
    }
}