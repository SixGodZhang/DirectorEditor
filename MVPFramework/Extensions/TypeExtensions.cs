using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Extensions
{
    public static class TypeExtensions
    {
        [Obsolete("已过时。")]
        static readonly IDictionary<RuntimeTypeHandle, IEnumerable<Type>> implementationTypeToViewInterfacesCache = new Dictionary<RuntimeTypeHandle, IEnumerable<Type>>();

        /// <summary>
        /// 查找当前类型的父类中所有实现IViewLogic的类型
        /// </summary>
        /// <param name="implementationType"></param>
        /// <returns></returns>
        [Obsolete("之前使用这种方式进行ViewLogic和Presenter绑定, 现在舍弃这种方式")]
        public static IEnumerable<Type> GetViewLogicInterfaces(this Type implementationType)
        {
            return implementationTypeToViewInterfacesCache.GetOrCreateValue(implementationType.TypeHandle, () =>
             implementationType
             .GetInterfaces()
             .Where(typeof(IViewLogic).IsAssignableFrom)
             .ToArray()
            );
        }

        /// <summary>
        /// 类型是否包含某个方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public static bool HasMethod(this Type type,string methodName)
        {
            return type.GetMethods().Where(m => m.Name == methodName).Count() > 0;
        }
    }
}
