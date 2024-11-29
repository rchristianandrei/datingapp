namespace api.Middleware
{
    public class ApiExceptionModel
    {
        public int StatusCode { get; set; }

        public string Message { get; set; } = "";

        public string Details { get; set; } = "";
    }
}
