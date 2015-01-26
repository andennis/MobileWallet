using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Repository;

namespace Pass.Notification.BL.Utils
{
    public static class Logger
    {
        private static readonly Dictionary<int, ILog> Loggers = new Dictionary<int, ILog>();
        private static ILog _log;

        static Logger()
        {
            Init();
        }

        public static void Info(string message, Exception exception = null)
        {
            _log.Info(message, exception);
        }

        public static void InfoFormat(string message, params object[] parameters)
        {
            _log.InfoFormat(message, parameters);
        }

        public static void Error(string message, Exception exception = null)
        {
            _log.Error(message, exception);
        }

        public static void ErrorFormat(string message, params object[] parameters)
        {
            _log.ErrorFormat(message, parameters);
        }

        public static void Warn(string message, Exception exception = null)
        {
            _log.Warn(message, exception);
        }

        public static void WarnFormat(string message, params object[] parameters)
        {
            _log.WarnFormat(message, parameters);
        }

        public static void Debug(string message, Exception exception = null)
        {
            _log.Debug(message, exception);
        }

        public static void DebugFormat(string message, params object[] parameters)
        {
            _log.DebugFormat(message, parameters);
        }

        private static void Init()
        {
            if (_log == null)
            {
                _log = InitLogger();
            }

            ThreadContext.Properties["ThreadId"] = Thread.CurrentThread.ManagedThreadId;
        }

        private static ILog InitLogger(int? siteId = null)
        {
            string siteIdStr = siteId.HasValue ? siteId.ToString() : string.Empty;
            string repositoryName = siteIdStr + "Repository";
            ILoggerRepository loggerRepository = LogManager.GetAllRepositories()
                .FirstOrDefault(r => r.Name == repositoryName)
                                                 ?? LogManager.CreateRepository(repositoryName);

            log4net.Config.XmlConfigurator.Configure(loggerRepository);
            ILog log = LogManager.GetLogger(repositoryName, "Logger");
            IAppender appender = loggerRepository.GetAppenders().ToList().FirstOrDefault(a => a.Name.Contains("Console"));
            if (appender != null)
            {
                appender.Close();
            }

            return log;
        }

    }
}
