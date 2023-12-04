using System.Text.Json.Serialization;

namespace Ocsf.Schema
{
    public class HttpRequest
    {
        [JsonPropertyName("args")]
        public string? Args { get; set; }

        [JsonPropertyName("url")]
        public Url? Url { get; set; }

        [JsonPropertyName("user_agent")]
        public string? UserAgent { get; set; }

        [JsonPropertyName("http_headers")]
        public List<HttpHeader>? HttpHeaders { get; set; }

        [JsonPropertyName("http_method")]
        public string? HttpMethod { get; set; }
    }
}
