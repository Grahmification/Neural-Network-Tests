namespace Neural_Network_Test_1
{
    partial class Form1
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
            button_Train = new Button();
            progressBar1 = new ProgressBar();
            textBox_learningSteps = new TextBox();
            panel1 = new Panel();
            progressBar_Error8 = new ProgressBar();
            progressBar_Error7 = new ProgressBar();
            progressBar_Error6 = new ProgressBar();
            progressBar_Error5 = new ProgressBar();
            progressBar_Error4 = new ProgressBar();
            progressBar_Error3 = new ProgressBar();
            progressBar_Error2 = new ProgressBar();
            progressBar_Error1 = new ProgressBar();
            textBox_learningRate = new TextBox();
            label2 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // button_Train
            // 
            button_Train.Location = new Point(19, 78);
            button_Train.Margin = new Padding(4, 3, 4, 3);
            button_Train.Name = "button_Train";
            button_Train.Size = new Size(182, 39);
            button_Train.TabIndex = 0;
            button_Train.Text = "Train Network";
            button_Train.UseVisualStyleBackColor = true;
            button_Train.Click += button1_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(4, 412);
            progressBar1.Margin = new Padding(4, 3, 4, 3);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(523, 27);
            progressBar1.TabIndex = 1;
            // 
            // textBox_learningSteps
            // 
            textBox_learningSteps.Location = new Point(113, 18);
            textBox_learningSteps.Margin = new Padding(4, 3, 4, 3);
            textBox_learningSteps.Name = "textBox_learningSteps";
            textBox_learningSteps.Size = new Size(87, 23);
            textBox_learningSteps.TabIndex = 2;
            textBox_learningSteps.Text = "5000";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlDark;
            panel1.Controls.Add(progressBar_Error8);
            panel1.Controls.Add(progressBar_Error7);
            panel1.Controls.Add(progressBar_Error6);
            panel1.Controls.Add(progressBar_Error5);
            panel1.Controls.Add(progressBar_Error4);
            panel1.Controls.Add(progressBar_Error3);
            panel1.Controls.Add(progressBar_Error2);
            panel1.Controls.Add(progressBar_Error1);
            panel1.Controls.Add(textBox_learningRate);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(textBox_learningSteps);
            panel1.Controls.Add(progressBar1);
            panel1.Controls.Add(button_Train);
            panel1.Location = new Point(14, 14);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(530, 442);
            panel1.TabIndex = 3;
            // 
            // progressBar_Error8
            // 
            progressBar_Error8.Location = new Point(4, 359);
            progressBar_Error8.Margin = new Padding(4, 3, 4, 3);
            progressBar_Error8.Name = "progressBar_Error8";
            progressBar_Error8.Size = new Size(523, 27);
            progressBar_Error8.TabIndex = 13;
            // 
            // progressBar_Error7
            // 
            progressBar_Error7.Location = new Point(4, 325);
            progressBar_Error7.Margin = new Padding(4, 3, 4, 3);
            progressBar_Error7.Name = "progressBar_Error7";
            progressBar_Error7.Size = new Size(523, 27);
            progressBar_Error7.TabIndex = 12;
            // 
            // progressBar_Error6
            // 
            progressBar_Error6.Location = new Point(4, 292);
            progressBar_Error6.Margin = new Padding(4, 3, 4, 3);
            progressBar_Error6.Name = "progressBar_Error6";
            progressBar_Error6.Size = new Size(523, 27);
            progressBar_Error6.TabIndex = 11;
            // 
            // progressBar_Error5
            // 
            progressBar_Error5.Location = new Point(4, 258);
            progressBar_Error5.Margin = new Padding(4, 3, 4, 3);
            progressBar_Error5.Name = "progressBar_Error5";
            progressBar_Error5.Size = new Size(523, 27);
            progressBar_Error5.TabIndex = 10;
            // 
            // progressBar_Error4
            // 
            progressBar_Error4.Location = new Point(4, 225);
            progressBar_Error4.Margin = new Padding(4, 3, 4, 3);
            progressBar_Error4.Name = "progressBar_Error4";
            progressBar_Error4.Size = new Size(523, 27);
            progressBar_Error4.TabIndex = 9;
            // 
            // progressBar_Error3
            // 
            progressBar_Error3.Location = new Point(4, 192);
            progressBar_Error3.Margin = new Padding(4, 3, 4, 3);
            progressBar_Error3.Name = "progressBar_Error3";
            progressBar_Error3.Size = new Size(523, 27);
            progressBar_Error3.TabIndex = 8;
            // 
            // progressBar_Error2
            // 
            progressBar_Error2.Location = new Point(4, 158);
            progressBar_Error2.Margin = new Padding(4, 3, 4, 3);
            progressBar_Error2.Name = "progressBar_Error2";
            progressBar_Error2.Size = new Size(523, 27);
            progressBar_Error2.TabIndex = 7;
            // 
            // progressBar_Error1
            // 
            progressBar_Error1.Location = new Point(4, 125);
            progressBar_Error1.Margin = new Padding(4, 3, 4, 3);
            progressBar_Error1.Name = "progressBar_Error1";
            progressBar_Error1.Size = new Size(523, 27);
            progressBar_Error1.TabIndex = 6;
            // 
            // textBox_learningRate
            // 
            textBox_learningRate.Location = new Point(113, 48);
            textBox_learningRate.Margin = new Padding(4, 3, 4, 3);
            textBox_learningRate.Name = "textBox_learningRate";
            textBox_learningRate.Size = new Size(87, 23);
            textBox_learningRate.TabIndex = 5;
            textBox_learningRate.Text = "0.0033";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 52);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 4;
            label2.Text = "Learning Rate";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 22);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(84, 15);
            label1.TabIndex = 3;
            label1.Text = "Learning Steps";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(771, 470);
            Controls.Add(panel1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button button_Train;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBox_learningSteps;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_learningRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar_Error2;
        private System.Windows.Forms.ProgressBar progressBar_Error1;
        private System.Windows.Forms.ProgressBar progressBar_Error5;
        private System.Windows.Forms.ProgressBar progressBar_Error4;
        private System.Windows.Forms.ProgressBar progressBar_Error3;
        private System.Windows.Forms.ProgressBar progressBar_Error8;
        private System.Windows.Forms.ProgressBar progressBar_Error7;
        private System.Windows.Forms.ProgressBar progressBar_Error6;
    }
}

