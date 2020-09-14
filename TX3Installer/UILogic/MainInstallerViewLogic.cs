using MVPFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TX3Installer.UIComponents;
using TX3Installer.Views;

namespace TX3Installer.UILogic
{
    public class MainInstallerViewLogic : ViewLogic<MainInstallerView, IMainInstallerView>, IMainInstallerView
    {
        public bool isCompeleted = false;

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

        public void SetExtractProgress(int value)
        {
            target.extractProgress.Value = Math.Max(0, Math.Min(100, value));
            if(target.extractProgress.Value == 100 && !isCompeleted)
            {
                isCompeleted = true;
                target.OnCompleteInstall();
            }
        }
    }
}
