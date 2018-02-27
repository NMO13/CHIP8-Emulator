namespace Chip8
{
    partial class MainForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.pictureBoxWithInterpolationMode1 = new DanTup.DaChip8.PictureBoxWithInterpolationMode();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWithInterpolationMode1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(154, 916);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(228, 70);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load ROM";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Location = new System.Drawing.Point(481, 916);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(216, 70);
            this.buttonPause.TabIndex = 3;
            this.buttonPause.Text = "Pause";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // pictureBoxWithInterpolationMode1
            // 
            this.pictureBoxWithInterpolationMode1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.pictureBoxWithInterpolationMode1.Location = new System.Drawing.Point(40, 46);
            this.pictureBoxWithInterpolationMode1.Name = "pictureBoxWithInterpolationMode1";
            this.pictureBoxWithInterpolationMode1.Size = new System.Drawing.Size(1707, 763);
            this.pictureBoxWithInterpolationMode1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxWithInterpolationMode1.TabIndex = 4;
            this.pictureBoxWithInterpolationMode1.TabStop = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1797, 1227);
            this.Controls.Add(this.pictureBoxWithInterpolationMode1);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.button1);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Chip8";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWithInterpolationMode1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonPause;
        private DanTup.DaChip8.PictureBoxWithInterpolationMode pictureBoxWithInterpolationMode1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

