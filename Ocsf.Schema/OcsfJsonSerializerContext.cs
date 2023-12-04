using System.Text.Json.Serialization;

namespace Ocsf.Schema
{
    [JsonSourceGenerationOptions(WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonSerializable(typeof(List<OcsfRoot>))]
    [JsonSerializable(typeof(OcsfRoot))]
    public partial class OcsfJsonSerializerContext : JsonSerializerContext
    {
        
    }

}
