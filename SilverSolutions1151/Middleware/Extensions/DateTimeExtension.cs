namespace SilverSolutions1151.Middleware.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime StartOfDay(this DateTime theDate)
        {
            return theDate.Date;
        }

        public static DateTime EndOfDay(this DateTime theDate)
        {
            return theDate.Date.AddDays(1).AddTicks(-1);
        }
        public static DateTime EnsureTime(this DateTime dateTime)
        {
            if (dateTime.TimeOfDay == TimeSpan.Zero)
            {
                dateTime = dateTime.Add(DateTime.Now.TimeOfDay);
            }
            return dateTime;
        }
    }
}
