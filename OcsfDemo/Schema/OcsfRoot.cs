﻿using System.Text.Json.Serialization;

namespace OcsfDemo.Schema
{

    /// <summary>
    /// 
    /// </summary>
    public class OcsfRoot
    {
        [JsonPropertyName("http_request")]
        public HttpRequest HttpRequest { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }

        [JsonPropertyName("user")]
        public User User { get; set; }

        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }

        [JsonPropertyName("servity")]
        public string Severity { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("type_name")]
        public string TypeName { get; set; }

        [JsonPropertyName("activity_id")]
        public int ActivityId { get; set; }

        [JsonPropertyName("type_uid")]
        public int TypeUid { get; set; }

        [JsonPropertyName("category_name")]
        public string CategoryName { get; set; }

        [JsonPropertyName("class_uid")]
        public int ClassUid { get; set; }

        [JsonPropertyName("category_uid")]
        public int CategoryUid { get; set; }

        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("timezone_offset")]
        public int TimezoneOffset { get; set; }

        [JsonPropertyName("activity_name")]
        public string ActivityName { get; set; }

        [JsonPropertyName("severity_id")]
        public int SeverityId { get; set; }

        [JsonPropertyName("src_endpoint")]
        public SrcEndpoint SourceEndpoint { get; set; }

        [JsonPropertyName("status_detail")]
        public string StatusDetail { get; set; }

        [JsonPropertyName("status_id")]
        public int StatusId { get; set; }

        [JsonPropertyName("user_result")]
        public UserResult UserResult { get; set; }
    }
}