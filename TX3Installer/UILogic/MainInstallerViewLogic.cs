using MVPFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TX3Installer.UIComponents;
using TX3Installer.Views;

namespace TX3Installer.UILogic
{
    public class MainInstallerViewLogic : ViewLogic<MainInstallerView, IMainInstallerView>, IMainInstallerView
    {
        public void Show()
        {
            target.Show();
        }

        public void Close()
        {
            target.Close();
        }

        public void Activate()
        {
            target.Activate();
        }
    }
}
