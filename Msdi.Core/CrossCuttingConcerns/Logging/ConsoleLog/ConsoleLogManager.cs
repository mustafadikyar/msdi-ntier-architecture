using System;

namespace Msdi.Core.CrossCuttingConcerns.Logging.ConsoleLog
{
    /// <summary>
    /// Console Logging (Can also be done using NLog, Log4net etc.)
    /// This is for demonstration only
    /// </summary>
    public class ConsoleLogManager : ILoggerService
    {
        private static string DateTimeStamp => DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.ffff ");

        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Log(string message)
        {
            Console.WriteLine(DateTimeStamp + "INFO " + message + Environment.NewLine);
        }

        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="exception">Exception to log</param>
        public void Log(Exception exception)
        {
            LogException(exception);
        }

        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">Debug Information to log</param>
        public void LogDebug(string message)
        {
            Console.WriteLine(DateTimeStamp + "DEBUG " + message + Environment.NewLine);
        }

        /// <summary>
        /// Logs a error message
        /// </summary>
        /// <param name="message">Error to log</param>
        public void LogError(string message)
        {
            Console.WriteLine(DateTimeStamp + "ERROR " + message + Environment.NewLine);
        }

        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="exception">Exception to log</param>
        public void LogException(Exception exception)
        {
            Console.WriteLine(DateTimeStamp + "ERROR " + exception + Environment.NewLine);
        }

        /// <summary>
        /// Logs a informational message
        /// </summary>
        /// <param name="message">Debug information to log</param>
        public void LogInfo(string message)
        {
            Console.WriteLine(DateTimeStamp + "INFO " + message + Environment.NewLine);
        }

        /// <summary>
        /// Logs a warning
        /// </summary>
        /// <param name="message">Warning message to log</param>
        public void LogWarn(string message)
        {
            Console.WriteLine(DateTimeStamp + "WARN " + message + Environment.NewLine);
        }
    }
}
