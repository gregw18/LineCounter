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

            GwLineCounter.FileLineCounter myCounter = new GwLineCounter.FileLineCounter ();
            int numLines = myCounter.GetNumberLines ("FileLineCounter.cs");

            Logger.Info("Number of lines returned is {numLines}", numLines);
        }
    }
}
