using System.Collections.Generic;
using System.ComponentModel;

namespace Simulated_annealing
{
    class Station : INotifyPropertyChanged
    {
        private int _numberOfStation;
        private readonly List<Path> _nearStation = new List<Path>();
        
        public int NumberOfStation
        {
            get { return _numberOfStation; }
            set { _numberOfStation = value; }
        }

        public Station(int NumberOfStation)
        {
            _numberOfStation = NumberOfStation;
        }

        public void AddNewNearStation(Station ourStation, Station nearStation, int distance)
        {
            _nearStation.Add(new Path(ourStation, nearStation, distance));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
