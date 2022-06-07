using System;
using System.Linq;

namespace Eco.Echolon.ApiClient.Model
{
    public class ApiResult
    {
        public Fault[] Faults { get; }
        public bool IsSucceeded => !Faults.Any();
        public bool IsFaulted => Faults.Any();


        protected ApiResult(Fault[] faults)
        {
            Faults = faults;
        }

        protected ApiResult()
        {
            Faults = Array.Empty<Fault>();
        }

        public static ApiResult<T> Success<T>(T input)
        {
            return new ApiResult<T>(input);
        }

        public static ApiResult Success()
        {
            return new ApiResult();
        }

        public static ApiResult Faulted(Fault[] faults)
        {
            if (faults == null || faults.Length == 0)
                throw new InvalidOperationException("Faults have to be non-null and length greater zero");
            
            return new ApiResult(faults);
        }

        public static ApiResult<T> Faulted<T>(Fault[] faults) 
        {
            return new ApiResult<T>(faults);
        }
    }

    public class Fault
    {
        public string Code { get; }
        public string Message { get; }

        public Fault(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }

    #nullable disable
    public class ApiResult<T> : ApiResult
    {
        private T Data;

        public ApiResult(T data)
        {
            Data = data;
        }

        public ApiResult(Fault[] faults) : base(faults)
        {
        }

        public T GetData() => Data;
    }
    #nullable restore
}