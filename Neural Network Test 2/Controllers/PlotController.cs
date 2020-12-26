/*
This code uses the Oxplot library under the MIT License.

MIT License

Copyright (c) 2014 OxyPlot contributors

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

 */

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
