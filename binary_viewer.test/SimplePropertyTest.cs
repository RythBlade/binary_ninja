using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using binary_viewer.Script;
using binary_viewer.Spec;
using System.Runtime.Serialization.Formatters.Binary;

namespace binary_viewer.test
{
    [TestClass]
    public class SimplePropertyTest
    {
        [TestMethod]
        public void TestSimplePropertyFloat()
        {
            // create our test script to parse
            string scriptBuffer = @"public class Main                     
{                                     
	public float m_valueOne;
}";

            // create our test buffer to display
            int lengthOfFakeBuffer = 4;
            byte[] targetFileBuffer = new byte[lengthOfFakeBuffer];

            float testValueToWrite = 99.0f;

            using (MemoryStream stream = new MemoryStream(targetFileBuffer, 0, lengthOfFakeBuffer))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(stream))
                {
                    float next = 0;
                    next = testValueToWrite; binaryWriter.Write(next);
                }
            }

            FileSpec fileSpecToTest = new FileSpec(targetFileBuffer, 0);
            IScriptConsumer scriptConsumer = new ScriptConsumerCSharp(scriptBuffer, fileSpecToTest);

            scriptConsumer.ParseScript();

            Assert.AreEqual(fileSpecToTest.File.Properties.Count, 1);
            Assert.AreEqual(fileSpecToTest.File.Properties[0].Name, "m_valueOne");
            Assert.IsTrue(fileSpecToTest.File.Properties[0].GetType() == typeof(ValueSpec));

            ValueSpec testSpec = (ValueSpec)fileSpecToTest.File.Properties[0];

            Assert.AreEqual(testSpec.TypeOfValue, Spec.ValueType.eFloat);
            Assert.AreEqual(testSpec.GetAsFloat(), testValueToWrite);
            Assert.AreEqual(testSpec.FirstByteOfValue, 0);
        }

        [TestMethod]
        public void TestSimplePropertyDouble()
        {
            // create our test script to parse
            string scriptBuffer = @"public class Main                     
{                                     
	public double m_valueOne;
}";

            // create our test buffer to display
            int lengthOfFakeBuffer = 8;
            byte[] targetFileBuffer = new byte[lengthOfFakeBuffer];

            double testValueToWrite = 101.977748395847245;

            using (MemoryStream stream = new MemoryStream(targetFileBuffer, 0, lengthOfFakeBuffer))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(stream))
                {
                    double next = 0;
                    next = testValueToWrite; binaryWriter.Write(next);
                }
            }

            FileSpec fileSpecToTest = new FileSpec(targetFileBuffer, 0);
            IScriptConsumer scriptConsumer = new ScriptConsumerCSharp(scriptBuffer, fileSpecToTest);

            scriptConsumer.ParseScript();

            Assert.AreEqual(fileSpecToTest.File.Properties.Count, 1);
            Assert.AreEqual(fileSpecToTest.File.Properties[0].Name, "m_valueOne");
            Assert.IsTrue(fileSpecToTest.File.Properties[0].GetType() == typeof(ValueSpec));

            ValueSpec testSpec = (ValueSpec)fileSpecToTest.File.Properties[0];

            Assert.AreEqual(testSpec.TypeOfValue, Spec.ValueType.eDouble);
            Assert.AreEqual(testSpec.GetAsDouble(), testValueToWrite);
            Assert.AreEqual(testSpec.FirstByteOfValue, 0);
        }

        [TestMethod]
        public void TestSimplePropertyInt32_1()
        {
            // create our test script to parse
            string scriptBuffer = @"public class Main                     
{                                     
	public int m_valueOne;
}";

            // create our test buffer to display
            int lengthOfFakeBuffer = 4;
            byte[] targetFileBuffer = new byte[lengthOfFakeBuffer];

            int testValueToWrite = 99;

            using (MemoryStream stream = new MemoryStream(targetFileBuffer, 0, lengthOfFakeBuffer))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(stream))
                {
                    int next = 0;
                    next = testValueToWrite; binaryWriter.Write(next);
                }
            }

            FileSpec fileSpecToTest = new FileSpec(targetFileBuffer, 0);
            IScriptConsumer scriptConsumer = new ScriptConsumerCSharp(scriptBuffer, fileSpecToTest);

            scriptConsumer.ParseScript();

            Assert.AreEqual(fileSpecToTest.File.Properties.Count, 1);
            Assert.AreEqual(fileSpecToTest.File.Properties[0].Name, "m_valueOne");
            Assert.IsTrue(fileSpecToTest.File.Properties[0].GetType() == typeof(ValueSpec));

            ValueSpec testSpec = (ValueSpec)fileSpecToTest.File.Properties[0];

            Assert.AreEqual(testSpec.TypeOfValue, Spec.ValueType.eInt32);
            Assert.AreEqual(testSpec.GetAsInt32(), testValueToWrite);
            Assert.AreEqual(testSpec.FirstByteOfValue, 0);
        }

        [TestMethod]
        public void TestSimplePropertyInt32_2()
        {
            // create our test script to parse
            string scriptBuffer = @"public class Main                     
{                                     
	public int m_valueOne;
}";

            // create our test buffer to display
            int lengthOfFakeBuffer = 4;
            byte[] targetFileBuffer = new byte[lengthOfFakeBuffer];

            int testValueToWrite = -99;

            using (MemoryStream stream = new MemoryStream(targetFileBuffer, 0, lengthOfFakeBuffer))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(stream))
                {
                    int next = 0;
                    next = testValueToWrite; binaryWriter.Write(next);
                }
            }

            FileSpec fileSpecToTest = new FileSpec(targetFileBuffer, 0);
            IScriptConsumer scriptConsumer = new ScriptConsumerCSharp(scriptBuffer, fileSpecToTest);

            scriptConsumer.ParseScript();

            Assert.AreEqual(fileSpecToTest.File.Properties.Count, 1);
            Assert.AreEqual(fileSpecToTest.File.Properties[0].Name, "m_valueOne");
            Assert.IsTrue(fileSpecToTest.File.Properties[0].GetType() == typeof(ValueSpec));

            ValueSpec testSpec = (ValueSpec)fileSpecToTest.File.Properties[0];

            Assert.AreEqual(testSpec.TypeOfValue, Spec.ValueType.eInt32);
            Assert.AreEqual(testSpec.GetAsInt32(), testValueToWrite);
            Assert.AreEqual(testSpec.FirstByteOfValue, 0);
        }

        [TestMethod]
        public void TestSimplePropertyUnsignedInt32()
        {
            // create our test script to parse
            string scriptBuffer = @"public class Main                     
{                                     
	public uint m_valueOne;
}";

            // create our test buffer to display
            int lengthOfFakeBuffer = 4;
            byte[] targetFileBuffer = new byte[lengthOfFakeBuffer];

            uint testValueToWrite = 99;

            using (MemoryStream stream = new MemoryStream(targetFileBuffer, 0, lengthOfFakeBuffer))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(stream))
                {
                    uint next = 0;
                    next = testValueToWrite; binaryWriter.Write(next);
                }
            }

            FileSpec fileSpecToTest = new FileSpec(targetFileBuffer, 0);
            IScriptConsumer scriptConsumer = new ScriptConsumerCSharp(scriptBuffer, fileSpecToTest);

            scriptConsumer.ParseScript();

            Assert.AreEqual(fileSpecToTest.File.Properties.Count, 1);
            Assert.AreEqual(fileSpecToTest.File.Properties[0].Name, "m_valueOne");
            Assert.IsTrue(fileSpecToTest.File.Properties[0].GetType() == typeof(ValueSpec));

            ValueSpec testSpec = (ValueSpec)fileSpecToTest.File.Properties[0];

            Assert.AreEqual(testSpec.TypeOfValue, Spec.ValueType.eUnsignedInt32);
            Assert.AreEqual(testSpec.GetAsUint32(), testValueToWrite);
            Assert.AreEqual(testSpec.FirstByteOfValue, 0);
        }

        [TestMethod]
        public void TestSimplePropertyInt64_1()
        {
            // create our test script to parse
            string scriptBuffer = @"public class Main                     
{                                     
	public long m_valueOne;
}";

            // create our test buffer to display
            int lengthOfFakeBuffer = 8;
            byte[] targetFileBuffer = new byte[lengthOfFakeBuffer];

            long testValueToWrite = 999999999999999;

            using (MemoryStream stream = new MemoryStream(targetFileBuffer, 0, lengthOfFakeBuffer))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(stream))
                {
                    long next = 0;
                    next = testValueToWrite; binaryWriter.Write(next);
                }
            }

            FileSpec fileSpecToTest = new FileSpec(targetFileBuffer, 0);
            IScriptConsumer scriptConsumer = new ScriptConsumerCSharp(scriptBuffer, fileSpecToTest);

            scriptConsumer.ParseScript();

            Assert.AreEqual(fileSpecToTest.File.Properties.Count, 1);
            Assert.AreEqual(fileSpecToTest.File.Properties[0].Name, "m_valueOne");
            Assert.IsTrue(fileSpecToTest.File.Properties[0].GetType() == typeof(ValueSpec));

            ValueSpec testSpec = (ValueSpec)fileSpecToTest.File.Properties[0];

            Assert.AreEqual(testSpec.TypeOfValue, Spec.ValueType.eInt64);
            Assert.AreEqual(testSpec.GetAsInt64(), testValueToWrite);
            Assert.AreEqual(testSpec.FirstByteOfValue, 0);
        }

        [TestMethod]
        public void TestSimplePropertyInt64_2()
        {
            // create our test script to parse
            string scriptBuffer = @"public class Main                     
{                                     
	public long m_valueOne;
}";

            // create our test buffer to display
            int lengthOfFakeBuffer = 8;
            byte[] targetFileBuffer = new byte[lengthOfFakeBuffer];

            long testValueToWrite = -999999999999999;

            using (MemoryStream stream = new MemoryStream(targetFileBuffer, 0, lengthOfFakeBuffer))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(stream))
                {
                    long next = 0;
                    next = testValueToWrite; binaryWriter.Write(next);
                }
            }

            FileSpec fileSpecToTest = new FileSpec(targetFileBuffer, 0);
            IScriptConsumer scriptConsumer = new ScriptConsumerCSharp(scriptBuffer, fileSpecToTest);

            scriptConsumer.ParseScript();

            Assert.AreEqual(fileSpecToTest.File.Properties.Count, 1);
            Assert.AreEqual(fileSpecToTest.File.Properties[0].Name, "m_valueOne");
            Assert.IsTrue(fileSpecToTest.File.Properties[0].GetType() == typeof(ValueSpec));

            ValueSpec testSpec = (ValueSpec)fileSpecToTest.File.Properties[0];

            Assert.AreEqual(testSpec.TypeOfValue, Spec.ValueType.eInt64);
            Assert.AreEqual(testSpec.GetAsInt64(), testValueToWrite);
            Assert.AreEqual(testSpec.FirstByteOfValue, 0);
        }

        [TestMethod]
        public void TestSimplePropertyUnsignedInt64()
        {
            // create our test script to parse
            string scriptBuffer = @"public class Main                     
{                                     
	public ulong m_valueOne;
}";

            // create our test buffer to display
            int lengthOfFakeBuffer = 8;
            byte[] targetFileBuffer = new byte[lengthOfFakeBuffer];

            ulong testValueToWrite = 999999999999999;

            using (MemoryStream stream = new MemoryStream(targetFileBuffer, 0, lengthOfFakeBuffer))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(stream))
                {
                    ulong next = 0;
                    next = testValueToWrite; binaryWriter.Write(next);
                }
            }

            FileSpec fileSpecToTest = new FileSpec(targetFileBuffer, 0);
            IScriptConsumer scriptConsumer = new ScriptConsumerCSharp(scriptBuffer, fileSpecToTest);

            scriptConsumer.ParseScript();

            Assert.AreEqual(fileSpecToTest.File.Properties.Count, 1);
            Assert.AreEqual(fileSpecToTest.File.Properties[0].Name, "m_valueOne");
            Assert.IsTrue(fileSpecToTest.File.Properties[0].GetType() == typeof(ValueSpec));

            ValueSpec testSpec = (ValueSpec)fileSpecToTest.File.Properties[0];

            Assert.AreEqual(testSpec.TypeOfValue, Spec.ValueType.eUnsignedInt64);
            Assert.AreEqual(testSpec.GetAsUint64(), testValueToWrite);
            Assert.AreEqual(testSpec.FirstByteOfValue, 0);
        }

        [TestMethod]
        public void TestSimplePropertyChar()
        {
            // create our test script to parse
            string scriptBuffer = @"public class Main                     
{                                     
	public char m_valueOne;
}";

            // create our test buffer to display
            int lengthOfFakeBuffer = 4;
            byte[] targetFileBuffer = new byte[lengthOfFakeBuffer];

            char testValueToWrite = 'c';

            using (MemoryStream stream = new MemoryStream(targetFileBuffer, 0, lengthOfFakeBuffer))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(stream))
                {
                    char next = 'a';
                    next = testValueToWrite; binaryWriter.Write(next);
                }
            }

            FileSpec fileSpecToTest = new FileSpec(targetFileBuffer, 0);
            IScriptConsumer scriptConsumer = new ScriptConsumerCSharp(scriptBuffer, fileSpecToTest);

            scriptConsumer.ParseScript();

            Assert.AreEqual(fileSpecToTest.File.Properties.Count, 1);
            Assert.AreEqual(fileSpecToTest.File.Properties[0].Name, "m_valueOne");
            Assert.IsTrue(fileSpecToTest.File.Properties[0].GetType() == typeof(ValueSpec));

            ValueSpec testSpec = (ValueSpec)fileSpecToTest.File.Properties[0];

            Assert.AreEqual(testSpec.TypeOfValue, Spec.ValueType.eChar);
            Assert.AreEqual(testSpec.GetAsChar(), testValueToWrite);
            Assert.AreEqual(testSpec.FirstByteOfValue, 0);
        }

        [TestMethod]
        public void TestSimplePropertyString()
        {
            Assert.Fail("String property not implemented!");
            // create our test script to parse
            /*string scriptBuffer = @"public class Main                     
{                                     
	public char m_valueOne;
}";

            // create our test buffer to display
            int lengthOfFakeBuffer = 4;
            byte[] targetFileBuffer = new byte[lengthOfFakeBuffer];

            char testValueToWrite = 'c';

            using (MemoryStream stream = new MemoryStream(targetFileBuffer, 0, lengthOfFakeBuffer))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(stream))
                {
                    char next = 'a';
                    next = testValueToWrite; binaryWriter.Write(next);
                }
            }

            FileSpec fileSpecToTest = new FileSpec(targetFileBuffer, 0);
            IScriptConsumer scriptConsumer = new ScriptConsumerCSharp(scriptBuffer, fileSpecToTest);

            scriptConsumer.ParseScript();

            Assert.AreEqual(fileSpecToTest.File.Properties.Count, 1);
            Assert.AreEqual(fileSpecToTest.File.Properties[0].Name, "m_valueOne");
            Assert.IsTrue(fileSpecToTest.File.Properties[0].GetType() == typeof(ValueSpec));

            ValueSpec testSpec = (ValueSpec)fileSpecToTest.File.Properties[0];

            Assert.AreEqual(testSpec.TypeOfValue, Spec.ValueType.eChar);
            Assert.AreEqual(testSpec.GetAsChar(), testValueToWrite);
            Assert.AreEqual(testSpec.FirstByteOfValue, 0);*/
        }
    }
}
