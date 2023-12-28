using Csv;
using Ocsf.Schema;

namespace Ocsf.Azure.Mapper
{
    public static class ActiveDirectoryLogMapper
    {
        public static OcsfRoot? Map(ICsvLine? line)
        {
            if (line == null) return null;

            //Base
            var dateUtc = DataExtensions.ConvertDateTimeToLong(line["Date (UTC)"]);
            
            var status = line["Status"];
            //var requestId = line["Request ID"];
            var userAgent = line["User agent"];
            //var correlationId = line["Correlation ID"];

            //Count: 1 The number of times that events in the same logical group occurred during the event Start Time to End Time period.

            //Severity:
            var authProtocol = line["Authentication Protocol"] == "ropc" ? AuthenticationProtocol.Other : AuthenticationProtocol.OpenID;
            //var latency = line["Latency"];
            var errorCode = line["Sign-in error code"];
            var message = string.IsNullOrEmpty(errorCode) ? status : line["Failure reason"];

            var severity = Severity.Informational;
            
            if(!string.IsNullOrEmpty(errorCode))
            {
                var errorCodeInt = int.Parse(errorCode);
                severity = ActiveDirectoryMappers.GetSeverityLevel(errorCodeInt);
            }

            //Class: 3002 Authentication events report authentication session activities such as user attempts a logon or logoff, successfully or otherwise.

            //Category: category_id, category_name = 3, "IDAM"

            //Authentication Protocol

            //User
            var userId = line["User ID"];
            var username = line["Username"];
            var userFullName = line["User"];
            var userType = line["User type"] == "member" ? UserType.User : UserType.Other;
            var ipAddress = line["IP address"];
            var userLocation = line["Location"];

            //Cloud The Cloud object contains information about a cloud account such as AWS Account ID, regions, etc.
            var cloud = new Cloud
            {
                Provider = "Azure",
                Region = "East US",
                Project = "",
                Zone = "",
                Account = new Account
                {
                    Id = line["Resource tenant ID"],
                    Name = "",
                    TypeId = 6,
                    TypeName = "Azure AD Account"
                }
            };

            //Application: Application,	Application ID,	Resource, Resource ID, Resource tenant ID

            var applicationId = line["Application ID"];
            var applicationName = line["Application"];
            var resource = line["Resource"];
            var resourceId = line["Resource ID"];
            var resourceTenantId = line["Resource tenant ID"];

            var ocsf = new OcsfRoot
            {
                ClassUid = ClassType.Authentication,
                ActivityId = AuthenticationActivity.Login,
                ActivityName = AuthenticationActivity.Login.Name,
                CategoryUid = CategoryType.IDAM,
                CategoryName = CategoryType.IDAM.Name,
                Time = dateUtc,
                Message = message,
                Status = status,
                StatusDetail = status,
                User = new User(userId, username, userFullName, userType),
                Count = 1,
                Cloud = cloud,              
                Severity = severity.ToString(),
                SeverityId = (int) severity,
                SourceEndpoint = new Endpoint
                {
                    IpAddress = ipAddress                    
                },
                AuthProtocol = authProtocol.ToString(),
                AuthProtocolId = (int) authProtocol,
                LogonType = LogonType.Interactive.ToString(),
                LogonTypeId = (int) LogonType.Interactive,
                HttpRequest = new HttpRequest
                {
                    UserAgent = userAgent,
                    Url = new Url
                    {
                        UrlValue = "https://login.microsoftonline.com/organizations/oauth2/v2.0/authorize",
                        WebsiteCategories =
                        [
                            (int) WebsiteCategory.Computer_Information_Security
                        ]
                    }
                }
            };

            return ocsf;
        }
    }
}