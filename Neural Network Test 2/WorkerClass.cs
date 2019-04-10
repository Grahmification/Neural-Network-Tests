using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace Neural_Network_Test_2
{
    public class WorkerClass
    {
        
        public static Color[,] loadImage(string folderPath, string fileName)
        {
            var output = new ImageClass(folderPath, fileName);
            return output.getPixels();
        }     
        public static neuralNetwork TrainNetworkFromImage(Color[,] inputData, Color[,] dataSolution, float learningRate, IProgress<double> progress)
        {
            //------------------------------ Initialize Network ------------------------------------

            neuralNetwork net = new neuralNetwork(new int[] { 3, 10, 10 , 10 , 3 }, learningRate);

            //------------------------------ Train Network ------------------------------------
            int width = inputData.GetLength(0);
            int height = inputData.GetLength(1);

            if (inputData.GetLength(0) != dataSolution.GetLength(0) || inputData.GetLength(1) != dataSolution.GetLength(1))
                throw new Exception("Input and Solution need to be the same size.");

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    float[] input = ImageClass.convertPixelToData(inputData[i, j]);
                    float[] soln = ImageClass.convertPixelToData(dataSolution[i, j]);

                    net.feedForward(input);
                    net.backPropagate(soln);
                }

                progress.Report((i + 1) / (double)width); //report progress for progress bar
            }

            return net;
        }
        public static ImageClass ApplyNetworkToImage(Color[,] inputData, neuralNetwork net, IProgress<double> progress)
        {
            //------------------------------ Train Network ------------------------------------
            int width = inputData.GetLength(0);
            int height = inputData.GetLength(1);

            var outputData = new Color[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    float[] input = ImageClass.convertPixelToData(inputData[i, j]); //convert input pixels to data
                    float[] output = net.feedForward(input); //process data through network
                    outputData[i, j] = ImageClass.convertDataToPixcel(output); //convert output to pixels
                }

                progress.Report((i + 1) / (double)width); //report progress for progress bar
            }

            var outputImage = new ImageClass(outputData); //convert output data to image
            return outputImage;
        }


        public static neuralNetwork Train2(string folderPath, string inputPicName, string solnPicName, float learningRate, IProgress<neuralNetwork.TrainingUpdate> progress)
        {
            try
            {
                ImageClass inputPic = new ImageClass(folderPath, inputPicName);
                ImageClass solnPic = new ImageClass(folderPath, solnPicName);

                var net = neuralNetwork.Train(inputPic.getNetworkData_neighbors2(), solnPic.getNetworkData(), learningRate, 1, progress);

                return net;
            }
            catch (AggregateException ae)
            {
                ae.Flatten();
            }
            return null;
        }
        public static ImageClass Process2(string folderPath, string inputPicName, neuralNetwork net, IProgress<double> progress)
        {
            ImageClass inputPic = new ImageClass(folderPath, inputPicName);

            var outData = neuralNetwork.ApplyNetwork(inputPic.getNetworkData_neighbors2(), net, progress);
            var output = new ImageClass(outData, inputPic.width, inputPic.height);

            return output;
        }


        //--------------- Uses Aforge ANN library ---------------
        public static ActivationNetwork Train3(string folderPath, string inputPicName, string solnPicName, float learningRate, IProgress<neuralNetwork.TrainingUpdate> progress)
        {
            try
            {
                ImageClass inputPic = new ImageClass(folderPath, inputPicName);
                ImageClass solnPic = new ImageClass(folderPath, solnPicName);

                var inputData = inputPic.getNetworkData_neighbors2();
                var solnData = solnPic.getNetworkData();

                // create neural network
                ActivationNetwork network = new ActivationNetwork(new BipolarSigmoidFunction(1), inputData.dataLength, 10, solnData.dataLength);
                //ActivationNetwork network = new ActivationNetwork(new ReluFunction(), inputData.dataLength, 10, 10, 10, solnData.dataLength);

                //create trainer
                BackPropagationLearning teacher = new BackPropagationLearning(network);
                teacher.LearningRate = learningRate;
                

                //--------------------------------- Check for errors in input data -------------------------

                if (inputData.Count != solnData.Count)
                    throw new Exception("Training Error: Input and solution need have the same amount of datapoints.");

                //------------------------------ Train Network ------------------------------------
                int Count = inputData.Count;
                neuralNetwork.TrainingUpdate updateData;

                int reportInterval = (int)Math.Round(Count / 1000.0);

          
                    for (int i = 0; i < Count; i++)
                    {
                        double error = teacher.Run(inputData.data_dbl(i), solnData.data_dbl(i));
                        
                        updateData.currentError = new float[] { (float)error };
                        
                        if (i % reportInterval == 0) //only send update every few training steps
                        {
                            updateData.currentEpoch = 0 + 1;
                            updateData.currentTrainingStep = i + 1;
                            updateData.maxEpochs = 1;
                            updateData.maxTrainingSteps = Count;

                            progress.Report(updateData); //report progress for UI elements
                        }
                    }

            
                return network;
            }
            catch (AggregateException ae)
            {
                ae.Flatten();
            }
            return null;
        }

        public static ImageClass Process3(string folderPath, string inputPicName, ActivationNetwork net, IProgress<double> progress)
        {
            ImageClass inputPic = new ImageClass(folderPath, inputPicName);

            var inputData = inputPic.getNetworkData_neighbors2();
            int Count = inputData.Count;
            int dataLength = inputData.data(0).Length;
            var Data = new float[Count][];

            int reportInterval = (int)Math.Round(Count / 100.0);

            for (int i = 0; i < Count; i++)
            {
                double[] moddedVal = new double[dataLength];
                var tmpinput = inputData.data_dbl(i);
                moddedVal = net.Compute(tmpinput);

                float[] tmp = new float[moddedVal.Length]; //needed to avoid object references being transfered

                for (int j = 0; j < moddedVal.Length; j++)
                {
                    tmp[j] = (float)moddedVal[j];
                }

                Data[i] = tmp;


                if (i % reportInterval == 0) //only send update every few training steps
                    progress.Report((i + 1) / (double)Count); //report progress for progress bar         
            }

            var output = new ImageClass(Data, inputPic.width, inputPic.height);
            return output;
        }



        public class ReluFunction : IActivationFunction 
        {

            public double Derivative(double x)
            {
                return 1.0;
            }

            public double Derivative2(double y)
            {
                return 1.0;
            }

            public double Function(double x)
            {

                return x * 1.0 ;
            }
        }


    }
}
