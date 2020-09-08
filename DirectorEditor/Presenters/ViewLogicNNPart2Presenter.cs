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
    [ViewLogicBinding(typeof(ViewLogicNNViewLogic))]
    public class ViewLogicNNPart2Presenter:Presenter<IViewLogicNNView>
    {
        public ViewLogicNNPart2Presenter()
        {
        }

        public void Show()
        {
            View.Show();
        }

        public void Close()
        {
            View.Close();
        }
    }
}
