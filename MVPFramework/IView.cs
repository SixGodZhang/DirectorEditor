using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework
{
    public interface IView
    {
        bool ThrowExceptionIfNoPresenterBound { get; }

        event EventHandler Load;
    }
}
