using Amazon.Lambda.S3Events;
using System.Text.Json.Serialization;

namespace Ocsf.Azure.ActiveDirectory;


public partial class Function
{
    [JsonSourceGenerationOptions(WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonSerializable(typeof(S3Event))]
    public partial class LambdaFunctionJsonSerializerContext : JsonSerializerContext
    {

    }
}