using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace SolidJokes.Web.Controllers
{
    public static class Helpers
    {
        //http://stackoverflow.com/questions/1600962/displaying-the-build-date
        public static String GetLinkerTime(this Assembly assembly, TimeZoneInfo target = null)
        {
            var filePath = assembly.Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;

            var buffer = new byte[2048];

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                stream.Read(buffer, 0, 2048);

            var offset = BitConverter.ToInt32(buffer, c_PeHeaderOffset);
            var secondsSince1970 = BitConverter.ToInt32(buffer, offset + c_LinkerTimestampOffset);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var linkTimeUtc = epoch.AddSeconds(secondsSince1970);

            var tz = target ?? TimeZoneInfo.Local;
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(linkTimeUtc, tz);

            // build done on west coast USA (Appveyor)
            // Server is in Ireland so want it to display UK Time.
            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            //var linkerTime = Assembly.GetExecutingAssembly().GetLinkerTime();
            var newDate = TimeZoneInfo.ConvertTime(localTime, TimeZoneInfo.Local, britishZone);

            return newDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture); ;
        }
    }
}