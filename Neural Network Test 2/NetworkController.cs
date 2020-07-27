using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using AForge.Neuro;
using AForge.Neuro.Learning;
using Neural_Network_Test_2.Neural;
using System.Threading;

namespace Neural_Network_Test_2
{
    public class NetworkController
    {
        //-------------------- Integrated Network ---------------------------------
        
        public float[] CurrentTrainingError { get { return _trainer?.CurrentErrors; } }

        public NeuralNetwork Network { get; private set; }

        public NetworkImage InputPic { get; private set; }
        public NetworkIOData InputData { get; private set; }
        public NetworkIOData SolnData { get; private set; }

        private NetworkTrainer _trainer;

        public async Task Train(string folderPath, string inputPicName, string solnPicName, float learningRate, IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default)
        {
            try
            {
                await PrepareInputData(folderPath, inputPicName, solnPicName, progress, cancel);

                var network = new NeuralNetwork(new int[] { InputData.DataSize, 10, 10, 10, SolnData.DataSize }, new TanhFunction());
                _trainer = new NetworkTrainer(network);

                await _trainer.TrainAsync(InputData, SolnData, learningRate, 1, progress, cancel);

                Network = network;

                progress.Report(new NetworkProgressArgs(1, NetworkStatus.Complete));
            }
            catch (OperationCanceledException)
            {
                progress.Report(new NetworkProgressArgs(0, NetworkStatus.Cancelled));
                throw new OperationCanceledException();
            }
        }
        public async Task<NetworkImage> Process(string folderPath, string inputPicName, IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default)
        {
            try
            {
                await PrepareInputData(folderPath, inputPicName, "", progress, cancel);

                var outputData = await Network.CalculateAsync(InputData, progress, 1000, cancel);

                progress.Report(new NetworkProgressArgs(1, NetworkStatus.Complete));
                return new NetworkImage(outputData, InputPic.Width, InputPic.Height);
            }
            catch (OperationCanceledException)
            {
                progress.Report(new NetworkProgressArgs(0, NetworkStatus.Cancelled));
                throw new OperationCanceledException();
            }
        }

        //--------------- Uses Aforge ANN library ---------------

        public ActivationNetwork AforgeNetwork { get; private set; }

        public async Task TrainAforge(string folderPath, string inputPicName, string solnPicName, float learningRate, IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default)
        {
            try
            {
                await PrepareInputData(folderPath, inputPicName, solnPicName, progress, cancel);
                AforgeNetwork = await Task.Run(() => TrainAforgeDoWork(InputData, SolnData, learningRate, progress), cancel);
                progress.Report(new NetworkProgressArgs(1, NetworkStatus.Complete));
            }
            catch (OperationCanceledException)
            {
                progress.Report(new NetworkProgressArgs(0, NetworkStatus.Cancelled));
                throw new OperationCanceledException();
            }
        }
        public async Task<NetworkImage> ProcessAforge(string folderPath, string inputPicName, IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default)
        {
            try
            {
                await PrepareInputData(folderPath, inputPicName, "", progress, cancel);     
                var Data = await Task.Run(() => ProcessAforgeDoWork(InputData, AforgeNetwork, progress), cancel);

                progress.Report(new NetworkProgressArgs(1, NetworkStatus.Complete));
                return new NetworkImage(Data, InputPic.Width, InputPic.Height);
            }
            catch (OperationCanceledException)
            {
                progress.Report(new NetworkProgressArgs(0, NetworkStatus.Cancelled));
                throw new OperationCanceledException();
            }
        }

        private ActivationNetwork TrainAforgeDoWork(NetworkIOData inputData, NetworkIOData solnData, float learningRate, IProgress<NetworkProgressArgs> progress)
        {
            // create neural network
            ActivationNetwork network = new ActivationNetwork(new BipolarSigmoidFunction(1), inputData.DataSize, 10, solnData.DataSize);
            //ActivationNetwork network = new ActivationNetwork(new ReluFunction(), inputData.dataLength, 10, 10, 10, solnData.dataLength);

            //create trainer
            BackPropagationLearning teacher = new BackPropagationLearning(network);
            teacher.LearningRate = learningRate;


            //--------------------------------- Check for errors in input data -------------------------

            if (inputData.Count != solnData.Count)
                throw new Exception("Training Error: Input and solution need have the same amount of datapoints.");

            //------------------------------ Train Network ------------------------------------
            int Count = inputData.Count;


            int reportInterval = (int)Math.Round(Count / 1000.0);


            for (int i = 0; i < Count; i++)
            {
                double error = teacher.Run(inputData.data_dbl(i), solnData.data_dbl(i));
                var currentError = new float[] { (float)error };
             
                if (i % reportInterval == 0) //only send update every few training steps
                {
                    progress.Report(new NetworkProgressArgs(NetworkTrainer.CalculateProgress(0, i, 1, Count), NetworkStatus.Training)); //report progress for UI elements
                }
            }

            return network;
        }
        private List<float[]> ProcessAforgeDoWork(NetworkIOData inputData, ActivationNetwork net, IProgress<NetworkProgressArgs> progress)
        {
            int Count = inputData.Count;
            int dataLength = inputData.data(0).Length;
            var Data = new List<float[]>();

            int reportInterval = (int)Math.Round(Count / 100.0);

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


                if (i % reportInterval == 0) //only send update every few training steps
                    progress.Report(new NetworkProgressArgs((i + 1) / (double)Count, NetworkStatus.Processing)); //report progress for progress bar         
            }

            return Data;
        }




        //--------------- Helpers ---------------

        private async Task PrepareInputData(string folderPath, string inputPicName, string solnPicName = "", IProgress<NetworkProgressArgs> loadProgress = default, CancellationToken cancel = default)
        {
            InputPic = new NetworkImage(folderPath, inputPicName);
            InputData = await InputPic.GetNetworkData_NeighborsAsync(loadProgress, cancel);

            if(solnPicName != "")
                SolnData = await new NetworkImage(folderPath, solnPicName).GetNetworkDataAsync(loadProgress, cancel);
        }

    }
}
