using MVPFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TX3Installer.Views
{
    public interface IMainInstallerView: IViewLogic
    {
        /// <summary>
        /// 设置解压数据进度
        /// </summary>
        /// <param name="value"></param>
        void SetExtractProgress(int value);
    }
}
