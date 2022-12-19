using System;
using System.Linq;
using System.Text;

namespace Eco.Echolon.ApiClient.Model
{
    public class FaultException : Exception
    {
        public FaultException(Fault[] faults) : base(faults.Aggregate(new StringBuilder(),
            (builder, fault) => builder.AppendLine($"FaultCode: {fault.Code}, Message: {fault.Message}"),
            builder => builder.ToString()))
        {
        }
    }
}