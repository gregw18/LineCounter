/*
    Program to count number of files and non-blank lines in those files, in current directory and
    all subdirectories. Can pass in pattern for files to include. i.e. *.cs. Defaults to *.* if none given.
*/

using System;
using System.IO;

using NLog;

using GwLineCounter;

namespace LineCounter
{
    class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            Logger.Info("Starting Main");
            ArgParser myParser = new ArgParser();
            if (myParser.ParseArgs(args))
            {
                var myCounter = new DirectoryLineCounter();
                myCounter.PopulateList(myParser.rootDir, myParser.fileSpec);
                DirectoryLineCounterResults myResults = myCounter.CountLines();
                myResults.Display();
            }
            else
            {
                // Didn't get valid arguments, so display intended usage.
                DisplayOptions();
            }
        }

        private static void DisplayOptions()
        {
            string options = "This program counts the number of nonblank lines in files that match the specified file ";
            options += "pattern, in the requested directory and all subdirectories.\n  It accepts 0, 1 or";
            options += " 2 options.\n  The first option is the directory to start in. ";
            options += "It defaults to the current directory.\n  The second option is a wildcard pattern for which files ";
            options += "to work on - e.g. *.txt. Currently, it only supports specifying the extension. It defaults to *.*.\n";
            options += "It will display the number of matching files that it found and the total number of nonblank ";
            options += "lines that it found in those files.";
            Console.WriteLine(options);
        }
    }

}
