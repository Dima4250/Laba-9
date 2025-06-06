using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_9.Models
{
    public class LogEntry
    {
        public string FileName { get; set; }
        public string ChangeType { get; set; }
        public DateTime Timestamp { get; set; }
        public string SourceDir { get; set; }
        public string TargetDir { get; set; }
    }


}
