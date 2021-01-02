using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Neural_Network_Test_2.Neural;

namespace Neural_Network_Test_2
{
    public class MainFormController
    {
        public Mainform View { get; private set; }

        PlotController PlotController; 
        INetworkController NetController = new NetworkControllerIntegrated();

        public MainFormController(Mainform view)
        {
            View = view;
            PlotController = new PlotController(view.ErrorPlot);

            View.TrainButtonClicked += onTrainRequest;
            View.ProcessButtonClicked += onProcessRequest;
            View.FormClosing += onFormClosing;

            View.AllowProcessing = false; //can't do this until we have trained the network
        }
        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (NetController.Processing || NetController.Training)
                {
                    var result = MessageBox.Show("A calculation is currently in progress, are you sure you want to exit?", "Operation in Progress", MessageBoxButtons.OKCancel);

                    if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true; //don't close
                        return; //skip cancelling processes
                    }
                }

                //stop all processes running
                NetController.CancelProcessing();
                NetController.CancelTraining();
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }      
        }
        
        private async void onTrainRequest(object sender, EventArgs e)
        {
            try
            {
                if(NetController?.Training == false) //start training
                {                  
                    PlotController.ResetData();
                    onTrainingStatusChange(true);

                    var progress = new Progress<NetworkProgressArgs>(s => onProgressUpdate(s));

                    var inputData = new NetworkImage(View.WorkingFolder, View.TrainingInputPic);
                    var solutionData = new NetworkImage(View.WorkingFolder, View.TrainingSolnPic);

                    await NetController.Train(inputData, solutionData, View.LearingRate, progress);
                    View.AllowProcessing = true; //now we can allow processing
                }
                else //stop training
                {
                    NetController.CancelTraining();                 
                }                       
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
            finally
            {
                onTrainingStatusChange(false);
            }
        }
        private async void onProcessRequest(object sender, EventArgs e)
        {
            try
            {
                if (NetController?.Processing == false) //start processing
                {
                    onProcessingStatusChange(true);
                    var progress = new Progress<NetworkProgressArgs>(s => onProgressUpdate(s));

                    var inputPic = new NetworkImage(View.WorkingFolder, View.ProcessPic);
                    
                    var outputData = await NetController.Process(inputPic, progress);
                    var outputImage = new NetworkImage(outputData.DataList, inputPic.Width, inputPic.Height);

                    outputImage.SaveImage(View.WorkingFolder, string.Format("{0} edited.{1}", inputPic.FileNameNoExtension, inputPic.FileExtension));

                }
                else //stop processing
                {
                    NetController.CancelProcessing();
                }          
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
            finally
            {
                onProcessingStatusChange(false);
            }
        }

        private void onProgressUpdate(NetworkProgressArgs s)
        {
            try
            {
                View.TrainingProgress = s.PercentProgress;
                View.StatusText = s.StatusString;

                if (s.Status == NetworkStatus.Training)
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
                        PlotController.AddDataPoint(s.Progress * 100.0, normalizedError[i], i);
                        PlotController.RefreshPlot();                     
                    }
                }
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
        }
        private void onTrainingStatusChange(bool started)
        {
            if (started) //training started
            {
                View.AllowProcessing = false;
                View.TrainingButtonText = "Cancel Training";
            }
            else //training stopped
            {
                View.TrainingButtonText = "Train NN";
            }
        }
        private void onProcessingStatusChange(bool started)
        {
            if (started) //training started
            {
                View.ProcessingButtonText = "Cancel Processing";
            }
            else //training stopped
            {
                View.ProcessingButtonText = "Process Picture";
            }

            View.AllowTraining = !started;
        }

    }
}
