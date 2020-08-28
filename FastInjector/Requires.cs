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

        private static void ThrowArgumentNullException(string paramName)
        {
            throw new ArgumentNullException(paramName);
        }
    }
}
