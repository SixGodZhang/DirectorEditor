//#define ENABLE_MAIN_ENTER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * LINQ Method Syntax
 * 
 */
#if ENABLE_MAIN_ENTER
class Tutorials_4
{
    static void Main(string[] args)
    {
        // string collection
        IList<string> stringList = new List<string>() {
            "C# Tutorials",
            "VB.NET Tutorials",
            "Learn C++",
            "MVC Tutorials" ,
            "Java"
        };

        // LINQ Query Syntax
        var result = stringList.Where(s => s.Contains("Tutorials"));
    }
}
#endif
