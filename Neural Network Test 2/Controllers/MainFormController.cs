using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Neural_Network_Test_2.Neural;
using System.Threading;


namespace Neural_Network_Test_2
{
    public class MainFormController
    {
        public Mainform View { get; private set; }

        PlotController PlotController; 
        INetworkController NetController = new NetworkControllerIntegrated();

        CancellationTokenSource tokenSource = new CancellationTokenSource();

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

                tokenSource.Cancel(); //stop all processes running
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
                    tokenSource = new CancellationTokenSource();

                    PlotController.ResetData();
                    onTrainingStatusChange(true);

                    var progress = new Progress<NetworkProgressArgs>(s => onProgressUpdate(s));

                    await NetController.Train(View.WorkingFolder, View.TrainingInputPic, View.TrainingSolnPic, View.LearingRate, progress, tokenSource.Token);
                    View.AllowProcessing = true; //now we can allow processing
                }
                else //stop training
                {
                    tokenSource.Cancel();                   
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
                    tokenSource = new CancellationTokenSource();

                    onProcessingStatusChange(true);
                    var progress = new Progress<NetworkProgressArgs>(s => onProgressUpdate(s));

                    var outputImage = await NetController.Process(View.WorkingFolder, View.ProcessPic, progress, tokenSource.Token);
                    outputImage.SaveImage(View.WorkingFolder, "edited_image.jpg");

                }
                else //stop processing
                {
                    tokenSource.Cancel();
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
