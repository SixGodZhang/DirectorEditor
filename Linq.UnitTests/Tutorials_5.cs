//#define ENABLE_MAIN_ENTER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
where用法:
<code><![CDATA[
    public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source,  Func<TSource, bool> predicate);
    public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source,  Func<TSource, int, bool> predicate);
]]></code>


**/


#if ENABLE_MAIN_ENTER
class Tutorials_5
{
    static void Main(string[] args)
    {

    }
}
#endif
