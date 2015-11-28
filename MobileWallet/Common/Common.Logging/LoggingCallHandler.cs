using Microsoft.Practices.Unity.InterceptionExtension;
using NLog;
using System;

namespace Common.Logging
{
    public class LoggingCallHandler : ICallHandler
    {      
         private  ILogger _logger;
         public LoggingCallHandler(ILogger logger)
        {
            _logger = logger;
        }
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            if (getNext == null) throw new ArgumentNullException("getNext");

            //Check NoLog attribute
            //bool isNoLogAttr = false;
            //isNoLogAttr = input
            //      .Target
            //      .GetType()
            //      .GetMethods()
            //      .Where(m => m.Name == input.MethodBase.Name)
            //      .FirstOrDefault()
            //      .CustomAttributes
            //      .FirstOrDefault(x => x.AttributeType.Name == "NoLog") != null;

            //if(isNoLogAttr)
            //{
            //    return getNext()(input, getNext);
            //}

            string className = input.Target.GetType().Name;           
            if (LogManager.IsLoggingEnabled())
            {
                WriteLog(className, String.Format("Invoking method {0}.{1}", className, input.MethodBase.Name), LogLevel.Trace);
            }

            // Invoke the next behavior in the chain.
            var result = getNext()(input, getNext);
           
            if (LogManager.IsLoggingEnabled())
            {
                if (result.Exception != null)
                {
                    WriteLog(className,
                        string.Format("Method {0}.{1} threw exception: {2}", className, input.MethodBase.Name, result.Exception.Message),
                        LogLevel.Error);
                }
                else
                {
                    WriteLog(className,
                        string.Format("Method {0}.{1} has successfully executed.", className, input.MethodBase.Name, result.ReturnValue),
                        LogLevel.Trace);
                }
            }

            return result;
        }

        public int Order
        {
            get;
            set;
        }

        private void WriteLog(string className, string message, LogLevel logLevel)
        {
            switch (logLevel.Name)
            {
                case "Debug": _logger.Debug(message); break;
                case "Fatal": _logger.Fatal(message); break;
                case "Error": _logger.Error(message); break;
                case "Info": _logger.Info(message); break;
                case "Trace": _logger.Trace(message); break;
                case "Warn": _logger.Warn(message); break;
                default: _logger.Trace(message); break;
            }
        }
    }
}