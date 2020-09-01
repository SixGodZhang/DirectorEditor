using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Binder
{
    public class PresenterCreatedEventArgs:EventArgs
    {
        readonly IPresenter presenter;

        public PresenterCreatedEventArgs(IPresenter presenter)
        {
            this.presenter = presenter;
        }

        public IPresenter Presenter
        {
            get { return presenter; }
        }
    }
}
