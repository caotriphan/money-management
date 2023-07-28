namespace MoneyApi.Extensions;

public static class DateTimeExtension
{
    public static DateTime ToStartOfDate(this DateTime current)
    {
        return new DateTime(current.Year, current.Month, current.Day, 0, 0, 0);
    }

    public static DateTime ToEndOfDate(this DateTime current)
    {
        return current.ToStartOfDate().AddDays(1).AddSeconds(-1);
    }

    public static DateTime ToStartOfWeek(this DateTime current)
    {
        return DateTime.Today.AddDays(-(int)current.DayOfWeek + (int)DayOfWeek.Monday).ToStartOfDate();
    }

    public static DateTime ToEndOfWeek(this DateTime current)
    {
        return current.ToStartOfWeek().AddDays(7).ToEndOfDate();
    }

    public static DateTime ToStartOfMonth(this DateTime current)
    {
        return new DateTime(current.Year, current.Month, 0, 0, 0, 0);
    }

    public static DateTime ToEndOfMonth(this DateTime current)
    {
        return new DateTime(current.Year, current.Month + 1, 0, 0, 0, 0).AddSeconds(-1);
    }
}
