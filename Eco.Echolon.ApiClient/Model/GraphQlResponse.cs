using System.Linq;

namespace Eco.Echolon.ApiClient.Model
{
    public class GraphQlResponse<T> 
    {
        public T Data { get; }
        public GraphQlError[] Errors { get; }
        public GraphQlResponse(T data, GraphQlError[] errors)
        {
            Data = data;
            Errors = errors;
        }

        public bool HasErrors()
        {
            return Errors.Any();
        }
    }

    public class GraphQlError
    {
        public string ErrorMessage { get; }
        public ErrorLocation[] Location { get; }

        public GraphQlError(string errorMessage, ErrorLocation[] location)
        {
            ErrorMessage = errorMessage;
            Location = location;
        }
    }

    public class ErrorLocation
    {
        public int Line { get; set; }
        public int Column { get; set; }

        public ErrorLocation(int line, int column)
        {
            Line = line;
            Column = column;
        }
    }
}