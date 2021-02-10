﻿/*
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
            cmdLineArgs myArgs = ParseArgs(args);
            myArgs.Display();

            var myCounter = new DirectoryLineCounter();
            myCounter.PopulateList(myArgs.rootDir, myArgs.fileSpec);
            DirectoryLineCounterResults myResults = myCounter.CountLines();
            myResults.Display();

            /*
            Logger.Info("Current working directory={Directory.GetCurrentDirectory()}", Directory.GetCurrentDirectory());

            string fileName = "FileLineCounter.cs";
            if (args.Length == 1)
                fileName = args[0];
            Logger.Info("Working on file {fileName}", fileName);

            GwLineCounter.FileLineCounter myCounter = new GwLineCounter.FileLineCounter ();
            int numLines = myCounter.GetNumberLines (fileName);

            Logger.Info("Number of lines returned is {numLines}", numLines);
            */
        }

        // Parse provided arguments. Could be 0, 1 or two, containing origin directory and fileSpec for files to match.
        // If want to provide fileSpec, have to also provide origin directory. Origin defaults to ., fileSpec
        // defaults to *.*.
        // Does not validate that directory exists or that fileSpec is valid.
        private static cmdLineArgs ParseArgs(string[] args)
        {
            var myArgs = new cmdLineArgs();
            myArgs.rootDir = ".";
            myArgs.fileSpec = "*.*";

            if (args.Length > 2)
            {
                DisplayOptions();
            }
            else if (args.Length > 0)
            {
                myArgs.rootDir = args[0];
                if (args.Length == 2)
                {
                    myArgs.fileSpec = args[1];
                }
            }

            return myArgs;
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

    // Hold the arguments for the program - directory to start in, string containing filespec to match - i.e. *.cs.
    public class cmdLineArgs
    {
        public string rootDir;
        public string fileSpec;

        public void Display()
        {
            Console.WriteLine($"rootDir:  {rootDir}");
            Console.WriteLine($"fileSpec: {fileSpec}");
        }
    }
}
