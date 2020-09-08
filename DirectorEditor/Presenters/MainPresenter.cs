using DirectorEditor.UILogic;
using DirectorEditor.Views;
using MVPFramework;
using MVPFramework.Binder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorEditor.Presenters
{
    [ViewLogicBinding(typeof(MainViewLogic))] // ViewLogicBinding 指定Presenter绑定的Logic
    public class MainPresenter: Presenter<IMainView>// IMainView 标记出MainPresenter可以访问MainViewLogic中的接口
    {
        public MainPresenter()
        {

        }
    }
}
