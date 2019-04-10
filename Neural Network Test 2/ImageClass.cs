using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Neural_Network_Test_2
{
    public class ImageClass
    {
        Bitmap image;

        public int width { get { return image.Width; } }
        public int height { get { return image.Height; } }

        public ImageClass(float[][] input, int width, int height)
        {
            Color[,] pixels = new Color[width, height];
            int counter = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    pixels[i, j] = convertDataToPixcel(input[counter]);
                    counter++;
                }
            }

            setPixels(pixels);

        }
        public ImageClass(Color[,] input)
        {
            setPixels(input);
        }
        public ImageClass(string folderPath, string fileName)
        {
            string fullPath = folderPath + "\\" + fileName;
            image = new Bitmap(fullPath);
        }

        public void saveImage(string folderPath, string fileName)
        {
            string fullPath = folderPath + "\\" + fileName;
            image.Save(fullPath);
        }

        public void setPixels(Color[,] input)
        {
            int width = input.GetLength(0);
            int height = input.GetLength(1);

            image = new Bitmap(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    image.SetPixel(i, j, input[i, j]);
                }
            }
        }
        public Color[,] getPixels()
        {
            var output = new Color[image.Width, image.Height];

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    output[i,j] = image.GetPixel(i, j);
                }
            }
                return output;
        }
        public static float[] convertPixelToData(Color input)
        {
            var output = new float[3];
            output[0] = input.R;
            output[1] = input.G;
            output[2] = input.B;

            for (int i = 0; i < output.Length; i++)
                output[i] /= 255.0f; //normalize each value to a decimal between 0 and 1.

            return output;
        }
        public static float[] convertPixelArrayToData(Color[] input)
        {
            var output = new float[3*input.Length];

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

        public static Color convertDataToPixcel(float[] input)
        {
            var output = Color.FromArgb(0, 0, 0); //initialize output as black color
            
            // ---------------un-normalize all input data ----------------------
            int[] normalizedInput = new int[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                input[i] *= 255; //was originally normalized between 0, 1 prior to processing

                if (input[i] > 255) {input[i] = 255;} //constrain between 0, 255
                if (input[i] < 0) {input[i] = 0;}

                input[i] = (float)Math.Round(input[i]); //round to nearest whole number

                normalizedInput[i] = (int)input[i]; //convert to integer
            }

            output = Color.FromArgb(normalizedInput[0], normalizedInput[1], normalizedInput[2]); //convert RGB to color 
            return output;
        }

        public neuralNetwork.NetworkIOData getNetworkData()
        {
            return new neuralNetwork.NetworkIOData(getConvertedPixels(), 3);
        }

        public neuralNetwork.NetworkIOData getNetworkData_neighbors()
        {
            var outputFloat = new float[image.Width * image.Height][];
            int counter = 0;

            for (int i = 0; i < this.image.Width; i++)
            {
                for (int j = 0; j < this.image.Height; j++)
                {
                    List<Color> pixelList = new List<Color>();

                    pixelList.Add(image.GetPixel(i, j));

                    if (i == 0) { pixelList.Add(image.GetPixel(i+1 , j)); }
                    else { pixelList.Add(image.GetPixel(i-1 , j)); }

                    if (i == image.Width - 1) { pixelList.Add(image.GetPixel(i-1 , j)); }
                    else { pixelList.Add(image.GetPixel(i + 1, j)); }

                    if (j == 0) { pixelList.Add(image.GetPixel(i, j + 1)); }
                    else { pixelList.Add(image.GetPixel(i, j - 1)); }

                    if (j == image.Height - 1) { pixelList.Add(image.GetPixel(i, j - 1)); }
                    else { pixelList.Add(image.GetPixel(i, j + 1)); }

                    
                    outputFloat[counter] = convertPixelArrayToData(pixelList.ToArray());
                    counter++;
                }
            }

            var output = new neuralNetwork.NetworkIOData(outputFloat, 15);

            return output;
        }

        public neuralNetwork.NetworkIOData getNetworkData_neighbors2()
        {
            var inputFloat = getPixels(); //gets all pre-converted pixels
            var outputFloat = new float[this.image.Width * this.image.Height][];
            int counter = 0;

            for (int i = 0; i < this.image.Width; i++)
            {
                for (int j = 0; j < this.image.Height; j++)
                {
                    var pixelList = new List<Color>();

                    pixelList.Add(inputFloat[i,j]);

                    if (i == 0) { pixelList.Add(inputFloat[i + 1, j]); }
                    else { pixelList.Add(inputFloat[i - 1, j]); }

                    if (i == image.Width - 1) { pixelList.Add(inputFloat[i - 1, j]); }
                    else { pixelList.Add(inputFloat[i + 1, j]); }

                    if (j == 0) { pixelList.Add(inputFloat[i, j + 1]); }
                    else { pixelList.Add(inputFloat[i, j - 1]); }

                    if (j == image.Height - 1) { pixelList.Add(inputFloat[i, j - 1]); }
                    else { pixelList.Add(inputFloat[i, j + 1]); }

                    outputFloat[counter] = convertPixelArrayToData(pixelList.ToArray());
                    counter++;
                }
            }

            var output = new neuralNetwork.NetworkIOData(outputFloat, 15);

            return output;
        }




        private float[][] getConvertedPixels()
        {
            var outputFloat = new float[image.Width * image.Height][];
            int counter = 0;

            for (int i = 0; i < this.image.Width; i++)
            {
                for (int j = 0; j < this.image.Height; j++)
                {
                    outputFloat[counter] = convertPixelToData(image.GetPixel(i, j));
                    counter++;
                }
            }
            return outputFloat;
        }

    }


    public class Pixel
    {
        int R; //RGB red component
        int G; //RGB green component
        int B; //RGB blue component
        int[] pos; //store pixel's position in image if needed

        public Pixel(int R, int G, int B)
        {
            this.R = R;
            this.G = G;
            this.B = B;

            this.pos = new int[]{ -1, -1 }; //no position given
        }
        public Pixel(int R, int G, int B, int[] pos)
        {
            this.R = R;
            this.G = G;
            this.B = B;

            this.pos = pos;
        }
    }
}
