/*
    Creates a test file, for testing reading number of blank lines.
    Receives name of full path and name of file, number of blank lines and
    number of non-blank lines it should contain. Creates that file, alternately
    emitting blank and non-blank lines until one runs out, then emitting rest of 
    other type.
*/

using System.IO;

namespace GwLineCounterTest
{
    public class TestFile
    {
        private string FileName;
        private int BlankLines;
        private int NonblankLines;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        // fileName should be just name of file, no path. i.e. myfile.txt.
        public TestFile(string fileName, int blankLines, int nonblankLines)
        {
            FileName = fileName;
            BlankLines = blankLines;
            NonblankLines = nonblankLines;
        }

        public void Create(string destDir)
        {
            string fullPath = Path.Combine(destDir, FileName);
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            if (Directory.Exists(destDir))
            {
                using (StreamWriter outFile = new StreamWriter(fullPath))
                {
                    // Write alternating blank and non-blank lines until hit lower of two,
                    // then write balance of lines.
                    int maxLines = (BlankLines > NonblankLines) ? BlankLines : NonblankLines;
                    for (int i = 0; i < maxLines; i++)
                    {
                        if (i < BlankLines) {outFile.WriteLine("");}
                        if (i < NonblankLines) {outFile.WriteLine("not a blank line.");}
                    }
                }
            }
            else
            {
                Logger.Error("TestFile.Create. Requested directory {destDir} doesn't exist", destDir);
            }
        }
    }
}

