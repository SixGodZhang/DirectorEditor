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
    public class FileOperationHelperTests
    {
        [TestMethod()]
        public void getSizeInCurrentDirectoryTest()
        {
            string tPath = "C:/Users/zhanghui03/source/repos/DirectorEditor/TestResources/xixi";
            var size = FileOperationHelper.getSizeInCurrentDirectory(tPath);
        }
    }
}