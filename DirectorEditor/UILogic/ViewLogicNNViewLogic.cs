using DirectorEditor.Presenters;
using DirectorEditor.UIComponents;
using DirectorEditor.Views;
using MVPFramework.Binder;
using MVPFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorEditor.UILogic
{
    /// <summary>
    /// 演示ViewLogic绑定多个Presenter
    /// </summary>
    [PresenterBinding(typeof(ViewLogicNNPart1Presenter))]
    [PresenterBinding(typeof(ViewLogicNNPart2Presenter))]
    public class ViewLogicNNViewLogic : ViewLogic<ViewLogicNNView, IViewLogicNNView>, IViewLogicNNView
    {


        public void Activate()
        {
            target?.Activate();
        }

        public void Close()
        {
            target?.Close();
        }

        public void Show()
        {
            target?.Show();
        }


    }
}
