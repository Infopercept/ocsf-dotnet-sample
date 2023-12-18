using System.Text.Json.Serialization;

namespace Ocsf.Schema
{
    /// <summary>
    /// 
    /// </summary>
    public class Cloud
    {
        [JsonPropertyName("account")]
        public Account? Account { get; set; }

        [JsonPropertyName("provider")]
        public string? Provider { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }

        [JsonPropertyName("project_uid")]
        public string? Project { get; set; }

        [JsonPropertyName("zone")]
        public string? Zone { get; set; }
    }
}
