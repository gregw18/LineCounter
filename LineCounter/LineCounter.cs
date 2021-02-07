using System;

namespace oldGwLineCounter
{
    class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public static void DoMe(string[] args)
        {
           Logger.Info("Hello from DoMe");
           foreach (String str in args)
                Console.WriteLine (str);
        }
    }
}