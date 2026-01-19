using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LogReader
{
    public static class AppLogger
    {
        private static readonly object _lock = new object();
        private static readonly SemaphoreSlim _logLock = new SemaphoreSlim(1, 1);
        private static readonly string _logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

        public static void Log(string message)
        {
            lock (_lock)
            {
                try
                {
                    if (!Directory.Exists(_logDirectory))
                    {
                        Directory.CreateDirectory(_logDirectory);
                    }

                    string logFileName = $"AppLog_{DateTime.Now:yyyy-MM-dd}.txt";
                    string logFilePath = Path.Combine(_logDirectory, logFileName);

                    string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {message}";

                    File.AppendAllText(logFilePath, line + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to write log: {ex.Message}");
                }
            }
        }


        public static async Task LogAsync(string message)
        {
            await _logLock.WaitAsync();
            try
            {
                try
                {
                    if (!Directory.Exists(_logDirectory))
                        Directory.CreateDirectory(_logDirectory);

                    string logFileName = $"AppLog_{DateTime.Now:yyyy-MM-dd}.txt";
                    string logFilePath = Path.Combine(_logDirectory, logFileName);

                    string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {message}";

                    await Task.Run(() => File.AppendAllText(logFilePath, line + Environment.NewLine));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to write log: {ex.Message}");
                }
            }
            finally
            {
                _logLock.Release();
            }
        }


    }
}
