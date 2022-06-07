using System;

namespace Eco.Echolon.ApiClient.Log
{
    public class LogService
    {
        private static readonly object SyncRoot = new object();
        private static LogService _instance;
        private ILogFactory _factory;

        public static LogService Instance
        {
            get
            {
                if (LogService._instance == null)
                {
                    lock (LogService.SyncRoot)
                    {
                        if (LogService._instance == null)
                            LogService._instance = new LogService();
                    }
                }

                return LogService._instance;
            }
        }

        public ILogFactory Factory
        {
            get
            {
                if (this._factory == null)
                    this._factory = (ILogFactory)new NullLoggerFactory();
                return this._factory;
            }
            set => this._factory = this._factory == null
                ? value
                : throw new InvalidOperationException("The dependency Factory can only be set once.");
        }

        public ILog GetLogger() => this.Factory.GetLogger();

        public ILog GetLogger(string name) => this.Factory.GetLogger(name);

        public ILog GetLogger(Type type) => this.Factory.GetLogger(type);
    }
}