using DirectorEditor.UILogic;
using DirectorEditor.Views;
using MVPFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorEditor.Presenters
{
    [ViewLogicBinding(typeof(AttributeAddressingViewLogic))]
    public class AttributeAddressingPresenter:Presenter<IAttributeAddressingView>
    {
        public AttributeAddressingPresenter()
        {
        }

        public void Show()
        {
            View.Show();
        }

        public void Activate()
        {
            View.Activate();
        }


    }
}
