using System;

namespace Eco.Echolon.ApiClient.Log
{
    public class NullLogger : ILog
    {
        private static readonly System.Lazy<NullLogger> Lazy =
            new System.Lazy<NullLogger>((Func<NullLogger>)(() => new NullLogger()));

        private NullLogger()
        {
        }

        public static ILog Instance => (ILog)NullLogger.Lazy.Value;

        public void Verbose(object message)
        {
        }

        public void Verbose(object message, Exception ex)
        {
        }

        public void VerboseFormat(string format, params object[] args)
        {
        }

        public void VerboseFormat(string format, Exception ex, params object[] args)
        {
        }

        public void Trace(object message)
        {
        }

        public void Trace(object message, Exception ex)
        {
        }

        public void TraceFormat(string format, params object[] args)
        {
        }

        public void TraceFormat(string format, Exception ex, params object[] args)
        {
        }

        public void Debug(object message)
        {
        }

        public void Debug(object message, Exception ex)
        {
        }

        public void DebugFormat(string format, params object[] args)
        {
        }

        public void DebugFormat(string format, Exception ex, params object[] args)
        {
        }

        public void Info(object message)
        {
        }

        public void Info(object message, Exception ex)
        {
        }

        public void InfoFormat(string format, params object[] args)
        {
        }

        public void InfoFormat(string format, Exception ex, params object[] args)
        {
        }

        public void Warn(object message)
        {
        }

        public void Warn(object message, Exception ex)
        {
        }

        public void WarnFormat(string format, params object[] args)
        {
        }

        public void WarnFormat(string format, Exception ex, params object[] args)
        {
        }

        public void Error(object message)
        {
        }

        public void Error(object message, Exception ex)
        {
        }

        public void ErrorFormat(string format, params object[] args)
        {
        }

        public void ErrorFormat(string format, Exception ex, params object[] args)
        {
        }

        public void Fatal(object message)
        {
        }

        public void Fatal(object message, Exception ex)
        {
        }

        public void FatalFormat(string format, params object[] args)
        {
        }

        public void FatalFormat(string format, Exception ex, params object[] args)
        {
        }

        public bool IsVerboseEnabled => false;

        public bool IsTraceEnabled => false;

        public bool IsDebugEnabled => false;

        public bool IsInfoEnabled => false;

        public bool IsWarnEnabled => false;

        public bool IsErrorEnabled => false;

        public bool IsFatalEnabled => false;
    }
}