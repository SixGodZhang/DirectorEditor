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
        private readonly PresenterBinder presenterBinder = new PresenterBinder();
        public IEnumerable<IPresenter> Presenters { get; set; }

        public bool ThrowExceptionIfNoPresenterBound { get; set; }
        public Action InitViewLogic;// 在View初始化完成时候调用
        public Action DestroyViewLogic;// 销毁viewlogic

        protected ViewLogic(ViewType type = ViewType.Single)
        {
            this._viewType = type;
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
