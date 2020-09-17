//using IWshRuntimeLibrary;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TX3Installer.Common;
using CSharpLib;
using Shortcut = CSharpLib.Shortcut;

namespace TX3Installer.UIComponents
{
    public partial class MainInstallerView : MaterialForm
    {
        /// <summary>
        /// 当前安装路径
        /// </summary>
        private string m_curInstallExePath = InstallerConfig.Config.DefaultInstallPath;

        public MainInstallerView()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;//设置该属性 为false
            this.curInstallPath.Text = m_curInstallExePath;
            this.Sizable = false;
        }

        /// <summary>
        /// 隐藏所有控件
        /// </summary>
        private void hideAllControls()
        {
            this.materialTextButton1.Visible = false;// 快速安装
            this.cMaterialLabel1.Visible = false;// 自定义安装
            this.materialCheckBox1.Visible = false;// 显示协议
        }

        /// <summary>
        /// 是否同意相关协议
        /// </summary>
        private void onCheckStateChanged(object sender, EventArgs e)
        {
            var isChecked = this.materialCheckBox1.CheckState == CheckState.Checked;
            this.materialTextButton1.Enabled = isChecked;
            this.cMaterialLabel1.Enabled = isChecked;
        }

        /// <summary>
        /// 立即安装
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void materialTextButton1_Click(object sender, EventArgs e)
        {
            if (this.extractProgress.Value > 0 && this.extractProgress.Value < 100)
            {
                MessageBox.Show("正在安装中。。。");
                return;
            }

            this.extractProgress.Visible = true;// 显示进度条
            string sPath = Path.Combine(Environment.CurrentDirectory, InstallerConfig.Config.Install7ZFile);
            string tPath = m_curInstallExePath;
            if (!Directory.Exists(tPath))
            {
                Directory.CreateDirectory(tPath);
            }
            if(Directory.GetFiles(tPath).Length > 0) // 如果目标目录存文件， 先删除
            {
                try
                {
                    FileOperationHelper.RemoveFullFolder(tPath);
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }

            var thread = new Thread(() => SevenZipHelper.Extract(sPath,tPath))
            {
                Name = "InstallThread",
                IsBackground = true
            };
            thread.Start();
        }

        /// <summary>
        /// 快速安装
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void materialTextButton3_Click(object sender, EventArgs e)
        {
            if (this.extractProgress.Value > 0 && this.extractProgress.Value < 100)
            {
                MessageBox.Show("正在安装中。。。");
                return;
            }

            this.customInstallPanel.Visible = false;// 隐藏界面
            this.extractProgress.Visible = true;// 显示进度条
            string sPath = Path.Combine(Environment.CurrentDirectory, InstallerConfig.Config.Install7ZFile);
            string tPath = m_curInstallExePath;
            if(!Directory.Exists(tPath))
            {
                Directory.CreateDirectory(tPath);
            }
            var thread = new Thread(() => SevenZipHelper.Extract(sPath, tPath))
            {
                Name = "InstallThread",
                IsBackground = true
            };
            thread.Start();
        }

        /// <summary>
        /// 完成安装
        /// </summary>
        public void OnCompleteInstall()
        {
            string exePath = Path.Combine(m_curInstallExePath, InstallerConfig.Config.ExeFile);
            if(System.IO.File.Exists(exePath))
            {
                CreateShortcutToDesktop(exePath);
                this.materialTextButton5.Visible = true;
            }
            else
            {
                MessageBox.Show(string.Format("未能为{0}创建快捷方式",InstallerConfig.Config.ExeFile));
            }
        }

        /// <summary>
        /// 在桌面上创建一个快捷程序
        /// </summary>
        /// <param name="exePath"></param>
        private void CreateShortcutToDesktop(string exePath)
        {
            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            // 快捷方式文件名
            var lnkPath = Path.Combine(deskDir, string.Format("{0}.lnk", InstallerConfig.Config.AppName));
            Shortcut shortcutProgress = new Shortcut();
            shortcutProgress.CreateShortcutToFile(exePath, lnkPath);
        }



        private void appShortcutToDesktop(string exePath)
        {
            exePath = Path.GetFileNameWithoutExtension(exePath);
            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            string tt = File.ReadAllText(deskDir + "\\Wox.lnk");
            MessageBox.Show(tt);


            //[InternetShortcut]
            //URL=
            //HotKey=
            //IconFile=
            //IconIndex=
            //ShowCommand=
            //Modified=
            //Roamed=
            //IDList=
            //Author=
            //WhatsNew=
            //Comment=
            //Desc=

            //[DEFAULT] ; HTML frameset
            //BASEURL=

            //[DOC<NestedFramesetInfo>]
            //        BASEURL=
            //ORIGURL=

            //[Bookmarklet] ; JavaScript bookmarklet
            //ExtendedURL=

            //[MonitoredItem]
            //        FeedUrl=
            //IsLivePreview=
            //PreviewSize=

            //[{000214A0-0000-0000-C000-000000000046}] ; FMTID_Intshcut property storage
            //Prop<2..2147483647>=
            using (StreamWriter writer = new StreamWriter(deskDir + "\\" + exePath + ".lnk"))
            {
                MessageBox.Show("come here ... 4");
                string app = System.Reflection.Assembly.GetExecutingAssembly().Location;
                MessageBox.Show("come here ... 7");
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + app);
                writer.WriteLine("IconIndex=0");
                string icon = app.Replace('\\', '/');
                writer.WriteLine("IconFile=" + icon);
                MessageBox.Show("come here ... 8");
            }
            MessageBox.Show("come here ... end");

        }

        public void CreateShortcuts(string path)
        {
            MessageBox.Show("come here ... 5");
            //path + Path.DirectorySeparatorChar + "DirectorEditor-" + bitness + ".exe";
            string exePath = path;
            try
            {
                MessageBox.Show("come here ... 6");
                // 创建桌面的快捷方式
                //IWshShortcut shortcut = (IWshShortcut)new WshShell().CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar + "DirectorEditor.lnk");
                //MessageBox.Show("come here ... 7");
                //shortcut.Description = "视频编辑器";
                //shortcut.WorkingDirectory = path;
                //shortcut.TargetPath = exePath;
                //shortcut.Save();
                MessageBox.Show("come here ... 8");

                // 创建开始菜单的快捷方式
                //string shortcutDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "DirectorEditor");
                //Directory.CreateDirectory(shortcutDirectory);
                //WshShell shell = new WshShell();
                //IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(Path.Combine(shortcutDirectory, "DirectorEditor.lnk"));
                //shortcut.Description = "DirectorEditor视频编辑器";
                //shortcut.WorkingDirectory = path;
                //shortcut.TargetPath = exePath;
                //shortcut.Save();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        // 自定义安装设置
        private void cMaterialLabel1_Click(object sender, EventArgs e)
        {
            this.customInstallPanel.Visible = true;
            this.materialTextButton1.Visible = false;
        }

        // 返回
        private void materialTextButton4_Click(object sender, EventArgs e)
        {
            this.customInstallPanel.Visible = false;
            this.materialTextButton1.Visible = true;
        }

        // 浏览
        private void materialTextButton2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = m_curInstallExePath;
                dialog.ShowNewFolderButton = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                    this.curInstallPath.Text = dialog.SelectedPath + Path.DirectorySeparatorChar + "DirectorEditor";
            }
        }

        /// <summary>
        /// 如果安装路径手动修改了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void curInstallPath_TextChanged(object sender, EventArgs e)
        {
            m_curInstallExePath = this.curInstallPath.Text;
        }

        // 立即体验
        private void materialTextButton5_Click(object sender, EventArgs e)
        {
            string exePath = Path.Combine(m_curInstallExePath, InstallerConfig.Config.ExeFile);
            FileOperationHelper.RunExe(exePath);

            this.Close();
        }
    }
}
