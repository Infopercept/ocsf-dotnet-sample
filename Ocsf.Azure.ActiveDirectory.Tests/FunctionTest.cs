using Xunit;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.S3Events;
using static Amazon.Lambda.S3Events.S3Event;


namespace Ocsf.Azure.ActiveDirectory.Tests;

public class FunctionTest
{
    [Fact]
    public void TestToUpperFunction()
    {
        var evt = new S3Event
        {
            Records =
            [
                new S3EventNotificationRecord()
                {
                    S3 = new S3Entity()
                    {
                        Bucket = new S3BucketEntity()
                        {
                            Name = "test-bucket"
                        },
                        Object = new S3ObjectEntity()
                        {
                            Key = "test-file.txt"
                        }
                    }
                }
                ]
        };

        // Invoke the lambda function and confirm the string was upper cased.
        var context = new TestLambdaContext();
        var output = Function.FunctionHandler(evt, context);

        Assert.Equal($"File {evt.Records[0].S3.Bucket.Name} uploaded to bucket {evt.Records[0].S3.Object.Key}{Environment.NewLine}", output.Result);
    }

    [Fact]
    public void TestMultilineInputFunction()
    {
        var evt = new S3Event
        {
            Records =
          [
              new S3EventNotificationRecord()
                {
                    S3 = new S3Entity()
                    {
                        Bucket = new S3BucketEntity()
                        {
                            Name = "test-bucket"
                        },
                        Object = new S3ObjectEntity()
                        {
                            Key = "test-file.txt"
                        }
                    }
                }
              ]
        };

        // Invoke the lambda function and confirm the string was upper cased.
        var context = new TestLambdaContext();
        var output = Function.FunctionHandler(evt, context);

        Assert.Equal($"File {evt.Records[0].S3.Bucket.Name} uploaded to bucket {evt.Records[0].S3.Object.Key}{Environment.NewLine}", output.Result);
    }
}