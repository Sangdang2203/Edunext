namespace Edunext_API.Helpers
{
    public class ApiResponse<T>
    {
        public T Value { get; set; }
        public string Message { get; set; }
        public ApiResponse(T value, string message)
        {
            Value = value;
            Message = message;
        }
    }
}
