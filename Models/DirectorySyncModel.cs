using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laba_9.Loggers;

namespace Laba_9.Models
{
    public class DirectorySyncModel
    {
        private readonly DirectoryComparer _comparer = new DirectoryComparer();
        private readonly ILogger _logger;
        private DateTime _lastSyncTime;

        public DirectorySyncModel(ILogger logger)
        {
            _logger = logger;
        }

        public List<FileDifference> GetDifferences(string dir1, string dir2)
        {
            var differences = _comparer.CompareDirectories(dir1, dir2);
            return FilterChangesBasedOnLog(differences);
        }

        public void SynchronizeDirectories(List<FileDifference> differences)
        {
            _lastSyncTime = DateTime.Now;

            foreach (var diff in differences)
            {
                try
                {
                    var sourcePath = Path.Combine(diff.SourceDirectory, diff.FileName);
                    var targetPath = Path.Combine(diff.TargetDirectory, diff.FileName);

                    if (diff.Type == FileDifferenceType.Created || diff.Type == FileDifferenceType.Modified)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
                        File.Copy(sourcePath, targetPath, true);
                        _logger.Log(new LogEntry
                        {
                            FileName = diff.FileName,
                            ChangeType = diff.Type.ToString(),
                            SourceDir = diff.SourceDirectory,
                            TargetDir = diff.TargetDirectory
                        });
                    }
                    else if (diff.Type == FileDifferenceType.Deleted)
                    {
                        if (File.Exists(targetPath))
                        {
                            File.Delete(targetPath);
                            _logger.Log(new LogEntry
                            {
                                FileName = diff.FileName,
                                ChangeType = diff.Type.ToString(),
                                SourceDir = diff.SourceDirectory,
                                TargetDir = diff.TargetDirectory
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при синхронизации файла {diff.FileName}: {ex.Message}");
                }
            }
        }

        private List<FileDifference> FilterChangesBasedOnLog(List<FileDifference> differences)
        {
            var filtered = new List<FileDifference>();

            foreach (var diff in differences)
            {
                var filePath = Path.Combine(diff.SourceDirectory, diff.FileName);
                if (!_logger.HasChangedSinceLastSync(diff.FileName, _lastSyncTime))
                {
                    filtered.Add(diff);
                }
            }

            return filtered;
        }
    }
}
