using System.Text.Json.Serialization;

namespace Ocsf.Schema
{
    /// <summary>
    /// 
    /// </summary>
    public class UserResult
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("uid")]
        public string Uid { get; set; }

        [JsonPropertyName("type_id")]
        public int TypeId { get; set; }

        [JsonPropertyName("credential_uid")]
        public string CredentialUid { get; set; }
    }
}
