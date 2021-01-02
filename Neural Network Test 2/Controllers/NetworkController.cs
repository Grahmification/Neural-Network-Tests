using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Neural_Network_Test_2.Neural;

namespace Neural_Network_Test_2
{
    public abstract class NetworkController : INetworkController
    {      
        public abstract float[] CurrentTrainingError { get; protected set; }
    
        public NetworkImage InputPic { get; private set; }
        public NetworkIOData InputData { get; private set; }
        public NetworkIOData SolnData { get; private set; }
        public float LearningRate { get; private set; }

        public bool Training { get; private set; } = false;
        public bool Processing { get; private set; } = false;
        public int ReportInterval { get; set; } = 10000;


        public virtual async Task Train(string folderPath, string inputPicName, string solnPicName, float learningRate, IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default)
        {
            try
            {
                LearningRate = learningRate;
                Training = true;
                
                await PrepareInputData(folderPath, inputPicName, solnPicName, progress, cancel);              
                await TrainDoWork(progress, ReportInterval, cancel); //call derived class to do training

                progress.Report(new NetworkProgressArgs(1, NetworkStatus.Complete));
            }
            finally
            {
                //notify the GUI if we are finished due to cancellation
                if (cancel.IsCancellationRequested)
                    progress.Report(new NetworkProgressArgs(0, NetworkStatus.Cancelled));

                Training = false;
            }
        }
        public virtual async Task<NetworkImage> Process(string folderPath, string inputPicName, IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default)
        {
            try
            {
                Processing = true;
                
                await PrepareInputData(folderPath, inputPicName, "", progress, cancel);
                var outputData = await ProcessDoWork(progress, ReportInterval, cancel); //call derived class to do calculation

                progress.Report(new NetworkProgressArgs(1, NetworkStatus.Complete));
                return new NetworkImage(outputData, InputPic.Width, InputPic.Height);
            }
            finally
            {
                //notify the GUI if we are finished due to cancellation
                if (cancel.IsCancellationRequested)
                    progress.Report(new NetworkProgressArgs(0, NetworkStatus.Cancelled));

                Processing = false;
            }
        }

        protected abstract Task TrainDoWork(IProgress<NetworkProgressArgs> progress, int reportInterval, CancellationToken cancel = default);
        protected abstract Task<List<float[]>> ProcessDoWork(IProgress<NetworkProgressArgs> progress, int reportInterval, CancellationToken cancel = default);
        protected int CalculateReportInterval(int reportInterval)
        {
            return (int)Math.Round(InputData.Count / (double)reportInterval);
        }

        private async Task PrepareInputData(string folderPath, string inputPicName, string solnPicName = "", IProgress<NetworkProgressArgs> progress = default, CancellationToken cancel = default)
        {
            progress.Report(new NetworkProgressArgs(0, NetworkStatus.LoadingData));

            InputPic = new NetworkImage(folderPath, inputPicName);
            InputData = await InputPic.GetNetworkData_NeighborsAsync(progress, cancel);

            if (solnPicName != "")
                SolnData = await new NetworkImage(folderPath, solnPicName).GetNetworkDataAsync(progress, cancel);
        }
    }
}
