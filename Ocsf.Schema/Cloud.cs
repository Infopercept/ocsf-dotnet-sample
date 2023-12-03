using System.Text.Json.Serialization;

namespace OcsfDemo.Schema
{
    /// <summary>
    /// 
    /// </summary>
    public class Cloud
    {
        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        // TODO: Add Optional parameters
    }
}
