
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
        [InlineData(2, 2)]
        [InlineData(2, 1)]
        public void TestCreateRootDirFiles(int matchingFiles, int nonmatchingFiles)
        {
            // Test varying numbers of files in the root dir.
            var myCreator = new TestDirectoryStructureCreator();
            TestDirectoryContents[] myContents = new TestDirectoryContents[1];
            TestFile[] testFiles = CreateTestFiles(matchingFiles);

            myContents[0] = new TestDirectoryContents("", "*.txt", nonmatchingFiles, testFiles);
            myCreator.CreateStruct(myContents);

            bool matched = StructureMatches(myContents, myCreator.testStructRootDir);        

            if (matched && deleteWhenFinished) { Directory.Delete(myCreator.testStructRootDir, true); }
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void TestCreateOneSubDir(int matchingFiles, int nonmatchingFiles)
        {
            var myCreator = new TestDirectoryStructureCreator();
            TestDirectoryContents[] myContents = new TestDirectoryContents[1];
            TestFile[] testFiles = CreateTestFiles(matchingFiles);
            myContents[0] = new TestDirectoryContents("dir1", "*.txt", nonmatchingFiles, testFiles);
            myCreator.CreateStruct(myContents);

            bool matched = StructureMatches(myContents, myCreator.testStructRootDir);        

            if (matched && deleteWhenFinished) { Directory.Delete(myCreator.testStructRootDir, true); }
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        public void TestCreateOneSubSubDir(int matchingFiles, int nonmatchingFiles)
        {
            // A sub-dir with 1, 1 files, plus a directory below that with various combinations.
            var myCreator = new TestDirectoryStructureCreator();
            TestDirectoryContents[] myContents = new TestDirectoryContents[2];
            TestFile[] testFiles = CreateTestFiles(1);
            myContents[0] = new TestDirectoryContents("dir1", "*.txt", 1, testFiles);

            testFiles = CreateTestFiles(matchingFiles);
            myContents[1] = new TestDirectoryContents(@"dir1\dir2", "*.txt", nonmatchingFiles, testFiles);

            myCreator.CreateStruct(myContents);

            bool matched = StructureMatches(myContents, myCreator.testStructRootDir);        

            if (matched && deleteWhenFinished) { Directory.Delete(myCreator.testStructRootDir, true); }
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        public void TestCreateTwoSubDirs(int matchingFiles, int nonmatchingFiles)
        {
            // One sub-dir with 1, 1 files, plus a second sub-dir at the same level, with various combinations.
            var myCreator = new TestDirectoryStructureCreator();
            TestDirectoryContents[] myContents = new TestDirectoryContents[2];
            TestFile[] testFiles = CreateTestFiles(1);
            myContents[0] = new TestDirectoryContents("dir1", "*.txt", 1, testFiles);

            testFiles = CreateTestFiles(matchingFiles);
            myContents[1] = new TestDirectoryContents("dir2", "*.txt", nonmatchingFiles, testFiles);

            myCreator.CreateStruct(myContents);

            bool matched = StructureMatches(myContents, myCreator.testStructRootDir);        

            if (matched && deleteWhenFinished) { Directory.Delete(myCreator.testStructRootDir, true); }
        }

        // Create array of TestFile, size matchingFiles. Assumes .txt extensions.
        private TestFile[] CreateTestFiles(int matchingFiles)
        {
            TestFile[] testFiles = new TestFile[matchingFiles];
            for (int i = 0; i < matchingFiles; i++)
            {
                testFiles[i] = new TestFile($"file{i}.txt", 1, 1);
            }

            return testFiles;
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
