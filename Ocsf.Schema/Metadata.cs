using System.Text.Json.Serialization;

namespace Ocsf.Schema
{
    public class Metadata
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("extension")]
        public Extension Extension { get; set; }

        [JsonPropertyName("product")]
        public Product Product { get; set; }

        [JsonPropertyName("labels")]
        public List<string> Labels { get; set; }

        [JsonPropertyName("profiles")]
        public List<string> Profiles { get; set; }

        [JsonPropertyName("log_name")]
        public string LogName { get; set; }

        [JsonPropertyName("log_provider")]
        public string LogProvider { get; set; }

        [JsonPropertyName("log_version")]
        public string LogVersion { get; set; }

        [JsonPropertyName("modified_time")]
        public long ModifiedTime { get; set; }

        [JsonPropertyName("original_time")]
        public string OriginalTime { get; set; }
    }
}
