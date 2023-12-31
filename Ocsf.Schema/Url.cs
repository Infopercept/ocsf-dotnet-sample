﻿using System.Text.Json.Serialization;


namespace Ocsf.Schema
{
    public class Url
    {
        [JsonPropertyName("port")]
        public int? Port { get; set; }

        [JsonPropertyName("scheme")]
        public string? Scheme { get; set; }

        [JsonPropertyName("path")]
        public string? Path { get; set; }

        [JsonPropertyName("hostname")]
        public string? Hostname { get; set; }

        [JsonPropertyName("query_string")]
        public string? QueryString { get; set; }

        [JsonPropertyName("category_ids")]
        public List<int> WebsiteCategories { get; set; }

        [JsonPropertyName("url_string")]
        public string? UrlValue { get; set; }

        public Url()
        {
            WebsiteCategories = new List<int>();
        }
    }
}
