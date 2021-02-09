
using System;
using System.IO;

using Xunit;

using GwLineCounterTest;
using GwLineCounter;

namespace testCounter
{
    public class TestDirectoryStructureCreatorTests
    {

        bool deleteWhenFinished = true;



        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        public void TestCreateRootDirFiles(int matchingFiles, int nonmatchingFiles)
        {
            // Test varying numbers of files in the root dir.
            var myCreator = new TestDirectoryStructureCreator();
            TestDirectoryContents[] myContents = new TestDirectoryContents[1];

            TestFile[] testFiles = new TestFile[matchingFiles];
            for (int i = 0; i < matchingFiles; i++)
            {
                testFiles[i] = new TestFile($"file{i}.txt", 1, 1);
            }
            myContents[0] = new TestDirectoryContents("", "*.txt", nonmatchingFiles, testFiles);
            myCreator.CreateStruct(myContents);

            bool matched = StructureMatches(myContents, myCreator.testStructRootDir);        

            if (matched && deleteWhenFinished) { Directory.Delete(myCreator.testStructRootDir, true); }
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

            bool matched = StructureMatches(myContents, myCreator.testStructRootDir);        

            if (matched && deleteWhenFinished) { Directory.Delete(myCreator.testStructRootDir, true); }
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
                structMatches = true;
            }

            return structMatches;
        }
    }
}
