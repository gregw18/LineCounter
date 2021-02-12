// Interface and implementation for counting number of non-blank lines in a file.

using System.IO;

namespace GwLineCounter
{
    public class FileLineCounter
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public int GetNumberLines(string fileName)
        {
            Logger.Info("Starting GetNumberLines for file {fileName}", fileName);
            int numLines = 0;
            if (File.Exists(fileName))
            {
                using (FileStream fs = File.OpenRead(fileName))
                {
                    using (TextReader reader = new StreamReader (fs))
                    {
                        string thisLine = "";
                        while (reader.Peek() >=0)
                        {
                            thisLine = reader.ReadLine();
                            if (thisLine.Trim().Length > 0)
                            {
                                numLines++;
                            }
                        }
                    }
                }
            }
            else
            {
                Logger.Error("GetNumberLines failed, file {fileName} doesn't exist.", fileName);
                numLines = -1;
            }
            Logger.Info($"{fileName}: {numLines}");
            
            return numLines;
        }
    }
}
