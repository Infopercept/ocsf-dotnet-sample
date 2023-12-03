using System.Text.Json.Serialization;

namespace Ocsf.Schema
{
    /// <summary>
    /// 
    /// </summary>
    public class User
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("uid")]
        public string uid { get; set; }

        [JsonPropertyName("type_id")]
        public int TypeId { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        public void SetUserType(UserType userType)
        {
            TypeId = (int)userType;
            Type = userType.ToString();
        }
    }
}
