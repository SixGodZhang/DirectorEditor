using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework
{
    /// <summary>
    /// 纯View
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// true: 如果此View没有绑定的Presenter， 则会在获取绑定的过程中抛出异常
        /// </summary>
        bool ThrowExceptionIfNoPresenterBound { get; set; }
    }

    /// <summary>
    /// View与Model绑定
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IView<TModel>:IView
    {
        TModel Model { get; set; }
    }
}
