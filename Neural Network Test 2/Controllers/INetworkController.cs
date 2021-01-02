using System;
using System.Threading.Tasks;
using Neural_Network_Test_2.Neural;

namespace Neural_Network_Test_2
{
    public interface INetworkController
    {
        float[] CurrentTrainingError { get; }

        NetworkIOData InputData { get; }
        NetworkIOData SolnData { get; }
        bool Training { get; }
        bool Processing { get; }
        float LearningRate { get; }

        Task Train(INetworkData inputData, INetworkData solutionData, float learningRate, IProgress<NetworkProgressArgs> progress);
        Task<NetworkIOData> Process(INetworkData inputData, IProgress<NetworkProgressArgs> progress);

        void CancelTraining();
        void CancelProcessing();
    }
}
