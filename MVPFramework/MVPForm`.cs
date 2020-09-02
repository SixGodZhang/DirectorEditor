using MVPFramework.Binder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVPFramework
{
    public partial class MVPForm<TModel> : Form,IView<TModel> where TModel: class
    {
        private readonly PresenterBinder presenterBinder = new PresenterBinder();
        public MVPForm()
        {
            ThrowExceptionIfNoPresenterBound = true;
            presenterBinder.PerformBinding(this);
        }

        public TModel Model { get; set ; }
        public bool ThrowExceptionIfNoPresenterBound { get; set; }
    }
}
