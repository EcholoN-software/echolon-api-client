using System;

namespace Eco.Echolon.ApiClient.Filter
{
    public class ParameterNotSuppliedException : Exception
    {
        public ParameterNotSuppliedException(string filterVariableName) : base($"Parameter: {filterVariableName} was not provided")
        {
        }
    }
}