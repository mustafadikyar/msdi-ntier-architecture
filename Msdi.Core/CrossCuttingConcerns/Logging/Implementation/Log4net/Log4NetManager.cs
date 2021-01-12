using log4net;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Msdi.Core.CrossCuttingConcerns.Logging.Log4net
{
    /// <summary>
    /// Logger using Log4net
    /// </summary>
    public class Log4NetManager : ILoggerService
    {
        private static ILog _log;

        public Log4NetManager(string name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(File.OpenRead("log4net.config"));

            ILoggerRepository loggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(loggerRepository, xmlDocument["log4net"]);

            _log = LogManager.GetLogger(loggerRepository.Name, name);
        }


        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Log(string message)
        {
            _log.Info(message + Environment.NewLine);
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
            _log.Debug(message + Environment.NewLine);
        }

        /// <summary>
        /// Logs a informational message
        /// </summary>
        /// <param name="message">Informational message to log</param>
        public void LogInfo(string message)
        {
            _log.Info(message + Environment.NewLine);
        }

        /// <summary>
        /// Logs a warning
        /// </summary>
        /// <param name="message">Warning message to log</param>
        public void LogWarn(string message)
        {
            _log.Warn(message + Environment.NewLine);
        }

        /// <summary>
        /// Logs a error message
        /// </summary>
        /// <param name="message">Error to log</param>
        public void LogError(string message)
        {
            _log.Error(message + Environment.NewLine);
        }

        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="exception">Exception to log</param>
        public void LogException(Exception exception)
        {
            _log.Error(exception + Environment.NewLine);
        }
    }
}
