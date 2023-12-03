using System.Text.Json.Serialization;

namespace OcsfDemo.Schema
{
    public class Url
    {
        [JsonPropertyName("port")]
        public int Port { get; set; }

        [JsonPropertyName("scheme")]
        public string Scheme { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("hostname")]
        public string Hostname { get; set; }

        [JsonPropertyName("query_string")]
        public string QueryString { get; set; }

        [JsonPropertyName("category_ids")]
        public List<WebsiteCategory> WebsiteCategories { get; set; }

        [JsonPropertyName("url_string")]
        public string UrlValue { get; set; }
    }
}
