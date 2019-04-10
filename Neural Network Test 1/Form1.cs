using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            int learningSteps = Int32.Parse(textBox_learningSteps.Text);
            float learningRate = float.Parse(textBox_learningRate.Text);

            var progress = new Progress<double>(s => progressBar1.Value = (int)(s*100));
            var errorProgress = new Progress<float[]>(s => updateErrorCharts(s));
            var net = await Task.Factory.StartNew<neuralNetwork>(() => trainXOR(progress, errorProgress , learningRate, learningSteps));

            btn.Enabled = true;

        }

        private void updateErrorCharts(float[] error)
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


        public neuralNetwork trainXOR(IProgress<double> progress, IProgress<float[]> errorUpdate, float learningRate, int learningSteps)
        {
            neuralNetwork net = new neuralNetwork(new int[] { 3, 25, 25, 1 }, learningRate); // 3 inputs, 2 x 25 neuron hidden layers, 1 output

            for (int i = 0; i < learningSteps; i++) //iterate over 5000 learning steps
            {
                float[] error = new float[8];

                net.feedForward(new float[] { 0, 0, 0 });
                error[0] = net.backPropagate(new float[] { 0 })[0];

                net.feedForward(new float[] { 0, 0, 1 });
                error[1] = net.backPropagate(new float[] { 1 })[0];

                net.feedForward(new float[] { 0, 1, 0 });
                error[2] = net.backPropagate(new float[] { 1 })[0];

                net.feedForward(new float[] { 0, 1, 1 });
                error[3] = net.backPropagate(new float[] { 0 })[0];

                net.feedForward(new float[] { 1, 0, 0 });
                error[4] = net.backPropagate(new float[] { 1 })[0];

                net.feedForward(new float[] { 1, 0, 1 });
                error[5] = net.backPropagate(new float[] { 0 })[0];

                net.feedForward(new float[] { 1, 1, 0 });
                error[6] = net.backPropagate(new float[] { 0 })[0];

                net.feedForward(new float[] { 1, 1, 1 });
                error[7] = net.backPropagate(new float[] { 1 })[0];

                if (i % 10 == 0 )
                {
                    errorUpdate.Report(error);
                    progress.Report(i * 1.0 / (learningSteps - 1));
                }
                
            }

            return net;
        }

        
    }



    public class neuralNetwork
    {
        // tutorial: https://www.youtube.com/watch?v=L_PByyJ9g-I
        // "Neural Network - Back-Propagation Tutorial In C#"

        int[] layer;
        Layer[] layers;

        public neuralNetwork(int[] layer, float learningRate)
        {
            this.layer = new int[layer.Length];
            for (int i = 0; i < layer.Length; i++)
                this.layer[i] = layer[i];

            layers = new Layer[layer.Length-1];

            for (int i = 0; i < layers.Length; i++)
            {
                layers[i] = new Layer(layer[i], layer[i + 1], learningRate);
            }

        }
        public float[] feedForward(float[] inputs)
        {
            layers[0].feedForward(inputs);

            for(int i = 1; i < layers.Length; i++)
            {
                layers[i].feedForward(layers[i - 1].outputs);
            }

            return layers[layers.Length - 1].outputs;
        }
        public float[] backPropagate(float[] expectedValues)
        {

            for (int i = layers.Length -1 ; i >= 0; i--)
            {
                if (i == layers.Length - 1)
                {
                    layers[i].backPropagateOutput(expectedValues);
                }
                else
                {
                    layers[i].backPropagateHidden(layers[i + 1].gamma, layers[i + 1].weights);
                }
            }

            for (int i = 0; i < layers.Length; i++)
            {
                layers[i].updateWeights(); //now that weight deltas have been calculated must apply all of them
            }

            return layers[layers.Length - 1].error; //returns error of final output layer
        }

        public class Layer
        {
            int numberOfInputs; //number of neurons in previous layer
            int numberOfOutputs; //number of neurons in current layer

            public float[] outputs;
            public float[] inputs;
            public float[,] weights;
            public float[,] weightDeltas; //amount to change weight by at each learning step
            public float[] gamma; //value needed for back-propagation
            public float[] error;
            public float learningRate;
            public static Random random = new Random();

            public Layer(int numberOfInputs, int numberOfOutputs, float learningRate)
            {
                this.numberOfInputs = numberOfInputs;
                this.numberOfOutputs = numberOfOutputs;
                this.learningRate = learningRate;

                outputs = new float[numberOfOutputs];
                inputs = new float[numberOfInputs];
                weights = new float[numberOfOutputs, numberOfInputs];
                weightDeltas = new float[numberOfOutputs, numberOfInputs];
                gamma = new float[numberOfOutputs];
                error = new float[numberOfOutputs];

                initializeWeights();

            }

            public void initializeWeights()
            {
                for(int i =0; i < numberOfOutputs; i++)
                {
                    for(int j = 0; j < numberOfInputs; j++)
                    {
                        weights[i, j] = (float)random.NextDouble() - 0.5f;
                    }
                }
            }
            public void updateWeights()
            {
                for (int i = 0; i < numberOfOutputs; i++)
                {
                    for (int j = 0; j < numberOfInputs; j++)
                    {
                        weights[i, j] -= weightDeltas[i, j] * learningRate;
                    }
                }
            } //update weights for each learning step

            public float[] feedForward(float[] input)
            {
                this.inputs = input;

                for (int i=0; i < numberOfOutputs; i++) //iterate over each neuron in current layer
                {
                    outputs[i] = 0;

                    for (int j = 0; j < numberOfInputs; j++) //iterate over each neuron in previous layer
                    {
                        outputs[i] += inputs[j] * weights[i, j];
                    }

                    outputs[i] = (float)Math.Tanh(outputs[i]); //squelch each current layer node value using Tanh
                }

                return outputs;
            }

            public void backPropagateOutput(float[] expected)
            {
                for (int i = 0; i < numberOfOutputs; i++)
                    error[i] = outputs[i] - expected[i]; //first calculate error

                for (int i = 0; i < numberOfOutputs; i++)
                    gamma[i] = error[i] * tanhDer(outputs[i]);

                //---------------update  weight deltas ----------------------------------

                for (int i = 0; i < numberOfOutputs; i++)
                {
                    for (int j = 0; j < numberOfInputs; j++)
                    {
                        weightDeltas[i, j] = gamma[i] * inputs[j];
                    }
                }
                
            } //back propagation function for output layer
            public void backPropagateHidden(float[] gammaForward, float[,] weightsForward)
            {
                for (int i = 0; i < numberOfOutputs; i++)
                {
                    gamma[i] = 0;

                    for (int j =0; j < gammaForward.Length; j++)
                    {
                        gamma[i] += gammaForward[j] * weightsForward[j, i];
                    }

                    gamma[i] *= tanhDer(outputs[i]);
                }

                //---------------update  weight deltas ----------------------------------

                for (int i = 0; i < numberOfOutputs; i++) 
                {
                    for (int j = 0; j < numberOfInputs; j++)
                    {
                        weightDeltas[i, j] = gamma[i] * inputs[j];
                    }
                }
                    
            } //back propagation function for hidden layers
            public float tanhDer(float value)
            {
                return 1 - (value * value); //calculate the derivative of tanh(x)
            }
        }

    }

}
