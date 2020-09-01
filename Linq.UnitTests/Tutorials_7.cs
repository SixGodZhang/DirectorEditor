#define ENABLE_MAIN_ENTER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Expression tree 用法
/// </summary>
public class Student
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
}

#if ENABLE_MAIN_ENTER
class Tutorials_7
{
    static void Main(string[] args)
    {
        /*
         * Func<Student, bool> isAdult = s => s.age >= 18;
         * public bool function(Student s)
         * {
         *   return s.Age > 18;
         * }
         */

        //Step 1: Create Parameter Expression in C#
        ParameterExpression pe = Expression.Parameter(typeof(Student), "s");
        //Step 2: Create Property Expression in C#
        MemberExpression me = Expression.Property(pe, "Age");
        //Step 3: Create Constant Expression in C#
        ConstantExpression constant = Expression.Constant(18, typeof(int));
        //Step 4: Create Binary Expression in C#
        BinaryExpression body = Expression.GreaterThanOrEqual(me, constant);
        //Step 5: Create Lambda Expression in C#
        var ExpressionTree = Expression.Lambda<Func<Student, bool>>(body, new[] { pe });

        Console.WriteLine("Expression Tree: {0}", ExpressionTree);
        Console.WriteLine("Expression Tree Body: {0}", ExpressionTree.Body);
        Console.WriteLine("Number of Parameters in Expression Tree: {0}",ExpressionTree.Parameters.Count);
        Console.WriteLine("Parameters in Expression Tree: {0}", ExpressionTree.Parameters[0]);

        // output:
        //Expression Tree: s => (s.Age >= 18)
        //Expression Tree Body: (s.Age >= 18)
        //Number of Parameters in Expression Tree: 1
        //Parameters in Expression Tree: s

    }
}
#endif
