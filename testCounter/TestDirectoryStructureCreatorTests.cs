
using System;
using System.IO;

using Xunit;

using GwLineCounterTest;
using GwLineCounter;

namespace testCounter
{
    public class TestDirectoryStructureCreatorTests
    {

        [Fact]
        public void TestCreateEmptyRoot()
        {
            var myCreator = new TestDirectoryStructureCreator();
            TestDirectoryContents[] myContents = new TestDirectoryContents[1];
            // Note: leaving TestFile[0] empty to indicate no files in the directory.
            TestFile[] testFiles = new TestFile[0];
            myContents[0] = new TestDirectoryContents("", "*.txt", 0, 0, testFiles);
            myCreator.CreateStruct(myContents);
        
            // Verify that have expected directories and sub-directories, with expected numbers of matching and 
            // non-matching files in each. Not testing line counting, as TestFileTests does that.
            //Assert.Equal(numNonblankLines, nLines);
        }

        [Fact]
        public void TestCreateOneEmptySubDir()
        {
            var myCreator = new TestDirectoryStructureCreator();
            TestDirectoryContents[] myContents = new TestDirectoryContents[1];
            // Note: leaving TestFile[0] empty to indicate no files in the directory.
            TestFile[] testFiles = new TestFile[0];
            myContents[0] = new TestDirectoryContents("dir1", "*.txt", 0, 0, testFiles);
            myCreator.CreateStruct(myContents);
        
            // Verify that have expected directories and sub-directories, with expected numbers of matching and 
            // non-matching files in each. Not testing line counting, as TestFileTests does that.
            //Assert.Equal(numNonblankLines, nLines);
        }

    }
}
