using System.IO;
using LogCore.Formatting;
using LogCore.Models;

namespace LogCore.Logging
{
    public static class LoggerFactory
    {
        private const string CentralLogDir = "logs";

        public static ILogger CreateLogger(LogLevel level)
        {
            Directory.CreateDirectory(CentralLogDir);

            string fileName = level switch
            {
                LogLevel.Info => "info.log",
                LogLevel.Warning => "warning.log",
                LogLevel.Error => "error.log",
                _ => "general.log"
            };

            string fullPath = Path.Combine(CentralLogDir, fileName);

            // ТУК демонстрираме Strategy:
            // за Info/Warning -> обикновен текст
            // за Error -> JSON формат
            ILogFormatter formatter = level switch
            {
                LogLevel.Error => new JsonLogFormatter(),
                _ => new PlainTextFormatter()
            };

            return new FileLogger(level, fullPath, formatter);
        }
    }
}
