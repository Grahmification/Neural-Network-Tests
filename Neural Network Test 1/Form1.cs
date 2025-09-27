namespace Neural_Network_Test_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;

            int learningSteps = int.Parse(textBox_learningSteps.Text);
            float learningRate = float.Parse(textBox_learningRate.Text);

            var progress = new Progress<double>(s => progressBar1.Value = (int)(s*100));
            var errorProgress = new Progress<float[]>(s => UpdateErrorCharts(s));
            var net = await Task.Factory.StartNew(() => TrainXOR(progress, errorProgress , learningRate, learningSteps));

            btn.Enabled = true;
        }

        private void UpdateErrorCharts(float[] error)
        {
            for(int i = 0; i < error.Length; i++)
            {
                error[i] = Math.Abs(error[i]);

                if (error[i] > 1) { error[i] = 1; }
            }
            progressBar_Error1.Value = (int)(error[0] * 100);
            progressBar_Error2.Value = (int)(error[1] * 100);
            progressBar_Error3.Value = (int)(error[2] * 100);
            progressBar_Error4.Value = (int)(error[3] * 100);
            progressBar_Error5.Value = (int)(error[4] * 100);
            progressBar_Error6.Value = (int)(error[5] * 100);
            progressBar_Error7.Value = (int)(error[6] * 100);
            progressBar_Error8.Value = (int)(error[7] * 100);
        }

        public NeuralNetwork TrainXOR(IProgress<double> progress, IProgress<float[]> errorUpdate, float learningRate, int learningSteps)
        {
            NeuralNetwork net = new([3, 25, 25, 1], learningRate); // 3 inputs, 2 x 25 neuron hidden layers, 1 output

            for (int i = 0; i < learningSteps; i++) // Iterate over 5000 learning steps
            {
                float[] error = new float[8];

                net.FeedForward([0, 0, 0]);
                error[0] = net.BackPropagate([0])[0];

                net.FeedForward([0, 0, 1]);
                error[1] = net.BackPropagate([1])[0];

                net.FeedForward([0, 1, 0]);
                error[2] = net.BackPropagate([1])[0];

                net.FeedForward([0, 1, 1]);
                error[3] = net.BackPropagate([0])[0];

                net.FeedForward([1, 0, 0]);
                error[4] = net.BackPropagate([1])[0];

                net.FeedForward([1, 0, 1]);
                error[5] = net.BackPropagate([0])[0];

                net.FeedForward([1, 1, 0]);
                error[6] = net.BackPropagate([0])[0];

                net.FeedForward([1, 1, 1]);
                error[7] = net.BackPropagate([1])[0];

                if (i % 10 == 0 )
                {
                    errorUpdate.Report(error);
                    progress.Report(i * 1.0 / (learningSteps - 1));
                }
                
            }

            return net;
        }
    }
}
