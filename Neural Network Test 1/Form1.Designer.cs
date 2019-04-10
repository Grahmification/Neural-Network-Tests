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
            this.button_Train = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textBox_learningSteps = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar_Error5 = new System.Windows.Forms.ProgressBar();
            this.progressBar_Error4 = new System.Windows.Forms.ProgressBar();
            this.progressBar_Error3 = new System.Windows.Forms.ProgressBar();
            this.progressBar_Error2 = new System.Windows.Forms.ProgressBar();
            this.progressBar_Error1 = new System.Windows.Forms.ProgressBar();
            this.textBox_learningRate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar_Error6 = new System.Windows.Forms.ProgressBar();
            this.progressBar_Error7 = new System.Windows.Forms.ProgressBar();
            this.progressBar_Error8 = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Train
            // 
            this.button_Train.Location = new System.Drawing.Point(16, 68);
            this.button_Train.Name = "button_Train";
            this.button_Train.Size = new System.Drawing.Size(156, 34);
            this.button_Train.TabIndex = 0;
            this.button_Train.Text = "Train Network";
            this.button_Train.UseVisualStyleBackColor = true;
            this.button_Train.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 357);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(448, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // textBox_learningSteps
            // 
            this.textBox_learningSteps.Location = new System.Drawing.Point(97, 16);
            this.textBox_learningSteps.Name = "textBox_learningSteps";
            this.textBox_learningSteps.Size = new System.Drawing.Size(75, 20);
            this.textBox_learningSteps.TabIndex = 2;
            this.textBox_learningSteps.Text = "5000";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.progressBar_Error8);
            this.panel1.Controls.Add(this.progressBar_Error7);
            this.panel1.Controls.Add(this.progressBar_Error6);
            this.panel1.Controls.Add(this.progressBar_Error5);
            this.panel1.Controls.Add(this.progressBar_Error4);
            this.panel1.Controls.Add(this.progressBar_Error3);
            this.panel1.Controls.Add(this.progressBar_Error2);
            this.panel1.Controls.Add(this.progressBar_Error1);
            this.panel1.Controls.Add(this.textBox_learningRate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox_learningSteps);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.button_Train);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(454, 383);
            this.panel1.TabIndex = 3;
            // 
            // progressBar_Error5
            // 
            this.progressBar_Error5.Location = new System.Drawing.Point(3, 224);
            this.progressBar_Error5.Name = "progressBar_Error5";
            this.progressBar_Error5.Size = new System.Drawing.Size(448, 23);
            this.progressBar_Error5.TabIndex = 10;
            // 
            // progressBar_Error4
            // 
            this.progressBar_Error4.Location = new System.Drawing.Point(3, 195);
            this.progressBar_Error4.Name = "progressBar_Error4";
            this.progressBar_Error4.Size = new System.Drawing.Size(448, 23);
            this.progressBar_Error4.TabIndex = 9;
            // 
            // progressBar_Error3
            // 
            this.progressBar_Error3.Location = new System.Drawing.Point(3, 166);
            this.progressBar_Error3.Name = "progressBar_Error3";
            this.progressBar_Error3.Size = new System.Drawing.Size(448, 23);
            this.progressBar_Error3.TabIndex = 8;
            // 
            // progressBar_Error2
            // 
            this.progressBar_Error2.Location = new System.Drawing.Point(3, 137);
            this.progressBar_Error2.Name = "progressBar_Error2";
            this.progressBar_Error2.Size = new System.Drawing.Size(448, 23);
            this.progressBar_Error2.TabIndex = 7;
            // 
            // progressBar_Error1
            // 
            this.progressBar_Error1.Location = new System.Drawing.Point(3, 108);
            this.progressBar_Error1.Name = "progressBar_Error1";
            this.progressBar_Error1.Size = new System.Drawing.Size(448, 23);
            this.progressBar_Error1.TabIndex = 6;
            // 
            // textBox_learningRate
            // 
            this.textBox_learningRate.Location = new System.Drawing.Point(97, 42);
            this.textBox_learningRate.Name = "textBox_learningRate";
            this.textBox_learningRate.Size = new System.Drawing.Size(75, 20);
            this.textBox_learningRate.TabIndex = 5;
            this.textBox_learningRate.Text = "0.0033";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Learning Rate";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Learning Steps";
            // 
            // progressBar_Error6
            // 
            this.progressBar_Error6.Location = new System.Drawing.Point(3, 253);
            this.progressBar_Error6.Name = "progressBar_Error6";
            this.progressBar_Error6.Size = new System.Drawing.Size(448, 23);
            this.progressBar_Error6.TabIndex = 11;
            // 
            // progressBar_Error7
            // 
            this.progressBar_Error7.Location = new System.Drawing.Point(3, 282);
            this.progressBar_Error7.Name = "progressBar_Error7";
            this.progressBar_Error7.Size = new System.Drawing.Size(448, 23);
            this.progressBar_Error7.TabIndex = 12;
            // 
            // progressBar_Error8
            // 
            this.progressBar_Error8.Location = new System.Drawing.Point(3, 311);
            this.progressBar_Error8.Name = "progressBar_Error8";
            this.progressBar_Error8.Size = new System.Drawing.Size(448, 23);
            this.progressBar_Error8.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 407);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

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

