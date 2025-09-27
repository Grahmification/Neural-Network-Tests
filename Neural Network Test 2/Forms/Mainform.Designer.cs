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
            panel1 = new Panel();
            textBox_LearningRate = new TextBox();
            label3 = new Label();
            button_Train = new Button();
            textBox_EditedPicture = new TextBox();
            textBox_inputPicture = new TextBox();
            label2 = new Label();
            label1 = new Label();
            progressBar_Training = new ProgressBar();
            textBox_Workingfolder = new TextBox();
            label4 = new Label();
            panel2 = new Panel();
            button_process = new Button();
            textBox_processImage = new TextBox();
            label7 = new Label();
            label_status = new Label();
            plotView1 = new OxyPlot.WindowsForms.PlotView();
            panel3 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = SystemColors.ControlDark;
            panel1.Controls.Add(textBox_LearningRate);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(button_Train);
            panel1.Controls.Add(textBox_EditedPicture);
            panel1.Controls.Add(textBox_inputPicture);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(14, 44);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(481, 136);
            panel1.TabIndex = 0;
            // 
            // textBox_LearningRate
            // 
            textBox_LearningRate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox_LearningRate.Location = new Point(96, 66);
            textBox_LearningRate.Margin = new Padding(4, 3, 4, 3);
            textBox_LearningRate.Name = "textBox_LearningRate";
            textBox_LearningRate.Size = new Size(108, 23);
            textBox_LearningRate.TabIndex = 6;
            textBox_LearningRate.Text = "0.05";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(4, 69);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 5;
            label3.Text = "Learning Rate";
            // 
            // button_Train
            // 
            button_Train.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            button_Train.Location = new Point(306, 66);
            button_Train.Margin = new Padding(4, 3, 4, 3);
            button_Train.Name = "button_Train";
            button_Train.Size = new Size(158, 60);
            button_Train.TabIndex = 4;
            button_Train.Text = "Train NN";
            button_Train.UseVisualStyleBackColor = true;
            button_Train.Click += button_Train_Click;
            // 
            // textBox_EditedPicture
            // 
            textBox_EditedPicture.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox_EditedPicture.Location = new Point(96, 36);
            textBox_EditedPicture.Margin = new Padding(4, 3, 4, 3);
            textBox_EditedPicture.Name = "textBox_EditedPicture";
            textBox_EditedPicture.Size = new Size(367, 23);
            textBox_EditedPicture.TabIndex = 3;
            textBox_EditedPicture.Text = "pic2.jpg";
            // 
            // textBox_inputPicture
            // 
            textBox_inputPicture.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox_inputPicture.Location = new Point(96, 6);
            textBox_inputPicture.Margin = new Padding(4, 3, 4, 3);
            textBox_inputPicture.Name = "textBox_inputPicture";
            textBox_inputPicture.Size = new Size(367, 23);
            textBox_inputPicture.TabIndex = 2;
            textBox_inputPicture.Text = "pic1.jpg";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 39);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(80, 15);
            label2.TabIndex = 1;
            label2.Text = "Edited Picture";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 9);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(75, 15);
            label1.TabIndex = 0;
            label1.Text = "Input Picture";
            // 
            // progressBar_Training
            // 
            progressBar_Training.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar_Training.Location = new Point(130, 7);
            progressBar_Training.Margin = new Padding(4, 3, 4, 3);
            progressBar_Training.Name = "progressBar_Training";
            progressBar_Training.Size = new Size(345, 27);
            progressBar_Training.TabIndex = 7;
            // 
            // textBox_Workingfolder
            // 
            textBox_Workingfolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox_Workingfolder.Location = new Point(113, 14);
            textBox_Workingfolder.Margin = new Padding(4, 3, 4, 3);
            textBox_Workingfolder.Name = "textBox_Workingfolder";
            textBox_Workingfolder.Size = new Size(381, 23);
            textBox_Workingfolder.TabIndex = 3;
            textBox_Workingfolder.Text = "C:\\Users\\graham\\Desktop\\Testdata";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 17);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(88, 15);
            label4.TabIndex = 4;
            label4.Text = "Working Folder";
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = SystemColors.ControlDark;
            panel2.Controls.Add(button_process);
            panel2.Controls.Add(textBox_processImage);
            panel2.Controls.Add(label7);
            panel2.Location = new Point(14, 187);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(481, 110);
            panel2.TabIndex = 5;
            // 
            // button_process
            // 
            button_process.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            button_process.Location = new Point(306, 36);
            button_process.Margin = new Padding(4, 3, 4, 3);
            button_process.Name = "button_process";
            button_process.Size = new Size(158, 60);
            button_process.TabIndex = 4;
            button_process.Text = "Process Picture";
            button_process.UseVisualStyleBackColor = true;
            button_process.Click += button_process_Click;
            // 
            // textBox_processImage
            // 
            textBox_processImage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox_processImage.Location = new Point(96, 6);
            textBox_processImage.Margin = new Padding(4, 3, 4, 3);
            textBox_processImage.Name = "textBox_processImage";
            textBox_processImage.Size = new Size(367, 23);
            textBox_processImage.TabIndex = 2;
            textBox_processImage.Text = "pic1.jpg";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(4, 9);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(75, 15);
            label7.TabIndex = 0;
            label7.Text = "Input Picture";
            // 
            // label_status
            // 
            label_status.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label_status.BackColor = SystemColors.Info;
            label_status.Location = new Point(7, 7);
            label_status.Margin = new Padding(4, 0, 4, 0);
            label_status.Name = "label_status";
            label_status.RightToLeft = RightToLeft.Yes;
            label_status.Size = new Size(115, 27);
            label_status.TabIndex = 9;
            label_status.Text = "Idle";
            label_status.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // plotView1
            // 
            plotView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            plotView1.BackColor = SystemColors.ControlDark;
            plotView1.Location = new Point(14, 350);
            plotView1.Margin = new Padding(4, 3, 4, 3);
            plotView1.Name = "plotView1";
            plotView1.PanCursor = Cursors.Hand;
            plotView1.Size = new Size(481, 373);
            plotView1.TabIndex = 6;
            plotView1.Text = "plotView1";
            plotView1.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView1.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView1.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel3.BackColor = SystemColors.ControlDark;
            panel3.Controls.Add(label_status);
            panel3.Controls.Add(progressBar_Training);
            panel3.Location = new Point(14, 303);
            panel3.Margin = new Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(481, 39);
            panel3.TabIndex = 7;
            // 
            // Mainform
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(513, 736);
            Controls.Add(panel3);
            Controls.Add(plotView1);
            Controls.Add(panel2);
            Controls.Add(label4);
            Controls.Add(textBox_Workingfolder);
            Controls.Add(panel1);
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(529, 775);
            Name = "Mainform";
            Text = "Neural Network Image Processor";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.Button button_process;
        private System.Windows.Forms.TextBox textBox_processImage;
        private System.Windows.Forms.Label label7;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Panel panel3;
    }
}

