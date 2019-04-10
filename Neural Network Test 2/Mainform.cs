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


namespace Neural_Network_Test_2
{
    public partial class Mainform : Form
    {
        
        neuralNetwork net;
        ActivationNetwork net2;

        PlotModel myModel;
        LineSeries[] lineSeries = new LineSeries[3];
        
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
            lineSeries[0].Points.Clear();
            lineSeries[1].Points.Clear();
            lineSeries[2].Points.Clear();
                         
            Button btn = (Button)sender;
            btn.Enabled = false;

            float learningRate = float.Parse(textBox_LearningRate.Text);
            string folderPath = textBox_Workingfolder.Text;
            string inputPicName = textBox_inputPicture.Text;
            string solnPicName = textBox_EditedPicture.Text;

            var progress = new Progress<neuralNetwork.TrainingUpdate>(s => updateTrainingGUI(s));
            //net = await Task.Factory.StartNew<neuralNetwork>(() => WorkerClass.Train2(folderPath, inputPicName, solnPicName, learningRate, progress));

            net2 = await Task.Factory.StartNew<ActivationNetwork>(() => WorkerClass.Train3(folderPath, inputPicName, solnPicName, learningRate, progress));
            btn.Enabled = true;


            /*
            var progress = new Progress<double>(s => progressBar_Training.Value = (int)(s * 100));
            var errorReport = new Progress<float[]>(s => updateErrors(s));
            
            Color[,] inputPic = WorkerClass.loadImage(folderPath, inputPicName);
            Color[,] solnPic = WorkerClass.loadImage(folderPath, solnPicName);

            int width = inputPic.GetLength(0);
            int height = inputPic.GetLength(1);

            net = await Task.Factory.StartNew<neuralNetwork>(() => WorkerClass.TrainNetworkFromImage(inputPic, solnPic, learningRate, progress));
            */

        }

        private void updateTrainingGUI(neuralNetwork.TrainingUpdate s)
        {
            try
            {
                float[] normalized = new float[s.currentError.Length];

                for (int i = 0; i < s.currentError.Length; i++)
                {
                    normalized[i] = Math.Abs(s.currentError[i]);

                    if (normalized[i] > 1) { normalized[i] = 1; }
                }

                double progress = (((s.currentEpoch - 1) * s.maxTrainingSteps) + s.currentTrainingStep) / (s.maxEpochs * s.maxTrainingSteps * 1.0);

                progressBar_Training.Value = (int)(progress*100.0);

                for (int i = 0; i < normalized.Length; i++)
                {
                    lineSeries[i].Points.Add(new DataPoint(progress * 100.0, normalized[i])); 
                }

                //lineSeries[0].Points.Add(new DataPoint(progress * 100.0, normalized[0]));
                //lineSeries[1].Points.Add(new DataPoint(progress * 100.0, normalized[1]));
                //lineSeries[2].Points.Add(new DataPoint(progress * 100.0, normalized[2]));

                myModel.InvalidatePlot(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private async void button_process_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                btn.Enabled = false;

                string folderPath = textBox_Workingfolder.Text;
                string inputPicName = textBox_processImage.Text;


                var progress = new Progress<double>(s => progressBar_processing.Value = (int)(s * 100));

                //var inputPic = WorkerClass.loadImage(folderPath, inputPicName);
                //int width = inputPic.GetLength(0);
                //int height = inputPic.GetLength(1);
                //var outputImage = await Task.Factory.StartNew<ImageClass>(() => WorkerClass.ApplyNetworkToImage(inputPic, net, progress));

                //var outputImage = await Task.Factory.StartNew<ImageClass>(() => WorkerClass.Process2(folderPath, inputPicName, net, progress));

                var outputImage = await Task.Factory.StartNew<ImageClass>(() => WorkerClass.Process3(folderPath, inputPicName, net2, progress));
                outputImage.saveImage(folderPath, "edited_image.jpg");
                btn.Enabled = true;

                


                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           

        }

       
    }
}
