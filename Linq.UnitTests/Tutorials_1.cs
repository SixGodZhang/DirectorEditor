// #define ENABLE_MAIN_ENTER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if ENABLE_MAIN_ENTER

namespace Linq.UnitTests
{
    class Tutorials_1
    {
        static void Main(string[] args)
        {
            string[] names = { "Bill", "Steve", "James", "Mohan" };

            // IEnumerable<string>
            var myLinqQuery = from name in names
                              where name.Contains('a')
                              select name;

            foreach (var name in myLinqQuery)
            {
                Console.WriteLine(name + " ");
            }
        }
    }
}

#endif
