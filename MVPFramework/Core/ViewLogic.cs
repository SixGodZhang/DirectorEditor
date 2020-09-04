using MVPFramework.Binder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVPFramework.Core
{
    public enum ViewType
    {
        None,
        Single,// 单例, 可以直接通过Instance字段进行访问
        Multi//可以允许创建多个
    }

    public abstract class ViewLogic<T1,T2> 
        where T1:Control
        where T2:class, IView
    {
        private ViewType _viewType = ViewType.None;
        public ViewType ViewType { get => _viewType; set => _viewType = value; }

        public T1 target;
        private readonly PresenterBinder presenterBinder;
        public IEnumerable<IPresenter> Presenters { get; set; }

        public bool ThrowExceptionIfNoPresenterBound { get; set; }
        public Action InitViewLogic;// 在View初始化完成时候调用
        public Action DestroyViewLogic;// 销毁viewlogic

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">type暂时没用了， 还是保留， 考虑到后面的设计会用到</param>
        /// <param name="strategy">将ViewLogic和Presenter绑定起来的寻址策略</param>
        protected ViewLogic(ViewType type = ViewType.Single, PresenterAddressingType addressingType = PresenterAddressingType.Composite)
        {
            this._viewType = type;
            presenterBinder = new PresenterBinder(addressingType: addressingType);
            target = this.GetType().Assembly.CreateInstance(typeof(T1).FullName) as T1;
            // TODO： 在此注册一些事件
            target.HandleDestroyed += ViewLogic_ControlDestroyed;
            EventInfo loadEvent = target.GetType().GetEvent("Load");
            if (loadEvent != null)
            {
                loadEvent.AddEventHandler(target, new EventHandler(ViewLogic_ControlLoad));
            }
           
            presenterBinder.PresenterCreated += ViewLogic_PresenterCreated;
            presenterBinder.PerformBinding(this as T2);
        }

        /// <summary>
        /// 控件具有Load事件时的注册函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewLogic_ControlLoad(object sender, EventArgs e)
        {
            if (this.InitViewLogic!= null)
            {
                InitViewLogic();
            }
        }

        /// <summary>
        /// 控件销毁时的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewLogic_ControlDestroyed(object sender, EventArgs e)
        {
            // 在Presenter上销毁View
            foreach (var presenter in Presenters)
            {
                presenter.DestroyView(new List<IView>() { this as T2 });
            }

            // 销毁全局引用
            if (this.DestroyViewLogic!= null)
            {
                DestroyViewLogic();
            }

        }

        private void ViewLogic_PresenterCreated(object sender, PresenterCreatedEventArgs e)
        {
            // e.Presenter 就是与此View绑定的Presenter
            Presenters = e.Presenters;
            foreach (var p in Presenters)
            {
                p.PresenterStatus = PresenterStatus.Inited;
            }
        }

    }
}
