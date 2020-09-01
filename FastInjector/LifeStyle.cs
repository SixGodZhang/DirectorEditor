using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace FastInjector
{
    /// <summary>
    /// 工厂，用于创建将缓存应用于所提供的委托
    /// </summary>
    /// <param name="transientInstanceCreator"></param>
    /// <returns></returns>
    public delegate Func<object> CreateLifestyleApplier(Func<object> transientInstanceCreator);

    /// <summary>
    /// Instances returned from the container can be cached.
    /// 可以缓存从容器返回的实例
    /// Lifestyle.Transient 表示注册的实例是暂时的。 在每次请求或者注入的时候，都会创建一个新的实例
    /// Lifestyle.Singleton 表示注册的实例可以被无限期的缓存先来。
    /// Lifestyle.CreateCustom 允许定义一个自定的lifestyle
    /// Lifestyle.CreateHybrid 允许创建一个lifestyle（由其它的lifestyole组合）
    /// 这是一个抽象类。可以被自定义的lifestyle实现。
    /// </summary>
    public abstract class Lifestyle
    {
        /// <summary>
        /// 这种lifestyle类型的实例不会被缓存。 在每次请求注册服务的时候都会创建一个新的实例
        /// <code lang ="cs"><![CDATA[
        /// var container = new Container();
        /// container.Register<ISomeService, SomeServiceImpl>(Lifestyle.Transient);
        /// ]]></code>
        /// 注意: Transient 是默认的lifestyle, 所以上面的代码可以简化为
        /// <code lang ="cs"><![CDATA[
        /// var container = new Container();
        /// // Transient registration.
        /// container.Register<ISomeService, SomeServeiceImple>();
        /// ]]></code>
        /// </summary>
        public static readonly Lifestyle Transient = new TransientLifestyle();

        /// <summary>
        /// 这种lifestyle可以通过容器配置来实现
        /// <code lang ="cs"><![CDATA[
        /// // Create a Container instance, configured with a scoped lifestyle.
        /// var container = new Container(new WebRequestLifestyle());
        /// 
        /// container.Register<ITimeProvider, RealTimeProvider>(Lifestyle.Scoped);
        /// ]]></code>
        /// </summary>
        public static readonly ScopedLifestyle Scoped = new ScopedProxyLifestyle();

        /// <summary>
        /// 这种lifestyle可以被缓存 并且 在容器的整个生命周期中只有一个实例.
        /// <code lang="cs"><![CDATA[
        /// var container = new Container();
        /// container.Register<ITimeProvider, RealTimeProvider>(Lifestyle.Singleton);
        /// ]]></code>
        /// </summary>
        public static readonly Lifestyle Singleton = new SingletonLifestyle();

        /// <summary>
        /// 未定义的Lifestyle
        /// </summary>
        public static readonly Lifestyle Unknown = new UnknownLifestyle();

        private static readonly MethodInfo OpenCreateRegistrationTConcreteMethod =
            GetMethod(lifestyle => lifestyle.CreateRegistration<object>(null));

        private static readonly MethodInfo OpenCreateRegistrationCoreTConcreteMethod =
            GetMethod(lifestyle => lifestyle.CreateRegistrationCore<object>(null));

        private static readonly MethodInfo OpenCreateRegistrationTServiceFuncMethod =
            GetMethod(lifestyle => lifestyle.CreateRegistration<object>(null, null));

        protected Lifestyle(string name)
        {
            Requires.IsNotNullOrEmpty(name, nameof(name));
            this.Name = name;
            this.IdentificationKey = new { Type = this.GetType(), Name = name };
        }

        /// <summary>
        ///  获取lifestyle的Name
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 获取lifestyle的长度。实现者必须实现此属性。诊断服务使用此值相互比较lifestyle以确定使用哪个lifestyle
        /// </summary>
        public abstract int Length { get; }
        /// <summary>
        /// 标志身份的唯一id
        /// </summary>
        internal object IdentificationKey { get; }

        internal virtual int ComponentLength(Container container) => this.Length;
        internal virtual int DependencyLength(Container container) => this.Length;

        /// <summary>
        /// The hybrid lifestyle allows mixing two lifestyles in a single registration. 
        /// Hybrid lifestyle 允许在一次注册中组合2种lifestyle
        /// </summary>
        /// <param name="defaultLifestyle"></param>
        /// <param name="fallbackLifestyle"></param>
        /// <code lang="cs"><![CDATA[
        /// //NOTE: WebRequestLifestyle is located in SimpleInjector.Integration.Web.dll.
        /// var hybridLifestyle = Lifestyle.CreateHybrid(
        ///     defaultLifestyle: new ThreadScopedLifestyle(),
        ///     fallbackLifestyle: Lifestyle.Transient);
        ///     
        /// // The create lifestyle can be reused for many registrations.
        /// container.Register<IUserRepository, SqlUserRepository>(hybridLifestyle);
        /// container.Register<ICustomerRepository, SqlCustomerRepository>(hybridLiefsytle);
        /// ]]></code>
        /// Hybrid lifestyles 可以相互嵌套
        /// <code lang="cs"><![CDATA[
        /// var mixedThreadScopedTransientLifestyle = Lifestyle.CreateHybrid(
        ///     new ThreadScopedLifestyle(),
        ///     Lifestyle.Transient);
        ///     
        /// var hybridLifestyle = Lifestyle.CreateHybrid(
        ///     new WebRequestLifestyle(),
        ///     mixedThreadScopedTransientLifestyle);
        /// ]]></code>
        /// 上述代码组合了三种lifestyle: Web Request, Thread Scoped and Transient
        /// <returns></returns>
        public static Lifestyle CreateHybrid(ScopedLifestyle defaultLifestyle, Lifestyle fallbackLifestyle)
        {
            Requires.IsNotNull(defaultLifestyle, nameof(defaultLifestyle));
            Requires.IsNotNull(fallbackLifestyle, nameof(fallbackLifestyle));

            return new HybridLifestyle(
                lifestyleSelector: container => defaultLifestyle.GetCurrentScope(container)!=null,
                trueLifestyle:defaultLifestyle,
                falseLifestyle:fallbackLifestyle
                );
        }

        public static ScopedLifestyle CreateHybrid(ScopedLifestyle defaultLifestyle, ScopedLifestyle fallbackLifestyle)
        {
            Requires.IsNotNull(defaultLifestyle, nameof(defaultLifestyle));
            Requires.IsNotNull(fallbackLifestyle, nameof(fallbackLifestyle));
            return new DefaultFallbackScopedHybridLifestyle(
                defaultLifestyle: defaultLifestyle,
                fallbackLifestyle: fallbackLifestyle
                );
        }

        /// <summary>
        /// 根据条件判定使用哪个lifestyle
        /// </summary>
        /// <param name="lifesytleSelector"></param>
        /// <param name="trueLifestyle"></param>
        /// <param name="falseLifestyle"></param>
        /// <example>
        /// <para>
        /// The following example shows the creation of a <b>HybridLifestyle</b> that mixes an 
        /// <b>WebRequestLifestyle</b> and <b>ThreadScopedLifestyle</b>:
        /// </para>
        /// <code lang="cs"><![CDATA[
        /// // NOTE: WebRequestLifestyle is located in SimpleInjector.Integration.Web.dll.
        /// var mixedScopeLifestyle = Lifestyle.CreateHybrid(
        ///     () => HttpContext.Current != null,
        ///     new WebRequestLifestyle(),
        ///     new ThreadScopedLifestyle());
        /// 
        /// // The created lifestyle can be reused for many registrations.
        /// container.Register<IUserRepository, SqlUserRepository>(mixedScopeLifestyle);
        /// container.Register<ICustomerRepository, SqlCustomerRepository>(mixedScopeLifestyle);
        /// ]]></code>
        /// <para>
        /// Hybrid lifestyles can be nested:
        /// </para>
        /// <code lang="cs"><![CDATA[
        /// var lifestyle = new ThreadScopedLifestyle();
        /// var mixedLifetimeTransientLifestyle = Lifestyle.CreateHybrid(
        ///     () => lifestyle.GetCurrentScope(container) != null,
        ///     lifestyle,
        ///     Lifestyle.Transient);
        /// 
        /// var mixedScopeLifestyle = Lifestyle.CreateHybrid(
        ///     () => HttpContext.Current != null,
        ///     new WebRequestLifestyle(),
        ///     mixedLifetimeTransientLifestyle);
        /// ]]></code>
        /// <para>
        /// The <b>mixedScopeLifestyle</b> now mixed three lifestyles: Web Request, Lifetime Scope and 
        /// Transient.
        /// </para>
        /// </example>
        /// <returns></returns>
        public static Lifestyle CreateHybrid(Func<bool> lifestyleSelector, Lifestyle trueLifestyle, Lifestyle falseLifestyle)
        {
            Requires.IsNotNull(lifestyleSelector, nameof(lifestyleSelector));
            Requires.IsNotNull(trueLifestyle, nameof(trueLifestyle));
            Requires.IsNotNull(falseLifestyle, nameof(falseLifestyle));

            return new HybridLifestyle(c => lifestyleSelector(), trueLifestyle, falseLifestyle);

        }

        /// <summary>
        /// 根据条件判定使用哪个lifestyle
        /// </summary>
        /// <param name="lifestyleSelector"></param>
        /// <param name="trueLifestyle"></param>
        /// <param name="falseLifestyle"></param>
        /// <returns></returns>
        /// <example>
        /// <para>
        /// The following example shows the creation of a <b>HybridLifestyle</b> that mixes an 
        /// <b>WebRequestLifestyle</b> and <b>ThreadScopedLifestyle</b>:
        /// </para>
        /// <code lang="cs"><![CDATA[
        /// // NOTE: WebRequestLifestyle is located in SimpleInjector.Integration.Web.dll.
        /// var mixedScopeLifestyle = Lifestyle.CreateHybrid(
        ///     () => HttpContext.Current != null,
        ///     new WebRequestLifestyle(),
        ///     new ThreadScopedLifestyle());
        /// 
        /// // The created lifestyle can be reused for many registrations.
        /// container.Register<IUserRepository, SqlUserRepository>(mixedScopeLifestyle);
        /// container.Register<ICustomerRepository, SqlCustomerRepository>(mixedScopeLifestyle);
        public static ScopedLifestyle CreateHybrid(Func<bool> lifestyleSelector, ScopedLifestyle trueLifestyle, ScopedLifestyle falseLifestyle)
        {
            Requires.IsNotNull(lifestyleSelector, nameof(lifestyleSelector));
            Requires.IsNotNull(trueLifestyle, nameof(trueLifestyle));
            Requires.IsNotNull(falseLifestyle, nameof(falseLifestyle));

            return new LifestyleSelectorScopedHybridLifestyle(c => lifestyleSelector(), trueLifestyle, falseLifestyle);
        }

        /// <summary>
        /// 创建自定义的lifestyle
        /// lifestyleApplierFactory 在每次注册服务的时候都会被调用。。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lifestyleApplierFactory"></param>
        /// <returns></returns>
        public static Lifestyle CreateCustom(string name, CreateLifestyleApplier lifestyleApplierFactory)
        {
            Requires.IsNotNullOrEmpty(name, nameof(name));
            Requires.IsNotNull(lifestyleApplierFactory, nameof(lifestyleApplierFactory));
            return new CustomLifestyle(name, lifestyleApplierFactory);
        }

        /// <summary>
        /// 创建服务提供者
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="container"></param>
        /// <returns></returns>
        public InstanceProducer<TService> CreateProducer<TService, TImplementation>(Container container)
            where TImplementation: class, TService
            where TService : class
        {
            return new InstanceProducer<TService>(this.CreateRegistration<TImplementation>(container));
        }

        public InstanceProducer<TService> CreateProducer<TService>(Type implementationType, Container container)
            where TService:class
        {
            Requires.IsNotNull(implementationType, nameof(implementationType));
            Requires.IsNotNull(container, nameof(container));

            Requires.IsNotOpenGenericType(implementationType, nameof(implementationType));
            Requires.ServiceIsAssignableFromImplementation(typeof(TService), implementationType, nameof(implementationType));

            return new InstanceProducer<TService>(this.CreateRegistration(implementationType, container));
        }

        public Registration CreateRegistration(Type concreteType, Container container)
        {
            Requires.IsNotNull(concreteType, nameof(concreteType));
            Requires.IsNotNull(container, nameof(container));

            Requires.IsReferenceType(concreteType, nameof(concreteType));
            Requires.IsNotOpenGenericType(concreteType, nameof(concreteType));

            return this.CreateRegistrationInternal(concreteType, container, preventTornLifestyles: true);
        }

        public Registration CreateRegistration(Type serviceType, Type implementationType, Container container)
        {
            Requires.IsNotNull(implementationType, nameof(implementationType));

            return this.CreateRegistration(implementationType, container);
        }

        public Registration CreateRegistration(Type serviceType, Func<object> instanceCreator, Container container)
        {
            Requires.IsNotNull(serviceType, nameof(serviceType));
            Requires.IsNotNull(instanceCreator, nameof(instanceCreator));
            Requires.IsNotNull(container, nameof(container));

            Requires.IsReferenceType(serviceType, nameof(serviceType));
            Requires.IsNotOpenGenericType(serviceType, nameof(serviceType));

            var closedCreateRegistrationMethod = OpenCreateRegistrationTServiceFuncMethod.MakeGenericMethod(serviceType);

            try
            {
                var typeSafeInstanceCreator = ConvertDelegateToTypeSafeDelegate(serviceType, instanceCreator);
                return (Registration)closedCreateRegistrationMethod.Invoke(this, new object[] { typeSafeInstanceCreator, container });
            }catch(MemberAccessException ex)
            {
                throw BuildUnableToResolveTypeDueToSecurityConfigException(serviceType, ex, nameof(serviceType));
            }
        }

        private static object ConvertDelegateToTypeSafeDelegate(Type serviceType, Func<object> instanceCreator)
        {
            var invocationExpression =
                Expression.Invoke(Expression.Constant(instanceCreator), Helpers.Array<Expression>.Empty);
            var convertExpression = Expression.Convert(invocationExpression, serviceType);

            return Expression.Lambda(convertExpression, Helpers.Array<ParameterExpression>.Empty).Compile();
        }


        public Registration CreateRegistration<TConcrete>(Container container) where TConcrete : class
        {
            Requires.IsNotNull(container, nameof(container));
            return this.CreateRegistrationInternal<TConcrete>(container, preventTornLifestyles: true);
        }

        public Registration CreateRegistration<TService>(Func<TService> instanceCreator, Container container)
            where TService:class
        {
            Requires.IsNotNull(instanceCreator, nameof(instanceCreator));
            Requires.IsNotNull(container, nameof(container));

            var registration = this.CreateRegistrationCore<TService>(instanceCreator, container);
            registration.WrapsInstanceCreationDelegate = true;
            return registration;
        }

        /// <summary>
        /// 泛型类创建注册机
        /// </summary>
        /// <typeparam name="TConcrete"></typeparam>
        /// <param name="container"></param>
        /// <param name="preventTornLifestyles"></param>
        /// <returns></returns>
        internal Registration CreateRegistrationInternal<TConcrete>(Container container, bool preventTornLifestyles)
            where TConcrete : class =>
            preventTornLifestyles
            ? this.CreateRegistrationFromCache<TConcrete>(container)
            : this.CreateRegistrationCore<TConcrete>(container);

        /// <summary>
        /// 创建注册机
        /// </summary>
        /// <param name="concreteType"></param>
        /// <param name="container"></param>
        /// <param name="preventTornLifestyles"></param>
        /// <returns></returns>
        private Registration CreateRegistrationInternal(Type concreteType, Container container, bool preventTornLifestyles)
        {
            var closedCreateRegistrationMethod = preventTornLifestyles
                ? OpenCreateRegistrationTConcreteMethod.MakeGenericMethod(concreteType)
                : OpenCreateRegistrationCoreTConcreteMethod.MakeGenericMethod(concreteType);

            try
            {
                return (Registration)closedCreateRegistrationMethod.Invoke(this, new object[] { container });
            }catch(MemberAccessException ex)
            {
                throw BuildUnableToResolveTypeDueToSecurityConfigException(concreteType, ex,
                    nameof(concreteType));
            }
        }

        /// <summary>
        /// 从缓存中创建Resgistration
        /// </summary>
        /// <typeparam name="TConcrete"></typeparam>
        /// <param name="container"></param>
        /// <returns></returns>
        private Registration CreateRegistrationFromCache<TConcrete>(Container container) where TConcrete:class
        {
            lock(container.LifestyleRegistrationCache)
            {
                WeakReference weakRegistration = this.GetLifestyleRegistrationEntryFromCache(typeof(TConcrete), container);
                var registration = (Registration)weakRegistration.Target;

                if (registration == null)
                {
                    registration = this.CreateRegistrationCore<TConcrete>(container);
                    weakRegistration.Target = registration;
                }

                return registration;
            }
        }

        /// <summary>
        /// 从缓存中获取Registration
        /// </summary>
        /// <param name="concreteType"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        private WeakReference GetLifestyleRegistrationEntryFromCache(Type concreteType, Container container)
        {
            var lifestyleCache = container.LifestyleRegistrationCache;

            Dictionary<Type, WeakReference> registrationCache;

            // 判断缓存中是否有此concreteType, 没有则新建
            if (!lifestyleCache.TryGetValue(this.IdentificationKey, out registrationCache))
            {
                registrationCache = new Dictionary<Type, WeakReference>(100);
                lifestyleCache[this.IdentificationKey] = registrationCache;
            }

            WeakReference weakRegistration;
            if (!registrationCache.TryGetValue(concreteType, out weakRegistration))
            {
                registrationCache[concreteType] = weakRegistration = new WeakReference(null);
            }

            return weakRegistration;

        }

        
        protected internal abstract Registration CreateRegistrationCore<TConcrete>(Container container)
            where TConcrete : class;

        protected internal abstract Registration CreateRegistrationCore<TService>(Func<TService> instanceCreator, Container container)
            where TService : class;

        /// <summary>
        /// 创建一个Registration实例。
        /// </summary>
        /// <param name="methodCall"></param>
        /// <returns></returns>
        private static MethodInfo GetMethod(Expression<Action<Lifestyle>> methodCall)
        {
            var body = methodCall.Body as MethodCallExpression;
            return body.Method.GetGenericMethodDefinition();
        }

        private static ArgumentException BuildUnableToResolveTypeDueToSecurityConfigException(
            Type type, MemberAccessException innerException, string paramName)
        {
            // This happens when the user tries to resolve an internal type inside a (Silverlight) sandbox.
            return new ArgumentException(
                StringResources.UnableToResolveTypeDueToSecurityConfiguration(type, innerException) +
                Environment.NewLine + "paramName: " + paramName, innerException);
        }


    }
}
