//#define ENABLE_MAIN_ENTER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Expression用法
/// </summary>
#if ENABLE_MAIN_ENTER

public class Student
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
}


class Tutorials_6
{
    Func<Student, bool> isTeenAger = s => s.Age > 12 && s.Age < 20;

    static void Main(string[] args)
    {
        Expression<Func<Student, bool>> isTeenAgerExpr = s => s.Age > 12 && s.Age < 20;
        //compile Expression using Compile method to invoke it as Delegate
        Func<Student, bool> isTeenAger = isTeenAgerExpr.Compile();
        //Invoke
        bool result = isTeenAger(new Student() { StudentID = 1, StudentName = "Steve", Age = 20 });
    }
}
#endif
