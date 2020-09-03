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
        private static IPresenterDiscoveryStrategy discoveryStrategy;
        public static IPresenterDiscoveryStrategy DiscoveryStrategy
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

        public PresenterBinder()
        {

        }

        /// <summary>
        /// 绑定ViewInstance
        /// </summary>
        /// <param name="viewInstance"></param>
        public void PerformBinding(IView viewInstance)
        {
            try
            {
                IPresenter presenter = PerformBinding(viewInstance, DiscoveryStrategy,Factory);
                OnPresenterCreated(new PresenterCreatedEventArgs(presenter));// Presenter创建完成
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
        /// 绑定
        /// </summary>
        /// <param name="candidate">viewInstance</param>
        /// <param name="presenterDiscoveryStrategy"></param>
        /// <param name="presenterCreatedCallback"></param>
        /// <param name="presenterFactory"></param>
        /// <returns></returns>
        private static IPresenter PerformBinding(
            IView candidate,
            IPresenterDiscoveryStrategy presenterDiscoveryStrategy,
            IPresenterFactory presenterFactory)
        {
            // 获取candidate所有的绑定信息
            // 绑定信息包含View和Predicate
            PresenterBinding presenterBinding = GetBinding(candidate,presenterDiscoveryStrategy);

            IPresenter newPresenter = null;
            bool canFindFromCacheStub = false;// 是否可以从缓存中找到对应实例

            // 这里现在PresenterStub中查找， 看是否可以找到实例, 如果可以找到, 则就不需要实例化了
            newPresenter = PresenterStub.Get(presenterBinding.PresenterType);
            // 如果在缓存中找到了此类型的Presenter， 清理掉它与View建立的连接
            if (newPresenter!= null && newPresenter.PresenterType == PresenterType.ModelView 
                && (newPresenter.PresenterStatus == PresenterStatus.Inited || newPresenter.PresenterStatus == PresenterStatus.OnlyDataAfterClear))
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
                        viewPropertyInfo.SetValue(newPresenter, presenterBinding.ViewInstance);
                    }
                    // set PresenterStatus Property
                    PropertyInfo statusPropertyInfo = newPresenter.GetType().GetProperty("PresenterStatus");
                    if (statusPropertyInfo != null && viewPropertyInfo.CanWrite)
                    {
                        statusPropertyInfo.SetValue(newPresenter, PresenterStatus.Initing);
                    }
                    canFindFromCacheStub = true;
                }
                catch(Exception ex)
                {
                    canFindFromCacheStub = false;
                }

            }

            if(!canFindFromCacheStub)// 如果在缓存中找不到此类型的Presenter, 则重新创建一个
            {
                newPresenter = BuildPresenterInternal(
                    presenterFactory,
                    presenterBinding);

                PresenterStub.Add(newPresenter);// 添加Presenter到Stub中去, 如果已存在, 则覆盖
            }
            return newPresenter;
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
        /// 创建Presenter方法
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
            return presenterFactory.Create(binding.PresenterType, binding.ViewType, binding.ViewInstance);
        }

        /// <summary>
        /// 查找绑定关系
        /// </summary>
        /// <param name="candidate"></param>
        /// <param name="presenterDiscoveryStrategy"></param>
        /// <returns></returns>
        private static PresenterBinding GetBinding(IView candidate,
            IPresenterDiscoveryStrategy presenterDiscoveryStrategy)
        {
            PresenterDiscoveryResult result = presenterDiscoveryStrategy.GetBinding(candidate);
            ThrowExceptionsForViewsWithNoPresenterBound(result);

            return result.Bindings.Single();
        }

        /// <summary>
        /// 如果结果不满足要求， 则抛出异常
        /// </summary>
        /// <param name="result"></param>
        private static void ThrowExceptionsForViewsWithNoPresenterBound(PresenterDiscoveryResult result)
        {
            if (result.Bindings.Empty() && result.ViewInstances.Where(v => v.ThrowExceptionIfNoPresenterBound).Any())

                throw new InvalidOperationException(string.Format(
                    CultureInfo.InvariantCulture,
                    @"Failed to find presenter for view instance of {0}.{1} If you do not want this exception to be thrown, set ThrowExceptionIfNoPresenterBound to false on your view.",
                    result.ViewInstances.Where(v => v.ThrowExceptionIfNoPresenterBound).Single().GetType().FullName,
                    result.Message
                    ));
        }

    }
}
