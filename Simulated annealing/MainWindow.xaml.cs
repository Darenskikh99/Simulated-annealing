using System.Windows;

namespace Simulated_annealing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RoadMap _myRoadMap;
        private Cell[,] _roadMap;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateNewTable_Click(object sender, RoutedEventArgs e)
        {
            WPRoadMap.Children.Clear();
            _myRoadMap = new RoadMap(int.Parse(NumberStation.Text));
            _roadMap = new Cell[int.Parse(NumberStation.Text) + 1, int.Parse(NumberStation.Text) + 1];

            for (var i = 0; i < _roadMap.GetLength(0); i++)
            {
                for (var j = 0; j < _roadMap.GetLength(1); j++)
                {
                    var cell = new Cell
                    {
                        Height = WPRoadMap.ActualHeight / (int.Parse(NumberStation.Text) + 1),
                        Width = WPRoadMap.ActualWidth / (int.Parse(NumberStation.Text) + 1),
                        FontSize = WPRoadMap.ActualWidth / int.Parse(NumberStation.Text) / 2
                    };
                    _roadMap[i, j] = cell;

                    if (i == 0 && j > 0)
                    {
                        _roadMap[i, j].CellText.Text = _myRoadMap.stationsMap[j - 1].NumberOfStation.ToString();
                        _roadMap[i, j].CellText.IsReadOnly = true;
                    }
                    if (j == 0 && i > 0)
                    {
                        _roadMap[i, j].CellText.Text = _myRoadMap.stationsMap[i - 1].NumberOfStation.ToString();
                        _roadMap[i, j].CellText.IsReadOnly = true;
                    }
                    if (i > 0 && j > 0 && i != j)
                    {
                        _roadMap[i, j].CellText.Text = _myRoadMap.FindDistance(i - 1, j - 1).ToString();
                    }
                }
            }
            for (var i = 0; i < _roadMap.GetLength(0); i++)
            {
                for (var j = 0; j < _roadMap.GetLength(1); j++)
                {
                    WPRoadMap.Children.Add(_roadMap[i, j]);
                }
            }

        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            SimulatedAnnealingAI simulatedAnnealingAI = new SimulatedAnnealingAI(_myRoadMap.roadMap, _myRoadMap.stationsMap,
                                                    double.Parse(InitialTemperature.Text), double.Parse(DegreeAlpha.Text));
            simulatedAnnealingAI.CreateFirstPath();
            simulatedAnnealingAI.SimulatedAnnealing(int.Parse(NumberIterartion.Text));

            Answer.Text = simulatedAnnealingAI.OurPath[0].NumberOfStation.ToString();
            for (var i = 1; i < simulatedAnnealingAI.OurPath.Count; i++)
            {
                Answer.Text += " - " + simulatedAnnealingAI.OurPath[i].NumberOfStation.ToString();
            }
            Answer.Text += "\nПройденный путь = " + simulatedAnnealingAI.DiatanceTreveled(simulatedAnnealingAI.OurPath).ToString();
        }
    }
}
