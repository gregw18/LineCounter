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

        public TestFile(string fileName, int blankLines, int nonblankLines)
        {
            FileName = fileName;
            BlankLines = blankLines;
            NonblankLines = nonblankLines;
        }

        public void Create()
        {
            if (File.Exists(FileName))
                File.Delete(FileName);

            using (StreamWriter outFile = new StreamWriter(FileName))
            {
                int maxLines = (BlankLines > NonblankLines) ? BlankLines : NonblankLines;
                for (int i = 0; i < maxLines; i++)
                {
                    if (i < BlankLines) {outFile.WriteLine("");}
                    if (i < NonblankLines) {outFile.WriteLine("not a blank line.");}
                }
            }
        }
    }
}

