using System;

namespace Msdi.Core.CrossCuttingConcerns.Logging
{
    public interface ILoggerService
    {
        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="message">Message to log</param>
        void Log(string message);

        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="exception">Exception to log</param>
        void Log(Exception exception);

        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">Debug Information to log</param>
        void LogDebug(string message);

        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">Debug information to log</param>
        void LogInfo(string message);

        /// <summary>
        /// Logs a warning
        /// </summary>
        /// <param name="message">Warning message to log</param>
        void LogWarn(string message);

        /// <summary>
        /// Logs a error message
        /// </summary>
        /// <param name="message">Error to log</param>
        void LogError(string message);

        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="exception">Exception to log</param>
        void LogException(Exception exception);
    }
}
