using MVPFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework
{
    public enum PresenterType
    {
        Default = 1,// 啥都没有
        View = 2,// 与View绑定了
        ModelView = 3, // ModelView 1:1 对应
        ModelViewNN // ModelView n:n对应
    }

    public enum PresenterStatus
    {
        Default = 1,// 默认状态
        Initing = 2, //正在初始化
        Inited = 3,// 初始化完成
        OnlyDataAfterClear = 4//Presenter清理之后,只留下了数据部分 (这个仅适用于PresenterType 是 ModelView的情况)
    }

    /// <summary>
    /// 普通接口
    /// </summary>
    public interface IPresenter
    {
        /// <summary>
        /// Presenter 类型
        /// </summary>
        PresenterType PresenterType { get; }
        /// <summary>
        /// Presenter 状态
        /// </summary>
        PresenterStatus PresenterStatus { get; set; }
        /// <summary>
        /// 销毁指定ViewLogic
        /// </summary>
        void DestroyView(IEnumerable<IViewLogic> view);
    }

    /// <summary>
    /// 泛型接口, 与View关联(无Model)
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public interface IPresenter<TView> : IPresenter where TView : class, IViewLogic
    {
        TView View { get; }
    }

    /// <summary>
    /// Presenter 抽象类 
    /// 定义一些通用的字段
    /// </summary>
    public abstract class AbstractPresenter
    {
        /// <summary>
        /// 创建事件。在创建的时候被调用【仅被调用一次】
        /// 【预定义触发时间 - Presenter 创建的最后一步】
        /// </summary>
        public event EventHandler<PresenterCreateEventArgs> CreateEvent;

        /// <summary>
        /// 初始化事件。每次对Presenter进行绑定时,都可以重新调用
        /// </summary>
        public event EventHandler<PresenterInitEventArgs> InitEvent;

        /// <summary>
        /// 取消事件。在取消指定视图绑定时调用
        /// </summary>
        public event EventHandler<PresenterCancelSingleViewLogicEventArgs> CancelSingleViewLogicBindingEvent;

        /// <summary>
        /// 取消事件。在取消多个指定视图绑定时调用
        /// </summary>
        public event EventHandler<PresenterCancelMultiViewLogicEventArgs> CancelMultiViewLogicBindingEvent;

        /// <summary>
        /// 取消事件。在取消所有视图绑定时调用
        /// </summary>
        public event EventHandler<PresenterCancelAllViewLogicEventArgs> CancelAllViewLogicBindingEvent;

        /// <summary>
        /// 绑定事件。在绑定某个视图时被调用
        /// </summary>
        public event EventHandler<PresenterSingleViewLogicBindingEventArgs> SingleViewLogicBindingEvent;

        /// <summary>
        /// 销毁事件。 在Presenter进行销毁时调用【仅被调用一次】
        /// </summary>
        public event EventHandler<PresenterDestoryEventArgs> DestoryEvent;

        /// <summary>
        /// 创建事件回调函数。
        /// </summary>
        /// <param name="arg"></param>
        public virtual void OnCreate(PresenterCreateEventArgs arg) { }
        /// <summary>
        /// 初始化事件回调函数。
        /// </summary>
        /// <param name="arg"></param>
        public virtual void OnInit(PresenterInitEventArgs arg) { }
        /// <summary>
        /// 取消单个视图事件绑定回调函数。
        /// </summary>
        /// <param name="arg"></param>
        public virtual void OnCancelSingleViewLogicBinding(PresenterCancelSingleViewLogicEventArgs arg) { }
        /// <summary>
        /// 取消多个视图事件绑定回调函数。
        /// </summary>
        /// <param name="arg"></param>
        public virtual void OnCancelMultiViewLogicBinding(PresenterCancelMultiViewLogicEventArgs arg) { }
        /// <summary>
        /// 取消所有视图绑定事件回调函数。
        /// </summary>
        /// <param name="arg"></param>
        public virtual void OnCancelAllViewLogicBinding(PresenterCancelAllViewLogicEventArgs arg) { }
        /// <summary>
        /// 注册单个视图绑定事件回调函数。
        /// </summary>
        /// <param name="arg"></param>
        public virtual void OnSingleViewLogicBinding(PresenterSingleViewLogicBindingEventArgs arg) { }
        /// <summary>
        /// 销毁Presenter事件回调函数。
        /// </summary>
        /// <param name="arg"></param>
        public virtual void OnDestory(PresenterDestoryEventArgs arg) { }
    }

    /// <summary>
    /// Presenter抽象类,无Model
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public abstract class Presenter<TView> : AbstractPresenter, IPresenter where TView : class, IViewLogic
    {
        private TView view; // View 在Presenter创建的时候就需要绑定
        private PresenterType presenterType = PresenterType.Default;
        private PresenterStatus presenterStatus = PresenterStatus.Default;

        protected Presenter(TView view)
        {
            this.view = view;
            this.presenterType = PresenterType.View;
        }

        /// <summary>
        /// View层(只处理显示)
        /// </summary>
        public TView View
        {
            get { return view; }
            set { view = value; }
        }

        public PresenterType PresenterType { get => this.presenterType; }
        public PresenterStatus PresenterStatus { get => this.presenterStatus; set => this.presenterStatus = value; }

        public void Destroy(IViewLogic view)
        {
            if (this.view.Equals(view))
            {
                this.view = null;
            }
        }

        /// <summary>
        /// 移除指定的view
        /// </summary>
        /// <param name="view"></param>
        public void DestroyView(IEnumerable<IViewLogic> view)
        {
            if(view.Contains(this.view))
            {
                this.view = null;
            }
        }
    }

    /// <summary>
    /// Presenter 接口, 有View、Model
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    public interface IPresenter<TView, TModel> : IPresenter
        where TView : class, IViewLogic
        where TModel : class, IModel
    {
        TView View { get; }// Presenter 中只能访问View
        TModel Model { get; set; }// Presenter 中可以访问和修改Model
    }


    /// <summary>
    /// Presenter抽象类。 
    /// 主要实现Presenter 同时与多个View或Model的绑定
    /// </summary>
    /// <typeparam name="TViewN"></typeparam>
    /// <typeparam name="TModelN"></typeparam>
    public abstract class PresenterNN<TViewN, TModelN> : AbstractPresenter, IPresenter
        where TViewN : IList<IViewLogic>
        where TModelN : IList<IModel>
    {
        private IList<IViewLogic> views;
        private IList<IModel> models;
        private PresenterType presenterType;
        private PresenterStatus presenterStatus = PresenterStatus.Default;

        public PresenterType PresenterType => this.presenterType;

        public PresenterStatus PresenterStatus
        {
            get => this.presenterStatus;
            set
            {
                if(this.presenterStatus!= value)
                {
                    this.presenterStatus = value;
                }
            }
        }

        public IList<IModel> Models
        {
            get
            {
                return this.models;
            }
        }

        public IList<IViewLogic> Views
        {
            get
            {
                return this.views;
            }
        }

        /// <summary>
        /// 获取指定类型的Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IModel GetModel<T>(T model) where T: IModel
        {
            var inputModelType = model.GetType();
            foreach (var m in this.models)
            {
                if (m.GetType().Equals(inputModelType))
                {
                    return m;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取指定类型的View
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public IViewLogic GetView<T>(T view) where T:IViewLogic
        {
            var inputViewType = view.GetType();
            foreach (var v in this.views)
            {
                if(v.GetType().Equals(inputViewType))
                {
                    return v;
                }
            }
            return null;
        }

        /// <summary>
        /// 添加一个View实例
        /// 只有当实例不存在，才会添加。不会出现覆盖的情况。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="view"></param>
        public void AddView<T>(T view) where T:IViewLogic
        {
            var inputViewType = view.GetType();
            if(!this.views.Contains(view))
            {
                this.views.Add(view);
            }

        }

        /// <summary>
        /// 从P层移除绑定的view
        /// </summary>
        /// <param name="view"></param>
        public void DestroyView(IEnumerable<IViewLogic> view)
        {
            foreach (var v in view)
            {
                this.views.Remove(v);
            }
        }

        protected PresenterNN()
        {
            this.presenterType = PresenterType.ModelViewNN;
            this.presenterStatus = PresenterStatus.Initing;
        }

        
    }



    /// <summary>
    /// 抽象类, 与View关联， 有View、Model
    /// 这里View和Model只做到了一一对应
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public abstract class Presenter<TView,TModel> : AbstractPresenter, IPresenter<TView>
        where TView : class , IViewLogic // View层
        where TModel :class , IModel // Model层
    {
        private TView view; 
        private PresenterType presenterType;
        private PresenterStatus presenterStatus = PresenterStatus.Default;
        public PresenterType PresenterType { get => this.presenterType; }
        public PresenterStatus PresenterStatus {
            get => this.presenterStatus; 
            set{
                if (this.presenterStatus!=value)
                {
                    this.presenterStatus = value;
                }
            }
        }

        public Action cacheMethodCallAction;// 如果界面还没有初始化完成，缓存一些提前调用的函数

        protected Presenter(TView view)
        {
            this.view = view;
            this.presenterType = PresenterType.ModelView;
            this.presenterStatus = PresenterStatus.Initing;
        }

        /// <summary>
        /// View层(只处理显示)
        /// </summary>
        public TView View
        {
            get { return view; }
            set { view = value; }
        }

        /// <summary>
        /// 只保存数据结构
        /// </summary>
        public TModel Model { get; set; }

        /// <summary>
        /// 清理Presenter中的视图部分
        /// </summary>
        public void ClearViewPart()
        {
            this.view = null;
            this.presenterStatus = PresenterStatus.OnlyDataAfterClear;
            this.cacheMethodCallAction = null;
        }

        /// <summary>
        /// 处理缓存的方法
        /// </summary>
        private void DoCacheMethodCallAction()
        {
            if (cacheMethodCallAction == null)
                return;
            cacheMethodCallAction();
            cacheMethodCallAction = null;
        }

        public void Destroy(IViewLogic view)
        {
            if(view.Equals(view))
            {
                this.view = null;
            }
        }

        /// <summary>
        /// 在P层移除指定的View
        /// </summary>
        /// <param name="view"></param>
        public void DestroyView(IEnumerable<IViewLogic> view)
        {
            if(view.Contains(this.view))
            {
                this.view = null;
            }
        }
    }

}
