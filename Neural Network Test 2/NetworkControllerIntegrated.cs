using System;
using System.Threading.Tasks;
using Neural_Network_Test_2.Neural;
using System.Threading;
using System.Collections.Generic;

namespace Neural_Network_Test_2
{
    public class NetworkControllerIntegrated : NetworkController, INetworkController
    {
        //-------------------- Integrated Network ---------------------------------
        public override float[] CurrentTrainingError { get { return _trainer?.CurrentErrors; } protected set { var tmp = value; } } //do nothing with the set


        private NetworkTrainer _trainer;
        private NeuralNetwork _network;

        protected async override Task TrainDoWork(IProgress<NetworkProgressArgs> progress, int reportInterval, CancellationToken cancel = default)
        {
            var network = new NeuralNetwork(new int[] { InputData.DataSize, 10, 10, 10, SolnData.DataSize }, new TanhFunction());
            _trainer = new NetworkTrainer(network);
            _trainer.TrainingReportInterval = reportInterval;
            
            await _trainer.TrainAsync(InputData, SolnData, LearningRate, 1, progress, cancel);

            _network = network;
        }

        protected async override Task<List<float[]>> ProcessDoWork(IProgress<NetworkProgressArgs> progress, int reportInterval, CancellationToken cancel = default)
        {
            return await _network.CalculateAsync(InputData, progress, reportInterval, cancel);
        }
    }
}
