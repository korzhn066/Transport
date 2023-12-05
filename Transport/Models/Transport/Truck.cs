using System;
using System.Collections.Generic;
using Transport.Enums;
using Transport.Interface;
using Transport.Models.Transport.Base;

namespace Transport.Models.Transport
{
    internal class Truck : TransportBase, ITransport
    {
        public string Name => "Truck";
        public void SetParameters(double startSpeed, double acceleration, double maxSpeed, 
            double? fuelCount = null, double? fuelCosumption = null)
        {
            StartSpeed = startSpeed;
            Acceleration = acceleration;
            MaxSpeed = maxSpeed;
            FuelCount = fuelCount;
            FuelConsumption = fuelCosumption;
        }

        public bool CheckRoad(RoadEnum road)
        {
            if (road == RoadEnum.Highway)
                return true;

            return false;
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

        private readonly Random _random = new ();

        private double lastOffsetX = 0;
        public TransportResponse GetTransportInfoInTime(double screenWidth, int time)
        {
            if (IsWorking && _random.Next(0, 10000) < 1)
                IsWorking = false;

            if (FuelCount <= 0)
                IsWorking = false;

            if (time % 60 == 0)
            {
                FuelCount -= FuelConsumption;
            }

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

