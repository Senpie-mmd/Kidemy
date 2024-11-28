using Kidemy.Application.Statics;
using System.Globalization;

namespace Kidemy.Application.Convertors;

public static class DateConvertor
{
    public static string ToUserShortTime(this DateTime dateUtc)
    {
        var userCulture = Thread.CurrentThread.CurrentCulture;

        var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(SystemTimeZones.GetTimeZoneStandardNameByCultureName(userCulture.Name));

        return TimeZoneInfo.ConvertTimeFromUtc(dateUtc, userTimeZone).ToString("t", userCulture.DateTimeFormat);
    }

    public static string ToUserLongTime(this DateTime dateUtc)
    {
        var userCulture = Thread.CurrentThread.CurrentCulture;

        var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(SystemTimeZones.GetTimeZoneStandardNameByCultureName(userCulture.Name));

        return TimeZoneInfo.ConvertTimeFromUtc(dateUtc, userTimeZone).ToString("HH:mm:ss", userCulture.DateTimeFormat);
    }

    public static string ToUserDate(this DateTime dateUtc)
    {
        var userCulture = Thread.CurrentThread.CurrentCulture;

        var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(SystemTimeZones.GetTimeZoneStandardNameByCultureName(userCulture.Name));

        return TimeZoneInfo.ConvertTimeFromUtc(dateUtc, userTimeZone).ToString("yyyy/MM/dd", userCulture.DateTimeFormat);
    }

    public static DateTime? ParseUserDateToUTC(this string dateTime)
    {
        var userCulture = Thread.CurrentThread.CurrentCulture;

        var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(SystemTimeZones.GetTimeZoneStandardNameByCultureName(userCulture.Name));

        var successfullyParsed = DateTime.TryParse(dateTime, out DateTime parsedDateTime);

        if (successfullyParsed)
        {
            return TimeZoneInfo.ConvertTimeToUtc(parsedDateTime, userTimeZone);
        }
        else
        {
            return null;
        }

    }

    public static DateTime? ParseUserDateToUTC(this DateTime dateTime)
    {
        var userCulture = Thread.CurrentThread.CurrentCulture;

        var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(SystemTimeZones.GetTimeZoneStandardNameByCultureName(userCulture.Name));

        return TimeZoneInfo.ConvertTimeToUtc(dateTime, userTimeZone);
       
    }

    public static string ToUserLongDateTime(this DateTime dateUtc)
    {
        var userCulture = Thread.CurrentThread.CurrentCulture;

        var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(SystemTimeZones.GetTimeZoneStandardNameByCultureName(userCulture.Name));

        return TimeZoneInfo.ConvertTimeFromUtc(dateUtc, userTimeZone).ToString("yyyy/MM/dd HH:mm:ss", userCulture.DateTimeFormat);
    }
    public static string ToUserLongDateTimeWithoutSecond(this DateTime dateUtc)
    {
        var userCulture = Thread.CurrentThread.CurrentCulture;

        var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(SystemTimeZones.GetTimeZoneStandardNameByCultureName(userCulture.Name));

        return TimeZoneInfo.ConvertTimeFromUtc(dateUtc, userTimeZone).ToString("yyyy/MM/dd HH:mm", userCulture.DateTimeFormat);
    }
    public static string ToUserTimeHourMinute(this DateTime dateUtc)
    {
        var userCulture = Thread.CurrentThread.CurrentCulture;

        var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(SystemTimeZones.GetTimeZoneStandardNameByCultureName(userCulture.Name));

        return TimeZoneInfo.ConvertTimeFromUtc(dateUtc, userTimeZone).ToString(" HH:mm", userCulture.DateTimeFormat);
    }

    public static string ToUserShortDateTime(this DateTime dateUtc)
    {
        var userCulture = Thread.CurrentThread.CurrentCulture;

        var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(SystemTimeZones.GetTimeZoneStandardNameByCultureName(userCulture.Name));

        return TimeZoneInfo.ConvertTimeFromUtc(dateUtc, userTimeZone).ToString("yyyy/MM/dd HH:mm", userCulture.DateTimeFormat);
    }


}

public enum ShamsiDateType
{
    Default,
    ShamsiYear,
    NumericDayNameMonth
}
