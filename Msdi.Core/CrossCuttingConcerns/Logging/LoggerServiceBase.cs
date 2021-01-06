using Msdi.Core.CrossCuttingConcerns.Logging.ConsoleLog;
using Msdi.Core.CrossCuttingConcerns.Logging.NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Msdi.Core.CrossCuttingConcerns.Logging
{

    public class LoggerServiceBase : ILoggerService
    {

        #region -- Private Attributes --

        /// <summary>
        /// The List of Loggers
        /// </summary>
        private readonly List<ILoggerService> _loggers = null;

        /// <summary>
        /// The console logger
        /// </summary>
        public readonly ConsoleLogManager ConsoleLogger;

        /// <summary>
        /// The log file logger
        /// </summary>
        public readonly NLogManager FileLogger;

        #endregion

        #region -- Constructor -- 

        /// <summary>
        /// Constructor
        /// </summary>
        public LoggerServiceBase()
        {
            if (_loggers == null)
            {
                // Reflection is expensive (takes approx 400ms) but this is called ONLY ONCE because AppLoggers is Singleton
                // As long as all new loggers use IWWILogger as interface, this will work without any modification.
                _loggers = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(asm => asm.GetTypes())
                    .Where(typ => typeof(ILoggerService).IsAssignableFrom(typ) && !typ.IsInterface && !typ.IsAbstract)
                    .Where(typ => typ != typeof(LoggerServiceBase))
                    .Select(typ => Activator.CreateInstance(typ) as ILoggerService)
                    .ToList();


                ConsoleLogger = _loggers.Where(logger => logger.GetType() == typeof(ConsoleLogManager))
                    .Select(logger => (ConsoleLogManager)logger)
                    .FirstOrDefault();

                //-----> NLogManager, Log4netManager etc.
                FileLogger = _loggers.Where(logger => logger.GetType() == typeof(NLogManager))
                    .Select(logger => (NLogManager)logger)
                    .FirstOrDefault();
            }
        }

        #endregion

        #region -- Public Methods for All Loggers --

        /// <summary>
        /// Logs a message for all Loggers in Application
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Log(string message)
        {
            Task.Run(() =>
            {
                _loggers.ForEach(currentLogger =>
                {
                    currentLogger.Log(message);
                });
            });
        }

        /// <summary>
        /// Logs an exception for all Loggers in Application
        /// </summary>
        /// <param name="exception">Exception to log</param>
        public void Log(Exception exception)
        {
            Task.Run(() =>
            {
                _loggers.ForEach(currentLogger =>
                {
                    currentLogger.LogException(exception);
                });

            });
        }

        /// <summary>
        /// Logs a debug message for all Loggers in Application
        /// </summary>
        /// <param name="message">Debug Information to log</param>
        public void LogDebug(string message)
        {
            Task.Run(() =>
            {
                _loggers.ForEach(currentLogger =>
                {
                    currentLogger.LogDebug(message);
                });

            });
        }

        /// <summary>
        /// Logs a informational message for all Loggers in Application
        /// </summary>
        /// <param name="message">Debug information to log</param>
        public void LogInfo(string message)
        {
            Task.Run(() =>
            {
                _loggers.ForEach(currentLogger =>
                {
                    currentLogger.LogInfo(message);
                });

            });
        }

        /// <summary>
        /// Logs a warning for all Loggers in Application
        /// </summary>
        /// <param name="message">Warning message to log</param>
        public void LogWarn(string message)
        {
            Task.Run(() =>
            {
                _loggers.ForEach(currentLogger =>
                {
                    currentLogger.LogWarn(message);
                });

            });
        }

        /// <summary>
        /// Logs a error message for all Loggers in Application
        /// </summary>
        /// <param name="message">Error to log</param>
        public void LogError(string message)
        {
            Task.Run(() =>
            {
                _loggers.ForEach(currentLogger =>
                {
                    currentLogger.LogError(message);
                });

            });
        }

        /// <summary>
        /// Logs an exception for all Loggers in Application
        /// </summary>
        /// <param name="exception">Exception to log</param>
        public void LogException(Exception exception)
        {
            Task.Run(() =>
            {
                _loggers.ForEach(currentLogger =>
                {
                    currentLogger.LogException(exception);
                });

            });
        }

        #endregion

        #region -- Public Methods for Console Logger --

        /// <summary>
        /// Logs a message using console logger
        /// </summary>
        /// <param name="message">Message to log</param>
        public void LogToConsole(string message)
        {
            ConsoleLogger.Log(message);
        }

        /// <summary>
        /// Logs an exception using console logger
        /// </summary>
        /// <param name="exception">Exception to log</param>
        public void LogToConsole(Exception exception)
        {
            ConsoleLogger.LogException(exception);
        }

        /// <summary>
        /// Logs a debug message using console logger
        /// </summary>
        /// <param name="message">Debug Information to log</param>
        public void LogDebugToConsole(string message)
        {
            ConsoleLogger.LogDebug(message);
        }

        /// <summary>
        /// Logs a informational message using console logger
        /// </summary>
        /// <param name="message">Debug information to log</param>
        public void LogInfoToConsole(string message)
        {
            ConsoleLogger.LogInfo(message);
        }

        /// <summary>
        /// Logs a warning using console logger
        /// </summary>
        /// <param name="message">Warning message to log</param>
        public void LogWarnToConsole(string message)
        {
            ConsoleLogger.LogWarn(message);
        }

        /// <summary>
        /// Logs a error message using console logger
        /// </summary>
        /// <param name="message">Error to log</param>
        public void LogErrorToConsole(string message)
        {
            ConsoleLogger.LogError(message);
        }

        /// <summary>
        /// Logs an exception using console logger
        /// </summary>
        /// <param name="exception">Exception to log</param>
        public void LogExceptionToConsole(Exception exception)
        {
            ConsoleLogger.LogError(exception.ToString());
        }

        #endregion

        #region -- Public Methods for NLog File Logger --

        /// <summary>
        /// Logs a message using NLog File Logger
        /// </summary>
        /// <param name="message">Message to log</param>
        public void LogToFile(string message)
        {
            FileLogger.Log(message);
        }

        /// <summary>
        /// Logs an exception using NLog File Logger
        /// </summary>
        /// <param name="exception">Exception to log</param>
        public void LogToFile(Exception exception)
        {
            FileLogger.LogException(exception);
        }

        /// <summary>
        /// Logs a debug message using NLog File Logger
        /// </summary>
        /// <param name="message">Debug Information to log</param>
        public void LogDebugToFile(string message)
        {
            FileLogger.LogDebug(message);
        }

        /// <summary>
        /// Logs a informational message using NLog File Logger
        /// </summary>
        /// <param name="message">Debug information to log</param>
        public void LogInfoToFile(string message)
        {
            FileLogger.LogInfo(message);
        }

        /// <summary>
        /// Logs a warning using NLog File Logger
        /// </summary>
        /// <param name="message">Warning message to log</param>
        public void LogWarnToFile(string message)
        {
            FileLogger.LogWarn(message);
        }

        /// <summary>
        /// Logs a error message using NLog File Logger
        /// </summary>
        /// <param name="message">Error to log</param>
        public void LogErrorToFile(string message)
        {
            FileLogger.LogError(message);
        }

        /// <summary>
        /// Logs an exception using NLog File Logger
        /// </summary>
        /// <param name="exception">Exception to log</param>
        public void LogExceptionToFile(Exception exception)
        {
            FileLogger.LogError(exception.ToString());
        }

        #endregion

    }
}
