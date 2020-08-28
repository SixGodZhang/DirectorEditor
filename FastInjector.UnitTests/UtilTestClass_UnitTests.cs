using System;
using System.Collections.Generic;
using System.Text;
using FastInjector;


namespace FastInjector.UnitTests
{
    class UtilTestClass_UnitTests
    {
        public void Initialize()
        {
            //这里写运行每一个测试用例时需要初始化的代码
        }

        public void TestDoActionA()
        {
            UtilTestClass t = new UtilTestClass();
            t.DoActionA();
        }
    }
}
