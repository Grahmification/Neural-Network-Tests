namespace Neural_Network_Test_2
{
    /// <summary>
    /// Wrapper for an image file
    /// </summary>
    public class Image
    {
        public Bitmap? BaseImage { get; private set; }
        public int Width => BaseImage?.Width ?? 0;
        public int Height => BaseImage?.Height ?? 0;
        
        public string FolderPath { get; private set; } = "";
        public string FileName { get; private set; } = "";
        public string FullPath => Path.Combine(FolderPath, FileName);
        public string FileExtension => FileName == "" ? "" : FileName.Split('.').Last();
        public string FileNameNoExtension => FileName == "" ? "" : FileName.Split('.').First();

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
                throw new Exception($"Image does not exist at {FullPath}");

            BaseImage = new Bitmap(FullPath);
        }

        public void SaveImage(string folderPath, string fileName)
        {
            if(BaseImage != null)
            {
                // If the directory doesn't exist, create it first
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string fullPath = Path.Combine(folderPath, fileName);
                BaseImage.Save(fullPath);
            }
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
            if (BaseImage == null)
                return new Color[0, 0];
            
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