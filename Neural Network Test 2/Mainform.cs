using System;
using System.Drawing;
using System.Windows.Forms;
using OxyPlot.WindowsForms;


namespace Neural_Network_Test_2
{
    public partial class Mainform : Form
    {
        MainFormController Controller = null;

        public PlotView ErrorPlot { get { return plotView1; }}

        public string WorkingFolder { get { return textBox_Workingfolder.Text; } }
        public string TrainingInputPic { get { return textBox_inputPicture.Text; } }
        public string TrainingSolnPic { get { return textBox_EditedPicture.Text; } }
        public string ProcessPic { get { return textBox_processImage.Text; } }
        public float LearingRate { get { return float.Parse(textBox_LearningRate.Text); } }

        public string StatusText { get { return label_status.Text; } set { label_status.Text = value; } }
        public int TrainingProgress { get { return progressBar_Training.Value;  } set { progressBar_Training.Value = value; } }
        public bool AllowTraining { get { return button_Train.Enabled; } set { button_Train.Enabled=value; } }
        public bool AllowProcessing { get { return button_process.Enabled; } set { button_process.Enabled = value; } }
        public string TrainingButtonText { get { return button_Train.Text; } set { button_Train.Text = value; } }
        public string ProcessingButtonText { get { return button_process.Text; } set { button_process.Text = value; } }

        public event EventHandler TrainButtonClicked;
        public event EventHandler ProcessButtonClicked;

    
        public Mainform()
        {
            InitializeComponent();
            Controller = new MainFormController(this);
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
