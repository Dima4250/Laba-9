using Laba_9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Laba_9.Views
{
    public interface IMainView
    {
        string Directory1 { get; set; }
        string Directory2 { get; set; }
        void SetDifferences(List<Models.FileDifference> differences);
        void ShowMessage(string message);
        void ShowLog(List<LogEntry> entries);
    }
}
