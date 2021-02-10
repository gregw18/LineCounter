// Parse provided arguments. Could be 0, 1 or two, containing origin directory and fileSpec for files to match.
// If want to provide fileSpec, have to also provide origin directory. Origin defaults to ., fileSpec
// defaults to *.*.
// Does not validate that directory exists or that fileSpec is valid.

using System;

//using NLog;

namespace GwLineCounter
{
    public class ArgParser
    {
        public string rootDir;
        public string fileSpec;

        public bool ParseArgs(string[] args)
        {
            bool succeeded = false;

            rootDir = ".";
            fileSpec = "*.*";


            if (args.Length == 0)
            {
                // Nothing provided, so use defaults.
                succeeded = true;
            }
            else if (args.Length > 2)
            {
                // Too much provided, give error.
                succeeded = false;
            }
            else
            {
                // Have 1 or 2 args. Grab first as root dir.
                rootDir = args[0];
                if (args.Length == 2)
                {
                    // If have 2 args, second is file spec.
                    fileSpec = args[1];
                }
                succeeded = true;
            }

            return succeeded;
        }
 
        public void Display()
        {
            Console.WriteLine($"rootDir:  {rootDir}");
            Console.WriteLine($"fileSpec: {fileSpec}");
        }
    }
}