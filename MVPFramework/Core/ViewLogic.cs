using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace MVPFramework
{
    /// <summary>
    /// 实现IViewLogic中的部分逻辑
    /// </summary>
    /// <typeparam name="T1">ViewLogic layer 绑定的控件</typeparam>
    /// <typeparam name="T2">ViewLogic 实现类</typeparam>
    public abstract class ViewLogic<T1,T2> 
        where T1:Control
        where T2:class, IViewLogic
    {
        /// <summary>
        /// 控件实例
        /// </summary>
        public T1 target;

        private IEnumerable<IPresenter> presenters;
        /// <summary>
        /// ViewLogic 关联的所有Presenters
        /// </summary>
        public IEnumerable<IPresenter> Presenters { get => presenters; set => presenters = value; }
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
        protected ViewLogic()
        {
            //this._viewType = type;
            //presenterBinder = new PresenterBinder(addressingType: addressingType);// 创建绑定器
            target = this.GetType().Assembly.CreateInstance(typeof(T1).FullName) as T1;// 创建组件
            // 特殊事件注册
            target.HandleDestroyed += ViewLogic_ControlDestroyed;
            EventInfo loadEvent = target.GetType().GetEvent("Load");
            if (loadEvent != null)
            {
                loadEvent.AddEventHandler(target, new EventHandler(ViewLogic_ControlLoad));
            }
            //
            //presenterBinder.PresenterCreated += ViewLogic_PresenterCreated;
            // 绑定
            //presenterBinder.PerformBinding(this as T2);
        }


        /// <summary>
        /// 获取指定的Presenter
        /// </summary>
        /// <param name="presenterType"></param>
        /// <returns></returns>
        public IPresenter GetSinglePresenter(Type presenterType)
        {
            if (presenters == null)
            {
                return null;
            }

            foreach (var presenter in presenters)
            {
                if (presenter.GetType() == presenterType)
                {
                    return presenter;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取指定类型的Presenter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetSinglePresenter<T>() where T: class,IPresenter
        {
            if(presenters == null)
            {
                return default(T);
            }

            foreach (var presenter in presenters)
            {
                if (presenter.GetType() == typeof(T))
                {
                    return presenter as T;
                }
            }

            return default(T);
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
                presenter.ClearViewPart(new List<IViewLogic>() { this as T2 });
            }

            //销毁ViewLogicEvent引用
            if (this.DestoryViewLogicEvent!=null)
            {
                this.DestoryViewLogicEvent(this,null);
            }

            // 如果是Presenter和View11关系,也就是强绑定
            //if (presenters.Count() == 1 && presenters.ElementAt(0).PresenterType == PresenterType.PresenterView11)
            //{

            //}
        }

    }
}
