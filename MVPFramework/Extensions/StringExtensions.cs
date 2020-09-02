using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPFramework.Extensions
{
    internal static class StringExtensions
    {
        /// <summary>
        /// 截取字符串到suffix最后一次出现的位置
        /// </summary>
        /// <param name="source"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        internal static string TrimFromEnd(this string source, string suffix)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(suffix))
                return source;
            var length = source.LastIndexOf(suffix, StringComparison.OrdinalIgnoreCase);
            return length > 0 ? source.Substring(0, length) : source;
        }
    }
}
