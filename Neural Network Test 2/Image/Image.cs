using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Neural_Network_Test_2
{
    public class Image
    {
        public Bitmap BaseImage { get; private set; }
        public int Width { get { return BaseImage.Width; } }
        public int Height { get { return BaseImage.Height; } }
        
        public string FolderPath { get; private set; } = "";
        public string FileName { get; private set; } = "";
        public string FullPath { get { return string.Format("{0}\\{1}", FolderPath, FileName); } }
        public string FileExtension { 
            get
            {
                if (FileName == "")
                    return "";
                else
                    return FileName.Split('.').Last();
            } 
        }
        public string FileNameNoExtension
        {
            get
            {
                if (FileName == "")
                    return "";
                else
                    return FileName.Split('.').First();
            }
        }

        public Image()
        {
            BaseImage = null;      
        }
        public Image(Color[,] input)
        {
            SetPixels(input);
        }
        public Image(string folderPath, string fileName)
        {
            FolderPath = folderPath;
            FileName = fileName;
            
            if (!File.Exists(FullPath))
                throw new Exception(string.Format("Image does not exist at {0}", FullPath));

            BaseImage = new Bitmap(FullPath);
        }

        public void SaveImage(string folderPath, string fileName)
        {
            //if the directory doesn't exist, create it first
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            
            string fullPath = folderPath + "\\" + fileName;
            BaseImage.Save(fullPath);
        }
        public void SetPixels(Color[,] input)
        {
            int width = input.GetLength(0);
            int height = input.GetLength(1);

            BaseImage = new Bitmap(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    BaseImage.SetPixel(i, j, input[i, j]);
                }
            }
        }
        public Color[,] GetPixels()
        {
            var output = new Color[Width, Height];

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    output[i, j] = BaseImage.GetPixel(i, j);
                }
            }
            return output;
        }
        
    }




}
