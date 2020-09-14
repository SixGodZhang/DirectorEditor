using DirectorEditor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TX3Installer.Common
{
    /// <summary>
    /// 7Zip 工具类
    /// </summary>
    public class SevenZipHelper
    {
        private static StringBuilder sevenZipOutput;// 7Zip解压log

        /// <summary>
        /// 解压指定文件到指定目录
        /// </summary>
        /// <param name="sourcePath"> 指定文件</param>
        /// <param name="targetDir"> 指定目录</param>
        /// <param name="monitor">是否监视解压的进度</param>
        public static void Extract(string sourcePath, string targetDir)
        {
            // 初始化
            sevenZipOutput = new StringBuilder();

            Process sevenZip = new Process();
            try
            {
                //读取资源写入
                string sevenZPath = Path.Combine(System.Environment.CurrentDirectory, "7z.exe");
                if (!File.Exists(sevenZPath))
                {
                    File.WriteAllBytes(sevenZPath, global::TX3Installer.Properties.Resources._7z1);
                }
                if (!File.Exists(sevenZPath.Replace("exe", "dll")))
                {
                    File.WriteAllBytes(sevenZPath.Replace("exe", "dll"), global::TX3Installer.Properties.Resources._7z);
                }
                
                sevenZip.StartInfo.FileName = sevenZPath;
                sevenZip.StartInfo.Arguments = "x -y -o\"" + targetDir + "\" \"" + sourcePath + "\"";
                sevenZip.StartInfo.UseShellExecute = false;
                sevenZip.StartInfo.CreateNoWindow = true;
                sevenZip.StartInfo.RedirectStandardOutput = true;
                sevenZip.StartInfo.RedirectStandardError = true;
                if (!sevenZip.Start())
                    throw new FileLoadException("7z could not start!");

                sevenZip.BeginOutputReadLine();
                sevenZip.BeginErrorReadLine();

                do
                {
                    Application.DoEvents();
                    SetProgress((int)(FileOperationHelper.getSizeInCurrentDirectory(targetDir) * 1.0 / InstallerConfig.Config.DataBytesSize * 100));
                } while (!sevenZip.WaitForExit(2000));
                SetProgress(100);
            }
            finally
            {
                if(sevenZip != null)
                {
                    sevenZip.Dispose();
                    sevenZip = null;
                }
            }

            // 处理输出消息
            string output = sevenZipOutput.ToString();
            int index = output.IndexOf("Error:");
            if (index != -1)
                throw new ArgumentException(output.Substring(index + 6).Trim().Replace("\n", "").Replace("\r", ""));
        }

        /// <summary>
        /// 设置进度条
        /// </summary>
        /// <param name="value"></param>
        private static void SetProgress(int value)
        {
            Trace.WriteLine("progress: " + value);
            try
            {
                ThreadPool.QueueUserWorkItem(val => PresenterStub.MainInstallerPresenter.SetExtractProgress(value), value);
            }
            catch(Exception ex)
            {

            }
        }



    }
}
