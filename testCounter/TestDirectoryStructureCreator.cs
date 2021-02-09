// Create one or more directories, containing one or more files, with specified number of blank
// and nonblank lines, for testing the file line counter.

using System.IO;

// using GwLineCounterTest;

namespace GwLineCounterTest
{
    public class TestDirectoryStructureCreator
    {
        public readonly string testStructRootDir;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public TestDirectoryStructureCreator()
        {
            string testDirName = Path.GetRandomFileName();
            testStructRootDir = Path.Combine(Path.GetTempPath() + testDirName);
            Logger.Info(testStructRootDir);
        }

        public bool CreateStruct(TestDirectoryContents[] testStruct){
            Logger.Info("TestDirectoryStructureCreator.CreateStruct starting.");
            // Make sure that root dir doesn't already exist, then create it.
            // (If it does exist, remove it, so can be sure will start off empty.)
            if (Directory.Exists(testStructRootDir))
            {
                Directory.Delete(testStructRootDir, true);
            }
            Directory.CreateDirectory(testStructRootDir);

            foreach (TestDirectoryContents testContents in testStruct)
            {
                testContents.Create(testStructRootDir);
            }
            
            return true;
        }

        public void RemoveStruct()
        {
            if (Directory.Exists(testStructRootDir))
            {
                Directory.Delete(testStructRootDir, true);
            }
        }
    }
}