using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector
{
    public partial class Container : IServiceProvider
    {
        private Dictionary<Type, InstanceProducer> rootProducerCache = new Dictionary<Type, InstanceProducer>(ReferenceEqualityComparer<Type>.Instance);
        internal object Options;

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

		public TService GetInstance<TService>() where TService: class
        {
            this.ThrowWhenDisposed();
            this.LockContainer();


            InstanceProducer instanceProducer;

			if(!this.rootProducerCache.TryGetValue(typeof(TService), out instanceProducer))
            {
                return (TService)this.GetInstanceForRootType<TService>();
            }

			if (instanceProducer == null)
            {
                this.ThrowMissingInstanceProducerException(typeof(TService));
            }

            return (TService)instanceProducer.GetInstance();
        }

		private object GetInstanceForRootType<TService>() where TService: class
        {
            InstanceProducer producer = this.GetInstanceProducerForType<TService>(InjectionConsumerInfo.Root);
            this.AppendRootInstanceProducer(typeof(TService), producer);
            return this.GetInstanceFromProducer(producer, typeof(TService));

        }

		private void ThrowMissingInstanceProducerException(Type serviceType)
        {
			if (Types.IsConcreteConstructableType(serviceType))
            {
                this.ThrowNotConstructableException(serviceType);
            }

            throw new ActivationException(StringResources.NoRegistrationForTypeFound(
                serviceType,
                this.HasRegistrations,
                this.ContainsOneToOneRegistrationForCollectionType(serviceType),
                this.ContainsCollectionRegistrationFor(serviceType),
                this.GetNonGenericDecoratorsThatWereSkippedDuringBatchRegistration(serviceType),
                this.GetLookalikesForMissingType(serviceType)));
				));
        }
    }
}
