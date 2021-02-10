
using System;
using System.IO;

using Xunit;

using GwLineCounterTest;
using GwLineCounter;

namespace testGwLineCounter
{
    // Integration tests for program - i.e. counting number of nonblank lines in matching files in given
    // directory and its sub-directories. Uses the TestDirectoryStructureCreator to create the desired
    // directories and files, under the user's temp dir, then removes the structure when finished, unless
    // set deleteWhenFinished flag to false, or a test fails.
    public class DirectoryLineCounterTests
    {

        const bool deleteWhenFinished = true;

        // Am putting same number of non-empty lines in every matching file, to make checking results simpler.
        const int linesPerMatchingFile = 5;

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(2, 1)]
        public void FilesInRootDir(int matchingFiles, int nonmatchingFiles)
        {
            // Test varying numbers of files in the root dir.
            var myCreator = new TestDirectoryStructureCreator();
            TestDirectoryContents[] myContents = new TestDirectoryContents[1];
            TestFile[] testFiles = CreateTestFiles(matchingFiles);

            myContents[0] = new TestDirectoryContents("", "*.txt", nonmatchingFiles, testFiles);
            myCreator.CreateStruct(myContents);
            DirectoryLineCounter myCounter = new DirectoryLineCounter();

            myCounter.PopulateList(myCreator.testStructRootDir, "*.txt");
            DirectoryLineCounterResults myResults = myCounter.CountLines();

            Assert.Equal(matchingFiles, myResults.numFiles);
            Assert.Equal(matchingFiles * linesPerMatchingFile, myResults.numLines);

            if (deleteWhenFinished) { Directory.Delete(myCreator.testStructRootDir, true); }
        }

        
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void FilesInOneSubDir(int matchingFiles, int nonmatchingFiles)
        {
            var myCreator = new TestDirectoryStructureCreator();
            TestDirectoryContents[] myContents = new TestDirectoryContents[1];
            TestFile[] testFiles = CreateTestFiles(matchingFiles);
            myContents[0] = new TestDirectoryContents("dir1", "*.txt", nonmatchingFiles, testFiles);
            myCreator.CreateStruct(myContents);
            DirectoryLineCounter myCounter = new DirectoryLineCounter();

            myCounter.PopulateList(myCreator.testStructRootDir, "*.txt");
            DirectoryLineCounterResults myResults = myCounter.CountLines();

            Assert.Equal(matchingFiles, myResults.numFiles);
            Assert.Equal(matchingFiles * linesPerMatchingFile, myResults.numLines);

            if (deleteWhenFinished) { Directory.Delete(myCreator.testStructRootDir, true); }
        }
        
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        public void FilesInOneSubSubDir(int matchingFiles, int nonmatchingFiles)
        {
            // A sub-dir with 1, 1 files, plus a directory below that with various combinations.
            var myCreator = new TestDirectoryStructureCreator();
            TestDirectoryContents[] myContents = new TestDirectoryContents[2];
            TestFile[] testFiles = CreateTestFiles(1);
            myContents[0] = new TestDirectoryContents("dir1", "*.txt", 1, testFiles);

            testFiles = CreateTestFiles(matchingFiles);
            myContents[1] = new TestDirectoryContents(@"dir1\dir2", "*.txt", nonmatchingFiles, testFiles);
            myCreator.CreateStruct(myContents);
            DirectoryLineCounter myCounter = new DirectoryLineCounter();

            myCounter.PopulateList(myCreator.testStructRootDir, "*.txt");
            DirectoryLineCounterResults myResults = myCounter.CountLines();

            // Expect matchingFiles in dir2 plus one file from dir1.
            int expectedFiles = matchingFiles + 1;
            Assert.Equal(expectedFiles, myResults.numFiles);
            Assert.Equal(expectedFiles * linesPerMatchingFile, myResults.numLines);

            if (deleteWhenFinished) { Directory.Delete(myCreator.testStructRootDir, true); }
        }
        
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        public void FilesInTwoSubDirs(int matchingFiles, int nonmatchingFiles)
        {
            // One sub-dir with 1, 1 files, plus a second sub-dir at the same level, with various combinations.
            var myCreator = new TestDirectoryStructureCreator();
            TestDirectoryContents[] myContents = new TestDirectoryContents[2];
            TestFile[] testFiles = CreateTestFiles(1);
            myContents[0] = new TestDirectoryContents("dir1", "*.txt", 1, testFiles);

            testFiles = CreateTestFiles(matchingFiles);
            myContents[1] = new TestDirectoryContents("dir2", "*.txt", nonmatchingFiles, testFiles);

            myCreator.CreateStruct(myContents);
            DirectoryLineCounter myCounter = new DirectoryLineCounter();

            myCounter.PopulateList(myCreator.testStructRootDir, "*.txt");
            DirectoryLineCounterResults myResults = myCounter.CountLines();

            // Expect matchingFiles in dir2 plus one file from dir1.
            int expectedFiles = matchingFiles + 1;
            Assert.Equal(expectedFiles, myResults.numFiles);
            Assert.Equal(expectedFiles * linesPerMatchingFile, myResults.numLines);

            if (deleteWhenFinished) { Directory.Delete(myCreator.testStructRootDir, true); }
        }

        // Create array of TestFile, size matchingFiles. Assumes .txt extensions.
        private TestFile[] CreateTestFiles(int matchingFiles)
        {
            TestFile[] testFiles = new TestFile[matchingFiles];
            for (int i = 0; i < matchingFiles; i++)
            {
                testFiles[i] = new TestFile($"file{i}.txt", 1, linesPerMatchingFile);
            }

            return testFiles;
        }

    }
}
