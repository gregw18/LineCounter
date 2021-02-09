// Coordinate counting lines in all files in current directory and all subdirectories.
// Also counts number of files that were included in the count.

using System.IO;

namespace GwLineCounter
{
    public class DirectoryLineCounterResults
    {
        public int numFiles;
        public int numLines;

        public DirectoryLineCounterResults()
        {
            numFiles = 0;
            numLines = 0;
        }
    }
}
