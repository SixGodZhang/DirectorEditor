using MVPFramework.Binder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        //private static T2 _instance = null;
        //public static T2 Instance {get=> _instance; set => _instance = value; }
        private ViewType _viewType = ViewType.None;
        public ViewType ViewType { get => _viewType; set => _viewType = value; }

        public T1 target;
        private readonly PresenterBinder presenterBinder = new PresenterBinder();
        public IEnumerable<IPresenter> Presenters { get; set; }

        public bool ThrowExceptionIfNoPresenterBound { get; set; }
        public Action DestroyViewLogic;// 销毁viewlogic

        protected ViewLogic(ViewType type = ViewType.Single)
        {
            // 获取Logic绑定的UI 类型
            //Type uiType = this.GetType().GetCustomAttributes(typeof(CustomLogicAttribute), false)
            //    .OfType<CustomLogicAttribute>()
            //    .Select(p => p.uiType).Single();
            this._viewType = type;
            target = this.GetType().Assembly.CreateInstance(typeof(T1).FullName) as T1;
            // TODO： 在此注册一些事件
            target.HandleDestroyed += ViewLogic_ControlDestroyed;
            //if (_viewType == ViewType.Single)
            //{
            //    _instance = this as T2;
            //}
            
            presenterBinder.PresenterCreated += ViewLogic_PresenterCreated;
            presenterBinder.PerformBinding(this as T2);
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
            DestroyViewLogic();

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
