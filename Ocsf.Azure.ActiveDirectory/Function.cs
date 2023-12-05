using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.S3Events;
using Amazon.Lambda.Serialization.SystemTextJson;
using Amazon.S3;
using Amazon.S3.Model;
using Csv;
using Ocsf.Azure.Mapper;
using Ocsf.Schema;
using Parquet.Serialization;
using System.Text;

namespace Ocsf.Azure.ActiveDirectory;

//[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

public class Function
{
    private static CsvOptions csvOptions = new()
    {
        RowsToSkip = 0, // Allows skipping of initial rows without csv data
                        //SkipRow = (row, idx) => string.IsNullOrEmpty(row) || row[0] == '#',
        Separator = ',', // Autodetects based on first row
        TrimData = false, // Can be used to trim each cell
        Comparer = null, // Can be used for case-insensitive comparison for names
        HeaderMode = HeaderMode.HeaderPresent, // Assumes first row is a header row
        ValidateColumnCount = false, // Checks each row immediately for column count
        ReturnEmptyForMissingColumn = false, // Allows for accessing invalid column names
        Aliases = null, // A collection of alternative column names
        AllowNewLineInEnclosedFieldValues = false, // Respects new line (either \r\n or \n) characters inside field values enclosed in double quotes.
        AllowBackSlashToEscapeQuote = false, // Allows the sequence "\"" to be a valid quoted value (in addition to the standard """")
        AllowSingleQuoteToEncloseFieldValues = false, // Allows the single-quote character to be used to enclose field values
        NewLine = Environment.NewLine // The new line string to use when multiline field values are read (Requires "AllowNewLineInEnclosedFieldValues" to be set to "true" for this to have any effect.)
    };

    /// <summary>
    /// The main entry point for the custom runtime.
    /// </summary>
    /// <param name="args"></param>
    private static async Task Main(string[] args)
    {
        Func<S3Event, ILambdaContext, Task<string>> handler = FunctionHandler;
        await LambdaBootstrapBuilder.Create(handler, new DefaultLambdaJsonSerializer())
            .Build()
            .RunAsync();
    }

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    ///
    /// To use this handler to respond to an AWS event, reference the appropriate package from 
    /// https://github.com/aws/aws-lambda-dotnet#events
    /// and change the string input parameter to the desired event type.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async static Task<string> FunctionHandler(S3Event evt, ILambdaContext context)
    {
        var s3Client = new AmazonS3Client();

        var bucketName = Environment.GetEnvironmentVariable("bucket_name");

        var accountId = Environment.GetEnvironmentVariable("account_id");
        var region = Environment.GetEnvironmentVariable("region");
        var sourceLocation = Environment.GetEnvironmentVariable("source_location");
        var eventDay = DateTime.UtcNow.ToString("yyyyMMdd");
        var destinationFileName = DateTime.UtcNow.ToString("HHmmss") + ".parquet";

        var destinationKey = string.Concat(sourceLocation, "/", "1.0/", "region=", region, "/", "accountId=", accountId, "/", "eventDay=", eventDay, "/", destinationFileName);

        context.Logger.LogLine("Destination bucket: " + bucketName);
        context.Logger.LogLine("Destination key: " + destinationKey);

        var output = new StringBuilder();

        try
        {
            var record = evt.Records[0];
            string sourceBucket = record.S3.Bucket.Name;
            string fileName = record.S3.Object.Key;
            output.AppendLine($"File {fileName} uploaded to bucket {sourceBucket}");
            context.Logger.LogLine($"File {fileName} uploaded to bucket {sourceBucket}");

            // Read the CSV file from the source bucket
            using var response = await s3Client.GetObjectAsync(sourceBucket, fileName);
            var list = new List<OcsfRoot>();
            foreach (var line in CsvReader.ReadFromStream(response.ResponseStream))
            {
                OcsfRoot? item = ActiveDirectoryLogMapper.Map(line);
                if (item != null)
                {
                    list.Add(item);
                }
            }

            // Write the modified records to a new CSV file
            using var stream = new MemoryStream();

            //using var writer = new Utf8JsonWriter(stream);
            //JsonSerializer.Serialize(writer, list, OcsfJsonSerializerContext.Default.ListOcsfRoot);
            //writer.Flush();

            await ParquetSerializer.SerializeAsync(list, stream);
            
            stream.Position = 0;

            // Upload the modified file to the destination bucket
            await s3Client.PutObjectAsync(new PutObjectRequest
            {
                BucketName = bucketName,
                Key = destinationKey,
                InputStream = stream
            });

            context.Logger.LogLine($"File processed and uploaded.");
        }
        catch (Exception ex)
        {
            context.Logger.LogLine($"Error: {ex.Message}");
        }

        return output.ToString();
    }
}