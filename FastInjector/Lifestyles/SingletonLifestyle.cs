using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector.Lifestyles
{
    internal sealed class SingletonLifestyle:Lifestyle
    {
        internal SingletonLifestyle():base("Singleton")
        {

        }

        public override int Length => 1000;


        internal static Registration CreateSingleInstanceRegistration(Type serviceType, object instance, Container container, Type implementationType = null)
        {
            Requires.IsNotNull(instance, nameof(instance));
            return new SingletonInstanceLifestyleRegistration(serviceType, implementationType ?? serviceType, instance, container);
        }

        protected internal override Registration CreateRegistrationCore<TConcrete>(Container container) =>
            new SingletonLifestyleRegistration<TConcrete>(container);

        protected internal override Registration CreateRegistrationCore<TService>(Func<TService> instanceCreator, Container container)
        {
            Requires.IsNotNull(instanceCreator, nameof(instanceCreator));

            return new SingletonLifestyleRegistration<TService>(container, instanceCreator);
        }

        private sealed class SingletonLifestyleRegistration<TImplementation>:Registration
            where TImplementation :class
        {
            private readonly object locker = new object();
            private readonly Func<TImplementation> instanceCreator;
            private object interceptedInstance;

            public SingletonLifestyleRegistration(Container container, Func<TImplementation> instanceCreator = null)
                :base(Lifestyle.Singleton, container)
            {
                this.instanceCreator = instanceCreator;
            }

            public override Type Implementation => typeof(TImplementation);

            public override Expression BuildExpression() =>
                Expression.Constant(this.GetInterceptedInstance(), this.ImplementationType);

            private object GetInterceptedInstance()
            {
                if (this.interceptedInstance == null)
                {
                    lock(this.locker)
                    {
                        if (this.interceptedInstance == null)
                        {
                            this.interceptedInstance = this.CreateInstanceWithNullCheck();
                            var disable = this.interceptedInstance as IDisposable;
                            if (disable != null)
                            {
                                this.Container.RegisterForDisposal(disposable);
                            }
                        }
                    }
                }

                return this.interceptedInstance;
            }

            private TImplementation CreateInstanceWithNullCheck()
            {
                Expression expression = this.instanceCreator == null
                    ? this.BuildTransientExpression()
                    : this.BuildTransientExpressiong(this.instanceCreator);

                Func<TImplementation> func = CompileExpressiong(expressiong);
                TImplementation instance = func();
                EnsureInstanceIsNotNull(instance);
                return instance;
            }

            private static Func<TImplementation> CompileExpression(Expression expression)
            {
                try
                {
                    return CompilationHelpers.CompileLambda<TImplementation>(expression);
                }catch(Exception ex)
                {
                    string message = StringResources.ErrorWhileBuildingDelegateFromExpression(typeof(TImplementation), expression, ex);

                    throw new ActivationException(message, ex);
                }
            }

            private static void EnsureInstanceIsNotNull(object instance)
            {
                if(instance == null)
                {
                    throw new ActivationException(StringResources.DelegateForTypeReturnedNull(typeof(TImplementation);
                }
            }


        }
    }
}
