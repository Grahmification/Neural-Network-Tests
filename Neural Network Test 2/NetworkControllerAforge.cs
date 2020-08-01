using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using AForge.Neuro;
using AForge.Neuro.Learning;
using Neural_Network_Test_2.Neural;


namespace Neural_Network_Test_2
{
    public class NetworkControllerAForge : NetworkController, INetworkController
    {
        public override float[] CurrentTrainingError { get; protected set; }

        public ActivationNetwork AforgeNetwork { get; private set; }

        protected async override Task TrainDoWork(IProgress<NetworkProgressArgs> progress, int reportInterval, CancellationToken cancel = default)
        {
            AforgeNetwork = await Task.Run(() => TrainAforgeDoWork(progress, reportInterval, cancel), cancel);
        }

        protected async override Task<List<float[]>> ProcessDoWork(IProgress<NetworkProgressArgs> progress, int reportInterval, CancellationToken cancel = default)
        {
            return await Task.Run(() => ProcessAforgeDoWork(InputData, AforgeNetwork, progress, reportInterval, cancel), cancel);
        }


        private ActivationNetwork TrainAforgeDoWork(IProgress<NetworkProgressArgs> progress, int reportInterval, CancellationToken cancel = default)
        {
            // create neural network
            ActivationNetwork network = new ActivationNetwork(new BipolarSigmoidFunction(1), InputData.DataSize, 10, SolnData.DataSize);
            //ActivationNetwork network = new ActivationNetwork(new ReluFunction(), inputData.dataLength, 10, 10, 10, solnData.dataLength);

            //create trainer
            BackPropagationLearning teacher = new BackPropagationLearning(network);
            teacher.LearningRate = LearningRate;

            //--------------------------------- Check for errors in input data -------------------------

            if (InputData.Count != SolnData.Count)
                throw new Exception("Training Error: Input and solution need have the same amount of datapoints.");

            //------------------------------ Train Network ------------------------------------
            int Count = InputData.Count;
            int interval = CalculateReportInterval(reportInterval);


            for (int i = 0; i < Count; i++)
            {
                cancel.ThrowIfCancellationRequested();
                double error = teacher.Run(InputData.data_dbl(i), SolnData.data_dbl(i));
                CurrentTrainingError = new float[] { (float)error };

                if (i % interval == 0) //only send update every few training steps
                {
                    progress.Report(new NetworkProgressArgs(NetworkTrainer.CalculateProgress(0, i, 1, Count), NetworkStatus.Training)); //report progress for UI elements
                }
            }

            return network;
        }
        private List<float[]> ProcessAforgeDoWork(NetworkIOData inputData, ActivationNetwork net, IProgress<NetworkProgressArgs> progress, int reportInterval, CancellationToken cancel = default)
        {
            int Count = inputData.Count;
            int dataLength = inputData.data(0).Length;
            var Data = new List<float[]>();

            int interval = CalculateReportInterval(reportInterval);

            for (int i = 0; i < Count; i++)
            {
                double[] moddedVal = new double[dataLength];
                var tmpinput = inputData.data_dbl(i);
                moddedVal = net.Compute(tmpinput);

                float[] tmp = new float[moddedVal.Length]; //needed to avoid object references being transfered

                for (int j = 0; j < moddedVal.Length; j++)
                {
                    tmp[j] = (float)moddedVal[j];
                }

                Data.Add(tmp);

                if (i % interval == 0) //only send update every few training steps
                    progress.Report(new NetworkProgressArgs((i + 1) / (double)Count, NetworkStatus.Processing)); //report progress for progress bar         
            }

            return Data;
        }



    }
}
