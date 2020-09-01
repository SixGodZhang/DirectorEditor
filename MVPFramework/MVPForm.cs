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
    public partial class MVPForm : Form,IView
    {
        private readonly PresenterBinder presenterBinder = new PresenterBinder();

        public MVPForm()
        {
            ThrowExceptionIfNoPresenterBound = true;// 如果没有绑定PresenterBound， 则会抛出异常
            presenterBinder.PerformBinding(this);
        }

        public bool ThrowExceptionIfNoPresenterBound { get; private set; }
    }
}
