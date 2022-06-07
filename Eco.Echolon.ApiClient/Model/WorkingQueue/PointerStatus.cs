using System;

namespace Eco.Echolon.ApiClient.Model.WorkingQueue
{
    public class PointerStatus
    {
        public StatusCode Code { get; set; }
        public DateTimeOffset Since { get; set; }
        public string Feedback { get; set; }

        public PointerStatus(StatusCode code, DateTimeOffset since, string feedback)
        {
            Code = code;
            Since = since;
            Feedback = feedback;
        }
    }
}