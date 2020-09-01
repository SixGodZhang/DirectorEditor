using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector.Lifestyles
{
    internal sealed class TransientLifestyle:Lifestyle
    {
        internal TransientLifestyle(): base("Transient")
        {

        }

        public override int Length => 1;

        protected internal override Registration CreateRegistrationCore<TConcrete>(Container container)
        {
            return new TransientLifestyleRegistration<TConcrete>(this, container);
        }

        protected internal override Registration CreateRegistrationCore<TService>(Func<TService> instanceCreator, Container container)
        {
            return new TransientLifestyleRegistration<TService>(this, container, instanceCreator);
        }

        private sealed class TransientLifestyleRegistration<TImplementation>:Registration
            where TImplementation:class
        {
            private readonly Func<TImplementation> instanceCreator;
            public TransientLifestyleRegistration(Lifestyle lifestyle, Container container, Func<TImplementation> instanceCreator = null)
                :base(lifestyle, container)
            {
                this.instanceCreator = instanceCreator;
            }
        }

        public override Expression BuildExpression() =>
            this.instanceCreator == null
            ? this.BuildTransientExpression()
            : this.BuildTransientExpression(this.instanceCreator);
    }
}
