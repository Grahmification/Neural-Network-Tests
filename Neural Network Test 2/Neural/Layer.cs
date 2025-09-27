namespace Neural_Network_Test_2.Neural
{
    /// <summary>
    /// Layer in a neural network
    /// </summary>
    public class Layer
    {
        private readonly int _numberOfInputs; // Number of neurons in previous layer
        private readonly int _numberOfOutputs; // Number of neurons in current layer
        private static readonly Random _random = new();

        public float[] Outputs { get; private set; }
        public float[] Inputs { get; private set; }
        public float[,] Weights { get; private set; }
        public float[,] weightDeltas { get; private set; } // Amount to change weight by at each learning step
        public float[] Gamma { get; private set; } // Value needed for back-propagation
        public float[] Error { get; private set; }

        public IActivationFunction ActFunction { get; private set; }

        public Layer(int numberOfInputs, int numberOfOutputs, IActivationFunction function)
        {
            _numberOfInputs = numberOfInputs;
            _numberOfOutputs = numberOfOutputs;
            ActFunction = function;

            Outputs = new float[numberOfOutputs];
            Inputs = new float[numberOfInputs];
            Weights = new float[numberOfOutputs, numberOfInputs];
            weightDeltas = new float[numberOfOutputs, numberOfInputs];
            Gamma = new float[numberOfOutputs];
            Error = new float[numberOfOutputs];

            InitializeWeights();
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

                Outputs[i] = ActFunction.Function(Outputs[i]); // Squelch each current layer node value using desired function
            }

            return Outputs;
        }

        /// <summary>
        /// Back propagation function for output layer
        /// </summary>
        /// <param name="expected">The expected result values</param>
        public void BackPropagateOutput(float[] expected)
        {
            for (int i = 0; i < _numberOfOutputs; i++)
                Error[i] = Outputs[i] - expected[i]; // First calculate error

            for (int i = 0; i < _numberOfOutputs; i++)
                Gamma[i] = Error[i] * ActFunction.Derivative(Outputs[i]);

            UpdateWeightDeltas(); // Update weight deltas

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

                Gamma[i] *= ActFunction.Derivative(Outputs[i]);
            }

            UpdateWeightDeltas(); // Update weight deltas
        }

        /// <summary>
        ///Uupdate weights for each learning step, occurs after all layers have been updated
        /// </summary>
        /// <param name="learningRate"></param>
        public void UpdateWeights(float learningRate)
        {
            for (int i = 0; i < _numberOfOutputs; i++)
            {
                for (int j = 0; j < _numberOfInputs; j++)
                {
                    Weights[i, j] -= weightDeltas[i, j] * learningRate;
                }
            }
        }

        /// <summary>
        /// Want weightings to start with random values
        /// </summary>
        private void InitializeWeights()
        {
            for (int i = 0; i < _numberOfOutputs; i++)
            {
                for (int j = 0; j < _numberOfInputs; j++)
                {
                    Weights[i, j] = (float)_random.NextDouble() - 0.5f;
                }
            }
        }

        private void UpdateWeightDeltas()
        {
            for (int i = 0; i < _numberOfOutputs; i++)
            {
                for (int j = 0; j < _numberOfInputs; j++)
                {
                    weightDeltas[i, j] = Gamma[i] * Inputs[j];
                }
            }
        }
    }
}
