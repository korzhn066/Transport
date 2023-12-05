using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Transport.Interface;
using Transport.Models;

namespace Transport
{
    internal static class RoadsCarsControlService
    {
        public static ObservableCollection<RectangleProps> CanvasFiling { get; } = new();
        public static ObservableCollection<string> RoadsList { get; } = new();

        private static readonly Dictionary<int, IRoad> _roads = new();

        private static readonly Dictionary<int, ITransport> _transport = new();

        public static readonly Dictionary<int, Magazine> TransportMagazine = new();
        public static bool IsStart { get; set; } = false;

        public static double ScreenWidth { get; set; }
        public static double ScreenHeight { get; set; }

        public static Response AddTransport(ITransport transport, int roadIndex)
        {
            if (IsStart)
                return new Response()
                {
                    Result = false,
                    Message = "Simlation started"
                };

            if (_roads.Count == 0)
                return new Response()
                {
                    Result = false,
                    Message = "You must add road"
                };

            if (_roads.Count <= roadIndex)
                return new Response()
                {
                    Result = false,
                    Message = "You must add road"
                };

            if (_transport.ContainsKey(roadIndex))
                return new Response()
                {
                    Result = false,
                    Message = "You print same road index"
                };

            if (!transport.CheckRoad(_roads[roadIndex].Name))
                return new Response()
                {
                    Result = false,
                    Message = "The" + transport.Name + "cannot be placed on the" + _roads[roadIndex].Name.ToString()
                };

            _transport.Add(roadIndex, transport);

            FillCanvas(0);

            return new Response();
        } 

        public static void AddRoad(IRoad road)
        {
            if (IsStart)
                return;

            RoadsList.Add(road.Name + " - " + _roads.Count.ToString());
            _roads.Add(_roads.Count, road);

            FillCanvas(0);
        }

        public static void FillCanvas(int time)
        {
            CanvasFiling.Clear();
            int i = 0;
            foreach (var road in _roads)
            {
                foreach (var rect in road.Value.GetRoadImage(ScreenWidth, ScreenHeight / _roads.Count, 0, (ScreenHeight / _roads.Count) * i))
                {
                    CanvasFiling.Add(rect);
                }
                i++;
            }

            if (time != 0)
            {
                i = 0;
                foreach (var transpot in _transport)
                {
                    foreach (var rect in transpot.Value.GetTransportImage(transpot.Key, _roads.Count, ScreenWidth, ScreenHeight,
                        TransportMagazine[time].OffsetsX.ElementAt(i)))
                    {
                        CanvasFiling.Add(rect);
                    }
                    i++;
                }
            }
            else
            {
                foreach (var transpot in _transport)
                {

                    foreach (var rect in transpot.Value.GetTransportImage(transpot.Key, _roads.Count, ScreenWidth, ScreenHeight, 0))
                    {
                        CanvasFiling.Add(rect);
                    }
                }
            }
            
        }
        public static void Start()
        {
            IsStart = true;

            for (int i = 0; i < 1000; i++)
            {
                var magazine = new Magazine();

                foreach (var transport in _transport)
                {
                    var transportResponse = transport.Value.GetTransportInfoInTime(ScreenWidth, i);

                    magazine.OffsetsX.Add(transportResponse.OffsetX);
                    magazine.IsWorking.Add(transportResponse.IsWorking);
                    magazine.RemainingFuel.Add(transportResponse.RemainingFuel);
                }

                TransportMagazine.Add(i, magazine);
            }
        }
    }
}
