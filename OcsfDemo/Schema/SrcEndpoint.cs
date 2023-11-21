using System.Text.Json.Serialization;

namespace OcsfDemo.Schema
{
    /// <summary>
    /// 
    /// </summary>
    public class SrcEndpoint
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
