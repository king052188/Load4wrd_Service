namespace load4wrd_service
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblServiceLabel = new System.Windows.Forms.Label();
            this.lblServiceStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnSettings = new System.Windows.Forms.PictureBox();
            this.pbServiceStatus = new System.Windows.Forms.PictureBox();
            this.btnPower = new System.Windows.Forms.PictureBox();
            this.lblLabelStatus = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbServiceStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblServiceLabel
            // 
            this.lblServiceLabel.AutoSize = true;
            this.lblServiceLabel.Location = new System.Drawing.Point(386, 12);
            this.lblServiceLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblServiceLabel.Name = "lblServiceLabel";
            this.lblServiceLabel.Size = new System.Drawing.Size(81, 15);
            this.lblServiceLabel.TabIndex = 1;
            this.lblServiceLabel.Text = "Start Service";
            // 
            // lblServiceStatus
            // 
            this.lblServiceStatus.Location = new System.Drawing.Point(365, 206);
            this.lblServiceStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblServiceStatus.Name = "lblServiceStatus";
            this.lblServiceStatus.Size = new System.Drawing.Size(108, 15);
            this.lblServiceStatus.TabIndex = 1;
            this.lblServiceStatus.Text = "Stopped";
            this.lblServiceStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(204, 206);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Settings";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(45, 206);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Logs";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::load4wrd_service.Properties.Resources.line_gray;
            this.pictureBox3.Location = new System.Drawing.Point(70, 248);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(380, 2);
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::load4wrd_service.Properties.Resources.layers;
            this.pictureBox2.Location = new System.Drawing.Point(67, 126);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(64, 64);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // btnSettings
            // 
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.Image = global::load4wrd_service.Properties.Resources.settings_1_;
            this.btnSettings.Location = new System.Drawing.Point(224, 126);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(64, 64);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.TabStop = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // pbServiceStatus
            // 
            this.pbServiceStatus.Image = ((System.Drawing.Image)(resources.GetObject("pbServiceStatus.Image")));
            this.pbServiceStatus.Location = new System.Drawing.Point(387, 126);
            this.pbServiceStatus.Name = "pbServiceStatus";
            this.pbServiceStatus.Size = new System.Drawing.Size(64, 64);
            this.pbServiceStatus.TabIndex = 2;
            this.pbServiceStatus.TabStop = false;
            // 
            // btnPower
            // 
            this.btnPower.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPower.Image = global::load4wrd_service.Properties.Resources.switch_off;
            this.btnPower.Location = new System.Drawing.Point(471, 5);
            this.btnPower.Margin = new System.Windows.Forms.Padding(2);
            this.btnPower.Name = "btnPower";
            this.btnPower.Size = new System.Drawing.Size(33, 35);
            this.btnPower.TabIndex = 0;
            this.btnPower.TabStop = false;
            this.btnPower.Click += new System.EventHandler(this.btnPower_Click);
            // 
            // lblLabelStatus
            // 
            this.lblLabelStatus.Location = new System.Drawing.Point(67, 259);
            this.lblLabelStatus.Name = "lblLabelStatus";
            this.lblLabelStatus.Size = new System.Drawing.Size(383, 40);
            this.lblLabelStatus.TabIndex = 6;
            this.lblLabelStatus.Text = "label3";
            this.lblLabelStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(24, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(141, 54);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 322);
            this.Controls.Add(this.lblLabelStatus);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.pbServiceStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblServiceStatus);
            this.Controls.Add(this.lblServiceLabel);
            this.Controls.Add(this.btnPower);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MAIN";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbServiceStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox btnPower;
        private System.Windows.Forms.Label lblServiceLabel;
        private System.Windows.Forms.PictureBox pbServiceStatus;
        private System.Windows.Forms.Label lblServiceStatus;
        private System.Windows.Forms.PictureBox btnSettings;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblLabelStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}