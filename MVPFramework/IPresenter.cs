using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework
{
    /// <summary>
    /// 普通接口
    /// </summary>
    public interface IPresenter
    {

    }

    /// <summary>
    /// 泛型接口, 与View关联
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public interface IPresenter<out TView> : IPresenter where TView : class, IView
    {
        TView View { get; }
    }

    /// <summary>
    /// 抽象类, 与View关联
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public abstract class Presenter<TView> : IPresenter<TView> where TView : class, IView
    {
        private readonly TView view;

        protected Presenter(TView view)
        {
            this.view = view;
        }

        public TView View
        {
            get { return view; }
        }
    }

}
