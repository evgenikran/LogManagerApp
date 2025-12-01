using System.Collections.Generic;
using System.IO;
using System.Linq;
using LogCore.Models;

namespace LogCore.Logging
{
    public sealed class LogManager
    {
        private static readonly System.Lazy<LogManager> _instance =
            new System.Lazy<LogManager>(() => new LogManager());

        public static LogManager Instance => _instance.Value;

        private readonly ILogger _infoLogger;
        private readonly ILogger _warningLogger;
        private readonly ILogger _errorLogger;

        private readonly string _centralLogFile;
        private readonly object _lockObj = new object();

        private LogManager()
        {
            Directory.CreateDirectory("logs");

            _infoLogger = LoggerFactory.CreateLogger(LogLevel.Info);
            _warningLogger = LoggerFactory.CreateLogger(LogLevel.Warning);
            _errorLogger = LoggerFactory.CreateLogger(LogLevel.Error);

            _centralLogFile = Path.Combine("logs", "all.log");
        }

        public void Log(LogLevel level, string message)
        {
            lock (_lockObj) // ❗ НЯМА ПАРАЛЕЛЕН ЗАПИС
            {
                ILogger logger = level switch
                {
                    LogLevel.Info => _infoLogger,
                    LogLevel.Warning => _warningLogger,
                    LogLevel.Error => _errorLogger,
                    _ => _infoLogger
                };

                logger.Log(message);

                var entry = new LogEntry(level, message);
                var line =
                    $"[{entry.Timestamp:yyyy-MM-dd HH:mm:ss}] [{entry.Level}] {entry.Message}";

                File.AppendAllText(_centralLogFile, line + System.Environment.NewLine);
            }
        }

        public IEnumerable<string> ReadByLevel(LogLevel level)
        {
            string fileName = level switch
            {
                LogLevel.Info => "info.log",
                LogLevel.Warning => "warning.log",
                LogLevel.Error => "error.log",
                _ => "general.log"
            };

            string path = Path.Combine("logs", fileName);

            return File.Exists(path)
                ? File.ReadAllLines(path)
                : Enumerable.Empty<string>();
        }

        public IEnumerable<string> ReadAll()
        {
            return File.Exists(_centralLogFile)
                ? File.ReadAllLines(_centralLogFile)
                : Enumerable.Empty<string>();
        }
    }
}
