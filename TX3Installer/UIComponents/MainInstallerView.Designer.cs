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
            this.extractProgress = new MaterialSkin.Controls.MaterialProgressBar();
            this.customInstallPanel = new System.Windows.Forms.Panel();
            this.materialTextButton4 = new MaterialSkin.Controls.MaterialTextButton();
            this.materialTextButton3 = new MaterialSkin.Controls.MaterialTextButton();
            this.materialTextButton2 = new MaterialSkin.Controls.MaterialTextButton();
            this.curInstallPath = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialTextButton5 = new MaterialSkin.Controls.MaterialTextButton();
            this.customInstallPanel.SuspendLayout();
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
            this.materialTextButton1.Click += new System.EventHandler(this.materialTextButton1_Click);
            // 
            // materialCheckBox1
            // 
            this.materialCheckBox1.BackColor = System.Drawing.Color.Transparent;
            this.materialCheckBox1.Checked = true;
            this.materialCheckBox1.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.materialCheckBox1.CheckStateChanged += new System.EventHandler(this.onCheckStateChanged);
            // 
            // cMaterialLabel1
            // 
            this.cMaterialLabel1.BackColor = System.Drawing.Color.Transparent;
            this.cMaterialLabel1.CustomFontName = "fzjz";
            this.cMaterialLabel1.CustomLabelDWNColor = System.Drawing.Color.Lime;
            this.cMaterialLabel1.CustomLabelNMLColor = System.Drawing.Color.Empty;
            this.cMaterialLabel1.CustomLabelOVRColor = System.Drawing.Color.Blue;
            this.cMaterialLabel1.Depth = 0;
            this.cMaterialLabel1.Font = new System.Drawing.Font("方正剪纸_GBK", 15.75F);
            this.cMaterialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cMaterialLabel1.Location = new System.Drawing.Point(785, 546);
            this.cMaterialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.cMaterialLabel1.Name = "cMaterialLabel1";
            this.cMaterialLabel1.Size = new System.Drawing.Size(136, 45);
            this.cMaterialLabel1.TabIndex = 4;
            this.cMaterialLabel1.Text = "自定义安装 >";
            this.cMaterialLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cMaterialLabel1.UseFontType = MaterialSkin.Common.FontType.CustomFont;
            this.cMaterialLabel1.Click += new System.EventHandler(this.cMaterialLabel1_Click);
            // 
            // extractProgress
            // 
            this.extractProgress.Depth = 0;
            this.extractProgress.ForeColor = System.Drawing.Color.Red;
            this.extractProgress.Location = new System.Drawing.Point(0, 595);
            this.extractProgress.MouseState = MaterialSkin.MouseState.HOVER;
            this.extractProgress.Name = "extractProgress";
            this.extractProgress.Size = new System.Drawing.Size(934, 5);
            this.extractProgress.TabIndex = 5;
            this.extractProgress.Visible = false;
            // 
            // customInstallPanel
            // 
            this.customInstallPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.customInstallPanel.Controls.Add(this.materialTextButton4);
            this.customInstallPanel.Controls.Add(this.materialTextButton3);
            this.customInstallPanel.Controls.Add(this.materialTextButton2);
            this.customInstallPanel.Controls.Add(this.curInstallPath);
            this.customInstallPanel.Controls.Add(this.materialLabel1);
            this.customInstallPanel.Location = new System.Drawing.Point(0, 437);
            this.customInstallPanel.Name = "customInstallPanel";
            this.customInstallPanel.Size = new System.Drawing.Size(934, 165);
            this.customInstallPanel.TabIndex = 6;
            this.customInstallPanel.Visible = false;
            // 
            // materialTextButton4
            // 
            this.materialTextButton4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialTextButton4.CustomFontName = null;
            this.materialTextButton4.CustomFontSize = 0F;
            this.materialTextButton4.Depth = 0;
            this.materialTextButton4.Icon = null;
            this.materialTextButton4.Location = new System.Drawing.Point(826, 131);
            this.materialTextButton4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTextButton4.Name = "materialTextButton4";
            this.materialTextButton4.Primary = true;
            this.materialTextButton4.Size = new System.Drawing.Size(75, 23);
            this.materialTextButton4.TabIndex = 4;
            this.materialTextButton4.Text = "返回";
            this.materialTextButton4.UseVisualStyleBackColor = true;
            this.materialTextButton4.Click += new System.EventHandler(this.materialTextButton4_Click);
            // 
            // materialTextButton3
            // 
            this.materialTextButton3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialTextButton3.CustomFontName = null;
            this.materialTextButton3.CustomFontSize = 0F;
            this.materialTextButton3.Depth = 0;
            this.materialTextButton3.Icon = null;
            this.materialTextButton3.Location = new System.Drawing.Point(716, 131);
            this.materialTextButton3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTextButton3.Name = "materialTextButton3";
            this.materialTextButton3.Primary = true;
            this.materialTextButton3.Size = new System.Drawing.Size(75, 23);
            this.materialTextButton3.TabIndex = 3;
            this.materialTextButton3.Text = "立即安装";
            this.materialTextButton3.UseVisualStyleBackColor = true;
            this.materialTextButton3.Click += new System.EventHandler(this.materialTextButton3_Click);
            // 
            // materialTextButton2
            // 
            this.materialTextButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialTextButton2.CustomFontName = null;
            this.materialTextButton2.CustomFontSize = 0F;
            this.materialTextButton2.Depth = 0;
            this.materialTextButton2.Icon = null;
            this.materialTextButton2.Location = new System.Drawing.Point(843, 60);
            this.materialTextButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTextButton2.Name = "materialTextButton2";
            this.materialTextButton2.Primary = true;
            this.materialTextButton2.Size = new System.Drawing.Size(75, 23);
            this.materialTextButton2.TabIndex = 2;
            this.materialTextButton2.Text = "浏览";
            this.materialTextButton2.UseVisualStyleBackColor = true;
            this.materialTextButton2.Click += new System.EventHandler(this.materialTextButton2_Click);
            // 
            // curInstallPath
            // 
            this.curInstallPath.BackColor = System.Drawing.Color.White;
            this.curInstallPath.Depth = 0;
            this.curInstallPath.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.curInstallPath.Hint = "";
            this.curInstallPath.Location = new System.Drawing.Point(39, 62);
            this.curInstallPath.MaxLength = 32767;
            this.curInstallPath.MouseState = MaterialSkin.MouseState.HOVER;
            this.curInstallPath.Name = "curInstallPath";
            this.curInstallPath.PasswordChar = '\0';
            this.curInstallPath.SelectedText = "";
            this.curInstallPath.SelectionLength = 0;
            this.curInstallPath.SelectionStart = 0;
            this.curInstallPath.Size = new System.Drawing.Size(783, 23);
            this.curInstallPath.TabIndex = 1;
            this.curInstallPath.TabStop = false;
            this.curInstallPath.Text = "E:\\DirectorEditor";
            this.curInstallPath.UseSystemPasswordChar = false;
            this.curInstallPath.TextChanged += new System.EventHandler(this.curInstallPath_TextChanged);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("方正剪纸_GBK", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(12, 19);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(72, 17);
            this.materialLabel1.TabIndex = 0;
            this.materialLabel1.Text = "安 装 位 置 ";
            // 
            // materialTextButton5
            // 
            this.materialTextButton5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTextButton5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialTextButton5.CustomFontName = "fzjz";
            this.materialTextButton5.CustomFontSize = 18F;
            this.materialTextButton5.Depth = 0;
            this.materialTextButton5.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.materialTextButton5.Icon = null;
            this.materialTextButton5.Location = new System.Drawing.Point(344, 265);
            this.materialTextButton5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTextButton5.Name = "materialTextButton5";
            this.materialTextButton5.Primary = true;
            this.materialTextButton5.Size = new System.Drawing.Size(245, 75);
            this.materialTextButton5.TabIndex = 7;
            this.materialTextButton5.Text = "立即体验";
            this.materialTextButton5.UseFontType = MaterialSkin.Common.FontType.CustomFont;
            this.materialTextButton5.UseVisualStyleBackColor = true;
            this.materialTextButton5.Visible = false;
            this.materialTextButton5.Click += new System.EventHandler(this.materialTextButton5_Click);
            // 
            // MainInstallerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 600);
            this.Controls.Add(this.materialTextButton5);
            this.Controls.Add(this.customInstallPanel);
            this.Controls.Add(this.cMaterialLabel1);
            this.Controls.Add(this.materialCheckBox1);
            this.Controls.Add(this.materialTextButton1);
            this.Controls.Add(this.extractProgress);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.Name = "MainInstallerView";
            this.Text = "视频编辑器安装器";
            this.customInstallPanel.ResumeLayout(false);
            this.customInstallPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTextButton materialTextButton1;
        private MaterialSkin.Controls.MaterialCheckBoxWithTip materialCheckBox1;
        private MaterialSkin.Controls.CMaterialLabel cMaterialLabel1;
        public MaterialSkin.Controls.MaterialProgressBar extractProgress;
        private System.Windows.Forms.Panel customInstallPanel;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialTextButton materialTextButton4;
        private MaterialSkin.Controls.MaterialTextButton materialTextButton3;
        private MaterialSkin.Controls.MaterialTextButton materialTextButton2;
        private MaterialSkin.Controls.MaterialSingleLineTextField curInstallPath;
        private MaterialSkin.Controls.MaterialTextButton materialTextButton5;
    }
}