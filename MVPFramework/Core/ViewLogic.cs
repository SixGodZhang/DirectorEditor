using MVPFramework.Binder;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace MVPFramework.Core
{
    [Obsolete("暂时还没考虑好怎么使用")]
    public enum ViewType
    {
        None,
        Single,// 单例, 可以直接通过Instance字段进行访问
        Multi//可以允许创建多个
    }

    /// <summary>
    /// 实现IViewLogic中的部分逻辑
    /// </summary>
    /// <typeparam name="T1">ViewLogic layer 绑定的控件</typeparam>
    /// <typeparam name="T2">ViewLogic 实现类</typeparam>
    public abstract class ViewLogic<T1,T2> 
        where T1:Control
        where T2:class, IViewLogic
    {
        [Obsolete("还没考虑好怎么用")]
        private ViewType _viewType = ViewType.None;
        [Obsolete("还没考虑好怎么用")]
        public ViewType ViewType { get => _viewType; set => _viewType = value; }

        /// <summary>
        /// 控件实例
        /// </summary>
        public T1 target;
        /// <summary>
        /// Presenter绑定器--> 处理ViewLogic和Presenter具体的绑定过程
        /// </summary>
        private readonly PresenterBinder presenterBinder;
        /// <summary>
        /// ViewLogic 关联的所有Presenters
        /// </summary>
        public IEnumerable<IPresenter> Presenters { get; set; }

        public bool ThrowExceptionIfNoPresenterBound { get; set; }
        /// <summary>
        /// View初始化回调
        /// </summary>
        public Action InitViewLogic;
        /// <summary>
        /// 销毁事件
        /// </summary>
        public event EventHandler DestoryViewLogicEvent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">type暂时没用了， 还是保留， 考虑到后面的设计会用到</param>
        /// <param name="strategy">将ViewLogic和Presenter绑定起来的寻址策略</param>
        protected ViewLogic( PresenterAddressingType addressingType = PresenterAddressingType.Composite)
        {
            //this._viewType = type;
            presenterBinder = new PresenterBinder(addressingType: addressingType);
            target = this.GetType().Assembly.CreateInstance(typeof(T1).FullName) as T1;
            // 特殊事件注册
            target.HandleDestroyed += ViewLogic_ControlDestroyed;
            EventInfo loadEvent = target.GetType().GetEvent("Load");
            if (loadEvent != null)
            {
                loadEvent.AddEventHandler(target, new EventHandler(ViewLogic_ControlLoad));
            }
            //
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
                presenter.DestroyView(new List<IViewLogic>() { this as T2 });
            }

            //销毁引用
            if (this.DestoryViewLogicEvent!=null)
            {
                this.DestoryViewLogicEvent(this,null);
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
