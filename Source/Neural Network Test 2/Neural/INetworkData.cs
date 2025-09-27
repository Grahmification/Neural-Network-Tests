namespace Neural_Network_Test_2.Neural
{
    /// <summary>
    /// Generic definition for neural network training data
    /// </summary>
    public interface INetworkData
    {
        Task<NetworkIOData> GetSolutionDataAsync(IProgress<NetworkProgressArgs>? progress, CancellationToken cancel = default);
        Task<NetworkIOData> GetInputDataAsync(IProgress<NetworkProgressArgs>? progress, CancellationToken cancel = default);
        NetworkIOData GetSolutionData(IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default);
        NetworkIOData GetInputData(IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default);
    }
}
