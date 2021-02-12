// Create one or more test directories under a temp root dir, containing one or more files, with specified
// number of blank and nonblank lines, for testing the file line counter.

using System.IO;

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
            // Make sure that root dir doesn't already exist, then create it.
            // (If it does exist, remove it, so can be sure will start off empty.)
            Logger.Info("TestDirectoryStructureCreator.CreateStruct starting.");
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

        // Remove the original temp dir, recursively, so that entire test structure is deleted.
        public void RemoveStruct()
        {
            if (Directory.Exists(testStructRootDir))
            {
                Directory.Delete(testStructRootDir, true);
            }
        }
    }
}