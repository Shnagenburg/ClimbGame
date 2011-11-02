namespace Climb
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
            this.btLaunch = new System.Windows.Forms.Button();
            this.cmbResolution = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSFX = new System.Windows.Forms.CheckBox();
            this.chkMusic = new System.Windows.Forms.CheckBox();
            this.fullscreenRadial = new System.Windows.Forms.RadioButton();
            this.windowedRadial = new System.Windows.Forms.RadioButton();
            this.letterBoxRadial = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btLaunch
            // 
            this.btLaunch.Location = new System.Drawing.Point(118, 60);
            this.btLaunch.Name = "btLaunch";
            this.btLaunch.Size = new System.Drawing.Size(75, 23);
            this.btLaunch.TabIndex = 0;
            this.btLaunch.Text = "Launch";
            this.btLaunch.UseVisualStyleBackColor = true;
            this.btLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // cmbResolution
            // 
            this.cmbResolution.FormattingEnabled = true;
            this.cmbResolution.Location = new System.Drawing.Point(93, 7);
            this.cmbResolution.Name = "cmbResolution";
            this.cmbResolution.Size = new System.Drawing.Size(121, 21);
            this.cmbResolution.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Resolution:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // chkSFX
            // 
            this.chkSFX.AutoSize = true;
            this.chkSFX.Location = new System.Drawing.Point(15, 34);
            this.chkSFX.Name = "chkSFX";
            this.chkSFX.Size = new System.Drawing.Size(93, 17);
            this.chkSFX.TabIndex = 5;
            this.chkSFX.Text = "Sound Effects";
            this.chkSFX.UseVisualStyleBackColor = true;
            // 
            // chkMusic
            // 
            this.chkMusic.AutoSize = true;
            this.chkMusic.Location = new System.Drawing.Point(130, 34);
            this.chkMusic.Name = "chkMusic";
            this.chkMusic.Size = new System.Drawing.Size(54, 17);
            this.chkMusic.TabIndex = 6;
            this.chkMusic.Text = "Music";
            this.chkMusic.UseVisualStyleBackColor = true;
            // 
            // fullscreenRadial
            // 
            this.fullscreenRadial.AutoSize = true;
            this.fullscreenRadial.Location = new System.Drawing.Point(220, 34);
            this.fullscreenRadial.Name = "fullscreenRadial";
            this.fullscreenRadial.Size = new System.Drawing.Size(78, 17);
            this.fullscreenRadial.TabIndex = 8;
            this.fullscreenRadial.Text = "Full Screen";
            this.fullscreenRadial.UseVisualStyleBackColor = true;
            this.fullscreenRadial.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // windowedRadial
            // 
            this.windowedRadial.AutoSize = true;
            this.windowedRadial.Checked = true;
            this.windowedRadial.Location = new System.Drawing.Point(220, 7);
            this.windowedRadial.Name = "windowedRadial";
            this.windowedRadial.Size = new System.Drawing.Size(64, 17);
            this.windowedRadial.TabIndex = 9;
            this.windowedRadial.TabStop = true;
            this.windowedRadial.Text = "Window";
            this.windowedRadial.UseVisualStyleBackColor = true;
            // 
            // letterBoxRadial
            // 
            this.letterBoxRadial.AutoSize = true;
            this.letterBoxRadial.Location = new System.Drawing.Point(220, 63);
            this.letterBoxRadial.Name = "letterBoxRadial";
            this.letterBoxRadial.Size = new System.Drawing.Size(73, 17);
            this.letterBoxRadial.TabIndex = 10;
            this.letterBoxRadial.Text = "Letter Box";
            this.letterBoxRadial.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 95);
            this.Controls.Add(this.letterBoxRadial);
            this.Controls.Add(this.windowedRadial);
            this.Controls.Add(this.fullscreenRadial);
            this.Controls.Add(this.chkMusic);
            this.Controls.Add(this.chkSFX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbResolution);
            this.Controls.Add(this.btLaunch);
            this.Name = "Form1";
            this.Text = "A Fantastical Adventure Awaits!!!";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btLaunch;
        private System.Windows.Forms.ComboBox cmbResolution;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkSFX;
        private System.Windows.Forms.CheckBox chkMusic;
        private System.Windows.Forms.RadioButton fullscreenRadial;
        private System.Windows.Forms.RadioButton windowedRadial;
        private System.Windows.Forms.RadioButton letterBoxRadial;
    }
}