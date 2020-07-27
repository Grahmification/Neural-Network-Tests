using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Network_Test_2.Neural
{
    public class NetworkIOData
    {
        private float[][] _data;
        public int DataSize { get; private set; }
        public int Count { get { return _data.Length; } }
        public List<float[]> DataList { get { return _data.ToList(); } }

        public NetworkIOData(List<float[]> data)
        {
            _data = data.ToArray();
            DataSize = data.First().Length;

            VerifyData();
        }
        public float[] data(int index)
        {
            return _data[index];
        }
        public double[] data_dbl(int index)
        {
            return Array.ConvertAll(_data[index], x => (double)x);
        }

        private void VerifyData()
        {
            for (int i = 0; i < _data.GetLength(0); i++)
            {
                if (_data[i].Length != DataSize)
                {
                    throw new Exception("NetworkIOData supplied with incorrect data length.");
                }
            }
        } //confirms that all data is the same size
    }

 



}
