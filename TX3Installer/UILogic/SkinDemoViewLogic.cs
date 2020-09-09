using DirectorEditor.Presenters;
using DirectorEditor.Views;
using MaterialSkinExample;
using MVPFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorEditor.UILogic
{
    public class SkinDemoViewLogic:ViewLogic<ThirdParty_MaterialSkin_DemoView,ISkinDemoView>,ISkinDemoView
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
