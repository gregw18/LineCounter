// Coordinate counting lines in all files in current directory and all subdirectories.
// Also counts number of files that were included in the line count.

using System.IO;

namespace GwLineCounter
{
    public class DirectoryLineCounter
    {
        private string[] fileNames;
        private string rootDir;
        private string fileSpec;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public void PopulateList(string dir, string myFileSpec)
        {
            Logger.Info($"Starting DirectoryLineCounter.PopulateList for directory {dir}, fileSpec {myFileSpec}.");
            rootDir = dir;
            fileSpec = myFileSpec;
            fileNames = Directory.GetFiles(rootDir, fileSpec, SearchOption.AllDirectories);
        }

        public DirectoryLineCounterResults CountLines()
        {
            Logger.Info($"Starting DirectoryLineCounter.CountLines for directory {rootDir}");
            DirectoryLineCounterResults myResults = new DirectoryLineCounterResults();
            FileLineCounter myCounter = new FileLineCounter();

            foreach (string fileName in fileNames)
            {
                myResults.numFiles++;
                myResults.numLines += myCounter.GetNumberLines(fileName);
            }
            Logger.Info("Finished counting:", myResults);

            return myResults;
        }
    }
}
