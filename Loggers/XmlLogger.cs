using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Laba_9.Models;

namespace Laba_9.Loggers
{
    public class XmlLogger : ILogger
    {
        private readonly string _logFilePath;
        private SyncLog _log;

        public XmlLogger(string logFilePath)
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
                var serializer = new XmlSerializer(typeof(SyncLog));
                using (var reader = new StreamReader(_logFilePath))
                {
                    _log = (SyncLog)serializer.Deserialize(reader);
                }
            }
            else
            {
                _log = new SyncLog();
            }
        }

        private void SaveLog()
        {
            var serializer = new XmlSerializer(typeof(SyncLog));
            using (var writer = new StreamWriter(_logFilePath))
            {
                serializer.Serialize(writer, _log);
            }
        }
    }
}
