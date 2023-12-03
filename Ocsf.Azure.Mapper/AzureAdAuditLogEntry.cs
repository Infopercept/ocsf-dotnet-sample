using Csv;
using Ocsf.Schema;

namespace Ocsf.Azure.Mapper
{
    public static class AzureAuditLogMapper
    {
        public static OcsfRoot Map(ICsvLine? line)
        {
            // Header is handled, each line will contain the actual row data
            var dateUtc = DataExtensions.ConvertDateTimeToLong(line["Date (UTC)"]);
            var correlationId = line["CorrelationId"];
            var service = line["Service"];
            var category = line["Category"];
            var activity = line["Activity"];
            var result = line["Result"];
            var resultReason = line["ResultReason"];
            var actorType = line["ActorType"];
            var actorName = actorType == "User" ? line["ActorUserPrincipalName"] : line["ActorDisplayName"];
            var username = line["ActorUserPrincipalName"];

            var ipAddress = line["IPAddress"];

            var ocsf = new OcsfRoot
            {
                ActivityId = 3001,
                Time = dateUtc,
                Message = correlationId,
                CategoryName = category,
                ActivityName = activity,
                Status = result,
                StatusDetail = result,
                Actor = actorType,
                Observables = service,
                User = new User
                {
                    Type = actorType,
                    Name = actorName,
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
                    Name = actorType,
                    IpAddress = ipAddress
                }
            };

            if (actorType == "User")
            {
                ocsf.User.SetUserType(UserType.User);
            }
            else
            {
                ocsf.User.SetUserType(UserType.System);
            }

            return ocsf;
        }
    }
}
