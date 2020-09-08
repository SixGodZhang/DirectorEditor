using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework
{
    public enum PresenterType
    {
        Default = 1,// 啥都没有
        View = 2,// Presenter:View = 1:1  && 没有Model
        PresenterView11 = 3, // Presenter: View =  1:1
        PresenterViewNN // Presenter : View = n:n
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
        //PresenterType GetPresenterType();
        /// <summary>
        /// Presenter 状态
        /// </summary>
        PresenterStatus PresenterStatus { get; set; }
        /// <summary>
        /// 销毁指定ViewLogic
        /// </summary>
        void ClearViewPart(IEnumerable<IViewLogic> view);
    }

    /// <summary>
    /// 泛型接口, 与View关联(无Model)
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public interface IPresenter<TView> : IPresenter where TView : class, IViewLogic
    {
        TView ViewLogic { get; }
    }


    /// <summary>
    /// Presenter抽象类,无Model
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public abstract class Presenter<TView> : IPresenter where TView : class, IViewLogic
    {
        private TView view; // View 在Presenter创建的时候就需要绑定
        private Type viewLogicType; // Presenter绑定的ViewLogic类型
        private static readonly PresenterType presenterType = PresenterType.View;
        private PresenterStatus presenterStatus = PresenterStatus.Default;

        protected Presenter()
        {
            //GetOrCreateViewLogic(this.GetType());
            // 设置Presenter<TView> 绑定的所有ViewLogic类型
            var allViewLogicType  = this.GetType().GetCustomAttributes(typeof(ViewLogicBindingAttribute), true)
                .OfType<ViewLogicBindingAttribute>()
                .Select(vlba => vlba.ViewLogicType)
                .ToArray();
            if (allViewLogicType.Count() != 1)
            {
                throw new ArgumentException(string.Format("{0} 没有绑定ViewLogic 或 绑定的ViewLogic数目超过此种PresenterType限制的最高数目:1",this.GetType().FullName));
            }

            viewLogicType = allViewLogicType[0];
        }

        /// <summary>
        /// 获取Presenter需要处理的ViewLogic， 如果不存在实例, 则新建一个
        /// </summary>
        /// <param name="viewLogicType"></param>
        /// <returns></returns>
        public IViewLogic GetOrCreateViewLogic(Type viewLogicType)
        {
            // 先判断此类型的实例是否存在
            IViewLogic returnViewLogicInstance = this.view;

            if (returnViewLogicInstance == null)// 如果不存在, 则新建
            {
                var newViewLogicInstance = viewLogicType.Assembly.CreateInstance(viewLogicType.FullName);
                // 对 viewlogic 中的 presenters 进行赋值
                FieldInfo viewLogicPresentersFieldInfo = newViewLogicInstance.GetType().BaseType.GetField("presenters", BindingFlags.Instance | BindingFlags.NonPublic);
                if (viewLogicPresentersFieldInfo != null)
                {
                    viewLogicPresentersFieldInfo.SetValue(newViewLogicInstance, new List<IPresenter>() { this as IPresenter });
                }


                EventInfo destoryViewLogic = newViewLogicInstance.GetType().GetEvent("DestoryViewLogicEvent");
                if (destoryViewLogic != null)
                {
                    destoryViewLogic.AddEventHandler(newViewLogicInstance, new EventHandler(DestroySingleViewLogic));
                }
                returnViewLogicInstance = newViewLogicInstance as IViewLogic;
                this.view = returnViewLogicInstance as TView;
            }

            return returnViewLogicInstance;
        }

        /// <summary>
        /// 从PresenterView中销毁指定的ViewLogic instance
        /// 只销毁实例, 不销毁类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DestroySingleViewLogic(object sender, EventArgs e)
        {
            if(this.view == sender)
            {
                this.view = null;
            }
        }

        /// <summary>
        /// View层(只处理显示)
        /// </summary>
        public TView View
        {
            get {
                if (this.view == null)
                {
                    GetOrCreateViewLogic(this.viewLogicType);
                }
                return this.view;
            }
            set { view = value; }
        }

        public PresenterType PresenterType { get => presenterType; }
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
        public void ClearViewPart(IEnumerable<IViewLogic> view)
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
    //public interface IPresenter<TView, TModel> : IPresenter
    //    where TView : class, IViewLogic
    //    where TModel : class, IModel
    //{
    //    TView View { get; }// Presenter 中只能访问View
    //    TModel Model { get; set; }// Presenter 中可以访问和修改Model
    //}


    /// <summary>
    /// Presenter抽象类。 
    /// 主要实现Presenter 同时与多个View或Model的绑定
    /// 包含1:1, N:N,1:N,N:1关系
    /// </summary>
    /// <typeparam name="TViewLogicN">ViewLogic集合</typeparam>
    /// <typeparam name="TModelN">Model(数据定义)集合</typeparam>
    public abstract class PresenterNN : IPresenter
    {
        private IList<IViewLogic> viewLogicList;
        private IDictionary<Type,IModel> modelDict;
        private IList<Type> viewLogicTypeList;// 保存PresenterNN绑定的ViewLogicType
        private static readonly PresenterType presenterType = PresenterType.PresenterViewNN;
        private PresenterStatus presenterStatus = PresenterStatus.Default;

        /// <summary>
        /// P层类型
        /// </summary>
        public PresenterType PresenterType => presenterType;
        /// <summary>
        /// P层状态
        /// </summary>
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

        /// <summary>
        /// 保存的所有数据
        /// </summary>
        public List<IModel> ModelList => this.modelDict.Values as List<IModel>;

        public IList<IViewLogic> ViewLogicList
        {
            get
            {
                return this.viewLogicList;
            }
        }

        /// <summary>
        /// Presenter绑定的所有 ViewLogicTypeList
        /// </summary>
        public IList<Type> ViewLogicTypeList
        {
            get
            {
                return this.viewLogicTypeList;
            }
        }

        /// <summary>
        /// 获取指定类型的Model
        /// 普通方法。
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IModel GetModel(Type model)
        {
            foreach (var m in this.modelDict)
            {
                if (m.Key.Equals(model))
                {
                    return m.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取指定类型的Model
        /// 泛型方法。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetModel<T>() where T:class,IModel
        {
            foreach (var m in this.modelDict)
            {
                if (m.Key.Equals(typeof(T)))
                {
                    return m.Value as T;
                }
            }
            return null;
        }

        /// <summary>
        /// 添加一个指定的Model,如果不存在才会添加)
        /// </summary>
        /// <param name="model"></param>
        public void AddModel(IModel model)
        {
            // 是否包含类型定义
            if(!this.modelDict.ContainsKey(model.GetType()))
            {
                this.modelDict.Add(model.GetType(), model);
            }
        }


        /// <summary>
        /// 获取指定类型的View
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public IViewLogic GetViewLogicInstance(Type viewLogicType)
        {
            if(this.viewLogicList == null)
            {
                return null;
            }

            foreach (var v in this.viewLogicList)
            {
                if(v.GetType().Equals(viewLogicType))
                {
                    return v;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取Presenter需要处理的ViewLogic， 如果不存在实例, 则新建一个
        /// </summary>
        /// <param name="viewLogicType"></param>
        /// <returns></returns>
        public IViewLogic GetOrCreateViewLogic(Type viewLogicType)
        {
            // 先判断此类型的实例是否存在
            IViewLogic returnViewLogicInstance = this.GetViewLogicInstance(viewLogicType);

            if (returnViewLogicInstance == null)// 如果不存在, 则新建
            {
                var newViewLogicInstance = viewLogicType.Assembly.CreateInstance(viewLogicType.FullName);
                // 对 viewlogic 中的 presenters 进行赋值
                FieldInfo viewLogicPresentersFieldInfo = newViewLogicInstance.GetType().BaseType.GetField("presenters", BindingFlags.Instance | BindingFlags.NonPublic);
                if (viewLogicPresentersFieldInfo != null)
                {
                    viewLogicPresentersFieldInfo.SetValue(newViewLogicInstance, new List<IPresenter>() { this as IPresenter });
                }

                //var destroyViewLogicEvent = newViewLogicInstance.GetType().GetField("DestroyViewLogic");
                //Action tt = destroyViewLogicEvent.GetValue(null) as Action;
                //Delegate.Combine(tt,)
                // 思考了一下, 还是用事件处理吧, 不用委托来搞了
                // 原因: 麻烦， 还要改结构, 但是效果却是一样的

                EventInfo destoryViewLogic = newViewLogicInstance.GetType().GetEvent("DestoryViewLogicEvent");
                if (destoryViewLogic!= null)
                {
                    destoryViewLogic.AddEventHandler(newViewLogicInstance, new EventHandler(DestroySingleViewLogic));
                }
                returnViewLogicInstance = newViewLogicInstance as IViewLogic;
                this.AddViewLogic(returnViewLogicInstance);
            }

            return returnViewLogicInstance;
        }

        /// <summary>
        /// 从PresenterView中销毁指定的ViewLogic instance
        /// 只销毁实例, 不销毁类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DestroySingleViewLogic(object sender, EventArgs e)
        {
            if (this.viewLogicList.Contains(sender))
            {
                viewLogicList.Remove(sender as IViewLogic);
            }
        }


        /// <summary>
        /// Presenter是否可以处理指定的ViewLogic
        /// 需要指定参数。
        /// </summary>
        /// <param name="viewLogicType"></param>
        /// <returns></returns>
        public bool hasViewLogicType(Type viewLogicType)
        {
            return this.viewLogicTypeList.Contains(viewLogicType);
        }

        /// <summary>
        /// Presenter是否可以处理指定的ViewLogic
        /// 泛型方法。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool hasViewLogicType<T>() where T:IViewLogic
        {

            return this.viewLogicTypeList.Contains(typeof(T));
        }

        /// <summary>
        /// 添加一个View实例
        /// 只有当实例不存在，才会添加。不会出现覆盖的情况。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="view"></param>
        public void AddViewLogic(IViewLogic view)
        {
            // 添加该viewlogic type
            if (!this.viewLogicTypeList.Contains(view.GetType()))
            {
                this.viewLogicTypeList.Add(view.GetType());
            }

            // 添加该viewlogic 实例
            if(!this.viewLogicList.Contains(view))
            {
                this.viewLogicList.Add(view);
            }

        }

        /// <summary>
        /// 从P层移除绑定的view
        /// </summary>
        /// <param name="view"></param>
        public void ClearViewPart(IEnumerable<IViewLogic> view)
        {
            foreach (var v in view)
            {
                this.viewLogicList.Remove(v);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected PresenterNN()
        {
            this.presenterStatus = PresenterStatus.Initing;

            // 初始化
            this.viewLogicList = new List<IViewLogic>();
            this.viewLogicTypeList = new List<Type>();
            this.modelDict = new Dictionary<Type, IModel>();

            // 设置PresenterNN 绑定的所有ViewLogic类型
            this.viewLogicTypeList = this.GetType().GetCustomAttributes(typeof(ViewLogicBindingAttribute), true)
                .OfType<ViewLogicBindingAttribute>()
                .Select(vlba => vlba.ViewLogicType)
                .ToArray();

        }

    }

    /// <summary>
    /// 抽象类, 与View关联， 有View、Model
    /// 这里View和Model只做到了一一对应
    /// </summary>
    /// <typeparam name="TViewLogic">ViewLogic层</typeparam>
    public abstract class Presenter<TViewLogic,TModel> : IPresenter<TViewLogic>
        where TViewLogic : class , IViewLogic // ViewLogic层
        where TModel :class , IModel // Model层
    {
        private TViewLogic viewLogic;
        private Type viewLogicType;// 绑定的ViewLogic的类型
        private static readonly PresenterType presenterType = PresenterType.PresenterView11;
        private PresenterStatus presenterStatus = PresenterStatus.Default;
        public PresenterType PresenterType { get => presenterType; }
        public PresenterStatus PresenterStatus {
            get => this.presenterStatus; 
            set{
                if (this.presenterStatus!=value)
                {
                    this.presenterStatus = value;
                }
            }
        }

        protected Presenter()
        {
            this.presenterStatus = PresenterStatus.Initing;
            var allViewLogicType = this.GetType().GetCustomAttributes(typeof(ViewLogicBindingAttribute), true)
            .OfType<ViewLogicBindingAttribute>()
            .Select(vlba => vlba.ViewLogicType)
            .ToArray();
                    if (allViewLogicType.Count() != 1)
                    {
                        throw new ArgumentException(string.Format("{0} 没有绑定ViewLogic类型 或 绑定的ViewLogic数目超过此种PresenterType限制的最高数目:1", this.GetType().FullName));
                    }

            viewLogicType = allViewLogicType[0];
        }

        /// <summary>
        /// 获取Presenter需要处理的ViewLogic， 如果不存在实例, 则新建一个
        /// </summary>
        /// <param name="viewLogicType"></param>
        /// <returns></returns>
        public IViewLogic GetOrCreateViewLogic(Type viewLogicType)
        {
            // 先判断此类型的实例是否存在
            IViewLogic returnViewLogicInstance = this.viewLogic;

            if (returnViewLogicInstance == null)// 如果不存在, 则新建
            {
                var newViewLogicInstance = viewLogicType.Assembly.CreateInstance(viewLogicType.FullName);
                // 对 viewlogic 中的 presenters 进行赋值
                var t = newViewLogicInstance.GetType().BaseType;
                FieldInfo viewLogicPresentersFieldInfo = newViewLogicInstance.GetType().BaseType.GetField("presenters", BindingFlags.Instance|BindingFlags.NonPublic);
                if (viewLogicPresentersFieldInfo!= null)
                {
                    viewLogicPresentersFieldInfo.SetValue(newViewLogicInstance, new List<IPresenter>() { this as IPresenter});
                }

                EventInfo destoryViewLogic = newViewLogicInstance.GetType().GetEvent("DestoryViewLogicEvent");
                if (destoryViewLogic != null)
                {
                    destoryViewLogic.AddEventHandler(newViewLogicInstance, new EventHandler(DestroySingleViewLogic));
                }
                returnViewLogicInstance = newViewLogicInstance as IViewLogic;
                this.viewLogic = returnViewLogicInstance as TViewLogic;
            }

            return returnViewLogicInstance;
        }

        /// <summary>
        /// 从PresenterView中销毁指定的ViewLogic instance
        /// 只销毁实例, 不销毁类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DestroySingleViewLogic(object sender, EventArgs e)
        {
            if (this.viewLogic == sender)
            {
                this.viewLogic = null;
            }
        }

        /// <summary>
        /// View层(只处理显示)
        /// </summary>
        public TViewLogic ViewLogic
        {
            get {
                if (viewLogic == null)
                {
                    GetOrCreateViewLogic(this.viewLogicType);
                }
                return viewLogic;
            }
            set { viewLogic = value; }
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
            this.viewLogic = null;
            this.presenterStatus = PresenterStatus.OnlyDataAfterClear;
        }

        /// <summary>
        /// 在P层移除指定的View
        /// </summary>
        /// <param name="viewLogics"></param>
        public void ClearViewPart(IEnumerable<IViewLogic> viewLogics)
        {
            if (viewLogics.Contains(this.viewLogic))
            {
                this.viewLogic = null;
                this.presenterStatus = PresenterStatus.OnlyDataAfterClear;
            }
        }

    }


}
