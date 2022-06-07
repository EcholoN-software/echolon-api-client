using System;

namespace Eco.Echolon.ApiClient.Log
{
    public interface ILog
    {
        void Verbose(object message);

        void Verbose(object message, Exception ex);

        void VerboseFormat(string format, params object[] args);

        void VerboseFormat(string format, Exception ex, params object[] args);

        void Trace(object message);

        void Trace(object message, Exception ex);

        void TraceFormat(string format, params object[] args);

        void TraceFormat(string format, Exception ex, params object[] args);

        void Debug(object message);

        void Debug(object message, Exception ex);

        void DebugFormat(string format, params object[] args);

        void DebugFormat(string format, Exception ex, params object[] args);

        void Info(object message);

        void Info(object message, Exception ex);

        void InfoFormat(string format, params object[] args);

        void InfoFormat(string format, Exception ex, params object[] args);

        void Warn(object message);

        void Warn(object message, Exception ex);

        void WarnFormat(string format, params object[] args);

        void WarnFormat(string format, Exception ex, params object[] args);

        void Error(object message);

        void Error(object message, Exception ex);

        void ErrorFormat(string format, params object[] args);

        void ErrorFormat(string format, Exception ex, params object[] args);

        void Fatal(object message);

        void Fatal(object message, Exception ex);

        void FatalFormat(string format, params object[] args);

        void FatalFormat(string format, Exception ex, params object[] args);

        bool IsVerboseEnabled { get; }

        bool IsTraceEnabled { get; }

        bool IsDebugEnabled { get; }

        bool IsInfoEnabled { get; }

        bool IsWarnEnabled { get; }

        bool IsErrorEnabled { get; }

        bool IsFatalEnabled { get; }
    }
}