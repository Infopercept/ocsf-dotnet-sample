using System.Text.Json.Serialization;

namespace Ocsf.Schema
{
    public class Account
    {
        [JsonPropertyName("uid")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("type")]
        public string? TypeName { get; set; }

        [JsonPropertyName("type_id")]
        public int TypeId { get; set; }
    }
}
