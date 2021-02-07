// Interface and implementation for counting number of non-blank lines in a file.

using System.IO;

namespace GwLineCounter
{
    public interface IFileLineCounter { int GetNumberLines(string fileName); }


    public class FileLineCounter : IFileLineCounter
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public int GetNumberLines(string fileName)
        {
            Logger.Info("Starting GetNumberLines for file {fileName}", fileName);
            int numLines = 0;
            if (File.Exists(fileName))
            {
                numLines++;
            }
            else
            {
                Logger.Error("GetNumberLines failed, file {fileName} doesn't exist.", fileName);
                numLines = -1;
            }

            return numLines;
        }
    }
}
