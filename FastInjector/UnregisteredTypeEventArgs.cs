using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector
{
    /// <summary>
    /// 为Constainer的ResolveUnregisteredType事件提供数并与之交互
    /// observer(观察者) 通过检测UnregisteredType确定未注册的类型是否被处理。
    /// 被处理的Register方法可以被调用去注册一个委托, 用来
    /// </summary>
    public class UnregisteredTypeEventArgs : EventArgs
    {
        internal UnregisteredTypeEventArgs(Type unregisteredServiceType)
        {
            this.UnregisteredServiceType = unregisteredServiceType;
        }

        /// <summary>
        /// 获取当前请求的但是还没有注册的服务类型
        /// </summary>
        public Type UnregisteredServiceType { get; }

        //获取一个值, 代表此实例是否已经被处理
        public bool Handled => this.Expression != null || this.Registration != null;

        internal Expression Expression { get; private set; }

        internal Registration Registration { get;private set;}

        public void Register(Func<object> instanceCreator)
        {
            Requires.IsNotNull(instanceCreator, nameof(instanceCreator));
            this.RequiresNotHandled();

            this.Expression =
                Expression.Call(typeof(UnregisteredTypeEventArgsCallHelper).GetMethod("GetInstance")
                .MakeGenericMethod(this.UnregisteredServiceType),
                Expression.Constant(instanceCreator));
        }

        public void Register(Registration registration)
        {
            Requires.IsNotNull(registration, nameof(registration));
            Requires.ServiceIsAssignableFromRegistration(this.UnregisteredServiceType, registration, nameof(registration));

            this.RequiresNotHandled();

            this.Registration = registration;
        }

        // 服务必须是没有被处理过的
        private void RequiresNotHandled()
        {
            if (this.Handled)
            {
                throw new ActivationException(
                    StringResources.MultipleObserversRegisteredTheSameTypeToResolveUnregisteredType(this.UnregisteredServiceType));
            }
        }

        internal static class UnregisteredTypeEventArgsCallHelper
        {
            public static TService GetInstance<TService>(Func<object> instanceCreator)
            {
                object instance;
                try
                {
                    instance = instanceCreator();
                }catch (Exception ex)
                {
                    throw new ActivationException(
                        StringResources.UnregisteredTypeEventArgsRegisterDelegateThrewAnException(
                            typeof(TService), ex), ex);
                }

                try
                {
                    return (TService)instance;
                }catch(InvalidCastException ex)
                {
                    throw new InvalidCastException(
                        StringResources.UnregisteredTypeEventArgsRegisterDelegateReturnedUncastableInstance(
                            typeof(TService), ex), ex
                        );
                }
            }
        }



    }
}
