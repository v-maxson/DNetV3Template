using Discord;

namespace DNetV3Template
{
    internal class Logger
    {
        private readonly string _guid;

        public Logger()
        {
            _guid = Guid.NewGuid().ToString()[^6..];
        }

        private static async Task Log(LogMessage message)
        {
            await Task.Run(() => Console.WriteLine(message.Stringify()));
        }

        public void Critical(string message, Exception exception)
        {
            _ = Log(new LogMessage(LogSeverity.Critical, _guid, message, exception));
        }

        public void Error(string message, Exception exception = null)
        {
            _ = Log(new LogMessage(LogSeverity.Error, _guid, message, exception));
        }

        public void Warning(string message, Exception exception = null)
        {
            _ = Log(new LogMessage(LogSeverity.Warning, _guid, message, exception));
        }

        public void Info(string message, Exception exception = null)
        {
            _ = Log(new LogMessage(LogSeverity.Info, _guid, message, exception));
        }

        public void Debug(string message, Exception exception = null)
        {
            _ = Log(new LogMessage(LogSeverity.Debug, _guid, message, exception));
        }

        public void Verbose(string message, Exception exception = null)
        {
            _ = Log(new LogMessage(LogSeverity.Verbose, _guid, message, exception));
        }
    }
}