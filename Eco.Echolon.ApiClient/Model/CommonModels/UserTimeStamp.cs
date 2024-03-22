using System;

namespace Eco.Echolon.ApiClient.Model.CommonModels
{
    public class UserTimeStamp
    {
        public DateTimeOffset On { get; }
        public string By { get; }

        public UserTimeStamp(DateTimeOffset on, string by)
        {
            On = on;
            By = by;
        }
    }
}