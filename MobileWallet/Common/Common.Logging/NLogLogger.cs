using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    public class NLogLogger : Logger, ILogger
    {
        private const string _loggerName = "NLogLogger";       
        public static NLogLogger GetLoggingService()
        {
            var logger = (NLogLogger)LogManager.GetLogger("NLogLogger", typeof(NLogLogger));
            return logger;
        }

        public void Debug(Exception exception, string format, params object[] args)
        {
            if (!base.IsDebugEnabled) return;
            var logEvent = GetLogEvent(_loggerName, LogLevel.Debug, exception, format, args);
            base.Log(typeof(NLogLogger), logEvent);
        }

        public void Error(Exception exception, string format, params object[] args)
        {
            if (!base.IsErrorEnabled) return;
            var logEvent = GetLogEvent(_loggerName, LogLevel.Error, exception, format, args);
            base.Log(typeof(NLogLogger), logEvent);
        }

        public void Fatal(Exception exception, string format, params object[] args)
        {
            if (!base.IsFatalEnabled) return;
            var logEvent = GetLogEvent(_loggerName, LogLevel.Fatal, exception, format, args);
            base.Log(typeof(NLogLogger), logEvent);
        }

        public void Info(Exception exception, string format, params object[] args)
        {
            if (!base.IsInfoEnabled) return;
            var logEvent = GetLogEvent(_loggerName, LogLevel.Info, exception, format, args);
            base.Log(typeof(NLogLogger), logEvent);
        }

        public void Trace(Exception exception, string format, params object[] args)
        {
            if (!base.IsTraceEnabled) return;
            var logEvent = GetLogEvent(_loggerName, LogLevel.Trace, exception, format, args);
            base.Log(typeof(NLogLogger), logEvent);
        }

        public void Warn(Exception exception, string format, params object[] args)
        {
            if (!base.IsWarnEnabled) return;
            var logEvent = GetLogEvent(_loggerName, LogLevel.Warn, exception, format, args);
            base.Log(typeof(NLogLogger), logEvent);
        }

        public void Debug(Exception exception)
        {
            Debug(exception, string.Empty);
        }

        public void Error(Exception exception)
        {
            Error(exception, string.Empty);
        }

        public void Fatal(Exception exception)
        {
            Fatal(exception, string.Empty);
        }

        public void Info(Exception exception)
        {
            Info(exception, string.Empty);
        }

        public void Trace(Exception exception)
        {
            Trace(exception, string.Empty);
        }

        public void Warn(Exception exception)
        {
            Warn(exception, string.Empty);
        }

        private LogEventInfo GetLogEvent(string loggerName, LogLevel level, Exception exception, string format, object[] args)
        {
            string assemblyProp = string.Empty;
            string classProp = string.Empty;
            string methodProp = string.Empty;
            string messageProp = string.Empty;
            string innerMessageProp = string.Empty;

            var logEvent = new LogEventInfo
                (level, loggerName, string.Format(format, args));

            if (exception != null)
            {
                assemblyProp = exception.Source;
                if (exception.TargetSite.DeclaringType != null) 
                    classProp = exception.TargetSite.DeclaringType.FullName;
                methodProp = exception.TargetSite.Name;
                messageProp = exception.Message;

                if (exception.InnerException != null)
                {
                    innerMessageProp = exception.InnerException.Message;
                }
            }

            logEvent.Properties["error-source"] = assemblyProp;
            logEvent.Properties["error-class"] = classProp;
            logEvent.Properties["error-method"] = methodProp;
            logEvent.Properties["error-message"] = messageProp;
            logEvent.Properties["inner-error-message"] = innerMessageProp;

            return logEvent;
        }
    }
}