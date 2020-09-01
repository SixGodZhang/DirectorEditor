using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector
{
    public sealed class PredicateContext
    {
        private readonly Func<Type> implementationTypeProvider;
        private Type implementationType;

        internal PredicateContext(InstanceProducer producer, InjectionConsumerInfo consumer, bool handled):
            this(producer.ServiceType, producer.Registration.ImplementationType, consumer, handled)
        {

        }

        internal PredicateContext(Type serviceType, Type implementationType, InjectionConsumerInfo consumer, bool handled)
        {
            this.ServiceType = serviceType;
            this.implementationType = implementationType;
            this.Consumer = consumer;
            this.Handled = handled;
        }

        internal PredicateContext(Type serviceType, Func<Type> implementationTypeProvider, InjectionConsumerInfo consumer, bool handled)
        {
            this.ServiceType = serviceType;
            this.implementationTypeProvider = implementationTypeProvider;
            this.Consumer = consumer;
            this.Handled = handled;
        }

        /// <summary>
        /// 获取已经关闭的的服务
        /// </summary>
        public Type ServiceType { get; }

        /// <summary>
        /// 获取已关闭的服务实现类型
        /// </summary>
        public Type ImplementationType
        {
            get
            {
                if (this.implementationType == null)
                {
                    this.implementationType = this.implementationTypeProvider();
                }

                return this.implementationType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Handled { get; }

        public InjectionConsumerInfo Consumer { get; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture,
            "{0}:{1}, {2}:{3},{4}:{5},{6}:{7}",
            nameof(this.ServiceType), this.ServiceType.ToFriendlyName(true),
            nameof(this.ImplementationType), this.ImplementationType.ToFriendlyName(true),
            nameof(this.Handled), this.Handled,
            nameof(this.Consumer), this.Consumer);
    }
}
