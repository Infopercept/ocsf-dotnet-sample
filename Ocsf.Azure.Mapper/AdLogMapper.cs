using Csv;
using Ocsf.Schema;

namespace Ocsf.Azure.Mapper
{
    public static class AdLogMapper
    {
        public static OcsfRoot Map(ICsvLine? line)
        {
            // Header is handled, each line will contain the actual row data
            var dateUtc = DataExtensions.ConvertDateTimeToLong(line["Date (UTC)"]);
            var requestId = line["Request ID"];
            var userAgent = line["User agent"];
            var correlationId = line["Correlation ID"];
            var userId = line["User ID"];
            var username = line["Username"];
            var userFullName = line["User"];
            var userType = line["User type"];

            var status = line["Status"];

            var ocsf = new OcsfRoot
            {
                ActivityId = 3001,
                Time = dateUtc,
                ClassName = "Azure Active Directory",
                Message = correlationId,
                Status = status,
                StatusDetail = status,
                User = new User
                {
                    Type = userType,
                    Name = username,
                    FullName = userFullName,
                    TypeId = (int)UserType.User,
                    uid = correlationId
                },
                Cloud = new Cloud
                {
                    Provider = "Azure",
                    Region = "East US"
                },
                Severity = Severity.Informational.ToString(),
                SeverityId = (int)Severity.Informational,
                SourceEndpoint = new Endpoint
                {
                    
                }
            };          

            return ocsf;
        }
    }
}