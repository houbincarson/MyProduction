namespace ProduceManager
{
    partial class frmLogin
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.picOk = new System.Windows.Forms.Button();
            this.txtUserPwd = new System.Windows.Forms.TextBox();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // picOk
            // 
            this.picOk.FlatAppearance.BorderSize = 0;
            this.picOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.picOk.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.picOk.Location = new System.Drawing.Point(378, 162);
            this.picOk.Name = "picOk";
            this.picOk.Size = new System.Drawing.Size(43, 26);
            this.picOk.TabIndex = 7;
            this.picOk.Text = "登录";
            this.picOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.picOk.UseVisualStyleBackColor = true;
            this.picOk.Click += new System.EventHandler(this.picOk_Click);
            // 
            // txtUserPwd
            // 
            this.txtUserPwd.Location = new System.Drawing.Point(289, 131);
            this.txtUserPwd.Name = "txtUserPwd";
            this.txtUserPwd.PasswordChar = '*';
            this.txtUserPwd.Size = new System.Drawing.Size(134, 21);
            this.txtUserPwd.TabIndex = 6;
            this.txtUserPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserPwd_KeyPress);
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(289, 99);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(134, 21);
            this.txtUserId.TabIndex = 5;
            this.txtUserId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserId_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(254, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "账号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(254, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "密码";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(497, 309);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picOk);
            this.Controls.Add(this.txtUserPwd);
            this.Controls.Add(this.txtUserId);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录界面";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogin_FormClosing);
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.Shown += new System.EventHandler(this.frmLogin_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmLogin_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button picOk;
        private System.Windows.Forms.TextBox txtUserPwd;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

