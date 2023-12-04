using System.Text.Json.Serialization;

namespace Ocsf.Schema
{
    public class ServiceData
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("version")]
        public string? Version { get; set; }

        [JsonPropertyName("uid")]
        public string? Uid { get; set; }

        [JsonPropertyName("labels")]
        public string[]? Labels { get; set; }
    }
}
