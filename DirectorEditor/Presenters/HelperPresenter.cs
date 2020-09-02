using DirectorEditor.Views;
using MVPFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorEditor.Presenters
{
    public class HelperPresenter:Presenter<IHelperView>
    {
        public HelperPresenter(IHelperView view):base(view)
        {

        }
    }
}
