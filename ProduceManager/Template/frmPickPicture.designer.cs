namespace ProduceManager
{
    partial class frmPickPicture
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
            this.components = new System.ComponentModel.Container();
            this.OpenSignal = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.PickPicture = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ComCamera = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSetPath = new System.Windows.Forms.Button();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenSignal
            // 
            this.OpenSignal.Location = new System.Drawing.Point(122, 479);
            this.OpenSignal.Name = "OpenSignal";
            this.OpenSignal.Size = new System.Drawing.Size(75, 23);
            this.OpenSignal.TabIndex = 12;
            this.OpenSignal.Text = "开启摄像头";
            this.OpenSignal.UseVisualStyleBackColor = true;
            this.OpenSignal.Click += new System.EventHandler(this.OpenSignal_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(370, 479);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 7;
            this.btnQuit.Text = "退出拍照";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // PickPicture
            // 
            this.PickPicture.Location = new System.Drawing.Point(250, 479);
            this.PickPicture.Name = "PickPicture";
            this.PickPicture.Size = new System.Drawing.Size(75, 23);
            this.PickPicture.TabIndex = 8;
            this.PickPicture.Text = "拍摄照片";
            this.PickPicture.UseVisualStyleBackColor = true;
            this.PickPicture.Click += new System.EventHandler(this.PickPicture_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(108, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "选择摄像头：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(145, 458);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 11;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(76, 121);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(417, 338);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // ComCamera
            // 
            this.ComCamera.FormattingEnabled = true;
            this.ComCamera.Location = new System.Drawing.Point(191, 12);
            this.ComCamera.Name = "ComCamera";
            this.ComCamera.Size = new System.Drawing.Size(199, 20);
            this.ComCamera.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(72, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "选择图片保存路径：";
            // 
            // btnSetPath
            // 
            this.btnSetPath.Location = new System.Drawing.Point(398, 44);
            this.btnSetPath.Name = "btnSetPath";
            this.btnSetPath.Size = new System.Drawing.Size(75, 23);
            this.btnSetPath.TabIndex = 15;
            this.btnSetPath.Text = "选择";
            this.btnSetPath.UseVisualStyleBackColor = true;
            this.btnSetPath.Click += new System.EventHandler(this.btnSetPath_Click);
            // 
            // txtImagePath
            // 
            this.txtImagePath.Location = new System.Drawing.Point(192, 44);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.ReadOnly = true;
            this.txtImagePath.Size = new System.Drawing.Size(198, 21);
            this.txtImagePath.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "设置自动拍照间隔时间：";
            this.label4.Visible = false;
            // 
            // txtMin
            // 
            this.txtMin.Location = new System.Drawing.Point(205, 83);
            this.txtMin.Name = "txtMin";
            this.txtMin.Size = new System.Drawing.Size(111, 21);
            this.txtMin.TabIndex = 18;
            this.txtMin.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(322, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "秒";
            this.label5.Visible = false;
            // 
            // frmPickPicture
            // 
            this.ClientSize = new System.Drawing.Size(569, 512);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtImagePath);
            this.Controls.Add(this.btnSetPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OpenSignal);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.PickPicture);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ComCamera);
            this.Name = "frmPickPicture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmPickPicture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
         
        private System.Windows.Forms.Button OpenSignal;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button PickPicture;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox ComCamera;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSetPath;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMin;
        private System.Windows.Forms.Label label5;
    }
}