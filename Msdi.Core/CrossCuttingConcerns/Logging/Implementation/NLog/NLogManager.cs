using NLog;
using System;

namespace Msdi.Core.CrossCuttingConcerns.Logging.NLog
{
    /// <summary>
    /// Logger using NLog
    /// </summary>
    public class NLogManager : ILoggerService
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Log(string message)
        {
            Logger.Info(message + Environment.NewLine);
        }

        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="exception">Exception to log</param>
        public void Log(Exception exception)
        {
            LogException(exception);
            Log(Environment.NewLine);
        }

        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">Debug Information to log</param>
        public void LogDebug(string message)
        {
            Logger.Debug(message + Environment.NewLine);
        }

        /// <summary>
        /// Logs a informational message
        /// </summary>
        /// <param name="message">Informational message to log</param>
        public void LogInfo(string message)
        {
            Logger.Info(message + Environment.NewLine);
        }

        /// <summary>
        /// Logs a warning
        /// </summary>
        /// <param name="message">Warning message to log</param>
        public void LogWarn(string message)
        {
            Logger.Warn(message + Environment.NewLine);
        }

        /// <summary>
        /// Logs a error message
        /// </summary>
        /// <param name="message">Error to log</param>
        public void LogError(string message)
        {
            Logger.Error(message + Environment.NewLine);
        }

        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="exception">Exception to log</param>
        public void LogException(Exception exception)
        {
            Logger.Error(exception + Environment.NewLine);
        }
    }
}
