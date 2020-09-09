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
            this.materialCheckBox1 = new MaterialSkin.Controls.MaterialCheckBoxWithTip();
            this.cMaterialLabel1 = new MaterialSkin.Controls.CMaterialLabel();
            this.SuspendLayout();
            // 
            // materialTextButton1
            // 
            this.materialTextButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTextButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialTextButton1.CustomFontName = "fzjz";
            this.materialTextButton1.CustomFontSize = 18F;
            this.materialTextButton1.Depth = 0;
            this.materialTextButton1.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.materialTextButton1.Icon = null;
            this.materialTextButton1.Location = new System.Drawing.Point(343, 270);
            this.materialTextButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTextButton1.Name = "materialTextButton1";
            this.materialTextButton1.Primary = true;
            this.materialTextButton1.Size = new System.Drawing.Size(245, 70);
            this.materialTextButton1.TabIndex = 0;
            this.materialTextButton1.Text = "快速安装";
            this.materialTextButton1.UseFontType = MaterialSkin.Common.FontType.CustomFont;
            this.materialTextButton1.UseVisualStyleBackColor = true;
            // 
            // materialCheckBox1
            // 
            this.materialCheckBox1.BackColor = System.Drawing.Color.Transparent;
            this.materialCheckBox1.CustomFontName = "fzjz";
            this.materialCheckBox1.CustomFontSize = 12F;
            this.materialCheckBox1.Depth = 0;
            this.materialCheckBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.materialCheckBox1.Location = new System.Drawing.Point(10, 561);
            this.materialCheckBox1.Margin = new System.Windows.Forms.Padding(0);
            this.materialCheckBox1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckBox1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckBox1.Name = "materialCheckBox1";
            this.materialCheckBox1.Ripple = true;
            this.materialCheckBox1.Size = new System.Drawing.Size(269, 30);
            this.materialCheckBox1.TabIndex = 2;
            this.materialCheckBox1.Text = "同意视频编辑器的用户许可协议";
            this.materialCheckBox1.UseFontType = MaterialSkin.Common.FontType.CustomFont;
            this.materialCheckBox1.UseVisualStyleBackColor = false;
            // 
            // cMaterialLabel1
            // 
            this.cMaterialLabel1.BackColor = System.Drawing.Color.Transparent;
            this.cMaterialLabel1.CustomFontName = "fzjz";
            this.cMaterialLabel1.CustomLabelDWNColor = System.Drawing.Color.Lime;
            this.cMaterialLabel1.CustomLabelNMLColor = System.Drawing.Color.Empty;
            this.cMaterialLabel1.CustomLabelOVRColor = System.Drawing.Color.Blue;
            this.cMaterialLabel1.Depth = 0;
            this.cMaterialLabel1.Font = new System.Drawing.Font("方正剪纸_GBK", 16F);
            this.cMaterialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cMaterialLabel1.Location = new System.Drawing.Point(785, 546);
            this.cMaterialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.cMaterialLabel1.Name = "cMaterialLabel1";
            this.cMaterialLabel1.Size = new System.Drawing.Size(136, 45);
            this.cMaterialLabel1.TabIndex = 4;
            this.cMaterialLabel1.Text = "自 定 义 安 装 >";
            this.cMaterialLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cMaterialLabel1.UseFontType = MaterialSkin.Common.FontType.CustomFont;
            // 
            // MainInstallerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 600);
            this.Controls.Add(this.cMaterialLabel1);
            this.Controls.Add(this.materialCheckBox1);
            this.Controls.Add(this.materialTextButton1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "MainInstallerView";
            this.Text = "视频编辑器安装器";
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTextButton materialTextButton1;
        private MaterialSkin.Controls.MaterialCheckBoxWithTip materialCheckBox1;
        private MaterialSkin.Controls.CMaterialLabel cMaterialLabel1;
        //private MaterialSkin.Controls.MaterialRaisedButton raisedButton1;
    }
}