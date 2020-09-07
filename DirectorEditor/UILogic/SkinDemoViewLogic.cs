using DirectorEditor.Presenters;
using DirectorEditor.Views;
using MaterialSkinExample;
using MVPFramework.Binder;
using MVPFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorEditor.UILogic
{
    [PresenterBinding(typeof(SkinDemoPresenter))]
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
