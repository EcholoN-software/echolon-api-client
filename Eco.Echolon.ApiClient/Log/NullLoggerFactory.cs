using System;

namespace Eco.Echolon.ApiClient.Log
{
    internal class NullLoggerFactory : ILogFactory
    {
        public ILog GetLogger() => NullLogger.Instance;

        public ILog GetLogger(string name) => NullLogger.Instance;

        public ILog GetLogger(Type type) => NullLogger.Instance;

        public bool IsConfigured { get; private set; }
    }
}