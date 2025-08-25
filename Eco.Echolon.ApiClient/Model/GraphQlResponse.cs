using System.Linq;

namespace Eco.Echolon.ApiClient.Model
{
    public class GraphQlResponse<T> : GraphQlResponse
    {
        public GraphQlResponse(T? data, GraphQlError[] errors) : base(errors)
        {
            Data = data;
        }

        public T? Data { get; }
    }

    public class GraphQlResponse
    {
        public GraphQlError[] Errors { get; }

        public GraphQlResponse(GraphQlError[] errors)
        {
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