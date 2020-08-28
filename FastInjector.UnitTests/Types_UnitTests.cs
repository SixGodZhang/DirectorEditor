using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FastInjector;
using System.Diagnostics;

namespace FastInjector.UnitTests
{
    class TestA
    {
        private int field_a = 1;
        public int Field_a { get => field_a; set => field_a = value; }

        public TestA()
        {
            
        }
    }

    class TestB<T1,T2>
    {
        public TestB()
        {

        }
    }

    [TestFixture]
    public class Types_UnitTests
    {
        [Test]
        public void ToFriendlyName_Test()
        {
            Type type = typeof(TestA);
            TestContext.WriteLine("TestA friendlyName: " + type.ToFriendlyName(true));

            Type listType = typeof(List<int>);
            TestContext.WriteLine("List<int> friendlyName: " + listType.ToFriendlyName(true));

            Type dictType = typeof(Dictionary<int,string>);
            TestContext.WriteLine("Dictionary<int,string> friendlyName: " + dictType.ToFriendlyName(true));

            Type actionType = typeof(Action<string, int, float>);
            TestContext.WriteLine("Action<string, int, float> friendlyName: " + actionType.ToFriendlyName(true));

            Type funcType = typeof(Func<string, int, float>);
            TestContext.WriteLine("Func<string, int, float> friendlyName: " + funcType.ToFriendlyName(true));
        }

        [Test]
        public void ToCSharpFriendlyName_Test()
        {
            TestContext.WriteLine("============================================");
            Type type = typeof(TestB<List<int>,List<int>>);
            TestContext.WriteLine("TestB<T1,T2> friendlyName: " + type.ToCSharpFriendlyName());

            Type listType = typeof(List<>);
            TestContext.WriteLine("listType<> friendlyName: " + listType.ToCSharpFriendlyName());
            TestContext.WriteLine("============================================");

        }

    }
}
