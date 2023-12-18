using Ocsf.Schema;

namespace Ocsf.Azure.Mapper
{
    public class ActiveDirectoryMappers
    {
        public static Severity GetSeverityLevel(int codeNumber)
        {
            // Compare with known ranges
            if ((codeNumber >= 16000 && codeNumber <= 16003) ||
                (codeNumber == 28002 || codeNumber == 28003) ||
                (codeNumber >= 51000 && codeNumber <= 67003))
            {
                return Severity.Low;
            }
            else if ((codeNumber >= 20001 && codeNumber <= 20033) ||
                     (codeNumber >= 40008 && codeNumber <= 40015) ||
                     (codeNumber >= 50097 && codeNumber <= 50194))
            {
                return Severity.Medium;
            }
            else if (codeNumber == 17003 ||
                     (codeNumber >= 50000 && codeNumber <= 500213) ||
                     (codeNumber >= 70000 && codeNumber <= 7500529))
            {
                return Severity.High;
            }
            else if (codeNumber == 230109 ||
                     (codeNumber >= 50027 && codeNumber <= 50089))
            {
                return Severity.Critical;
            }

            return Severity.Unknown;
        }
    }
}
