namespace Simulated_annealing
{
    class Path
    {
        private readonly Station _end;
        private readonly Station _start;
        private readonly int _distance;
        
        public Path(Station end, Station start, int distance)
        {
            _end = end;
            _start = start;
            _distance = distance;
        }

        public int GetDistance(Station A, Station B)
        {
            if (_start.NumberOfStation == A.NumberOfStation && _end.NumberOfStation == B.NumberOfStation
                || _start.NumberOfStation == B.NumberOfStation && _end.NumberOfStation == A.NumberOfStation)
            {
                return _distance;
            }
            else
            {
                return 0;
            }
        }

        public bool HaveRoad(Station A, Station B)
        {
            if (_start.NumberOfStation == A.NumberOfStation && _end.NumberOfStation == B.NumberOfStation
                || _start.NumberOfStation == B.NumberOfStation && _end.NumberOfStation == A.NumberOfStation)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
