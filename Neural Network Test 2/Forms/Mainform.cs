/*
This code uses the Oxplot library under the MIT License.

MIT License

Copyright (c) 2014 OxyPlot contributors

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

 */

using System;
using System.Windows.Forms;
using OxyPlot.WindowsForms;


namespace Neural_Network_Test_2
{
    public partial class Mainform : Form
    {       
        public PlotView ErrorPlot { get { return plotView1; }}

        public string WorkingFolder { get { return textBox_Workingfolder.Text; } set { textBox_Workingfolder.Text = value; } }
        public string TrainingInputPic { get { return textBox_inputPicture.Text; } set { textBox_inputPicture.Text = value; } }
        public string TrainingSolnPic { get { return textBox_EditedPicture.Text; } set { textBox_EditedPicture.Text = value; } }
        public string ProcessPic { get { return textBox_processImage.Text; } set { textBox_processImage.Text = value; } }
        public float LearingRate { get { return float.Parse(textBox_LearningRate.Text); } }

        public string StatusText { get { return label_status.Text; } set { label_status.Text = value; } }
        public int TrainingProgress { get { return progressBar_Training.Value;  } set { progressBar_Training.Value = value; } }
        public bool AllowTraining { get { return button_Train.Enabled; } set { button_Train.Enabled=value; } }
        public bool AllowProcessing { get { return button_process.Enabled; } set { button_process.Enabled = value; } }
        public string TrainingButtonText { get { return button_Train.Text; } set { button_Train.Text = value; } }
        public string ProcessingButtonText { get { return button_process.Text; } set { button_process.Text = value; } }

        public event EventHandler TrainButtonClicked;
        public event EventHandler ProcessButtonClicked;

        private MainFormController Controller = null;

        public Mainform()
        {
            InitializeComponent();
            Controller = new MainFormController(this);
        }

        public void DisplayError(Exception ex)
        {
            MessageBox.Show(string.Format("An error occurred: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button_Train_Click(object sender, EventArgs e)
        {
            TrainButtonClicked?.Invoke(sender, e);
        }
        private void button_process_Click(object sender, EventArgs e)
        {
            ProcessButtonClicked?.Invoke(sender, e);
        }

    }
}
