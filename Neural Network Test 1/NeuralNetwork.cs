namespace Neural_Network_Test_1
{
    // Tutorial: https://www.youtube.com/watch?v=L_PByyJ9g-I
    // "Neural Network - Back-Propagation Tutorial In C#"

    /// <summary>
    /// A neural network
    /// </summary>
    public class NeuralNetwork
    {
        public int[] Layer { get; private set; }
        public Layer[] Layers { get; private set; }

        public NeuralNetwork(int[] layer, float learningRate)
        {
            Layer = new int[layer.Length];
            for (int i = 0; i < layer.Length; i++)
                Layer[i] = layer[i];

            Layers = new Layer[layer.Length - 1];

            for (int i = 0; i < Layers.Length; i++)
            {
                Layers[i] = new Layer(layer[i], layer[i + 1], learningRate);
            }
        }
        public float[] FeedForward(float[] inputs)
        {
            Layers[0].FeedForward(inputs);

            for (int i = 1; i < Layers.Length; i++)
            {
                Layers[i].FeedForward(Layers[i - 1].Outputs);
            }

            return Layers[^1].Outputs;
        }
        public float[] BackPropagate(float[] expectedValues)
        {
            for (int i = Layers.Length - 1; i >= 0; i--)
            {
                if (i == Layers.Length - 1)
                {
                    Layers[i].BackPropagateOutput(expectedValues);
                }
                else
                {
                    Layers[i].BackPropagateHidden(Layers[i + 1].Gamma, Layers[i + 1].Weights);
                }
            }

            for (int i = 0; i < Layers.Length; i++)
            {
                Layers[i].UpdateWeights(); // Now that weight deltas have been calculated must apply all of them
            }

            return Layers[^1].Error; // Returns error of final output layer
        }
    }
}
