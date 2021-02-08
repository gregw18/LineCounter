// Create one or more directories, containing one or more files, with specified number of blank
// and nonblank lines, for testing the file line counter.

using System.IO;

// using GwLineCounterTest;

namespace GwLineCounterTest
{
    public class TestDirectoryStructureCreator
    {
        private string testStructRootDir;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public TestDirectoryStructureCreator()
        {
            string testDirName = Path.GetRandomFileName();
            testStructRootDir = Path.Combine(Path.GetTempPath() + testDirName);
            Logger.Info(testStructRootDir);
        }

        public bool CreateStruct(TestDirectoryContents[] testStruct){
            return true;
        }
    }
}