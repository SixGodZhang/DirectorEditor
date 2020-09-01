using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector
{
    /// <summary>
    /// 包含创建一个实现类的上下文信息
    /// </summary>
    public class TypeFactoryContext
    {
        internal TypeFactoryContext(Type serviceType, InjectionConsumerInfo consumer)
        {
            this.ServiceType = serviceType;
            this.Consumer = consumer;
        }

        public Type ServiceType { get; }

        public InjectionConsumerInfo Consumer { get; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture,
            "{0}:{1}, {2}: {3})",
            nameof(this.ServiceType), this.ServiceType.ToFriendlyName(true),
            nameof(this.Consumer), this.Consumer);
    }
}
