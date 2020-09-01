//#define ENABLE_MAIN_ENTER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if ENABLE_MAIN_ENTER
class Student
{
    public int StudentID { get; set; }
    public String StudentName { get; set; }
    public int Age { get; set; }
}

delegate bool FindStudent(Student std);

class StudentExtension
{
    public static Student[] where(Student[] stdArray, FindStudent del)
    {
        int i = 0;
        Student[] result = new Student[10];
        foreach (var std in stdArray)
        {
            if (del(std))
            {
                result[i] = std;
                i++;
            }
        }

        return result;
    }
}


class Tutorials_2
{
    static void Main(string[] args)
    {
        Student[] studentArray = {
            new Student() { StudentID = 1, StudentName = "John", Age = 18 },
            new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 },
            new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 },
            new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 },
            new Student() { StudentID = 5, StudentName = "Ron" , Age = 31 },
            new Student() { StudentID = 6, StudentName = "Chris",  Age = 17 },
            new Student() { StudentID = 7, StudentName = "Rob",Age = 19  },
        };

        // C# 2.0 之前的语法
        //Student[] students = new Student[10];
        //int i = 0;
        //foreach (Student std in studentArray)
        //{
        //    if (std.Age > 12 && std.Age < 20)
        //    {
        //        students[i] = std;
        //        i++;
        //    }
        //}

        // C# 2.0 之后使用delegate做法
        //Student[] students = StudentExtension.where(studentArray, delegate (Student std)
        //{
        //    return std.Age > 12 && std.Age < 20;
        //});

        // 使用Linq的做法
        Student[] teenAgerStudnets = studentArray.Where(s => s.Age > 12 && s.Age < 20).ToArray();


    }
}
#endif
