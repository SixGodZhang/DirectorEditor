using Microsoft.VisualStudio.TestTools.UnitTesting;
using TX3Installer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TX3Installer.Common.Tests
{
    [TestClass()]
    public class SevenZipHelperTests
    {
        [TestMethod()]
        public void ExtractTest()
        {
            string sPath = "C:/Users/zhanghui03/source/repos/DirectorEditor/TestResources/testzip.7z";
            string tPath = "C:/Users/zhanghui03/source/repos/DirectorEditor/TestResources/xixi";
            SevenZipHelper.Extract(sPath, tPath);
        }
    }
}