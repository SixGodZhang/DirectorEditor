using System;
using System.Threading;

namespace FastInjector
{
    /// <summary>
    /// 依赖注入的的容器
    /// </summary>
    public class Container : IDisposable
    {
        private static long counter;// 计数器

        private readonly object locker = new object();// locker
        private readonly long containerId;//容器Id

        public Container()
        {
            this.containerId = Interlocked.Increment(ref counter);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
