namespace Neural_Network_Test_2.Neural
{
    /// <summary>
    /// For training a neural network
    /// </summary>
    class NetworkTrainer(NeuralNetwork network)
    {
        public float[] CurrentErrors { get; private set; } = [];
        public double CurrentAvgError => CalcAverageError(CurrentErrors);
        public int TrainingReportInterval { get; set; } = 1000;
        public NeuralNetwork Network { get; private set; } = network;

        public async Task TrainAsync(NetworkIOData inputData, NetworkIOData dataSolution, float learningRate, int epochs, IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default)
        {
            await Task.Run(() => Train(inputData, dataSolution, learningRate, epochs, progress, cancel), cancel);
        }
        
        public void Train(NetworkIOData inputData, NetworkIOData dataSolution, float learningRate, int epochs, IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default)
        {
            //--------------------------------- Check for errors in input data -------------------------

            if (inputData.Count != dataSolution.Count)
                throw new Exception("Training Error: Input and solution need have the same amount of datapoints.");

            if (inputData.DataSize != Network.InputSize || dataSolution.DataSize != Network.OutputSize)
                throw new Exception("Training Error: Input or solution data size not compatable with network input or output size");

            //------------------------------ Train Network ------------------------------------
            int Count = inputData.Count;
            int reportInterval = (int)Math.Round(Count / (double)TrainingReportInterval);

            for (int e = 0; e < epochs; e++)
            {
                for (int i = 0; i < Count; i++)
                {
                    cancel.ThrowIfCancellationRequested();

                    Network.FeedForward(inputData.GetData(i));
                    CurrentErrors = Network.BackPropagate(dataSolution.GetData(i), learningRate);

                    if (i % reportInterval == 0) // Only send update every few training steps
                    {
                        progress.Report(new NetworkProgressArgs(CalculateProgress(e, i, epochs, Count), NetworkStatus.Training)); // Report progress for UI elements
                    }
                }
            }
        }
       
        public static double CalculateProgress(int currentEpoch, int currentTrainingStep, int maxEpochs, int maxTrainingSteps)
        {
            return ((currentEpoch * maxTrainingSteps) + currentTrainingStep + 1.0) / (maxEpochs * maxTrainingSteps);
        }

        private static double CalcAverageError(float[] input)
        {
            double output = 0;
            for (int j = 0; j < input.Length; j++) // Compute the average error
                output += input[j];
            output /= input.Length;

            return output;
        }
    }
}
