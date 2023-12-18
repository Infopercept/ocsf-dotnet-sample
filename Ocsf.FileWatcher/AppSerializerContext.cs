// See https://aka.ms/new-console-template for more information
using System.Text.Json.Serialization;

[JsonSourceGenerationOptions(WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(Settings))]
public partial class AppSerializerContext : JsonSerializerContext
{

}