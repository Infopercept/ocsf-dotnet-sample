using Csv;
using Ocsf.Schema;

namespace Ocsf.Azure.Mapper
{
    public static class AdLogMapper
    {
        public static OcsfRoot? Map(ICsvLine? line)
        {
            if (line == null) return null;

            //Base
            var status = line["Status"];
            var dateUtc = DataExtensions.ConvertDateTimeToLong(line["Date (UTC)"]);
            var requestId = line["Request ID"];
            var userAgent = line["User agent"];
            var correlationId = line["Correlation ID"];
            
            //User
            var userId = line["User ID"];
            var username = line["Username"];
            var userFullName = line["User"];
            var userType = line["User type"];

            //Cloud


            //Actor

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
                    Uid = correlationId
                },
              /*  Actor = new Actor
                {
                    Id = userId,
                    Type = userType,
                    TypeId = (int)UserType.User,
                    Uid = correlationId
                },*/
                Cloud = new Cloud
                {
                    Provider = "Azure",
                    Region = "East US"                    
                },
                Severity = Severity.Informational.ToString(),
                SeverityId = (int)Severity.Informational,
                SourceEndpoint = new Endpoint
                {
                    
                },
                HttpRequest = new HttpRequest
                {
                    UserAgent = userAgent,
                    Url = new Url
                    {
                        UrlValue = correlationId,
                        WebsiteCategories = new List<WebsiteCategory>
                        {
                            WebsiteCategory.Computer_Information_Security
                        }
                    }
                }
            };          

            return ocsf;
        }
    }
}