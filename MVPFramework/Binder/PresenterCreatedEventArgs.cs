using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Binder
{
    public class PresenterCreatedEventArgs:EventArgs
    {
        readonly IEnumerable<IPresenter> presenters;

        public PresenterCreatedEventArgs(IEnumerable<IPresenter> presenters)
        {
            this.presenters = presenters;
        }

        public IEnumerable<IPresenter> Presenters
        {
            get { return presenters; }
        }
    }
}
