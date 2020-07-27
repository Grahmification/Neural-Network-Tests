using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using AForge.Neuro;
using AForge.Neuro.Learning;
using Neural_Network_Test_2.Neural;
using System.Threading;

namespace Neural_Network_Test_2
{
    public partial class Mainform : Form
    {
        NetworkController NetController = new NetworkController();

        PlotModel myModel;
        LineSeries[] lineSeries = new LineSeries[3];

        CancellationTokenSource tokenSource = new CancellationTokenSource();
        
        public Mainform()
        {
            InitializeComponent();
        }

        private void Mainform_Load(object sender, EventArgs e)
        {
            myModel = new PlotModel { Title = "Errors"};
            lineSeries[0] = new LineSeries { MarkerType = MarkerType.None, Color = OxyColor.FromRgb(255, 0, 0) };
            lineSeries[1] = new LineSeries { MarkerType = MarkerType.None, Color = OxyColor.FromRgb(0, 255, 0) };
            lineSeries[2] = new LineSeries { MarkerType = MarkerType.None, Color = OxyColor.FromRgb(0, 0, 255) };
            myModel.Series.Add(lineSeries[0]);
            myModel.Series.Add(lineSeries[1]);
            myModel.Series.Add(lineSeries[2]);
            this.plotView1.Model = myModel;

           
        }

        private async void button_Train_Click(object sender, EventArgs e)
        {
            Button btn = new Button();

            try
            {
                tokenSource = new CancellationTokenSource();
                var cancelToken = tokenSource.Token;
            
                lineSeries[0].Points.Clear();
                lineSeries[1].Points.Clear();
                lineSeries[2].Points.Clear();

                btn = (Button)sender;
                btn.Enabled = false;

                float learningRate = float.Parse(textBox_LearningRate.Text);
                string folderPath = textBox_Workingfolder.Text;
                string inputPicName = textBox_inputPicture.Text;
                string solnPicName = textBox_EditedPicture.Text;

                var progress = new Progress<NetworkProgressArgs>(s => updateTrainingGUI(s));
             
                await NetController.Train(folderPath, inputPicName, solnPicName, learningRate, progress, cancelToken);           
            }
            catch (OperationCanceledException) {}
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btn.Enabled = true;
            }
        }
        private async void button_process_Click(object sender, EventArgs e)
        {
            Button btn = new Button();

            try
            {
                tokenSource = new CancellationTokenSource();
                var cancelToken = tokenSource.Token;

                btn = (Button)sender;
                btn.Enabled = false;

                string folderPath = textBox_Workingfolder.Text;
                string inputPicName = textBox_processImage.Text;


                var progress = new Progress<NetworkProgressArgs>(s => progressBar_processing.Value = s.PercentProgress);

                var outputImage = await NetController.Process(folderPath, inputPicName, progress, cancelToken);
                outputImage.SaveImage(folderPath, "edited_image.jpg");
                btn.Enabled = true;

            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btn.Enabled = true;
            }
        }
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
        }

        private void updateTrainingGUI(NetworkProgressArgs s)
        {
            try
            {
                progressBar_Training.Value = s.PercentProgress;
                label_status.Text = s.StatusString;


                if(s.Status == NetworkStatus.Training)
                {
                    var error = NetController.CurrentTrainingError;
                    List<float> normalizedError = new List<float>();

                    for (int i = 0; i < error.Length; i++)
                    {
                        var tmp = Math.Abs(error[i]);
                        if (tmp > 1) { tmp = 1; }

                        normalizedError.Add(tmp);
                    }

                    for (int i = 0; i < normalizedError.Count; i++)
                    {
                        lineSeries[i].Points.Add(new DataPoint(s.Progress * 100.0, normalizedError[i]));

                        myModel.InvalidatePlot(true);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    
    }
}
