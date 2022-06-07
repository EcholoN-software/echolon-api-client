using System;

namespace Eco.Echolon.ApiClient.Model.Results
{
    #nullable disable
    public class ProcessDefinitionResult 
    {
        public string Id { get; set; }
        public ProcessDescriptionResult Description { get; set; }
        public SchemaDefinitionResult Schema { get; set; }
    }

    public class ProcessDescriptionResult
    {
        public ActivityDescriptionResult[] Activities { get; set; }
        public ConnectionDescriptionResult[] Connections { get; set; }
    }

    public class ConnectionDescriptionResult
    {
        public Decimal FromId { get; set; }
        public Decimal ToId { get; set; }
        public string FromPort { get; set; }
        public string ToPort { get; set; }
    }

    public class ActivityDescriptionResult
    {
        public Decimal Id { get; set; }
        public string Type { get; set; }
        public Metadata[] Metadata { get; set; }
    }
    #nullable restore
}