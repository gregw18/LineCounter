// Interface and implementation for counting number of non-blank lines in a file.

using System;
using Xunit;

using GwLineCounterTest;
using GwLineCounter;

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

            var myCounter = new FileLineCounter();
            int nLines = myCounter.GetNumberLines("test1.txt");
            Assert.Equal(10, nLines);
        }
    }
}
