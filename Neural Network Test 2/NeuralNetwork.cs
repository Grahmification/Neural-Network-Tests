using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord;

namespace Neural_Network_Test_2
{
    
    

    
    
    public class neuralNetwork
    {
        /*
         *  tutorial: https://www.youtube.com/watch?v=L_PByyJ9g-I
         *  "Neural Network - Back-Propagation Tutorial In C#"
         * 
         * other useful links:
         * https://www.youtube.com/watch?v=0bYLr6Kr57w
         * http://kostiantyn-dvornik.blogspot.ca/2015/04/neural-network-image-processing-tutorial.html       
        */

        int[] layer;
        Layer[] layers;

        public neuralNetwork(int[] layer, float learningRate)
        {
            this.layer = new int[layer.Length];
            for (int i = 0; i < layer.Length; i++)
                this.layer[i] = layer[i];

            layers = new Layer[layer.Length - 1];

            for (int i = 0; i < layers.Length; i++)
            {
                layers[i] = new Layer(layer[i], layer[i + 1], learningRate);
            }

        }
        public float[] feedForward(float[] inputs)
        {
            layers[0].feedForward(inputs);

            for (int i = 1; i < layers.Length; i++)
            {
                layers[i].feedForward(layers[i - 1].outputs);
            }

            return layers[layers.Length - 1].outputs;
        }
        public float[] backPropagate(float[] expectedValues)
        {

            for (int i = layers.Length - 1; i >= 0; i--)
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

        public static neuralNetwork Train(NetworkIOData inputData, NetworkIOData dataSolution, float learningRate, int epochs, IProgress<TrainingUpdate> progress)
        {
            //------------------------------ Initialize Network ------------------------------------

            neuralNetwork net = new neuralNetwork(new int[] {inputData.dataLength, 10, 10, 10, dataSolution.dataLength}, learningRate);

            //--------------------------------- Check for errors in input data -------------------------
            
            if (inputData.Count != dataSolution.Count)
                throw new Exception("Training Error: Input and solution need have the same amount of datapoints.");

            //------------------------------ Train Network ------------------------------------
            int Count = inputData.Count;
            TrainingUpdate updateData;
         
            int reportInterval = (int)Math.Round(Count / 1000.0);

            for (int e = 0; e < epochs; e++)
            {
                for (int i = 0; i < Count; i++)
                {
                    net.feedForward(inputData.data(i));
                    updateData.currentError = net.backPropagate(dataSolution.data(i));

                    if (i % reportInterval == 0) //only send update every few training steps
                    {
                        updateData.currentEpoch = e+1;
                        updateData.currentTrainingStep = i+1;
                        updateData.maxEpochs = epochs;
                        updateData.maxTrainingSteps = Count;

                        progress.Report(updateData); //report progress for UI elements
                    }
                }
            }

            return net;
        }
        public static float[][] ApplyNetwork(NetworkIOData inputData, neuralNetwork net, IProgress<double> progress)
        {
            int Count = inputData.Count;    
            int dataLength = inputData.data(0).Length;
            var Data = new float[Count][];
     
            int reportInterval = (int)Math.Round(Count / 100.0);

            for (int i = 0; i < Count; i++)
            {                
                float[] moddedVal = new float[dataLength];
                moddedVal = net.feedForward(inputData.data(i));

                float[] tmp = new float[moddedVal.Length]; //needed to avoid object references being transfered

                for (int j = 0; j < moddedVal.Length; j++)
                {
                    tmp[j] = moddedVal[j];
                }

                Data[i] = tmp;
                

                if (i % reportInterval == 0) //only send update every few training steps
                    progress.Report((i + 1) / (double)Count); //report progress for progress bar         
            }
       
            return Data;
        }


        public static double calcAverage(float[] input)
        {
            double output = 0;
            for (int j = 0; j < input.Length; j++) //compute the average error
                output += input[j];
            output /= input.Length;
            

            return output;
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
                for (int i = 0; i < numberOfOutputs; i++)
                {
                    for (int j = 0; j < numberOfInputs; j++)
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

                for (int i = 0; i < numberOfOutputs; i++) //iterate over each neuron in current layer
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

                    for (int j = 0; j < gammaForward.Length; j++)
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

        public class NetworkIOData
        {
            private float[][] _data;
            public int dataLength;
           
            public int Count
            {
                get { return _data.Length; }
            }
            public NetworkIOData(float[][] data, int dataLength)
            {
                this._data = data;
                this.dataLength = dataLength;

                for (int i=0; i < data.Length / dataLength; i++)
                {
                    if (data[i].Length != dataLength)
                    {
                        throw new Exception("NetworkIOData supplied with incorrect data length.");
                    }
                }
            }  
            public float[] data(int index)
            {
                return _data[index];
            }
            public double[] data_dbl(int index)
            {
                return Array.ConvertAll(_data[index], x => (double)x);  
                //return _data[index].Select(d => (double)d).ToArray();
            }
       
        }

        public struct TrainingUpdate
        {
            public int currentTrainingStep;
            public int currentEpoch;
            public int maxTrainingSteps;
            public int maxEpochs;
            
            public float[] currentError;
        }
            
        

    }
}
