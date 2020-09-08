namespace DirectorEditor
{
    partial class MainView
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.topMenu = new System.Windows.Forms.MenuStrip();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.attributeBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.thirdPartyHZHDialogBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.thirdPartyMaterialSKinDemoBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.dataPart1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataPart2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testChangeHelpInfoBtn = new System.Windows.Forms.Button();
            this.viewLogicNNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewLogicNNCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // topMenu
            // 
            this.topMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpMenu,
            this.attributeBtn,
            this.thirdPartyHZHDialogBtn,
            this.thirdPartyMaterialSKinDemoBtn,
            this.dataPart1ToolStripMenuItem,
            this.dataPart2ToolStripMenuItem,
            this.viewLogicNNToolStripMenuItem,
            this.viewLogicNNCloseToolStripMenuItem});
            this.topMenu.Location = new System.Drawing.Point(0, 0);
            this.topMenu.Name = "topMenu";
            this.topMenu.Size = new System.Drawing.Size(800, 25);
            this.topMenu.TabIndex = 0;
            this.topMenu.Text = "menuStrip1";
            // 
            // helpMenu
            // 
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 21);
            this.helpMenu.Text = "帮助";
            this.helpMenu.Click += new System.EventHandler(this.helpMenu_Click);
            // 
            // attributeBtn
            // 
            this.attributeBtn.Name = "attributeBtn";
            this.attributeBtn.Size = new System.Drawing.Size(80, 21);
            this.attributeBtn.Text = "装饰器寻址";
            this.attributeBtn.Click += new System.EventHandler(this.attributeBtn_Click);
            // 
            // thirdPartyHZHDialogBtn
            // 
            this.thirdPartyHZHDialogBtn.Name = "thirdPartyHZHDialogBtn";
            this.thirdPartyHZHDialogBtn.Size = new System.Drawing.Size(69, 21);
            this.thirdPartyHZHDialogBtn.Text = "HZH控件";
            this.thirdPartyHZHDialogBtn.Click += new System.EventHandler(this.thirdPartyDialogBtn_Click);
            // 
            // thirdPartyMaterialSKinDemoBtn
            // 
            this.thirdPartyMaterialSKinDemoBtn.Name = "thirdPartyMaterialSKinDemoBtn";
            this.thirdPartyMaterialSKinDemoBtn.Size = new System.Drawing.Size(116, 21);
            this.thirdPartyMaterialSKinDemoBtn.Text = "MaterialSkin控件";
            this.thirdPartyMaterialSKinDemoBtn.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // dataPart1ToolStripMenuItem
            // 
            this.dataPart1ToolStripMenuItem.Name = "dataPart1ToolStripMenuItem";
            this.dataPart1ToolStripMenuItem.Size = new System.Drawing.Size(77, 21);
            this.dataPart1ToolStripMenuItem.Text = "DataPart1";
            this.dataPart1ToolStripMenuItem.Click += new System.EventHandler(this.dataPart1ToolStripMenuItem_Click);
            // 
            // dataPart2ToolStripMenuItem
            // 
            this.dataPart2ToolStripMenuItem.Name = "dataPart2ToolStripMenuItem";
            this.dataPart2ToolStripMenuItem.Size = new System.Drawing.Size(77, 21);
            this.dataPart2ToolStripMenuItem.Text = "DataPart2";
            this.dataPart2ToolStripMenuItem.Click += new System.EventHandler(this.dataPart2ToolStripMenuItem_Click);
            // 
            // testChangeHelpInfoBtn
            // 
            this.testChangeHelpInfoBtn.Location = new System.Drawing.Point(12, 44);
            this.testChangeHelpInfoBtn.Name = "testChangeHelpInfoBtn";
            this.testChangeHelpInfoBtn.Size = new System.Drawing.Size(145, 51);
            this.testChangeHelpInfoBtn.TabIndex = 1;
            this.testChangeHelpInfoBtn.Text = "点击可以改变帮助信息";
            this.testChangeHelpInfoBtn.UseVisualStyleBackColor = true;
            this.testChangeHelpInfoBtn.Click += new System.EventHandler(this.testChangeHelpInfoBtn_Click);
            // 
            // viewLogicNNToolStripMenuItem
            // 
            this.viewLogicNNToolStripMenuItem.Name = "viewLogicNNToolStripMenuItem";
            this.viewLogicNNToolStripMenuItem.Size = new System.Drawing.Size(132, 21);
            this.viewLogicNNToolStripMenuItem.Text = "ViewLogicNN open";
            this.viewLogicNNToolStripMenuItem.Click += new System.EventHandler(this.viewLogicNNToolStripMenuItem_Click);
            // 
            // viewLogicNNCloseToolStripMenuItem
            // 
            this.viewLogicNNCloseToolStripMenuItem.Name = "viewLogicNNCloseToolStripMenuItem";
            this.viewLogicNNCloseToolStripMenuItem.Size = new System.Drawing.Size(132, 21);
            this.viewLogicNNCloseToolStripMenuItem.Text = "ViewLogicNN close";
            this.viewLogicNNCloseToolStripMenuItem.Click += new System.EventHandler(this.viewLogicNNCloseToolStripMenuItem_Click);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.testChangeHelpInfoBtn);
            this.Controls.Add(this.topMenu);
            this.MainMenuStrip = this.topMenu;
            this.Name = "MainView";
            this.Text = "Director演示窗口";
            this.topMenu.ResumeLayout(false);
            this.topMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip topMenu;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.Button testChangeHelpInfoBtn;
        private System.Windows.Forms.ToolStripMenuItem attributeBtn;
        private System.Windows.Forms.ToolStripMenuItem thirdPartyHZHDialogBtn;
        private System.Windows.Forms.ToolStripMenuItem thirdPartyMaterialSKinDemoBtn;
        private System.Windows.Forms.ToolStripMenuItem dataPart1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataPart2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewLogicNNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewLogicNNCloseToolStripMenuItem;
    }
}

