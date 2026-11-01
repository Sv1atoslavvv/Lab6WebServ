using System.Text;
using Microsoft.Extensions.Logging;

namespace EmailSenderWeb.Logging;

public sealed class FileLoggerProvider : ILoggerProvider
{
    private readonly string _logDirectory;
    private readonly object _sync = new();

    public FileLoggerProvider(string logDirectory)
    {
        _logDirectory = logDirectory;
        Directory.CreateDirectory(_logDirectory);
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(categoryName, _logDirectory, _sync);
    }

    public void Dispose()
    {
    }

    private sealed class FileLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly string _logDirectory;
        private readonly object _sync;

        public FileLogger(string categoryName, string logDirectory, object sync)
        {
            _categoryName = categoryName;
            _logDirectory = logDirectory;
            _sync = sync;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None && logLevel >= LogLevel.Information;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var timestamp = DateTimeOffset.Now;
            var message = formatter(state, exception);
            if (exception is not null)
            {
                message = $"{message}{Environment.NewLine}{exception}";
            }

            var fileName = $"requests-{timestamp:yyyy-MM-dd}.log";
            var filePath = Path.Combine(_logDirectory, fileName);
            var line = $"{timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} " +
                       $"[{logLevel}] {_categoryName}: {message}{Environment.NewLine}";

            lock (_sync)
            {
                File.AppendAllText(filePath, line, Encoding.UTF8);
            }
        }
    }
}
