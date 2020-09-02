using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Extensions
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// 获取Assembly的限定名
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static string GetNameSafe(this Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            return new AssemblyName(assembly.FullName).Name;
        }
    }
}
