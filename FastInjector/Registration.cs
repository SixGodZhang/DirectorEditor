using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector
{
    /// <summary>
    /// Registration 基于lifestyle 缓存了单个服务， 并且允许构建一个Expression用来描述服务的创建过程
    /// Lifestyle: 为每一个已经注册的服务创建一个Registration实例.
    /// </summary>
    public abstract class Registration
    {
        private static readonly Action<object> NoOp = instance => { };
        private readonly HashSet<KnownRelationship> knownRelationships = new HashSet<KnownRelationship>();
        private ParameterDictionary<OverriddenParameter> overriddenParameters;
        private Action<object> instanceInitializer;

        protected Registration(Lifestyle lifestyle, Container container)
        {
            Requires.IsNotNull(lifestyle, nameof(lifestyle));
            Requires.IsNotNull(container, nameof(container));

            this.Lifestyle = lifestyle;
            this.Container = container;
        }

        /// <summary>
        /// 获取Lifestyle 可以创建注册器
        /// </summary>
        public Lifestyle Lifestyle { get; }

        /// <summary>
        /// 获取此注册器的容器实例
        /// </summary>
        public Container Container { get; }

        /// <summary>
        /// 获取将要创建的实例类型
        /// </summary>
        public Type ImplementationType;

        internal bool IsCollection { get; set; }

        internal virtual bool MustBeVerified => false;

        /// <summary>
        /// 此属性用来声明注册机是否包含一个isntanceCreator委托对象
        /// </summary>
        internal bool WrapsInstanceCreationDelegate { get; set; }

        public abstract Expression BuildExpression();

        public KnownRelationship[] GetRelationshipsO() => this.GetRelationshipsCore();

        public void InitializeInstance(object instance)
        {
            Requires.IsNotNull(instance, nameof(instance));
            Requires.ServiceIsAssignableFromImplementation(this.ImplementationType, instance.GetType(), nameof(instance));

            if (this.instanceInitializer == null)
            {
                this.instanceInitializer = this.BuildInstanceInitializer();
            }

            this.instanceInitializer(instance);
        }

        private Action<object> BuildInstanceInitializer()
        {
            Type type = this.ImplementationType;
            var parameter = Expression.Parameter(typeof(object));
            var castedParameter = Expression.Convert(parameter, type);

            Expression expression = castedParameter;

            expression = this.Wrap
        }

        /// <summary>
        /// 包装表达式, 并注入一些属性
        /// </summary>
        /// <param name="implementationType"></param>
        /// <param name="expressionToWrap"></param>
        /// <returns></returns>
        internal Expression WrapWithPropertyInjector(Type implementationType, Expression expressionToWrap)
        {
            if (this.Container.Options.PropertySelectionBehavior is DefaultPropertySelectionBehavior)
            {
                return expressionToWrap;
            }

            if (typeof(Container).IsAssignableFrom(implementationType))
            {
                return expressionToWrap;
            }

            return this.WrapWithPropertyInjectorInternal(implementationType, expressionToWrap);
        }

        private Expression WrapWithPropertyInjectorInternal(Type implementationType, Expression expressionToWrap)
        {
            PropertyInfo[] properties = this.GetPropertiesToInject(implementationType);
            if (properties.Any())
            {
                PropertyInjectionHelper.VerifyProperties(properties);
                var data = PropertyInjectorHelper.BuildPropertyInjectionExpression(
                    this.Container, implementationType, properties, expressionToWrap);

                expressionToWrap = data.Expression;
                this.AddRelationships(implementationType, data.Producers);
            }
            return expressionToWrap;
        }

        private PropertyInfo[] GetPropertiesToInject(Type implementationType)
        {
            var propertySelector = this.Container.Options.PropertySelectionBehavior;
            var candidates = PropertyInjectionHelper.GetCandidateInjectionPropertiesFor(implementationType);
            return candidates.Length == 0
                ? candidates
                : candidates.Where(p => propertySelector.SelectProperty(implementationType, p)).ToArray();
        }

        internal virtual KnownRelationship[] GetRelationshipsCore()
        {
            lock(this.knownRelationships)
            {
                return this.knownRelationships.ToArray();
            }
        }


    }
}
