using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laba_9.Models;
using Laba_9.Views;

namespace Laba_9.Loggers
{
    public interface ILogger
    {
        void Log(LogEntry entry);
        List<LogEntry> GetLogEntries();
        bool HasChangedSinceLastSync(string filePath, DateTime lastSyncTime);
    }

}
