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
    
        public NetworkIOData InputData { get; private set; }
        public NetworkIOData SolnData { get; private set; }
        public float LearningRate { get; private set; }
        public int ReportInterval { get; set; } = 10000;

        public bool Training { get; private set; } = false;
        public bool Processing { get; private set; } = false; 
        public bool NetWorkTrained { get; private set; } = false;

        private CancellationTokenSource trainingTokenSource = new CancellationTokenSource();
        private CancellationTokenSource processingTokenSource = new CancellationTokenSource();

        public virtual async Task Train(INetworkData inputData, INetworkData solutionData, float learningRate, IProgress<NetworkProgressArgs> progress)
        {
            try
            {
                trainingTokenSource = new CancellationTokenSource();
                LearningRate = learningRate;
                Training = true;
                NetWorkTrained = false;
                
                await PrepareInputData(inputData, solutionData, progress, trainingTokenSource.Token);              
                await TrainDoWork(progress, trainingTokenSource.Token); //call derived class to do training

                NetWorkTrained = true;
                progress.Report(new NetworkProgressArgs(1, NetworkStatus.Complete));
            }
            finally
            {
                //notify the GUI if we are finished due to cancellation
                if (trainingTokenSource.IsCancellationRequested)
                    progress.Report(new NetworkProgressArgs(0, NetworkStatus.Cancelled));

                Training = false;
            }
        }
        public virtual async Task<NetworkIOData> Process(INetworkData inputData, IProgress<NetworkProgressArgs> progress)
        {
            try
            {
                //the network must be trained before we can process using it. 
                if(!NetWorkTrained)
                    throw new Exception("The network has not been trained yet.");

                processingTokenSource = new CancellationTokenSource();
                Processing = true;
                
                await PrepareInputData(inputData, null, progress, processingTokenSource.Token);
                var outputData = await ProcessDoWork(progress, processingTokenSource.Token); //call derived class to do calculation
                
                progress.Report(new NetworkProgressArgs(1, NetworkStatus.Complete));
                return new NetworkIOData(outputData);
            }
            finally
            {
                //notify the GUI if we are finished due to cancellation
                if (processingTokenSource.IsCancellationRequested)
                    progress.Report(new NetworkProgressArgs(0, NetworkStatus.Cancelled));

                Processing = false;
            }
        }

        public void CancelTraining()
        {
            trainingTokenSource.Cancel();
        }
        public void CancelProcessing()
        {
            processingTokenSource.Cancel();
        }

        /// <summary>
        /// This function gets called by the derived class. It determines how the training is actually executed.
        /// </summary>
        /// <param name="progress">Progress reporter</param>
        /// <param name="cancel">Cancellation token for the process.</param>
        /// <returns></returns>
        protected abstract Task TrainDoWork(IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default);

        /// <summary>
        /// This function gets called by the derived class. It determines how the processing is actually executed.
        /// </summary>
        /// <param name="progress">Progress reporter</param>
        /// <param name="cancel">Cancellation token for the process.</param>
        /// <returns></returns>
        protected abstract Task<List<float[]>> ProcessDoWork(IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default);
        protected int CalculateReportInterval(int reportInterval)
        {
            return (int)Math.Round(InputData.Count / (double)reportInterval);
        }

        private async Task PrepareInputData(INetworkData inputData, INetworkData solutionData = null, IProgress<NetworkProgressArgs> progress = default, CancellationToken cancel = default)
        {
            progress.Report(new NetworkProgressArgs(0, NetworkStatus.LoadingData));

            InputData = await inputData.GetInputDataAsync(progress, cancel);

            //this method is also used for processing, when there is no solution specified
            if (!(solutionData is null))
                SolnData = await solutionData.GetSolutionDataAsync(progress, cancel);
        }
    }
}
