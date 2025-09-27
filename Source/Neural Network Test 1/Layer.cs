namespace Neural_Network_Test_1
{
    /// <summary>
    /// Layer in a neural network
    /// </summary>
    public class Layer
    {
        private readonly int _numberOfInputs; // Number of neurons in previous layer
        private readonly int _numberOfOutputs; // Number of neurons in current layer
        private readonly static Random _random = new();

        public float[] Outputs { get; private set; }
        public float[] Inputs { get; private set; }
        public float[,] Weights { get; private set; }
        public float[,] WeightDeltas { get; private set; } // Amount to change weight by at each learning step
        public float[] Gamma { get; private set; } // Value needed for back-propagation
        public float[] Error { get; private set; }
        public float LearningRate { get; private set; }

        public Layer(int numberOfInputs, int numberOfOutputs, float learningRate)
        {
            _numberOfInputs = numberOfInputs;
            _numberOfOutputs = numberOfOutputs;
            LearningRate = learningRate;

            Outputs = new float[numberOfOutputs];
            Inputs = new float[numberOfInputs];
            Weights = new float[numberOfOutputs, numberOfInputs];
            WeightDeltas = new float[numberOfOutputs, numberOfInputs];
            Gamma = new float[numberOfOutputs];
            Error = new float[numberOfOutputs];

            InitializeWeights();
        }

        public void InitializeWeights()
        {
            for (int i = 0; i < _numberOfOutputs; i++)
            {
                for (int j = 0; j < _numberOfInputs; j++)
                {
                    Weights[i, j] = (float)_random.NextDouble() - 0.5f;
                }
            }
        }

        /// <summary>
        /// Update weights for each learning step
        /// </summary>
        public void UpdateWeights()
        {
            for (int i = 0; i < _numberOfOutputs; i++)
            {
                for (int j = 0; j < _numberOfInputs; j++)
                {
                    Weights[i, j] -= WeightDeltas[i, j] * LearningRate;
                }
            }
        }
        public float[] FeedForward(float[] input)
        {
            Inputs = input;

            for (int i = 0; i < _numberOfOutputs; i++) // Iterate over each neuron in current layer
            {
                Outputs[i] = 0;

                for (int j = 0; j < _numberOfInputs; j++) // Iterate over each neuron in previous layer
                {
                    Outputs[i] += Inputs[j] * Weights[i, j];
                }

                Outputs[i] = (float)Math.Tanh(Outputs[i]); // Squelch each current layer node value using Tanh
            }

            return Outputs;
        }

        /// <summary>
        /// Back propagation function for output layer
        /// </summary>
        /// <param name="expected">Expected output value</param>
        public void BackPropagateOutput(float[] expected)
        {
            for (int i = 0; i < _numberOfOutputs; i++)
                Error[i] = Outputs[i] - expected[i]; // First calculate error

            for (int i = 0; i < _numberOfOutputs; i++)
                Gamma[i] = Error[i] * TanhDer(Outputs[i]);

            //---------------update  weight deltas ----------------------------------

            for (int i = 0; i < _numberOfOutputs; i++)
            {
                for (int j = 0; j < _numberOfInputs; j++)
                {
                    WeightDeltas[i, j] = Gamma[i] * Inputs[j];
                }
            }
        }

        /// <summary>
        /// Back propagation function for hidden layers
        /// </summary>
        /// <param name="gammaForward"></param>
        /// <param name="weightsForward"></param>
        public void BackPropagateHidden(float[] gammaForward, float[,] weightsForward)
        {
            for (int i = 0; i < _numberOfOutputs; i++)
            {
                Gamma[i] = 0;

                for (int j = 0; j < gammaForward.Length; j++)
                {
                    Gamma[i] += gammaForward[j] * weightsForward[j, i];
                }

                Gamma[i] *= TanhDer(Outputs[i]);
            }

            //---------------update  weight deltas ----------------------------------

            for (int i = 0; i < _numberOfOutputs; i++)
            {
                for (int j = 0; j < _numberOfInputs; j++)
                {
                    WeightDeltas[i, j] = Gamma[i] * Inputs[j];
                }
            }

        }

        /// <summary>
        /// Calculate the derivative of tanh(x)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float TanhDer(float value)
        {
            return 1 - (value * value);
        }
    }
}
