using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laba_9.Models;
using Laba_9.Views;
using Laba_9.Loggers;

namespace Laba_9.Presenters
{
    public class MainPresenter
    {
        private readonly IMainView _view;
        private readonly DirectorySyncModel _model;
        private List<FileDifference> _currentDifferences;
        private readonly ILogger _xmlLogger;
        private readonly ILogger _jsonLogger;

        public MainPresenter(IMainView view)
        {
            _view = view;
            _xmlLogger = new XmlLogger("sync_log.xml");
            _jsonLogger = new JsonLogger("sync_log.json");
            _model = new DirectorySyncModel(_xmlLogger); // Используем XML по умолчанию
            _currentDifferences = new List<FileDifference>();
        }

        public void CompareDirectories()
        {
            _currentDifferences = _model.GetDifferences(_view.Directory1, _view.Directory2);
            _view.SetDifferences(_currentDifferences);
        }

        public void SynchronizeDirectories()
        {
            if (_currentDifferences.Count == 0)
            {
                _view.ShowMessage("Нет различий для синхронизации");
                return;
            }

            _model.SynchronizeDirectories(_currentDifferences);
            _view.ShowMessage("Синхронизация завершена");
            CompareDirectories();
        }

        public void ViewXmlLog()
        {
            var entries = _xmlLogger.GetLogEntries();
            _view.ShowLog(entries);
        }

        public void ViewJsonLog()
        {
            var entries = _jsonLogger.GetLogEntries();
            _view.ShowLog(entries);
        }
    }
}
