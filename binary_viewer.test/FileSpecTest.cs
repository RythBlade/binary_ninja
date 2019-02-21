using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using binary_viewer.Spec;

namespace binary_viewer.test
{
    [TestClass]
    public class FileSpecTest
    {
        [TestMethod]
        public void TestFileSpecConstructor()
        {
            FileSpec fileSpec = new FileSpec(null, 0);

            Assert.AreEqual(fileSpec.IndexOfFirstByte, 0);
            Assert.AreEqual(fileSpec.FileBuffer, null);
            Assert.AreNotEqual(fileSpec.File, null);
        }
    }
}
