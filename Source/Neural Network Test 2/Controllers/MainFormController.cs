using Neural_Network_Test_2.Neural;

namespace Neural_Network_Test_2
{
    /// <summary>
    /// Main logic controlling class
    /// </summary>
    public class MainFormController
    {
        public Mainform View { get; private set; }

        public PlotController PlotController { get; private set; }
        public INetworkController NetController { get; private set; } = new NetworkControllerIntegrated();

        public MainFormController(Mainform view)
        {
            View = view;
            PlotController = new PlotController(view.ErrorPlot);

            View.TrainButtonClicked += OnTrainRequest;
            View.ProcessButtonClicked += OnProcessRequest;
            View.FormClosing += OnFormClosing;

            View.AllowProcessing = false; // Can't do this until we have trained the network

            LoadSettings();
        }
        private void OnFormClosing(object? sender, FormClosingEventArgs e)
        {
            try
            {
                if (NetController.Processing || NetController.Training)
                {
                    var result = MessageBox.Show("A calculation is currently in progress, are you sure you want to exit?", "Operation in Progress", MessageBoxButtons.OKCancel);

                    if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true; // Don't close
                        return; // Skip cancelling processes
                    }
                }

                // Stop all processes running
                NetController.CancelProcessing();
                NetController.CancelTraining();

                // Save user settings
                SaveSettings();
            }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
        }
        
        private async void OnTrainRequest(object? sender, EventArgs e)
        {
            try
            {
                if(NetController?.Training == false) // Start training
                {
                    PlotController.ResetData();
                    OnTrainingStatusChange(true);

                    var progress = new Progress<NetworkProgressArgs>(s => OnProgressUpdate(s));

                    var inputData = new NetworkImage(View.WorkingFolder, View.TrainingInputPic);
                    var solutionData = new NetworkImage(View.WorkingFolder, View.TrainingSolnPic);

                    await NetController.Train(inputData, solutionData, View.LearingRate, progress);
                    View.AllowProcessing = true; // Now we can allow processing
                }
                else // Stop training
                {
                    NetController?.CancelTraining();
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
            finally
            {
                OnTrainingStatusChange(false);
            }
        }
        private async void OnProcessRequest(object? sender, EventArgs e)
        {
            try
            {
                if (NetController?.Processing == false) // Start processing
                {
                    OnProcessingStatusChange(true);
                    var progress = new Progress<NetworkProgressArgs>(s => OnProgressUpdate(s));

                    var inputPic = new NetworkImage(View.WorkingFolder, View.ProcessPic);
                    
                    var outputData = await NetController.Process(inputPic, progress);
                    var outputImage = new NetworkImage(outputData.DataList, inputPic.Width, inputPic.Height);

                    outputImage.SaveImage(View.WorkingFolder, $"{inputPic.FileNameNoExtension} edited.{inputPic.FileExtension}");

                }
                else // Stop processing
                {
                    NetController?.CancelProcessing();
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                View.DisplayError(ex);
            }
            finally
            {
                OnProcessingStatusChange(false);
            }
        }

        private void OnProgressUpdate(NetworkProgressArgs s)
        {
            try
            {
                View.TrainingProgress = s.PercentProgress;
                View.StatusText = s.StatusString;

                if (s.Status == NetworkStatus.Training)
                {
                    var error = NetController.CurrentTrainingError;
                    List<float> normalizedError = [];

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
        private void OnTrainingStatusChange(bool started)
        {
            if (started) // Training started
            {
                View.AllowProcessing = false;
                View.TrainingButtonText = "Cancel Training";
            }
            else // Training stopped
            {
                View.TrainingButtonText = "Train NN";
            }
        }
        private void OnProcessingStatusChange(bool started)
        {
            if (started) // Training started
            {
                View.ProcessingButtonText = "Cancel Processing";
            }
            else // Training stopped
            {
                View.ProcessingButtonText = "Process Picture";
            }

            View.AllowTraining = !started;
        }

        /// <summary>
        /// Loads saved user settings into the GUI.
        /// </summary>
        private void LoadSettings()
        {
            View.WorkingFolder = Properties.Settings.Default.WorkingFolder;
            View.TrainingInputPic = Properties.Settings.Default.TrainingInputPic;
            View.TrainingSolnPic = Properties.Settings.Default.TrainingSolutionPic;
            View.ProcessPic = Properties.Settings.Default.ProcessingInputPic;
        }

        /// <summary>
        /// Saves user settings from the GUI
        /// </summary>
        private void SaveSettings()
        {
            Properties.Settings.Default.WorkingFolder = View.WorkingFolder;
            Properties.Settings.Default.TrainingInputPic = View.TrainingInputPic;
            Properties.Settings.Default.TrainingSolutionPic = View.TrainingSolnPic;
            Properties.Settings.Default.ProcessingInputPic = View.ProcessPic;

            Properties.Settings.Default.Save();
        }
    }
}
