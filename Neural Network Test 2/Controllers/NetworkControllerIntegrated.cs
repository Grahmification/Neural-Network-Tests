using Neural_Network_Test_2.Neural;

namespace Neural_Network_Test_2
{
    /// <summary>
    /// Controls logic of how to interact with the specified neural network
    /// </summary>
    public class NetworkControllerIntegrated : NetworkController, INetworkController
    {
        private NetworkTrainer? _trainer = null;
        private NeuralNetwork? _network = null;

        public override float[] CurrentTrainingError { get => _trainer?.CurrentErrors ?? [0]; protected set { var tmp = value; } } // Do nothing with the set

        protected async override Task TrainDoWork(IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default)
        {
            if (InputData != null && SolnData != null)
            {
                var network = new NeuralNetwork([InputData.DataSize, 10, 10, 10, SolnData.DataSize], new TanhFunction());
                //var network = new NeuralNetwork(new int[] { InputData.DataSize, 10, SolnData.DataSize }, new BipolarSigmoidFunction(1));

                _trainer = new NetworkTrainer(network)
                {
                    TrainingReportInterval = ReportInterval
                };

                await _trainer.TrainAsync(InputData, SolnData, LearningRate, 1, progress, cancel);

                _network = network;
            }
        }

        protected async override Task<List<float[]>> ProcessDoWork(IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default)
        {
            if (_network is null)
                throw new NullReferenceException("The network has not been trained yet.");

            if (InputData is null)
                throw new NullReferenceException("Input data has not been specified yet.");

            return await _network.CalculateAsync(InputData, progress, ReportInterval, cancel);
        }
    }
}
