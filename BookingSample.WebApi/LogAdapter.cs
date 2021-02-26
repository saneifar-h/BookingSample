using System;
using BookingSample.Domain;
using log4net;

namespace BookingSample.WebApi
{
    public class LogAdapter : ILogAdapter
    {
        private readonly ILog _log;

        public LogAdapter(IConfigurationLookup configurationManager)
        {
            var loggerName = configurationManager.GetValue("LoggerName") ?? "Default";
            _log = LogManager.GetLogger(loggerName);
        }

        public void Debug(object obj)
        {
            _log.Debug(obj);
        }

        public void Debug(object obj, Exception exp)
        {
            _log.Debug(obj, exp);
        }

        public void Error(object obj)
        {
            _log.Error(obj);
        }

        public void Error(object obj, Exception exp)
        {
            _log.Error(obj, exp);
        }

        public void Fatal(object obj)
        {
            _log.Fatal(obj);
        }

        public void Fatal(object obj, Exception exp)
        {
            _log.Fatal(obj, exp);
        }

        public void Info(object obj)
        {
            _log.Info(obj);
        }

        public void Info(object obj, Exception exp)
        {
            _log.Info(obj, exp);
        }

        public void Warn(object obj)
        {
            _log.Warn(obj);
        }

        public void Warn(object obj, Exception exp)
        {
            _log.Warn(obj, exp);
        }
    }
}