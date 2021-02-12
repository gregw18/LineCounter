// Creates a test file, for testing reading number of nonblank lines.
// Receives name of full path and name of file, number of blank lines and
// number of nonblank lines it should contain. Creates that file, alternately
// emitting blank and nonblank lines until one runs out, then emitting rest of 
// the other type.

using System.IO;

namespace GwLineCounterTest
{
    public class TestFile
    {
        private string fileName;
        private int blankLines;
        private int nonblankLines;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        // fileName should be just name of file, no path. i.e. myfile.txt.
        public TestFile(string myFileName, int numBlankLines, int numNonblankLines)
        {
            fileName = myFileName;
            blankLines = numBlankLines;
            nonblankLines = numNonblankLines;
        }

        public void Create(string destDir)
        {
            string fullPath = Path.Combine(destDir, fileName);
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            if (Directory.Exists(destDir))
            {
                using (StreamWriter outFile = new StreamWriter(fullPath))
                {
                    // Write alternating blank and non-blank lines until hit lower of two,
                    // then write balance of lines.
                    int maxLines = (blankLines > nonblankLines) ? blankLines : nonblankLines;
                    for (int i = 0; i < maxLines; i++)
                    {
                        if (i < blankLines) {outFile.WriteLine("");}
                        if (i < nonblankLines) {outFile.WriteLine("not a blank line.");}
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
