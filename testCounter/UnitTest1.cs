// Interface and implementation for counting number of non-blank lines in a file.

using System;
using Xunit;

using GwLineCounterTest;

namespace testCounter
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(4, 2+2);
        }

        [Fact]
        public void TestCreateFile()
        {
            var testFile = new TestFile("test1.txt", 5, 10);
            testFile.Create();
            Assert.Equal(1, 1);
        }
    }
}
