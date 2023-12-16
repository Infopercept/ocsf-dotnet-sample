// See https://aka.ms/new-console-template for more information
using System.Text.Json.Serialization;

public class Settings
{
    [JsonPropertyName("OcsfClass")]
    public int OcsfClass { get; set; }

    [JsonPropertyName("ProgramType")]
    public string? ProgramType { get; set; }

    [JsonPropertyName("SourceFolder")]
    public string? SourceFolder { get; set; }

    [JsonPropertyName("DestinationFolder")]
    public string? DestinationFolder { get; set; }

    public override string ToString()
    {
        return $"OcsfClass: {OcsfClass}, ProgramType: {ProgramType}, SourceFolder: {SourceFolder}, DestinationFolder: {DestinationFolder}";
    }

}