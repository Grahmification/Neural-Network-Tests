using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Neural_Network_Test_2.Neural
{
    public class NeuralNetwork
    {
        /*
         *  tutorial: https://www.youtube.com/watch?v=L_PByyJ9g-I
         *  "Neural Network - Back-Propagation Tutorial In C#"
         * 
         * other useful links:
         * https://www.youtube.com/watch?v=0bYLr6Kr57w
         * http://kostiantyn-dvornik.blogspot.ca/2015/04/neural-network-image-processing-tutorial.html       
        */

        public int[] LayerNodes { get; private set; }
        private Layer[] layers;

        public int InputSize { get { return LayerNodes.First(); } }
        public int OutputSize { get { return LayerNodes.Last(); } }

        public NeuralNetwork(int[] layerNodes, IActivationFunction function)
        {
            LayerNodes = layerNodes;
            layers = new Layer[layerNodes.Length - 1];
            
            for (int i = 0; i < layers.Length; i++)
            {
                layers[i] = new Layer(layerNodes[i], layerNodes[i + 1], function);
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
        public float[] backPropagate(float[] expectedValues, float learningRate)
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
                layers[i].updateWeights(learningRate); //now that weight deltas have been calculated must apply all of them
            }

            return layers[layers.Length - 1].error; //returns error of final output layer
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

            int Count = inputData.Count;
            int dataLength = inputData.data(0).Length;
            int reportInterval = (int)Math.Round(Count / (double)ProcessReportInterval);
            
            var output = new List<float[]>();

            for (int i = 0; i < Count; i++)
            {
                cancel.ThrowIfCancellationRequested();
                
                var moddedVal = feedForward(inputData.data(i));

                float[] tmp = new float[moddedVal.Length]; //needed to avoid object references being transfered

                for (int j = 0; j < moddedVal.Length; j++)
                    tmp[j] = moddedVal[j];

                output.Add(tmp);


                if (i % reportInterval == 0) //only send update every few training steps
                {
                    double prog = (i + 1) / (double)Count;
                    progress.Report(new NetworkProgressArgs(prog, NetworkStatus.Processing));
                }
       
            }

            return output;
        }
    }
}
