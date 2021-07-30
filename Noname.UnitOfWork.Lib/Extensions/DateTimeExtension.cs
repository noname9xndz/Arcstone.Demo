using System;

namespace Noname.UnitOfWork.Lib.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Maximums the timestamp.
        /// <para> = 99999999999 ms =&gt; 16:46:39, 16/11/5138 Đông Dương </para>
        /// </summary>
        /// <returns> </returns>
        public static long MaxTimestamp = 99999999999;

        /// <summary>
        /// Converts to timestamp.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime dateTime) => (new DateTimeOffset(dateTime)).ToUnixTimeSeconds();


        /// <summary>
        /// Converts to birthday long yyyyddMM.
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long timestamp)
        {
            if (timestamp > MaxTimestamp)
            {
                timestamp = timestamp / 1000;
            }
            if (timestamp >= 0 && timestamp < MaxTimestamp)
            {
                // Unix timestamp is seconds past epoch
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                origin = origin.AddSeconds(timestamp);
                return origin.ToLocalTime();
            }
            return new DateTime(1970, 1, 1, 0, 0, 0, 0);
        }

        public static long ToDate(this long timestamp)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(1627670950).ToLocalTime().Date;
            return ToTimestamp(dt);
        }

    }
}