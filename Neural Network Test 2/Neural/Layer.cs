using System;

namespace Neural_Network_Test_2.Neural
{
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
        public static Random random = new Random();

        public IActivationFunction ActFunction { get; private set; }

        public Layer(int numberOfInputs, int numberOfOutputs, IActivationFunction function)
        {
            this.numberOfInputs = numberOfInputs;
            this.numberOfOutputs = numberOfOutputs;
            this.ActFunction = function;

            outputs = new float[numberOfOutputs];
            inputs = new float[numberOfInputs];
            weights = new float[numberOfOutputs, numberOfInputs];
            weightDeltas = new float[numberOfOutputs, numberOfInputs];
            gamma = new float[numberOfOutputs];
            error = new float[numberOfOutputs];

            initializeWeights();
        }


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

                outputs[i] = ActFunction.Function(outputs[i]); //squelch each current layer node value using desired function
            }

            return outputs;
        }
        public void backPropagateOutput(float[] expected)
        {
            for (int i = 0; i < numberOfOutputs; i++)
                error[i] = outputs[i] - expected[i]; //first calculate error

            for (int i = 0; i < numberOfOutputs; i++)
                gamma[i] = error[i] * ActFunction.Derivative(outputs[i]);

            UpdateWeightDeltas(); //update weight deltas

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

                gamma[i] *= ActFunction.Derivative(outputs[i]);
            }

            UpdateWeightDeltas(); //update weight deltas

        } //back propagation function for hidden layers
        public void updateWeights(float learningRate)
        {
            for (int i = 0; i < numberOfOutputs; i++)
            {
                for (int j = 0; j < numberOfInputs; j++)
                {
                    weights[i, j] -= weightDeltas[i, j] * learningRate;
                }
            }
        } //update weights for each learning step, occurs after all layers have been updated

        private void initializeWeights()
        {
            for (int i = 0; i < numberOfOutputs; i++)
            {
                for (int j = 0; j < numberOfInputs; j++)
                {
                    weights[i, j] = (float)random.NextDouble() - 0.5f;
                }
            }
        } //want weightings to start with random values
        private void UpdateWeightDeltas()
        {
            for (int i = 0; i < numberOfOutputs; i++)
            {
                for (int j = 0; j < numberOfInputs; j++)
                {
                    weightDeltas[i, j] = gamma[i] * inputs[j];
                }
            }
        }
    }
}
