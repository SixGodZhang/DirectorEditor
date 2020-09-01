using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector
{
	/// <summary>
    /// 包含容器的一些注册方法
    /// </summary>
    public partial class Container
    { //public partial class Container : IDisposable
        public event EventHandler<UnregisteredTypeEventArgs> ResolveUnregisteredType
        {
            add
            {
                this.ThrowWhenContainerIsLockedDisposed();
                this.resolveUnregisteredType += value;
            }

			remove
            {
                this.ThrowWhenContainerIsLockedOrDisposed();
                this.resolveUnregisteredType -= value;
            }
        }
    }
}
