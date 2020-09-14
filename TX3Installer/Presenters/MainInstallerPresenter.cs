using MVPFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TX3Installer.UILogic;
using TX3Installer.Views;

namespace TX3Installer.Presenters
{
    [ViewLogicBinding(typeof(MainInstallerViewLogic))]
    public class MainInstallerPresenter:Presenter<IMainInstallerView>
    {
        public void Show()
        {
            View.Show();
        }

        public void Close()
        {
            View.Close();
        }

        /// <summary>
        /// 设置解压进度条
        /// </summary>
        /// <param name="value"></param>
        public void SetExtractProgress(int value)
        {
            View.SetExtractProgress(value);
        }
    }
}
