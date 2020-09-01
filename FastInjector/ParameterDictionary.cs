using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FastInjector
{
    internal sealed class ParameterDictionary<TValue>:Dictionary<ParameterInfo, TValue>
    {
        public ParameterDictionary():base(ParameterInfoComparer.Instance)
        {
        }

        public ParameterDictionary(IEnumerable<TValue> collection, Func<TValue,ParameterInfo> keySelector)
            :this()
        {
            foreach (var item in collection)
            {
                this.Add(keySelector(item), item);
            }
        }

        /// <summary>
        /// 比较器
        /// </summary>
        private sealed class ParameterInfoComparer : IEqualityComparer<ParameterInfo>
        {
            public static readonly ParameterInfoComparer Instance = new ParameterInfoComparer();

            public bool Equals(ParameterInfo x, ParameterInfo y) =>
                x.Name == y.Name
                && x.ParameterType == y.ParameterType
                && Equals(x.Member, y.Member);

            private static bool Equals(MemberInfo x, MemberInfo y) =>
                x.DeclaringType == y.DeclaringType && x.Name == y.Name;

            public int GetHashCode(ParameterInfo obj)=>
                (obj?.Name ?? string.Empty).GetHashCode()
                    ^ obj.ParameterType.GetHashCode()
                    ^ GetHashCode(obj.Member);

            private static int GetHashCode(MemberInfo obj) =>
                obj.DeclaringType.GetHashCode() ^ obj.Name.GetHashCode();
        }

    }
}
