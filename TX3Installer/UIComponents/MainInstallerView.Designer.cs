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
            this.btnNowInstall = new MaterialSkin.Controls.MaterialTextButton();
            this.checkboxAgreement = new MaterialSkin.Controls.MaterialCheckBoxWithTip();
            this.lbCustomInstallPath = new MaterialSkin.Controls.CMaterialLabel();
            this.extractProgress = new MaterialSkin.Controls.MaterialProgressBar();
            this.customInstallPanel = new System.Windows.Forms.Panel();
            this.btnReturnMainView = new MaterialSkin.Controls.MaterialTextButton();
            this.btnCustomInstall = new MaterialSkin.Controls.MaterialTextButton();
            this.btnOpenWindowExploer = new MaterialSkin.Controls.MaterialTextButton();
            this.curInstallPath = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.btnExperienceNow = new MaterialSkin.Controls.MaterialTextButton();
            this.customInstallPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialTextButton1
            // 
            this.btnNowInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNowInstall.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNowInstall.CustomFontName = "fzjz";
            this.btnNowInstall.CustomFontSize = 18F;
            this.btnNowInstall.Depth = 0;
            this.btnNowInstall.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNowInstall.Icon = null;
            this.btnNowInstall.Location = new System.Drawing.Point(343, 270);
            this.btnNowInstall.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnNowInstall.Name = "materialTextButton1";
            this.btnNowInstall.Primary = true;
            this.btnNowInstall.Size = new System.Drawing.Size(245, 70);
            this.btnNowInstall.TabIndex = 0;
            this.btnNowInstall.Text = "快速安装";
            this.btnNowInstall.UseFontType = MaterialSkin.Common.FontType.CustomFont;
            this.btnNowInstall.UseVisualStyleBackColor = true;
            this.btnNowInstall.Click += new System.EventHandler(this.materialTextButton1_Click);
            // 
            // checkboxAgreement
            // 
            this.checkboxAgreement.BackColor = System.Drawing.Color.Transparent;
            this.checkboxAgreement.Checked = true;
            this.checkboxAgreement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxAgreement.CustomFontName = "fzjz";
            this.checkboxAgreement.CustomFontSize = 12F;
            this.checkboxAgreement.Depth = 0;
            this.checkboxAgreement.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkboxAgreement.Location = new System.Drawing.Point(10, 561);
            this.checkboxAgreement.Margin = new System.Windows.Forms.Padding(0);
            this.checkboxAgreement.MouseLocation = new System.Drawing.Point(-1, -1);
            this.checkboxAgreement.MouseState = MaterialSkin.MouseState.HOVER;
            this.checkboxAgreement.Name = "checkboxAgreement";
            this.checkboxAgreement.Ripple = true;
            this.checkboxAgreement.Size = new System.Drawing.Size(269, 30);
            this.checkboxAgreement.TabIndex = 2;
            this.checkboxAgreement.Text = "同意视频编辑器的用户许可协议";
            this.checkboxAgreement.UseFontType = MaterialSkin.Common.FontType.CustomFont;
            this.checkboxAgreement.UseVisualStyleBackColor = false;
            this.checkboxAgreement.CheckStateChanged += new System.EventHandler(this.onCheckStateChanged);
            // 
            // lbCustomInstallPath
            // 
            this.lbCustomInstallPath.BackColor = System.Drawing.Color.Transparent;
            this.lbCustomInstallPath.CustomFontName = "fzjz";
            this.lbCustomInstallPath.CustomLabelDWNColor = System.Drawing.Color.Lime;
            this.lbCustomInstallPath.CustomLabelNMLColor = System.Drawing.Color.Empty;
            this.lbCustomInstallPath.CustomLabelOVRColor = System.Drawing.Color.Blue;
            this.lbCustomInstallPath.Depth = 0;
            this.lbCustomInstallPath.Font = new System.Drawing.Font("方正剪纸_GBK", 15.75F);
            this.lbCustomInstallPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbCustomInstallPath.Location = new System.Drawing.Point(785, 546);
            this.lbCustomInstallPath.MouseState = MaterialSkin.MouseState.HOVER;
            this.lbCustomInstallPath.Name = "lbCustomInstallPath";
            this.lbCustomInstallPath.Size = new System.Drawing.Size(136, 45);
            this.lbCustomInstallPath.TabIndex = 4;
            this.lbCustomInstallPath.Text = "自 定 义 安 装  >";
            this.lbCustomInstallPath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbCustomInstallPath.UseFontType = MaterialSkin.Common.FontType.CustomFont;
            this.lbCustomInstallPath.Click += new System.EventHandler(this.cMaterialLabel1_Click);
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
            this.customInstallPanel.Controls.Add(this.btnReturnMainView);
            this.customInstallPanel.Controls.Add(this.btnCustomInstall);
            this.customInstallPanel.Controls.Add(this.btnOpenWindowExploer);
            this.customInstallPanel.Controls.Add(this.curInstallPath);
            this.customInstallPanel.Controls.Add(this.materialLabel1);
            this.customInstallPanel.Location = new System.Drawing.Point(0, 437);
            this.customInstallPanel.Name = "customInstallPanel";
            this.customInstallPanel.Size = new System.Drawing.Size(934, 165);
            this.customInstallPanel.TabIndex = 6;
            // 
            // btnReturnMainView
            // 
            this.btnReturnMainView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnReturnMainView.CustomFontName = null;
            this.btnReturnMainView.CustomFontSize = 0F;
            this.btnReturnMainView.Depth = 0;
            this.btnReturnMainView.Icon = null;
            this.btnReturnMainView.Location = new System.Drawing.Point(826, 131);
            this.btnReturnMainView.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnReturnMainView.Name = "btnReturnMainView";
            this.btnReturnMainView.Primary = true;
            this.btnReturnMainView.Size = new System.Drawing.Size(75, 23);
            this.btnReturnMainView.TabIndex = 4;
            this.btnReturnMainView.Text = "返回";
            this.btnReturnMainView.UseVisualStyleBackColor = true;
            this.btnReturnMainView.Click += new System.EventHandler(this.materialTextButton4_Click);
            // 
            // btnCustomInstall
            // 
            this.btnCustomInstall.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCustomInstall.CustomFontName = null;
            this.btnCustomInstall.CustomFontSize = 0F;
            this.btnCustomInstall.Depth = 0;
            this.btnCustomInstall.Icon = null;
            this.btnCustomInstall.Location = new System.Drawing.Point(716, 131);
            this.btnCustomInstall.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCustomInstall.Name = "btnCustomInstall";
            this.btnCustomInstall.Primary = true;
            this.btnCustomInstall.Size = new System.Drawing.Size(75, 23);
            this.btnCustomInstall.TabIndex = 3;
            this.btnCustomInstall.Text = "立即安装";
            this.btnCustomInstall.UseVisualStyleBackColor = true;
            this.btnCustomInstall.Click += new System.EventHandler(this.materialTextButton3_Click);
            // 
            // btnOpenWindowExploer
            // 
            this.btnOpenWindowExploer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOpenWindowExploer.CustomFontName = null;
            this.btnOpenWindowExploer.CustomFontSize = 0F;
            this.btnOpenWindowExploer.Depth = 0;
            this.btnOpenWindowExploer.Icon = null;
            this.btnOpenWindowExploer.Location = new System.Drawing.Point(843, 60);
            this.btnOpenWindowExploer.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnOpenWindowExploer.Name = "btnOpenWindowExploer";
            this.btnOpenWindowExploer.Primary = true;
            this.btnOpenWindowExploer.Size = new System.Drawing.Size(75, 23);
            this.btnOpenWindowExploer.TabIndex = 2;
            this.btnOpenWindowExploer.Text = "浏览";
            this.btnOpenWindowExploer.UseVisualStyleBackColor = true;
            this.btnOpenWindowExploer.Click += new System.EventHandler(this.materialTextButton2_Click);
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
            // btnExperienceNow
            // 
            this.btnExperienceNow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExperienceNow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExperienceNow.CustomFontName = "fzjz";
            this.btnExperienceNow.CustomFontSize = 18F;
            this.btnExperienceNow.Depth = 0;
            this.btnExperienceNow.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExperienceNow.Icon = null;
            this.btnExperienceNow.Location = new System.Drawing.Point(344, 265);
            this.btnExperienceNow.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnExperienceNow.Name = "btnExperienceNow";
            this.btnExperienceNow.Primary = true;
            this.btnExperienceNow.Size = new System.Drawing.Size(245, 75);
            this.btnExperienceNow.TabIndex = 7;
            this.btnExperienceNow.Text = "立即体验";
            this.btnExperienceNow.UseFontType = MaterialSkin.Common.FontType.CustomFont;
            this.btnExperienceNow.UseVisualStyleBackColor = true;
            this.btnExperienceNow.Visible = false;
            this.btnExperienceNow.Click += new System.EventHandler(this.materialTextButton5_Click);
            // 
            // MainInstallerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 600);
            this.Controls.Add(this.btnExperienceNow);
            this.Controls.Add(this.customInstallPanel);
            this.Controls.Add(this.lbCustomInstallPath);
            this.Controls.Add(this.checkboxAgreement);
            this.Controls.Add(this.btnNowInstall);
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

        private MaterialSkin.Controls.MaterialTextButton btnNowInstall;
        private MaterialSkin.Controls.MaterialCheckBoxWithTip checkboxAgreement;
        private MaterialSkin.Controls.CMaterialLabel lbCustomInstallPath;
        public MaterialSkin.Controls.MaterialProgressBar extractProgress;
        private System.Windows.Forms.Panel customInstallPanel;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialTextButton btnReturnMainView;
        private MaterialSkin.Controls.MaterialTextButton btnCustomInstall;
        private MaterialSkin.Controls.MaterialTextButton btnOpenWindowExploer;
        private MaterialSkin.Controls.MaterialSingleLineTextField curInstallPath;
        private MaterialSkin.Controls.MaterialTextButton btnExperienceNow;
    }
}