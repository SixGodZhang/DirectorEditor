//using IWshRuntimeLibrary;
using System;
using System.IO;
using System.Windows.Forms;

namespace TX3Installer.Common
{
    /// <summary>
    /// 安装操作
    /// </summary>
    public class InstallOperation
    {
        //private static int bitness = Marshal.SizeOf(typeof(IntPtr)) * 8;

        /// <summary>
        /// 创建快捷键
        /// </summary>
        /// <param name="path"></param>
        public static void CreateShortcuts(string path)
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
    }
}