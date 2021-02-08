// Interface and implementation for counting number of non-blank lines in a file.

using System;
using System.IO;

using Xunit;

using GwLineCounterTest;
using GwLineCounter;

namespace testCounter
{
    public class TestFileTests
    {

        [Theory]
        [InlineData("test1.txt", 5, 10)]
        [InlineData("test2.txt", 5, 2)]
        [InlineData("test3.txt", 0, 2)]
        [InlineData("test4.txt", 5, 0)]
        [InlineData("test5.txt", 0, 0)]
        public void TestCreateFile( string fileName, int numBlankLines, int numNonblankLines)
        {
            var testFile = new TestFile(fileName, numBlankLines, numNonblankLines);
            testFile.Create(Directory.GetCurrentDirectory());

            var myCounter = new FileLineCounter();
            int nLines = myCounter.GetNumberLines(fileName);
            Assert.Equal(numNonblankLines, nLines);
        }
    }
}
