namespace DirectorEditor.UIComponents
{
    partial class DataPart2View
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbVipLevel = new System.Windows.Forms.Label();
            this.lbUserID = new System.Windows.Forms.Label();
            this.lbUserPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "VIP等级";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "用户ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "用户密码";
            // 
            // lbVipLevel
            // 
            this.lbVipLevel.AutoSize = true;
            this.lbVipLevel.Location = new System.Drawing.Point(212, 65);
            this.lbVipLevel.Name = "lbVipLevel";
            this.lbVipLevel.Size = new System.Drawing.Size(11, 12);
            this.lbVipLevel.TabIndex = 3;
            this.lbVipLevel.Text = "0";
            // 
            // lbUserID
            // 
            this.lbUserID.AutoSize = true;
            this.lbUserID.Location = new System.Drawing.Point(212, 105);
            this.lbUserID.Name = "lbUserID";
            this.lbUserID.Size = new System.Drawing.Size(11, 12);
            this.lbUserID.TabIndex = 4;
            this.lbUserID.Text = "0";
            // 
            // lbUserPassword
            // 
            this.lbUserPassword.AutoSize = true;
            this.lbUserPassword.Location = new System.Drawing.Point(212, 152);
            this.lbUserPassword.Name = "lbUserPassword";
            this.lbUserPassword.Size = new System.Drawing.Size(11, 12);
            this.lbUserPassword.TabIndex = 5;
            this.lbUserPassword.Text = "0";
            // 
            // DataPart2View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.lbUserPassword);
            this.Controls.Add(this.lbUserID);
            this.Controls.Add(this.lbVipLevel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DataPart2View";
            this.Text = "DataPart2View";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label lbVipLevel;
        public System.Windows.Forms.Label lbUserID;
        public System.Windows.Forms.Label lbUserPassword;
    }
}