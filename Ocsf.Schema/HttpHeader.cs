using System.Text.Json.Serialization;

namespace Ocsf.Schema
{
    public class HttpHeader
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }
}
