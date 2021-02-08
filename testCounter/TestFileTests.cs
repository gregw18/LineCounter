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
        [Fact]
        public void TestCreateFile_MoreNonblanks()
        {
            var fileName = "test1.txt";
            var testFile = new TestFile(fileName, 5, 10);
            testFile.Create(Directory.GetCurrentDirectory());

            var myCounter = new FileLineCounter();
            int nLines = myCounter.GetNumberLines(fileName);
            Assert.Equal(10, nLines);
        }

        [Fact]
        public void TestCreateFile_MoreBlanks()
        {
            var fileName = "test2.txt";
            var testFile = new TestFile(fileName, 5, 2);
            testFile.Create(Directory.GetCurrentDirectory());

            var myCounter = new FileLineCounter();
            int nLines = myCounter.GetNumberLines(fileName);
            Assert.Equal(2, nLines);
        }

        [Fact]
        public void TestCreateFile_ZeroBlanks()
        {
            var fileName = "test3.txt";
            var testFile = new TestFile(fileName, 0, 2);
            testFile.Create(Directory.GetCurrentDirectory());

            var myCounter = new FileLineCounter();
            int nLines = myCounter.GetNumberLines(fileName);
            Assert.Equal(2, nLines);
        }

        [Fact]
        public void TestCreateFile_ZeroNonBlanks()
        {
            var fileName = "test4.txt";
            var testFile = new TestFile(fileName, 5, 0);
            testFile.Create(Directory.GetCurrentDirectory());

            var myCounter = new FileLineCounter();
            int nLines = myCounter.GetNumberLines(fileName);
            Assert.Equal(0, nLines);
        }

        [Fact]
        public void TestCreateFile_ZeroLines()
        {
            var fileName = "test5.txt";
            var testFile = new TestFile(fileName, 0, 0);
            testFile.Create(Directory.GetCurrentDirectory());

            var myCounter = new FileLineCounter();
            int nLines = myCounter.GetNumberLines(fileName);
            Assert.Equal(0, nLines);
        }


    }
}
