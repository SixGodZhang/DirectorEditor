using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector.Lifestyles
{
    internal class UnknownLifestyle:Lifestyle
    {
        internal UnknownLifestyle(): base("Unknown")
        {

        }

        public override int Length
        {
            get
            {
                throw new NotSupportedException("The length property is not supported for this lifestyle.");
            }
        }

        internal override int ComponentLength(Container container) => Singleton.ComponentLength(container);
        internal override int DependencyLength(Container container) => Transient.DependencyLength(container);

        protected internal override Registration CreateRegistrationCore<TConcrete>(Container container)
        {
            throw new InvalidOperationException(
                "The unknown lifestyle does not allow creation of registrations.");
        }

        protected internal override Registration CreateRegistrationCore<TService>(Func<TService> instanceCreator, Container container)
        {
            throw new InvalidOperationException(
                "The unknown lifestyle does not allow creation of registrations.");
        }
    }
}
