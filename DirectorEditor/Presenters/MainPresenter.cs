using DirectorEditor.Views;
using MVPFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorEditor.Presenters
{
    public class MainPresenter: Presenter<IMainView>
    {
        public MainPresenter(IMainView view):base(view)
        {
            View.Load += LoadFormViewCallback;
        }

        private void LoadFormViewCallback(object sender, EventArgs e)
        {
            
        }
    }
}
