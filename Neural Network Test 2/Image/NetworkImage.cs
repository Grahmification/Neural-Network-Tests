using System;
using System.Drawing;
using System.Collections.Generic;
using Neural_Network_Test_2.Neural;
using System.Threading.Tasks;
using System.Threading;

namespace Neural_Network_Test_2
{
    public class NetworkImage : Image
    {
        public NetworkImage(List<float[]> pixelData, int width, int height) : base()
        {
            Color[,] pixels = new Color[width, height];
            int counter = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    pixels[i, j] = ConvertDataToPixel(pixelData[counter]);
                    counter++;
                }
            }

            SetPixels(pixels);
        }
        public NetworkImage(string folderPath, string fileName) : base(folderPath, fileName)
        {

        }


        public async Task<NetworkIOData> GetNetworkDataAsync(IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default)
        {
            return await Task.Run(() => GetNetworkData(progress, cancel), cancel); 
        }   
        public async Task<NetworkIOData> GetNetworkData_NeighborsAsync(IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default)
        {
             return await Task.Run(() => GetNetworkData_Neighbors(progress, cancel), cancel);     
        }

        public NetworkIOData GetNetworkData(IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default)
        {
            var pixelData = new List<float[]>();
            int counter = 0;
            int ValueCount = Width * Height;

            for (int i = 0; i < Width; i++)
            {    
                for (int j = 0; j < Height; j++)
                {
                    cancel.ThrowIfCancellationRequested();

                    pixelData.Add(ConvertPixelToData(BaseImage.GetPixel(i, j)));
                    counter++;                   
                }
                
                progress.Report(new NetworkProgressArgs(counter / (double)ValueCount, NetworkStatus.PreparingData)); //only report progress every row
            }

            return new NetworkIOData(pixelData);
        }
        public NetworkIOData GetNetworkData_Neighbors(IProgress<NetworkProgressArgs> progress, CancellationToken cancel = default)
        {
            var inputFloat = GetPixels(); //gets all pre-converted pixels
            var pixelData = new List<float[]>();

            int counter = 0;
            int ValueCount = Width * Height;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    cancel.ThrowIfCancellationRequested();

                    var pixelList = new List<Color>();

                    pixelList.Add(inputFloat[i, j]);

                    if (i == 0) { pixelList.Add(inputFloat[i + 1, j]); }
                    else { pixelList.Add(inputFloat[i - 1, j]); }

                    if (i == Width - 1) { pixelList.Add(inputFloat[i - 1, j]); }
                    else { pixelList.Add(inputFloat[i + 1, j]); }

                    if (j == 0) { pixelList.Add(inputFloat[i, j + 1]); }
                    else { pixelList.Add(inputFloat[i, j - 1]); }

                    if (j == Height - 1) { pixelList.Add(inputFloat[i, j - 1]); }
                    else { pixelList.Add(inputFloat[i, j + 1]); }

                    pixelData.Add(ConvertPixelArrayToData(pixelList.ToArray()));
                    counter++;                 
                }
                
                progress.Report(new NetworkProgressArgs(counter / (double)ValueCount, NetworkStatus.PreparingData)); //only report progress every row
            }

            return new NetworkIOData(pixelData);
        }



        public static float[] ConvertPixelToData(Color input)
        {
            var output = new float[] { input.R, input.G, input.B };

            for (int i = 0; i < output.Length; i++)
                output[i] /= 255.0f; //normalize each value to a decimal between 0 and 1.

            return output;
        }
        public static float[] ConvertPixelArrayToData(Color[] input)
        {
            var output = new float[3 * input.Length];

            int counter = 0;

            for (int i = 0; i < input.Length; i++)
            {
                output[counter] = input[i].R;
                output[counter + 1] = input[i].G;
                output[counter + 2] = input[i].B;

                counter += 3;
            }

            for (int i = 0; i < output.Length; i++)
                output[i] /= 255.0f; //normalize each value to a decimal between 0 and 1.

            return output;
        }
        public static Color ConvertDataToPixel(float[] input)
        {
            var output = Color.FromArgb(0, 0, 0); //initialize output as black color

            // ---------------un-normalize all input data ----------------------
            int[] normalizedInput = new int[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                input[i] *= 255; //was originally normalized between 0, 1 prior to processing

                if (input[i] > 255) { input[i] = 255; } //constrain between 0, 255
                if (input[i] < 0) { input[i] = 0; }

                input[i] = (float)Math.Round(input[i]); //round to nearest whole number

                normalizedInput[i] = (int)input[i]; //convert to integer
            }

            output = Color.FromArgb(normalizedInput[0], normalizedInput[1], normalizedInput[2]); //convert RGB to color 
            return output;
        }
    }
}
