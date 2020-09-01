using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FastInjector
{
    /// <summary>
    /// 依赖注入的的容器
    /// </summary>
    public partial class Container : IDisposable
    {
        //cache
        internal readonly Dictionary<object, Dictionary<Type, WeakReference>> LifestyleRegistrationCache =
            new Dictionary<object, Dictionary<Type, WeakReference>>();

        private readonly List<IInstanceInitializer> instanceInitializers = new List<IInstanceInitializer>();
        private readonly List<ContextualResolveInterceptor> resolveInterceptors = new List<ContextualResolveInterceptor>();
        private readonly IDictionary items = new Dictionary<object, object>();

        private readonly Scope disposableSingletonsScope;

        private readonly Dictionary<Type, IRegistrationEntry> explictRegistrations = new Dictionary<Type, IRegistrationEntry>(64);

        private readonly Dictionary<Type, CollectionResolver> collectionResolvers = new Dictionary<Type, CollectionResolver>();

        // 这个list中包含所有还没有在容器中注册的producer实例
        private readonly ConditionalHashSet<InstanceProducer> externProducers = new ConditionalHashSet<InstanceProducer>();

        private readonly Dictionary<Type, InstanceProducer> unregisteredConcreteTypeInstanceProducers = new Dictionary<Type, InstanceProducer>();


        private bool locked;
        private string stackTraceThatLockedTheContainer;
        private bool disposed;
        private string stackTraceThatDisposedTheContainer;

        private EventHandler<UnregisterTypeEventArgs> resolveUnregisteredType;

        private static long counter;// 计数器

        private readonly object locker = new object();// locker
        private readonly long containerId;//容器Id

        public Container()
        {
            this.containerId = Interlocked.Increment(ref counter);
        }

        //
        private interface IInstanceInitializer
        {
            bool AppliesTo(Type implementaionType, InitializerContext context);
            Action<T> CreateAction<T>(InitializerContext context);
        }

        private sealed class ContextualResolveInterceptor
        {
            public readonly ContextualResolveInterceptor Interceptor;
            public readonly Predicate<InitializerContext> Predicate;// 检测InitializerContext是否符合条件

            public ContextualResolveInterceptor(ContextualResolveInterceptor interceptor,Predicate<InitializerContext> predicate)
            {
                this.Interceptor = interceptor;
                this.Predicate = predicate;
            }
        }

        internal void LockContainer()
        {
            if(!this.locked)
            {
                this.FlagContainerAsLocked();
            }
        }

        private void FlagContainerAsLocked()
        {
            lock(this.locker)
            {
                GetStackTrace(ref this.stackTraceThatLockedTheContainer);
                this.locked = true;
            }
        }

        static partial void GetStackTrace(ref string stackTrace);


        internal void ThrowWhenDisposed()
        {
            if (this.disposed)
            {
                this.ThrowConstainerDisposedException();
            }
        }

        internal bool HasRegistrations => this.explictRegistrations.Any() || this.collectionResolvers.Any();


        private void ThrowContainerDisposedException()
        {
            throw new ObjectDisposedException(
                objectName: null,
                message: StringResources.ContainerCanNotBeUsedAfterDisposal(this.GetType(),
                this.stackTraceThatDisposedTheContainer));
        }

        // ThrowWhenContainerIsLockedOrDisposed
        internal void ThrowWhenContainerIsLockedOrDisposed()
        {
            this.ThrowWhenDisposed();

            lock(this.locker)
            {
                if(this.locked)
                {
                    throw new InvalidOperationException(StringResources.ContainerCanNotBeChangedAfterUse(
                        this.stackTraceThatLockedTheContainer));
                }
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
