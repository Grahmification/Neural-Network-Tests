using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Network_Test_2
{
    public class ImageClass2
    {
        Bitmap image;

        public int width { get { return image.Width; } }
        public int height { get { return image.Height; } }

        public ImageClass2(string folderPath, string fileName)
        {
            string fullPath = folderPath + "\\" + fileName;
            image = new Bitmap(fullPath);
        }

        public void saveImage(string folderPath, string fileName)
        {
            string fullPath = folderPath + "\\" + fileName;
            image.Save(fullPath);
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


        private Pixel_RGB[,] getPixels_RGB()
        {
            var output = new Pixel_RGB[image.Width, image.Height];

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    output[i, j] = new Pixel_RGB(image.GetPixel(i, j), new int[]{i, j});
                }
            }
            return output;
        }

        private void setPixels_RGB(Pixel_RGB[,] input)
        {
            
        }

        private void setPixels(Color[,] input)
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

       

    }

    public interface IPixel
    {
        public int Xpos;
        public int Ypos;
        public float[] NetworkData;
        public Color ColorObject;
      
    }


    public class Pixel_RGB : IPixel
    {
        
        private int R; //RGB red component
        private int G; //RGB green component
        private int B; //RGB blue component
        private int[] pos; //store pixel's position in image if needed

        public int IPixel.Xpos { get { return pos[0]; } } //return horizontal position of pixel
        public int IPixel.Ypos { get { return pos[1]; } } //return vertical position of pixel
        public float[] IPixel.NetworkData
        {
            get
            {
                return new float[] { R / 255f, G / 255f, B / 255f };
            }
        } //returns normalized RGB data from 0 to 1
        public Color IPixel.ColorObject { get {  return Color.FromArgb(this.R, this.B, this.G); } }

        public Pixel_RGB(Color clr, int[] pos)
        {
            this.R = clr.R;
            this.G = clr.G;
            this.B = clr.B;

            this.pos = pos;
        }
        public Pixel_RGB(float[] networkData, int[] pos)
        {
            var normalizedData = convertNetworkData(networkData);

            this.R = normalizedData[0];
            this.G = normalizedData[1];
            this.B = normalizedData[2];

            this.pos = pos;
        }


        private int[] convertNetworkData(float[] input)
        {
            // ---------------un-normalize all input data ----------------------
            int[] normalizedInput = new int[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                input[i] *= 255; //was originally normalized between 0, 1 prior to processing
                
                if (input[i] > 255) { input[i] = 255; } //constrain between 0, 255
                else if (input[i] < 0) { input[i] = 0; }
                else { input[i] = (int)Math.Round(input[i]); } //if not constraining, round to closest int

                normalizedInput[i] = (int)input[i]; 
            }

            return normalizedInput;
        }

    }
}
