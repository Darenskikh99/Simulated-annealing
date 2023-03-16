using System;
using System.Collections.Generic;

namespace Simulated_annealing
{
    class SimulatedAnnealingAI
    {
        private readonly List<Path> _allPaths;
        private readonly List<Station> _allStations;
        private readonly List<Station> _ourPath;
        private readonly Random _random = new Random();
        private readonly double _epcilon;

        private double _coefficientBoltzmann;
        private double _temperature;

        public SimulatedAnnealingAI(List<Path> allPaths, List<Station> allStations, double temperature, double alphaDegree)
        {
            _allPaths = allPaths;
            _allStations = allStations;
            _ourPath = new List<Station>();
            _temperature = temperature;
            _epcilon = alphaDegree;
        }

        public List<Station> OurPath
        {
            get { return _ourPath; }
        }

        /// <summary>
        /// Метод имитации отжига.
        /// </summary>
        /// <param name="numberIteration"> Количество итераций. </param>
        public void SimulatedAnnealing(int numberIteration)
        {
            List<Station> newCondition = new();
            foreach(var station in _ourPath)
            {
                newCondition.Add(station);
            }
            for(var i = 0; i < numberIteration; i++)
            {
                newCondition.RemoveAt(newCondition.Count - 1);

                var replacement1 = _random.Next(0, _allStations.Count - 1); // Создаем новое случайное решение
                var replacement2 = _random.Next(0, _allStations.Count - 1); //

                while( replacement1 == replacement2 )
                {
                    replacement1 = _random.Next(0, _allStations.Count - 1);
                    replacement2 = _random.Next(0, _allStations.Count - 1);
                }

                var replacementStation = newCondition[replacement1];        //
                newCondition[replacement1] = newCondition[replacement2];    //
                newCondition[replacement2] = replacementStation;            //
                newCondition.Add(newCondition[0]);

                if(DiatanceTreveled(_ourPath) > DiatanceTreveled(newCondition)) // Если новое решение выгоднее последнего, то записываем его
                {
                    _ourPath.Clear();
                    foreach (var station in newCondition)
                    {
                        _ourPath.Add(station);
                    }
                }
                else
                {
                    _coefficientBoltzmann = 100 * Math.Pow(-Math.E, (DiatanceTreveled(_ourPath) - DiatanceTreveled(newCondition)) / _temperature);    // Иначе считаем коэфициент Больцмана
                    if(_random.Next(100) < _coefficientBoltzmann)   //Если Коэфициент Больцмана больше случайного значения, записываем новое решение
                    {
                        _ourPath.Clear();
                        foreach (var station in newCondition)
                        {
                            _ourPath.Add(station);
                        }
                    }
                }
                TemperatureChange();
            }
        }

        /// <summary>
        /// Изменение температуры.
        /// </summary>
        public void TemperatureChange()
        {
            _temperature *= (1 - _epcilon);
        }

        /// <summary>
        /// Создание первого случайного решения.
        /// </summary>
        public void CreateFirstPath()
        {
            _ourPath.Add(_allStations[_random.Next(0, _allStations.Count)]);    // Добавление первой станции, путем выбора рандомной станции из всего списка
            while(_ourPath.Count < _allStations.Count)
            {
                int newStation = _random.Next(0, _allStations.Count);
                if (!CheckHave(_ourPath, _allStations[newStation]))  // Если эту станцию (случайно выбранную) мы еще не проходили
                {
                    _ourPath.Add(_allStations[newStation]);
                }
            }
            _ourPath.Add(_ourPath[0]);  // Замыкаем цикл
        }

        /// <summary>
        /// Функция высчитывающая длину пройденного пути.
        /// </summary>
        /// <param name="stations"> Список пройденных станций. </param>
        /// <returns> Пройденная дистанция. </returns>
        public int DiatanceTreveled(List<Station> stations)
        {
            int distance = 0;
            for(var i = 0; i < stations.Count - 1; i++)
            {
                foreach(var path in _allPaths) // Проверяем каждый существующий путь
                {
                    if(path.HaveRoad(stations[i], stations[i + 1])) // Если между станциями есть дорога
                    {
                        distance += path.GetDistance(stations[i], stations[i + 1]); // Получаем растояние между ними
                        break;                                                      // Выходим из цикла поиска путей
                    } 
                }
            }
            return distance;
        }
        /// <summary>
        /// Проверка наличия станции в списке станций
        /// </summary>
        /// <param name="stations"> Список станция </param>
        /// <param name="station"> Станция, которую хотим проверить </param>
        /// <returns> Да / Нет </returns>
        public static bool CheckHave(List<Station> stations, Station station)
        {
            var answer = false;
            foreach (var stat in stations)
            {
                if (stat.NumberOfStation == station.NumberOfStation)
                {
                    answer = true; break;
                }
            }
            return answer;
        }
    }
}
