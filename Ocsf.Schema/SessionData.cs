using System.Text.Json.Serialization;

namespace Ocsf.Schema
{
    public class SessionData
    {
        [JsonPropertyName("uid")]
        public string? Uid { get; set; }

        [JsonPropertyName("issuer")]
        public string? Issuer { get; set; }

        [JsonPropertyName("created_time")]
        public long CreatedTime { get; set; }

        [JsonPropertyName("is_remote")]
        public bool IsRemote { get; set; }
    }
}
