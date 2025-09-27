namespace Neural_Network_Test_2
{
    /// <summary>
    /// A color pixel in an image
    /// </summary>
    public class Pixel
    {
        public int R { get; private set; } = -1; // RGB red component
        public int G { get; private set; } = -1; // RGB green component
        public int B { get; private set; } = -1; // RGB blue component
        public int[] Position { get; private set; } = [-1, -1]; // Store pixel's position in image if needed

        public Pixel(int r, int g, int b, int[]? pos = null)
        {
            R = r;
            G = g;
            B = b;

            // No position given if null [-1, -1]
            Position = pos ?? ([-1, -1]);
        }
    }
}

