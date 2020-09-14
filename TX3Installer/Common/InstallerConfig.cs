namespace TX3Installer.Common
{
    /// <summary>
    /// 应用程序的配置文件
    /// </summary>
    public class InstallerConfig
    {
        /// <summary>
        /// 数据总大小
        /// </summary>
        public readonly ulong DataBytesSize;

        /// <summary>
        /// 默认安装路径
        /// </summary>
        public readonly string DefaultInstallPath;

        /// <summary>
        /// 需要解压文件的MD5
        /// </summary>
        public readonly string FileMD5;

        /// <summary>
        /// 安装文件
        /// </summary>
        public readonly string Install7ZFile;

        /// <summary>
        /// 程序的版本号
        /// </summary>
        public readonly string Version;

        /// <summary>
        /// 可执行文件名
        /// </summary>
        public readonly string ExeFile;

        private static InstallerConfig m_config;
        private static ulong m_dataBytesSize = 5499043916;
        private static string m_defaultInstallPath = "D:\\Director";
        private static string m_install7ZFile = "DirectorEditor.7z";
        private static string m_exeFile = "DirectorEditor.exe";

        public InstallerConfig(ulong dataBytesSize)
        {
            DataBytesSize = dataBytesSize;
            DefaultInstallPath = m_defaultInstallPath;
            Install7ZFile = m_install7ZFile;
            ExeFile = m_exeFile;
        }

        public static InstallerConfig Config
        {
            get
            {
                if (m_config == null)
                {
                    m_config = new InstallerConfig(m_dataBytesSize);
                }

                return m_config;
            }
        }
    }
}