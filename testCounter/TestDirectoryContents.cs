// Class that describes the contents of a test directory. I.e. the directory name, the filespec for files
// that we want to match, the number of matching files, the number of non-matching files and the number of 
// blank and nonblank lines in each matching file. It also creates the directory, under a given parent,
// with the requested contents.

using System;
using System.IO;

namespace GwLineCounterTest
{
    public class TestDirectoryContents
    {
        public string DirName;
        public TestFile[] TestFiles;
        private int numNonMatchingFiles;
        private int numMatchingFiles;
        private string fileSpec;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public TestDirectoryContents(string dirName, string myFileSpec, int matchingFiles, int nonMatchingFiles)
        {
            DirName = dirName;
            // TestFiles = new TestFile[matchingFiles];
            numNonMatchingFiles = nonMatchingFiles;
            numMatchingFiles = matchingFiles;
            fileSpec = myFileSpec;
        }

        public bool Create(string rootDir, TestFile[] testFiles)
        {
            bool succeeded = false;

            TestFiles = testFiles;
            try
            {
                
                if (Directory.Exists(rootDir))
                {
                    string fullDestDir = Path.Combine(rootDir, DirName);
                    Directory.CreateDirectory(fullDestDir);

                    for (int i = 0; i < numNonMatchingFiles; i++)
                    {
                        // Create non-matching file.
                        CreateNonmatchingFile(fullDestDir);
                    }

                    foreach (TestFile testFile in TestFiles)
                    {
                        // Create matching file.
                        testFile.Create(fullDestDir);
                    }
                    succeeded = true;
                }
                else
                {
                    Logger.Error("TestDirectoryContents.Create. Provided root directory {rootDir} does not exist.", rootDir);
                }
            }
            catch (IOException ex)
            {
                Logger.Error("Exception in TestDirectoryContents.Create", ex);
                throw;
            }

            return succeeded;
        }

        // Create a single file, that doesn't match the fileSpec.
        private bool CreateNonmatchingFile(string fullDestDir)
        {
            bool succeeded = false;

            string fileName = GetNonmatchingFilename(fullDestDir);
            string fullPath = Path.Combine(fullDestDir, fileName);

            if (Directory.Exists(fullDestDir))
            {
                using (StreamWriter outFile = new StreamWriter(fullPath))
                {
                    outFile.WriteLine("Not a match.");
                }
                succeeded = true;
            }
            else
            {
                Logger.Error("TestFile.Create. Requested directory {fullDestDir} doesn't exist", fullDestDir);
            }

            return succeeded;
        }

        // Create a random filename, that doesn't match our fileSpec, and that doesn't exist in our directory.
        // Returns just file name, not path.
        private string GetNonmatchingFilename(string fullDestDir)
        {
            string randomName = "";
            while (true)
            {
                randomName = Path.GetRandomFileName();
                if (Path.GetExtension(randomName) == Path.GetExtension(fileSpec))
                {
                     continue;
                }
                else if (File.Exists(Path.Combine(fullDestDir, randomName))) 
                {
                    continue;
                }
                else 
                {
                    break;
                } 
            }
            Logger.Info("TestDirectoryContents.GetNonmatchingFilename returned filename {randomName}", randomName);
            
            return randomName;
        }
    }
}
