using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector
{
    internal interface IRegistrationEntry
    {
        IEnumerable<InstanceProducer> CurrentProducers { get; }

        void Add(InstanceProducer producer);
        void AddGeneric(Type serviceType, Type implementationType, Lifestyle lifestyle, Predicate<PredicateContext> predicate = null);
        void Add(Type serviceType, Func<TypeFactoryContext, Type> implementationTypeFactory, Lifestyle lifestyle, Predicate<PredicateContext> predicate = null);
        InstanceProducer TryGetInstanceProducer(Type serviceType, InjectionConsumerInfo consumer);
        int GetNumberOfConditionalRegistrationsFor(Type serviceType);
    }
}
