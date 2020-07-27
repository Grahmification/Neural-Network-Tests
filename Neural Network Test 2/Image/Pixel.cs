namespace Neural_Network_Test_2
{
    public class Pixel
    {
        public int R { get; private set; } = -1; //RGB red component
        public int G { get; private set; } = -1; //RGB green component
        public int B { get; private set; } = -1; //RGB blue component
        public int[] Pos { get; private set; } = new int[] { -1, -1 }; //store pixel's position in image if needed

        public Pixel(int R, int G, int B)
        {
            this.R = R;
            this.G = G;
            this.B = B;

            this.Pos = new int[] { -1, -1 }; //no position given
        }
        public Pixel(int R, int G, int B, int[] pos)
        {
            this.R = R;
            this.G = G;
            this.B = B;

            this.Pos = pos;
        }
    }
}

