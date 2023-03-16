using System;
using System.Collections.Generic;

namespace Simulated_annealing
{
    class RoadMap
    {
        private readonly Random _random = new();

        public List<Path> roadMap;
        public List<Station> stationsMap;

        public RoadMap(int numberStations)
        {
            roadMap = new List<Path>();
            stationsMap = new List<Station>();

            for (var i = 0; i < numberStations; i++)
            {
                stationsMap.Add(new Station(i + 1));
                for (var j = 0; j < i; j++)
                {
                    var distance = _random.Next(1, 100);

                    roadMap.Add(new Path(stationsMap[i], stationsMap[j], distance));

                    stationsMap[i].AddNewNearStation(stationsMap[i], stationsMap[j], distance);
                    stationsMap[j].AddNewNearStation(stationsMap[j], stationsMap[i], distance);
                }
            }
        }

        public int FindDistance(int i, int j)
        {
            int distance = 0;
            foreach (var K in roadMap)
            {
                if (K.HaveRoad(stationsMap[i], stationsMap[j]))
                {
                    distance = K.GetDistance(stationsMap[i], stationsMap[j]);
                }
            }
            return distance;
        }
    }
}
