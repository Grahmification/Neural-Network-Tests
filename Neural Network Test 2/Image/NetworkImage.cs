using Neural_Network_Test_2.Neural;

namespace Neural_Network_Test_2
{
    /// <summary>
    /// Pre-processes an image into data that can be fed into a neural network
    /// </summary>
    public class NetworkImage : Image, INetworkData
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
        public NetworkImage(string folderPath, string fileName) : base(folderPath, fileName) { }

        public async Task<NetworkIOData> GetSolutionDataAsync(IProgress<NetworkProgressArgs>? progress, CancellationToken cancel = default)
        {
            return await Task.Run(() => GetSolutionData(progress, cancel), cancel);
        }   
        public async Task<NetworkIOData> GetInputDataAsync(IProgress<NetworkProgressArgs>? progress, CancellationToken cancel = default)
        {
            return await Task.Run(() => GetInputData(progress, cancel), cancel);
        }

        public NetworkIOData GetSolutionData(IProgress<NetworkProgressArgs>? progress, CancellationToken cancel = default)
        {
            var pixelData = new List<float[]>();
            int counter = 0;
            int valueCount = Width * Height;

            if (BaseImage == null) { return new NetworkIOData(pixelData); }

            for (int i = 0; i < Width; i++)
            {    
                for (int j = 0; j < Height; j++)
                {
                    cancel.ThrowIfCancellationRequested();

                    pixelData.Add(ConvertPixelToData(BaseImage.GetPixel(i, j)));
                    counter++;
                }
                
                progress?.Report(new NetworkProgressArgs(counter / (double)valueCount, NetworkStatus.PreparingData)); // Only report progress every row
            }

            return new NetworkIOData(pixelData);
        }
        public NetworkIOData GetInputData(IProgress<NetworkProgressArgs>? progress, CancellationToken cancel = default)
        {
            var inputFloat = GetPixels(); // Gets all pre-converted pixels
            var pixelData = new List<float[]>();

            int counter = 0;
            int ValueCount = Width * Height;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    cancel.ThrowIfCancellationRequested();

                    var pixelList = new List<Color>
                    {
                        inputFloat[i, j]
                    };

                    if (i == 0) { pixelList.Add(inputFloat[i + 1, j]); }
                    else { pixelList.Add(inputFloat[i - 1, j]); }

                    if (i == Width - 1) { pixelList.Add(inputFloat[i - 1, j]); }
                    else { pixelList.Add(inputFloat[i + 1, j]); }

                    if (j == 0) { pixelList.Add(inputFloat[i, j + 1]); }
                    else { pixelList.Add(inputFloat[i, j - 1]); }

                    if (j == Height - 1) { pixelList.Add(inputFloat[i, j - 1]); }
                    else { pixelList.Add(inputFloat[i, j + 1]); }

                    pixelData.Add(ConvertPixelArrayToData([.. pixelList]));
                    counter++;
                }
                
                progress?.Report(new NetworkProgressArgs(counter / (double)ValueCount, NetworkStatus.PreparingData)); // Only report progress every row
            }

            return new NetworkIOData(pixelData);
        }

        public static float[] ConvertPixelToData(Color input)
        {
            float[] output = [input.R, input.G, input.B];

            for (int i = 0; i < output.Length; i++)
                output[i] /= 255.0f; // Normalize each value to a decimal between 0 and 1.

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
                output[i] /= 255.0f; // Normalize each value to a decimal between 0 and 1.

            return output;
        }
        public static Color ConvertDataToPixel(float[] input)
        {
            // --------------- Un-normalize all input data ----------------------
            int[] normalizedInput = new int[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                input[i] *= 255; // Was originally normalized between 0, 1 prior to processing

                if (input[i] > 255) { input[i] = 255; } // Constrain between 0, 255
                if (input[i] < 0) { input[i] = 0; }

                input[i] = (float)Math.Round(input[i]); // Round to nearest whole number

                normalizedInput[i] = (int)input[i]; // Convert to integer
            }

            return Color.FromArgb(normalizedInput[0], normalizedInput[1], normalizedInput[2]); // Convert RGB to color 
        }
    }
}
