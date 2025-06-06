using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Laba_9.Models;

namespace Laba_9.Loggers
{
    public class JsonLogger : ILogger
    {
        private readonly string _logFilePath;
        private SyncLog _log;

        public JsonLogger(string logFilePath)
        {
            _logFilePath = logFilePath;
            LoadLog();
        }

        public void Log(LogEntry entry)
        {
            entry.Timestamp = DateTime.Now;
            _log.Entries.Add(entry);
            SaveLog();
        }

        public List<LogEntry> GetLogEntries()
        {
            return _log.Entries;
        }

        public bool HasChangedSinceLastSync(string filePath, DateTime lastSyncTime)
        {
            foreach (var entry in _log.Entries)
            {
                if (entry.FileName == filePath && entry.Timestamp > lastSyncTime)
                    return true;
            }
            return false;
        }

        private void LoadLog()
        {
            if (File.Exists(_logFilePath))
            {
                var json = File.ReadAllText(_logFilePath);
                _log = JsonSerializer.Deserialize<SyncLog>(json);
            }
            else
            {
                _log = new SyncLog();
            }
        }

        private void SaveLog()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(_log, options);
            File.WriteAllText(_logFilePath, json);
        }
    }
}
