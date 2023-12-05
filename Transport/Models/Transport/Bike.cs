﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Enums;
using Transport.Interface;
using Transport.Models.Transport.Base;

namespace Transport.Models.Transport
{
    internal class Bike : TransportBase, ITransport
    { 
        public string Name => "Bike";
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
            if (road == RoadEnum.Sidewalk)
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

        private readonly Random _random = new();
        private double lastOffsetX = 0;
        public TransportResponse GetTransportInfoInTime(double screenWidth, int time)
        {
            if (IsWorking && _random.Next(0, 1000) < 1)
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

