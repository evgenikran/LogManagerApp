using LogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogCore.Formatting
{
    public interface ILogFormatter
    {
        string Format(LogEntry entry);
    }
}