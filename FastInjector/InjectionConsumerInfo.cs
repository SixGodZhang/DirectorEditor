using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector
{
    // 包含依赖注入的消费者的上下文信息
     public class InjectionConsumerInfo
    {
        internal static readonly InjectionConsumerInfo Root = null;

        public InjectionConsumerInfo(ParameterInfo parameter)
        {
            Requires.IsNotNull(parameter, nameof(parameter));

            this.Target = new InjectionTargetInfo(parameter);
            this.ImplementationType = parameter.Member.DeclaringType;
        }

        public InjectionConsumerInfo(Type implementationType, PropertyInfo property)
        {
            Requires.IsNotNull(implementationType, nameof(implementationType));
            Requires.IsNotNull(property, nameof(property));

            this.Target = new InjectionTargetInfo(property);
            this.ImplementationType = implementationType;
        }

        public Type ImplementationType { get; }

        public InjectionTargetInfo Target { get; }

        public override string ToString() =>
            "{ ImplementationType: " + this.ImplementationType.ToFriendlyName(true) +
            ", Target.Name: '" + this.Target.Name + "' }";


    }
}
