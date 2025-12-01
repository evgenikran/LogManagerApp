using System.IO;
using LogCore.Formatting;
using LogCore.Models;

namespace LogCore.Logging
{
    public class FileLogger : ILogger
    {
        public LogLevel Level { get; }
        private readonly string _filePath;
        private readonly ILogFormatter _formatter;

        public FileLogger(LogLevel level, string filePath, ILogFormatter formatter)
        {
            Level = level;
            _filePath = filePath;
            _formatter = formatter;
        }

        public void Log(string message)
        {
            var entry = new LogEntry(Level, message);
            string line = _formatter.Format(entry);

            File.AppendAllText(_filePath, line + System.Environment.NewLine);
        }
    }
}
