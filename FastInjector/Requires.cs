using System;
using System.Collections.Generic;
using System.Text;

namespace FastInjector
{
    internal class Requires
    {
        /// <summary>
        /// 判断实例是否为Null
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="paramName"></param>
        internal static void IsNotNull(object instance, string paramName)
        {
            if (instance == null)
            {
                ThrowArgumentNullException(paramName);
            }
        }

        internal static void IsNotNullOrEmpty(string instance,string paramName)
        {
            bool result = string.IsNullOrEmpty(instance);
            if (result)
            {
                throw new ArgumentException("Value can not be empty.", paramName);
            }
        }

        internal static void IsReferenceType(Type type, string paramName)
        {
            if (!type.IsClass && !type.IsInterface())
            {
                //throw new ArgumentException(StringResources.)
            }
        }

        internal static void IsNotOpenGenericType(Type type, string paramName)
        {
            // We check for ContainsGenericParameters to see whether there is a Generic Parameter 
            // to find out if this type can be created.
            if (type.ContainsGenericParameters())
            {
                throw new ArgumentException(StringResources.SuppliedTypeIsAnOpenGenericType(type), paramName);
            }
        }

        internal static void ServiceIsAssignableFromImplementation(Type service, Type implementation,
            string paramName)
        {
            if (!service.IsAssignableFrom(implementation))
            {
                ThrowSuppliedTypeDoesNotInheritFromOrImplement(service, implementation, paramName);
            }
        }

        private static void ThrowArgumentNullException(string paramName)
        {
            throw new ArgumentNullException(paramName);
        }

        internal static void IsNotPartiallyClosed(Type openGenericServiceType, string paramName)
        {
            if (openGenericServiceType.IsPartiallyClosed())
            {
                throw new ArgumentException(
                    StringResources.ServiceTypeCannotBeAPartiallyClosedType(openGenericServiceType), paramName);
            }
        }

        internal static void ServiceIsAssignableFromRegistration(Type service, Registration registration,
            string paramName)
        {
            if (!service.IsAssignableFrom(registration.ImplementationType))
            {
                ThrowSuppliedElementDoesNotInheritFromOrImplement(service, registration.ImplementationType,
                    "registration", paramName);
            }
        }

        private static void ThrowSuppliedElementDoesNotInheritFromOrImplement(Type service,
            Type implementation, string elementDescription, string paramName)
        {
            throw new ArgumentException(
                StringResources.SuppliedElementDoesNotInheritFromOrImplement(service, implementation,
                    elementDescription),
                paramName);
        }

        private static void ThrowSuppliedTypeDoesNotInheritFromOrImplement(Type service, Type implementation,
            string paramName)
        {
            throw new ArgumentException(
                StringResources.SuppliedTypeDoesNotInheritFromOrImplement(service, implementation),
                paramName);
        }
    }
}
