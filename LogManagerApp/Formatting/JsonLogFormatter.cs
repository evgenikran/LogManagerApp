using LogCore.Models;
using System.Text.Json;

namespace LogCore.Formatting
{
    public class JsonLogFormatter : ILogFormatter
    {
        public string Format(LogEntry entry)
        {
            var obj = new
            {
                timestamp = entry.Timestamp,
                level = entry.Level.ToString(),
                message = entry.Message
            };

            return JsonSerializer.Serialize(obj);
        }
    }
}
