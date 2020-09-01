using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector.Lifestyles
{
    internal sealed class HybridLifestyle : Lifestyle, IHybridLifestyle
    {
        private readonly Predicate<Container> lifestyleSelector;
        private readonly Lifestyle trueLifestyle;
        private readonly Lifestyle falseLifestyle;

        internal HybridLifestyle(Predicate<Container> lifestyleSeletor, Lifestyle trueLifestyle, Lifestyle falseLifestyle)
            :base("Hybrid " + GetHybridName(trueLifestyle) + " / " + GetHybridName(falseLifestyle))
        {
            this.lifestyleSelector = lifestyleSelector;
            this.trueLifestyle = trueLifestyle;
            this.falseLifestyle = falseLifestyle;
        }

        public override int Length
        {
            get { throw new NotSupportedException("The length property is not supported for this lifestyle."); }
        }

        internal override int ComponentLength(Container container) =>
            Math.Max(
                this.trueLifestyle.ComponentLength(container),
                this.falseLifestyle.ComponentLength(container));

        internal override int DependencyLength(Container container) =>
            Math.Min(
                this.trueLifestyle.DependencyLength(container),
                this.falseLifestyle.DependencyLength(container));


        protected internal override Registration CreateRegistrationCore<TConcrete>(Container container)
        {
            Func<bool> test = () => this.lifestyleSelector(container);
            return new HybridRegistration(typeof(TConcrete), test,
                this.trueLifestyle, CreateRegistration<TConcrete>(container),
                this.falseLifestyle.CreateRegistration<TConcrete>(container),
                this,
                container);
        }

        protected internal override Registration CreateRegistrationCore<TService>(Func<TService> instanceCreator, Container container)
        {
            Func<bool> test = () => this.lifestyleSelector(container);
            return new HybridRegistration(typeof(TService), test,
                this.trueLifestyle.CreateRegistration(instanceCreator, container),
                this.falseLifestyle.CreateRegistration(instanceCreator, container),
                this,
                container);
        }

        string IHybridLifestyle.GetHybridName() =>
             GetHybridName(this.trueLifestyle) + " / " + GetHybridName(this.falseLifestyle);

        internal static string GetHybridName(Lifestyle lifestyle) =>
            (lifestyle as IHybridLifestyle)?.GetHybridName() ?? lifestyle.Name;
    }
}
