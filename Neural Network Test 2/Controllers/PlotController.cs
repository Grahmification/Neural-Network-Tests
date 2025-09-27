using OxyPlot.WindowsForms;
using OxyPlot.Series;
using OxyPlot;

namespace Neural_Network_Test_2
{
    public class PlotController
    {
        public PlotView View { get; private set; }

        private LineSeries[] plotLines = new LineSeries[3];

        public PlotController(PlotView view)
        {
            View = view;
            InitializePlot();
        }
        public void InitializePlot()
        {
            var myModel = new PlotModel { Title = "Training Errors" };
            plotLines[0] = new LineSeries { MarkerType = MarkerType.None, Color = OxyColor.FromRgb(255, 0, 0) };
            plotLines[1] = new LineSeries { MarkerType = MarkerType.None, Color = OxyColor.FromRgb(0, 255, 0) };
            plotLines[2] = new LineSeries { MarkerType = MarkerType.None, Color = OxyColor.FromRgb(0, 0, 255) };
            myModel.Series.Add(plotLines[0]);
            myModel.Series.Add(plotLines[1]);
            myModel.Series.Add(plotLines[2]);
            View.Model = myModel;
        }
        public void RefreshPlot()
        {
            View.Model.InvalidatePlot(true);
        }
        public void ResetData()
        {
            foreach(LineSeries LS in plotLines)
            {
                LS.Points.Clear();
            }
        }
        public void AddDataPoint(double x, double y, int lineIndex)
        {
            plotLines[lineIndex].Points.Add(new DataPoint(x, y));
        }
    }
}
