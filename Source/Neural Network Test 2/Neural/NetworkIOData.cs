namespace Neural_Network_Test_2.Neural
{
    /// <summary>
    /// Input data for the neural network
    /// </summary>
    public class NetworkIOData
    {
        private readonly float[][] _data;

        public int DataSize { get; private set; }
        public int Count => _data.Length;
        public List<float[]> DataList => _data.ToList();

        public NetworkIOData(List<float[]> data)
        {
            _data = [.. data];
            DataSize = data.First().Length;

            VerifyData();
        }
        public float[] GetData(int index)
        {
            return _data[index];
        }
        public double[] GetDataDouble(int index)
        {
            return Array.ConvertAll(_data[index], x => (double)x);
        }

        /// <summary>
        /// Confirms that all data is the same size
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void VerifyData()
        {
            for (int i = 0; i < _data.GetLength(0); i++)
            {
                if (_data[i].Length != DataSize)
                {
                    throw new Exception("NetworkIOData supplied with incorrect data length.");
                }
            }
        }
    }

 



}
