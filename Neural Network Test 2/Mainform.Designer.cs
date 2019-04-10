namespace Neural_Network_Test_2
{
    partial class Mainform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar_Training = new System.Windows.Forms.ProgressBar();
            this.textBox_LearningRate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_Train = new System.Windows.Forms.Button();
            this.textBox_EditedPicture = new System.Windows.Forms.TextBox();
            this.textBox_inputPicture = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Workingfolder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.progressBar_processing = new System.Windows.Forms.ProgressBar();
            this.button_process = new System.Windows.Forms.Button();
            this.textBox_processImage = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.plotView1 = new OxyPlot.WindowsForms.PlotView();
            this.userControlTest1 = new Neural_Network_Test_2.UserControlTest();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.progressBar_Training);
            this.panel1.Controls.Add(this.textBox_LearningRate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.button_Train);
            this.panel1.Controls.Add(this.textBox_EditedPicture);
            this.panel1.Controls.Add(this.textBox_inputPicture);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(412, 118);
            this.panel1.TabIndex = 0;
            // 
            // progressBar_Training
            // 
            this.progressBar_Training.Location = new System.Drawing.Point(6, 86);
            this.progressBar_Training.Name = "progressBar_Training";
            this.progressBar_Training.Size = new System.Drawing.Size(250, 23);
            this.progressBar_Training.TabIndex = 7;
            // 
            // textBox_LearningRate
            // 
            this.textBox_LearningRate.Location = new System.Drawing.Point(82, 57);
            this.textBox_LearningRate.Name = "textBox_LearningRate";
            this.textBox_LearningRate.Size = new System.Drawing.Size(93, 20);
            this.textBox_LearningRate.TabIndex = 6;
            this.textBox_LearningRate.Text = "0.05";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Learning Rate";
            // 
            // button_Train
            // 
            this.button_Train.Location = new System.Drawing.Point(262, 57);
            this.button_Train.Name = "button_Train";
            this.button_Train.Size = new System.Drawing.Size(135, 52);
            this.button_Train.TabIndex = 4;
            this.button_Train.Text = "Train NN";
            this.button_Train.UseVisualStyleBackColor = true;
            this.button_Train.Click += new System.EventHandler(this.button_Train_Click);
            // 
            // textBox_EditedPicture
            // 
            this.textBox_EditedPicture.Location = new System.Drawing.Point(82, 31);
            this.textBox_EditedPicture.Name = "textBox_EditedPicture";
            this.textBox_EditedPicture.Size = new System.Drawing.Size(315, 20);
            this.textBox_EditedPicture.TabIndex = 3;
            this.textBox_EditedPicture.Text = "pic2.jpg";
            // 
            // textBox_inputPicture
            // 
            this.textBox_inputPicture.Location = new System.Drawing.Point(82, 5);
            this.textBox_inputPicture.Name = "textBox_inputPicture";
            this.textBox_inputPicture.Size = new System.Drawing.Size(315, 20);
            this.textBox_inputPicture.TabIndex = 2;
            this.textBox_inputPicture.Text = "pic1.jpg";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Edited Picture";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input Picture";
            // 
            // textBox_Workingfolder
            // 
            this.textBox_Workingfolder.Location = new System.Drawing.Point(97, 12);
            this.textBox_Workingfolder.Name = "textBox_Workingfolder";
            this.textBox_Workingfolder.Size = new System.Drawing.Size(327, 20);
            this.textBox_Workingfolder.TabIndex = 3;
            this.textBox_Workingfolder.Text = "C:\\Users\\Graham\\Desktop\\New folder";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Working Folder";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Controls.Add(this.userControlTest1);
            this.panel2.Controls.Add(this.progressBar_processing);
            this.panel2.Controls.Add(this.button_process);
            this.panel2.Controls.Add(this.textBox_processImage);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(12, 162);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(412, 95);
            this.panel2.TabIndex = 5;
            // 
            // progressBar_processing
            // 
            this.progressBar_processing.Location = new System.Drawing.Point(6, 31);
            this.progressBar_processing.Name = "progressBar_processing";
            this.progressBar_processing.Size = new System.Drawing.Size(250, 23);
            this.progressBar_processing.TabIndex = 7;
            // 
            // button_process
            // 
            this.button_process.Location = new System.Drawing.Point(262, 31);
            this.button_process.Name = "button_process";
            this.button_process.Size = new System.Drawing.Size(135, 52);
            this.button_process.TabIndex = 4;
            this.button_process.Text = "Process Picture";
            this.button_process.UseVisualStyleBackColor = true;
            this.button_process.Click += new System.EventHandler(this.button_process_Click);
            // 
            // textBox_processImage
            // 
            this.textBox_processImage.Location = new System.Drawing.Point(82, 5);
            this.textBox_processImage.Name = "textBox_processImage";
            this.textBox_processImage.Size = new System.Drawing.Size(315, 20);
            this.textBox_processImage.TabIndex = 2;
            this.textBox_processImage.Text = "pic1.jpg";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Input Picture";
            // 
            // plotView1
            // 
            this.plotView1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.plotView1.Location = new System.Drawing.Point(12, 263);
            this.plotView1.Name = "plotView1";
            this.plotView1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView1.Size = new System.Drawing.Size(412, 323);
            this.plotView1.TabIndex = 6;
            this.plotView1.Text = "plotView1";
            this.plotView1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // userControlTest1
            // 
            this.userControlTest1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userControlTest1.Location = new System.Drawing.Point(11, 52);
            this.userControlTest1.Name = "userControlTest1";
            this.userControlTest1.Size = new System.Drawing.Size(254, 40);
            this.userControlTest1.TabIndex = 8;
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 597);
            this.Controls.Add(this.plotView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_Workingfolder);
            this.Controls.Add(this.panel1);
            this.Name = "Mainform";
            this.Text = "Neural Network Test 2";
            this.Load += new System.EventHandler(this.Mainform_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_inputPicture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_EditedPicture;
        private System.Windows.Forms.Button button_Train;
        private System.Windows.Forms.TextBox textBox_LearningRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar_Training;
        private System.Windows.Forms.TextBox textBox_Workingfolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ProgressBar progressBar_processing;
        private System.Windows.Forms.Button button_process;
        private System.Windows.Forms.TextBox textBox_processImage;
        private System.Windows.Forms.Label label7;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private UserControlTest userControlTest1;
    }
}

