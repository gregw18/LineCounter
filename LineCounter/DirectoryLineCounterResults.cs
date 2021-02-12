// Class tracks results from counting. Tracks number of files looked at and the total number of
// nonblank lines in those files.

using System;

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

        public void Display()
        {
            Console.WriteLine($"{numFiles} matching files found.");
            Console.WriteLine($"{numLines} nonblank lines found.");
        }
    }
}
