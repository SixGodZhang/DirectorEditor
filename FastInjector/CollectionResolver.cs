using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector
{
    internal abstract class CollectionResolver
    {
        private readonly List<RegistrationGroup> registrationGroups = new List<RegistrationGroup>();
        //producer cache
        private readonly Dictionary<Type, InstanceProducer> producerCache = new Dictionary<Type, InstanceProducer>();
        private bool verified;
        protected CollectionResolver(Container container, Type serviceType)
        {
            Requires.IsNotPartiallyClosed(serviceType, nameof(serviceType));
            this.Container = container;
            this.ServiceType = serviceType;
        }

        protected IEnumerable<RegistrationGroup> RegistrationGroups => this.registrationGroups;

        protected Type ServiceType { get; }
        protected Container Container { get; }

        internal abstract void AddControlledRegistrations(Type serviceType, ContainerControlledItem[] registrations, bool append);

        internal abstract void RegisterUncontrolledCollection(Type serviceType, InstanceProducer producer);

        /// <summary>
        /// 获取一个producer的实例
        /// </summary>
        /// <param name="elementType"></param>
        /// <returns></returns>
        internal InstanceProducer TryGetInstanceProducer(Type elementType) =>
            this.ServiceType == elementType || this.ServiceType.IsGenericTypeDefinitionOf(elementType)
            ? this.GetInstanceProducerFromCache(elementType)
            : null;


        private InstanceProducer GetInstanceProducerFromCache(Type closedServiceType)
        {
            lock(this.producerCache)
            {
                InstanceProducer producer;
                if(!this.producerCache.TryGetValue(closedServiceType,out producer))
                {
                    this.producerCache[closedServiceType] =
                        producer = this.BuildCollectionProducer(closedServiceType);
                }

                return producer;
            }
        }

        internal void ResolveUnregisteredType(object sender, UnregisteredTypeEventArgs e)
        {
            if (typeof(IEnumerable<>).IsGenericTypeDefinitionOf(e.UnregisteredServiceType))
            {
                Type closedServiceType = e.UnregisteredServiceType.GetGenericArguments().Single();

                if (this.ServiceType.IsGenericTypeDefinitionOf(closedServiceType))
                {
                    var producer = this.GetInstanceProducerFromCache(closedServiceType);
                    if (producer!= null)
                    {
                        e.Register(producer.Registration);
                    }
                }
            }
        }

        protected abstract InstanceProducer BuildCollectionProducer(Type closedServiceType);

        protected sealed class RegistrationGroup
        {

        }
    }

    
}
