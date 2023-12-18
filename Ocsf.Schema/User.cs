using System.Text.Json.Serialization;

namespace Ocsf.Schema
{
    /// <summary>
    /// User [21] object: The User object describes the characteristics of a user/person or a security principal.Defined by D3FEND d3f:UserAccount.
    /// </summary>
    public class User
    {
        [JsonPropertyName("uid")]
        public string? Uid { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }
        
        [JsonPropertyName("type_id")]
        public int? TypeId { get; set; }
        
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        public User() { }

        public User(string? uid, string? name, string? fullName, UserType userType)
        {
            Uid = uid;
            Name = name;
            FullName = fullName;
            TypeId = (int) userType;
            Type = userType.ToString();
        }
    }
}
