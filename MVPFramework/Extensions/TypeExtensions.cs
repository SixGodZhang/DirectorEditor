using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Extensions
{
    public static class TypeExtensions
    {
        static readonly IDictionary<RuntimeTypeHandle, IEnumerable<Type>> implementationTypeToViewInterfacesCache = new Dictionary<RuntimeTypeHandle, IEnumerable<Type>>();

        public static IEnumerable<Type> GetViewInterfaces(this Type implementationType)
        {
            var implementationTypeHandle = implementationType.TypeHandle;
            return implementationTypeToViewInterfacesCache.GetOrCreateValue(implementationTypeHandle, () =>
             implementationType
             .GetInterfaces()
             .Where(typeof(IView).IsAssignableFrom)
             .ToArray()
            );
        }
    }
}
