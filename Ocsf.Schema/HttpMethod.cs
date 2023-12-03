namespace OcsfDemo.Schema
{
    public readonly struct HttpMethod
    {
        private string Value { get; }

        // Constructor to initialize the struct with a string value
        private HttpMethod(string value)
        {
            Value = value;
        }

        // Overriding ToString() for easy display of the string value
        public override string ToString()
        {
            return Value;
        }

        public static readonly HttpMethod CONNECT = new("CONNECT");
        public static readonly HttpMethod DELETE = new("DELETE");
        public static readonly HttpMethod GET = new("GET");
        public static readonly HttpMethod HEAD = new("HEAD");
        public static readonly HttpMethod OPTIONS = new("OPTIONS");
        public static readonly HttpMethod PATCH = new("PATCH");
        public static readonly HttpMethod POST = new("POST");
        public static readonly HttpMethod PUT = new("PUT");
        public static readonly HttpMethod TRACE = new("TRACE");
    }
}
