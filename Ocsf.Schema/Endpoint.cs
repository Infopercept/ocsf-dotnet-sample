using System.Text.Json.Serialization;

namespace Ocsf.Schema
{
    /// <summary>
    /// TODO: Reevalute this class. It should sync with documentation and base class for NetworkEndpoint
    /// </summary>
    public class Endpoint
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("ip")]
        public string IpAddress { get; set; }

        [JsonPropertyName("hostname")]
        public string Hostname { get; set; }

        [JsonPropertyName("mac")]
        public string MacAddress { get; set; }

        [JsonPropertyName("instance_uid")]
        public string InstanceUid { get; set; }

        [JsonPropertyName("interface_name")]
        public string InterfaceName { get; set; }

        [JsonPropertyName("interface_uid")]
        public string InterfaceUid { get; set; }

        [JsonPropertyName("svc_name")]
        public string ServiceName { get; set; }
    }
}
