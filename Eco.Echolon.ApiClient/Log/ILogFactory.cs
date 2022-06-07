using System;

namespace Eco.Echolon.ApiClient.Log
{
    public interface ILogFactory
    {
        ILog GetLogger();

        ILog GetLogger(string name);

        ILog GetLogger(Type type);

        bool IsConfigured { get; }
    }
}