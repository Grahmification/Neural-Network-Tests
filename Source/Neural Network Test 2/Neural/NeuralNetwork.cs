namespace Neural_Network_Test_2.Neural
{
    /*
     * Tutorial: https://www.youtube.com/watch?v=L_PByyJ9g-I
     * "Neural Network - Back-Propagation Tutorial In C#"
     * 
     * Other useful links:
     * https://www.youtube.com/watch?v=0bYLr6Kr57w
     * http://kostiantyn-dvornik.blogspot.ca/2015/04/neural-network-image-processing-tutorial.html 
     */

    /// <summary>
    /// A neural network model
    /// </summary>
    public class NeuralNetwork
    {
        private readonly Layer[] layers;

        public int[] LayerNodes { get; private set; }
        public int InputSize => LayerNodes.First();
        public int OutputSize => LayerNodes.Last();

        public NeuralNetwork(int[] layerNodes, IActivationFunction function)
        {
            LayerNodes = layerNodes;
            layers = new Layer[layerNodes.Length - 1];
            
            for (int i = 0; i < layers.Length; i++)
            {
                layers[i] = new Layer(layerNodes[i], layerNodes[i + 1], function);
            }
        }
        public float[] FeedForward(float[] inputs)
        {
            layers[0].FeedForward(inputs);

            for (int i = 1; i < layers.Length; i++)
            {
                layers[i].FeedForward(layers[i - 1].Outputs);
            }

            return layers[^1].Outputs;
        }
        public float[] BackPropagate(float[] expectedValues, float learningRate)
        {
            for (int i = layers.Length - 1; i >= 0; i--)
            {
                if (i == layers.Length - 1)
                {
                    layers[i].BackPropagateOutput(expectedValues);
                }
                else
                {
                    layers[i].BackPropagateHidden(layers[i + 1].Gamma, layers[i + 1].Weights);
                }
            }

            for (int i = 0; i < layers.Length; i++)
            {
                layers[i].UpdateWeights(learningRate); // Now that weight deltas have been calculated must apply all of them
            }

            return layers[^1].Error; // Returns error of final output layer
        }

        public async Task<List<float[]>> CalculateAsync(NetworkIOData inputData, IProgress<NetworkProgressArgs> progress, double ProcessReportInterval = 100, CancellationToken cancel = default)
        {
            return await Task.Run(() => Calculate(inputData, progress, ProcessReportInterval, cancel), cancel);
        }
        public List<float[]> Calculate(NetworkIOData inputData, IProgress<NetworkProgressArgs> progress, double ProcessReportInterval = 100, CancellationToken cancel = default)
        {
            //--------------------------------- Check for errors in input data -------------------------
            if (inputData.DataSize != InputSize)
                throw new Exception("Processing Error: Input data size not compatable with network inputsize");

            int count = inputData.Count;
            int reportInterval = (int)Math.Round(count / (double)ProcessReportInterval);
            
            var output = new List<float[]>();

            for (int i = 0; i < count; i++)
            {
                cancel.ThrowIfCancellationRequested();
                
                var moddedVal = FeedForward(inputData.GetData(i));

                float[] tmp = new float[moddedVal.Length]; // Needed to avoid object references being transfered

                for (int j = 0; j < moddedVal.Length; j++)
                    tmp[j] = moddedVal[j];

                output.Add(tmp);

                if (i % reportInterval == 0) // Only send update every few training steps
                {
                    double prog = (i + 1) / (double)count;
                    progress.Report(new NetworkProgressArgs(prog, NetworkStatus.Processing));
                }
       
            }

            return output;
        }
    }
}
