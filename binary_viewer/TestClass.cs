using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace binary_viewer
{

    public class TestClass : Attribute
    {
        public TestClass()
        {
        }

        public TestClass(string variableName)
        {
            m_variableName = variableName;
        }

        public string m_variableName = string.Empty;
    }

    public class FirstTest
    {
        [TestClass]
        public int m_testOne;

        [TestClass("m_testOne")]
        public int m_testTwo;
    }
}
