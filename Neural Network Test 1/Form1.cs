namespace Neural_Network_Test_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;

            int learningSteps = int.Parse(textBox_learningSteps.Text);
            float learningRate = float.Parse(textBox_learningRate.Text);

            var progress = new Progress<double>(s => progressBar1.Value = (int)(s*100));
            var errorProgress = new Progress<float[]>(s => UpdateErrorCharts(s));
            var net = await Task.Factory.StartNew(() => TrainXOR(progress, errorProgress , learningRate, learningSteps));

            btn.Enabled = true;
        }

        private void UpdateErrorCharts(float[] error)
        {
            for(int i = 0; i < error.Length; i++)
            {
                error[i] = Math.Abs(error[i]);

                if (error[i] > 1) { error[i] = 1; }
            }
            progressBar_Error1.Value = (int)(error[0] * 100);
            progressBar_Error2.Value = (int)(error[1] * 100);
            progressBar_Error3.Value = (int)(error[2] * 100);
            progressBar_Error4.Value = (int)(error[3] * 100);
            progressBar_Error5.Value = (int)(error[4] * 100);
            progressBar_Error6.Value = (int)(error[5] * 100);
            progressBar_Error7.Value = (int)(error[6] * 100);
            progressBar_Error8.Value = (int)(error[7] * 100);
        }

        public NeuralNetwork TrainXOR(IProgress<double> progress, IProgress<float[]> errorUpdate, float learningRate, int learningSteps)
        {
            NeuralNetwork net = new([3, 25, 25, 1], learningRate); // 3 inputs, 2 x 25 neuron hidden layers, 1 output

            for (int i = 0; i < learningSteps; i++) // Iterate over 5000 learning steps
            {
                float[] error = new float[8];

                net.FeedForward([0, 0, 0]);
                error[0] = net.BackPropagate([0])[0];

                net.FeedForward([0, 0, 1]);
                error[1] = net.BackPropagate([1])[0];

                net.FeedForward([0, 1, 0]);
                error[2] = net.BackPropagate([1])[0];

                net.FeedForward([0, 1, 1]);
                error[3] = net.BackPropagate([0])[0];

                net.FeedForward([1, 0, 0]);
                error[4] = net.BackPropagate([1])[0];

                net.FeedForward([1, 0, 1]);
                error[5] = net.BackPropagate([0])[0];

                net.FeedForward([1, 1, 0]);
                error[6] = net.BackPropagate([0])[0];

                net.FeedForward([1, 1, 1]);
                error[7] = net.BackPropagate([1])[0];

                if (i % 10 == 0 )
                {
                    errorUpdate.Report(error);
                    progress.Report(i * 1.0 / (learningSteps - 1));
                }
                
            }

            return net;
        }
    }


    /// <summary>
    /// A neural network
    /// </summary>
    public class NeuralNetwork
    {
        // tutorial: https://www.youtube.com/watch?v=L_PByyJ9g-I
        // "Neural Network - Back-Propagation Tutorial In C#"

        public int[] Layer { get; private set; }
        public Layer[] Layers { get; private set; }

        public NeuralNetwork(int[] layer, float learningRate)
        {
            Layer = new int[layer.Length];
            for (int i = 0; i < layer.Length; i++)
                Layer[i] = layer[i];

            Layers = new Layer[layer.Length-1];

            for (int i = 0; i < Layers.Length; i++)
            {
                Layers[i] = new Layer(layer[i], layer[i + 1], learningRate);
            }
        }
        public float[] FeedForward(float[] inputs)
        {
            Layers[0].FeedForward(inputs);

            for(int i = 1; i < Layers.Length; i++)
            {
                Layers[i].FeedForward(Layers[i - 1].Outputs);
            }

            return Layers[^1].Outputs;
        }
        public float[] BackPropagate(float[] expectedValues)
        {
            for (int i = Layers.Length -1 ; i >= 0; i--)
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
