using System.Text.Json.Serialization;

namespace Ocsf.Schema
{
    /// <summary>
    /// 
    /// </summary>
    public class Product
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("uid")]
        public string Uid { get; set; }

        [JsonPropertyName("lang")]
        public string Language { get; set; }

        [JsonPropertyName("url_string")]
        public string UrlString { get; set; }

        [JsonPropertyName("vendor_name")]
        public string VendorName { get; set; }
    }
}