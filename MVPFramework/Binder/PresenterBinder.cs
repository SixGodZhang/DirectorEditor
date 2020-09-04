using MVPFramework.Core;
using MVPFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Binder
{
    /// <summary>
    /// ViewLogic 与 Presenter 的查找方式
    /// </summary>
    public enum PresenterAddressingType
    {
        Attribute = 1, //基于装饰器寻址
        Convention = 2, // 基于特殊命名规则寻址
        Composite = 100,// 组合策略寻址 
    }

    public sealed class PresenterBinder
    {
        private static IPresenterFactory factory;
        /// <summary>
        /// Presenter 生成类
        /// </summary>
        public static IPresenterFactory Factory
        {
            get
            {
                return factory ?? (factory = new DefaultPresenterFactory());
            }
            set
            {
                factory = value ?? throw new ArgumentNullException("value");
                if (factory != null)
                {
                    throw new InvalidOperationException(
                        factory is DefaultPresenterFactory
                            ? "The factory has already been set, and can be not changed at a later time. In this case, it has been set to the default implementation. This happens if the factory is used before being explicitly set. If you wanted to supply your own factory, you need to do this in your Application_Start event."
                            : "You can only set your factory once, and should really do this in Application_Start.");
                }

            }
        }

        /// <summary>
        /// 检索Presenter的策略
        /// </summary>
        private IPresenterDiscoveryStrategy discoveryStrategy;
        public IPresenterDiscoveryStrategy DiscoveryStrategy
        {
            get
            {
                return discoveryStrategy ?? (discoveryStrategy = new CompositePresenterDiscoveryStrategy(
                    new AttributeBasePresenterDiscoveryStrategy(),// 通过特性搜索
                    new ConventionBasedPresenterDiscoveryStrategy()// 通过命名规则搜索
                    ));
            }
            set
            {
                discoveryStrategy = value ?? throw new ArgumentNullException("value");
            }

        }

        /// <summary>
        /// Presenter 创建完成之后的回调函数
        /// </summary>
        public event EventHandler<PresenterCreatedEventArgs> PresenterCreated;

        /// <summary>
        /// 可以指定固定的检索策略来初始化Presenter
        /// 目的: 加快检索速度, 灵活配置检索方案
        /// </summary>
        /// <param name="strategy"></param>
        public PresenterBinder(PresenterAddressingType addressingType = PresenterAddressingType.Composite)
        {
            this.EnsureDiscoveryStrategy(addressingType);
        }

        /// <summary>
        /// 绑定ViewInstance
        /// </summary>
        /// <param name="viewInstance"></param>
        public void PerformBinding(IView viewInstance)
        {
            try
            {
                IEnumerable<IPresenter> presenters = PerformBinding(viewInstance, DiscoveryStrategy,Factory);
                OnPresenterCreated(new PresenterCreatedEventArgs(presenters));// Presenter初始化
            }
            catch(Exception e)
            {
                // 这里的异常只捕获， 不处理
            }
        }

        /// <summary>
        /// Presenter创建完成之后回调函数
        /// </summary>
        /// <param name="args"></param>
        private void OnPresenterCreated(PresenterCreatedEventArgs args)
        {
            if (PresenterCreated != null)
            {
                PresenterCreated(this, args);
            }
        }

        /// <summary>
        /// 确定绑定器的寻址策略
        /// </summary>
        /// <param name="addressingType"></param>
        private void EnsureDiscoveryStrategy(PresenterAddressingType addressingType)
        {
            switch (addressingType)
            {
                case PresenterAddressingType.Attribute:
                    this.discoveryStrategy = new AttributeBasePresenterDiscoveryStrategy();
                    break;
                case PresenterAddressingType.Convention:
                    this.discoveryStrategy = new ConventionBasedPresenterDiscoveryStrategy();
                    break;
                default:
                    this.discoveryStrategy = new CompositePresenterDiscoveryStrategy(
                        new AttributeBasePresenterDiscoveryStrategy(),// 通过特性搜索
                        new ConventionBasedPresenterDiscoveryStrategy()// 通过命名规则搜索
                        );
                    break;
            }
        }

        /// <summary>
        /// 绑定
        /// </summary>
        /// <param name="candidate">viewInstance</param>
        /// <param name="presenterDiscoveryStrategy"></param>
        /// <param name="presenterCreatedCallback"></param>
        /// <param name="presenterFactory"></param>
        /// <returns></returns>
        private static IEnumerable<IPresenter> PerformBinding(
            IView candidate,
            IPresenterDiscoveryStrategy presenterDiscoveryStrategy,
            IPresenterFactory presenterFactory)
        {
            // 获取candidate所有的绑定信息
            // 绑定信息包含View和Predicate
            // 根据策略找到所有
            IEnumerable<PresenterBinding> presenterBindings = GetBinding(candidate,presenterDiscoveryStrategy);

            List<IPresenter> newPresenters = null;
            List<Type> failedPresenterTypes = null;

            // 这里现在PresenterStub中查找， 看是否可以找到实例
            // 可以找到的实例保存在newPresenters中, 在cache中找不到的实例输出到failedPresenterTypes中
            newPresenters = PresenterStub.Gets(presenterBindings.Select(p => p.PresenterType), out failedPresenterTypes);

            if(newPresenters!=null && newPresenters.Count() > 0)
            {
                // 调用缓存中的Presenter初始化失败的Presenter
                List<IPresenter> failedPresenterFromCache = new List<IPresenter>();

                // 遍历所有可以找到的、符合要求的Presenter
                for (int i = 0;i<newPresenters.Count(); i++)
                {
                    IPresenter newPresenter = newPresenters.ElementAt(i);
                    switch(newPresenter.PresenterType)
                    {
                        case PresenterType.ModelView:
                            if (newPresenter.PresenterStatus == PresenterStatus.Inited || newPresenter.PresenterStatus == PresenterStatus.OnlyDataAfterClear)
                            {
                                try
                                {
                                    // call ClearViewPart
                                    MethodInfo mi = newPresenter.GetType().GetMethod("ClearViewPart");
                                    if (mi != null)
                                    {
                                        mi.Invoke(newPresenter, null);
                                    }
                                    // set View Property
                                    PropertyInfo viewPropertyInfo = newPresenter.GetType().GetProperty("View");
                                    if (viewPropertyInfo != null && viewPropertyInfo.CanWrite)
                                    {
                                        viewPropertyInfo.SetValue(newPresenter, candidate);
                                    }
                                    // set PresenterStatus Property
                                    PropertyInfo statusPropertyInfo = newPresenter.GetType().GetProperty("PresenterStatus");
                                    if (statusPropertyInfo != null && viewPropertyInfo.CanWrite)
                                    {
                                        statusPropertyInfo.SetValue(newPresenter, PresenterStatus.Initing);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    failedPresenterFromCache.Add(newPresenter);
                                }
                            }
                            else
                            {//如果newPresenter 状态不对, 则认为其初始化失败, 准备重新初始化
                                failedPresenterFromCache.Add(newPresenter);
                            }
                            break;
                        case PresenterType.ModelViewNN:
                            if (newPresenter.PresenterStatus == PresenterStatus.Inited)
                            {
                                try
                                {
                                    MethodInfo mi = newPresenter.GetType().GetMethod("AddView");
                                    if (mi != null)
                                    {
                                        mi.Invoke(newPresenter, new object[] { candidate });
                                    }
                                }catch(Exception ex)
                                {
                                    failedPresenterFromCache.Add(newPresenter);
                                }
                            }
                            else
                            {
                                failedPresenterFromCache.Add(newPresenter);
                            }
                            break;
                        case PresenterType.View:
                            break;
                        default:
                            break;
                    }
                }

                
                if (failedPresenterFromCache.Count() > 0)
                {
                    // 从newPresenter中排除那些创建失败的
                    newPresenters = newPresenters.Where(p => !failedPresenterFromCache.Contains(p)) as List<IPresenter>;

                    //遍历所有失败的Presenter， 然后重新创建
                    foreach (var failedPresenter in failedPresenterFromCache)
                    {
                        IPresenter presenter = BuildPresenterInternal(presenterFactory, failedPresenter.GetType(), candidate.GetType(), candidate);
                        newPresenters.Add(presenter);
                        PresenterStub.Add(presenter);// 添加到缓存中, 会将直接错误的进行覆盖
                    }
                } 
            }

            // 创建不存在缓存中的实例
            foreach (var presenterType in failedPresenterTypes)
            {
                IPresenter presenterInstance = BuildPresenterInternal(presenterFactory, presenterType, candidate.GetType(), candidate);
                newPresenters.Add(presenterInstance);// 添加到结果中
                PresenterStub.Add(presenterInstance);// 添加到缓存中
            }

            return newPresenters;
        }

        [Obsolete("暂时用不到【IEnumerable<PresenterBinding>】这种情况, 所以将其标记为过时")]
        /// <summary>
        ///  创建Presenter实例
        /// </summary>
        /// <param name="presenterCreatedCallback"></param>
        /// <param name="presenterFactory"></param>
        /// <param name="bindings"></param>
        /// <returns></returns>
        private static IPresenter BuildPresenter(
            Action<IPresenter> presenterCreatedCallback,// 创建Presenter成功之后的回调函数
            IPresenterFactory presenterFactory,// 创建Presenter的工厂, 会在匹配的Presenter类中找到一个和绑定View相同的接口的参数
            IEnumerable<PresenterBinding> bindings)// 找到的PresenterBinding集合, 一般来说只有一个
        {
            return bindings.Select(binding =>
                    BuildPresenterInternal(presenterFactory,binding))
                    .ToList().First();
        }

        /// <summary>
        /// 创建Presenter方法(通过PresenterBinding创建的方法)
        /// </summary>
        /// <param name="presenterCreatedCallback"></param>
        /// <param name="presenterFactory"></param>
        /// <param name="binding"></param>
        /// <returns></returns>
        private static IPresenter BuildPresenterInternal(
           IPresenterFactory presenterFactory,// 创建Presenter的工厂, 会在匹配的Presenter类中找到一个和绑定View相同的接口的参数
           PresenterBinding binding)// View 和 PresenterBinding 的对应关系
        {
            // 获取Presenter的实例
            return presenterFactory.Create(binding.PresenterType, binding.ViewLogicType, binding.ViewInstance);
        }

        /// <summary>
        /// 创建Presenter方法
        /// </summary>
        /// <param name="presenterFactory"></param>
        /// <param name="presenterType"></param>
        /// <param name="viewType"></param>
        /// <param name="viewInstnace"></param>
        /// <returns></returns>
        private static IPresenter BuildPresenterInternal(
            IPresenterFactory presenterFactory,
            Type presenterType,
            Type viewType,
            IView viewInstnace)
        {
            return presenterFactory.Create(presenterType, viewType, viewInstnace);
        }

        /// <summary>
        /// 查找绑定关系
        /// </summary>
        /// <param name="candidate"></param>
        /// <param name="presenterDiscoveryStrategy"></param>
        /// <returns></returns>
        private static IEnumerable<PresenterBinding> GetBinding(IView viewInstance,IPresenterDiscoveryStrategy presenterDiscoveryStrategy)
        {
            // 查找所有与View关联的Presenter数据
            PresenterDiscoveryResult result = presenterDiscoveryStrategy.GetBinding(viewInstance);
            ThrowExceptionsForViewsWithNoPresenterBound(result);

            return result.Bindings;
        }

        /// <summary>
        /// 如果结果不满足要求， 则抛出异常
        /// </summary>
        /// <param name="result"></param>
        private static void ThrowExceptionsForViewsWithNoPresenterBound(PresenterDiscoveryResult result)
        {
            if (result.Bindings.Empty() && result.ViewInstance.ThrowExceptionIfNoPresenterBound)

                throw new InvalidOperationException(string.Format(
                    CultureInfo.InvariantCulture,
                    @"Failed to find presenter for view instance of {0}.{1} If you do not want this exception to be thrown, set ThrowExceptionIfNoPresenterBound to false on your view.",
                    result.ViewInstance.GetType().FullName,
                    result.Message
                    ));
        }

    }
}
