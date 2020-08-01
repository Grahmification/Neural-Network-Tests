using System;
using System.Threading.Tasks;
using System.Threading;
using Neural_Network_Test_2.Neural;

namespace Neural_Network_Test_2
{
    public interface INetworkController
    {
        float[] CurrentTrainingError { get; }

        NetworkImage InputPic { get; }
        NetworkIOData InputData { get; }
        NetworkIOData SolnData { get; }
        bool Training { get; }
        bool Processing { get; }
        float LearningRate { get; }

        Task Train(string folderPath, string inputPicName, string solnPicName, float learningRate, IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default);

        Task<NetworkImage> Process(string folderPath, string inputPicName, IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default);
    }
}
