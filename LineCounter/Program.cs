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
            Logger.Info("Current working directory={Directory.GetCurrentDirectory()}", Directory.GetCurrentDirectory());

            string fileName = "FileLineCounter.cs";
            if (args.Length == 1)
                fileName = args[0];
            Logger.Info("Working on file {fileName}", fileName);

            GwLineCounter.FileLineCounter myCounter = new GwLineCounter.FileLineCounter ();
            int numLines = myCounter.GetNumberLines (fileName);

            Logger.Info("Number of lines returned is {numLines}", numLines);
        }
    }
}
