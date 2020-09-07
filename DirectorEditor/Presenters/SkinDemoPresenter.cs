using DirectorEditor.Views;
using MVPFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorEditor.Presenters
{
    public class SkinDemoPresenter:Presenter<ISkinDemoView>
    {
        public SkinDemoPresenter(ISkinDemoView view):base(view)
        {
            View = view;
        }

        /// <summary>
        ///  显示界面
        /// </summary>
        public void Show()
        {
            View.Show();
        }

        /// <summary>
        ///  界面获取焦点
        /// </summary>
        public void Activate()
        {
            View.Activate();
        }

    }
}
