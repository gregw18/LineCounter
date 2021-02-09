
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
            myContents[0] = new TestDirectoryContents("", "*.txt", 0, testFiles);
            myCreator.CreateStruct(myContents);
        
            // Verify that have expected directories and sub-directories, with expected numbers of matching and 
            // non-matching files in each. Not testing line counting, as TestFileTests does that.
            StructureMatches(myContents, myCreator.testStructRootDir);
        }

        [Fact]
        public void TestCreateOneEmptySubDir()
        {
            var myCreator = new TestDirectoryStructureCreator();
            TestDirectoryContents[] myContents = new TestDirectoryContents[1];
            // Note: leaving TestFile[0] empty to indicate no files in the directory.
            TestFile[] testFiles = new TestFile[0];
            myContents[0] = new TestDirectoryContents("dir1", "*.txt", 0, testFiles);
            myCreator.CreateStruct(myContents);

            StructureMatches(myContents, myCreator.testStructRootDir);        
        }

        private bool StructureMatches(TestDirectoryContents[] myContents, string rootDir)
        {
            bool structMatches = false;

            foreach (TestDirectoryContents dirContents in myContents)
            {
                string fullPath = Path.Combine(rootDir, dirContents.DirName);
                Assert.True(Directory.Exists(fullPath), $"Missing directory {fullPath}.");

                int numMatches = Directory.GetFiles(fullPath, dirContents.fileSpec).Length;
                string errMsg = $"Number of matching files failed in directory {fullPath}, expected: {dirContents.TestFiles.Length}, actual: {numMatches}.";
                Assert.True(dirContents.TestFiles.Length == numMatches, errMsg);

                int nonMatches = Directory.GetFiles(fullPath).Length - numMatches;
                errMsg = $"Number of nonmatching files failed in directory {fullPath}, expected: {dirContents.numNonmatchingFiles}, actual: {nonMatches}.";
                Assert.True(dirContents.numNonmatchingFiles == nonMatches, errMsg);
            }

            return structMatches;
        }
    }
}
