using System.Globalization;

namespace Ocsf.Azure.Mapper
{
    public static class DataExtensions
    {
        public static long ConvertDateTimeToLong(string utcString)
        {
            if (!DateTime.TryParse(utcString, null, DateTimeStyles.AdjustToUniversal, out DateTime dateTime))
            {
                return 0;
            }

            // Unix epoch starts on 1970-01-01T00:00:00Z
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan elapsedTime = dateTime - epoch;
            return elapsedTime.Ticks / TimeSpan.TicksPerMillisecond; // Convert ticks to milliseconds
        }
    }
}
