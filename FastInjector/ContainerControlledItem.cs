using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector
{
    /// <summary>
    /// 容器控制的集合应该同时提供类型和直接注册
    /// </summary>
    internal sealed class ContainerControlledItem
    {
        /// <summary>
        /// 不能为null. 
        /// </summary>
        public readonly Type ImplementationType;

        /// <summary>
        /// 可以为null
        /// </summary>
        public readonly Registration Registration;

        private ContainerControlledItem(Registration registration)
        {
            Requires.IsNotNull(registration, nameof(registration));
            this.Registration = registration;
            this.ImplementationType = registration.ImplementationType;
        }

        private ContainerControlledItem(Type implementationType)
        {
            Requires.IsNotNull(implementationType, nameof(implementationType));
            this.ImplementationType = implementationType;
        }

        // 从注册类创建
        public static ContainerControlledItem CreateFromRegistration(Registration registration) =>
            new ContainerControlledItem(registration);

        // 根据类型创建
        public static ContainerControlledItem CreateFromType(Type implementationType) =>
            new ContainerControlledItem(implementationType);
    }
}
