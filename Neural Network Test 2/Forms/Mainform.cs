using OxyPlot.WindowsForms;


namespace Neural_Network_Test_2
{
    public partial class Mainform : Form
    {       
        public PlotView ErrorPlot => plotView1;

        public string WorkingFolder { get => textBox_Workingfolder.Text; set => textBox_Workingfolder.Text = value; }
        public string TrainingInputPic { get => textBox_inputPicture.Text; set => textBox_inputPicture.Text = value; }
        public string TrainingSolnPic { get => textBox_EditedPicture.Text; set => textBox_EditedPicture.Text = value; }
        public string ProcessPic { get => textBox_processImage.Text; set => textBox_processImage.Text = value; }
        public float LearingRate => float.Parse(textBox_LearningRate.Text);

        public string StatusText { get => label_status.Text; set => label_status.Text = value; }
        public int TrainingProgress { get => progressBar_Training.Value; set => progressBar_Training.Value = value; }
        public bool AllowTraining { get => button_Train.Enabled; set => button_Train.Enabled = value; }
        public bool AllowProcessing { get => button_process.Enabled; set => button_process.Enabled = value; }
        public string TrainingButtonText { get => button_Train.Text; set => button_Train.Text = value; }
        public string ProcessingButtonText { get => button_process.Text; set => button_process.Text = value; }

        public event EventHandler? TrainButtonClicked;
        public event EventHandler? ProcessButtonClicked;

        private readonly MainFormController? Controller = null;

        public Mainform()
        {
            InitializeComponent();
            Controller = new MainFormController(this);
        }

        public void DisplayError(Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
