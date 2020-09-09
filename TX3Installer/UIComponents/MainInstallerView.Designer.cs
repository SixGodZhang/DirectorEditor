namespace TX3Installer.UIComponents
{
    partial class MainInstallerView
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
            this.materialTextButton1 = new MaterialSkin.Controls.MaterialTextButton();
            this.SuspendLayout();
            // 
            // materialTextButton1
            // 
            this.materialTextButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTextButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialTextButton1.Depth = 0;
            this.materialTextButton1.Icon = null;
            this.materialTextButton1.Location = new System.Drawing.Point(335, 235);
            this.materialTextButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTextButton1.Name = "materialTextButton1";
            this.materialTextButton1.Primary = true;
            this.materialTextButton1.Size = new System.Drawing.Size(210, 70);
            this.materialTextButton1.TabIndex = 0;
            this.materialTextButton1.Text = "快速安装";
            this.materialTextButton1.UseVisualStyleBackColor = true;
            // 
            // MainInstallerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 553);
            this.Controls.Add(this.materialTextButton1);
            this.Name = "MainInstallerView";
            this.Text = "视频编辑器安装器";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTextButton materialTextButton1;
        //private MaterialSkin.Controls.MaterialRaisedButton raisedButton1;
    }
}