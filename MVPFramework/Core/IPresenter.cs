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
        ModelView = 3 // 与View绑定了, 并且还还持有一个Model属性
    }

    public enum PresenterStatus
    {
        Default = 1,// 默认状态
        Initing = 2, //正在初始化
        Inited = 3// 初始化完成
    }

    /// <summary>
    /// 普通接口
    /// </summary>
    public interface IPresenter
    {
        PresenterType PresenterType { get; }
        PresenterStatus PresenterStatus { get; set; }
    }

    /// <summary>
    /// 泛型接口, 与View关联(无Model)
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public interface IPresenter<TView> : IPresenter where TView : class, IView
    {
        TView View { get; }
    }

    /// <summary>
    /// Presenter抽象类,无Model
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public abstract class Presenter<TView>:IPresenter where TView:class, IView
    {
        private readonly TView view; // View 在Presenter创建的时候就需要绑定
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
        }

        public PresenterType PresenterType { get => this.presenterType; }
        public PresenterStatus PresenterStatus { get => this.presenterStatus; set => this.presenterStatus = value; }
    }

    /// <summary>
    /// Presenter 接口, 有View、Model
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    public interface IPresenter<TView, TModel> :IPresenter
        where TView :class, IView
        where TModel:class, IModel
    {
        TView View { get; }// Presenter 中只能访问View
        TModel Model { get; set; }// Presenter 中可以访问和修改Model
    }

    /// <summary>
    /// 抽象类, 与View关联， 有View、Model
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public abstract class Presenter<TView,TModel> : IPresenter<TView>
        where TView : class , IView // View层
        where TModel :class , IModel // Model层
    {
        private readonly TView view; // View 在Presenter创建的时候就需要绑定
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
        }

        /// <summary>
        /// 只保存数据结构
        /// </summary>
        public TModel Model { get; set; }

        /// <summary>
        /// 处理缓存的方法0.
        /// </summary>
        private void DoCacheMethodCallAction()
        {
            if (cacheMethodCallAction == null)
                return;
            cacheMethodCallAction();
        }
    }

}
