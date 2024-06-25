using EA.Tekton.TechnicalTest.Cross.Enums;
using EA.Tekton.TechnicalTest.Cross.Enums.Exts;

using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace EA.Tekton.TechnicalTest.Cross.Helpers;

public static class FunHelper
{
    public const string TimeZoneCst = "Central Standard Time";
    public const string TimeZoneEst = "Eastern Standard Time";
    public const string TimeZonePst = "SA Pacific Standard Time";

    public static string GetDate(DateTime date, CrossEnum.DateFormat dateFormat, CrossEnum.Separator separator)
    {
        var dateProc = string.Empty;
        switch (dateFormat)
        {
            case CrossEnum.DateFormat.Dmy:
                dateProc = $"{date.Day.ToString(CultureInfo.InvariantCulture).Trim().PadLeft(2, '0')}{separator.ToStringAttribute()}{date.Month.ToString(CultureInfo.InvariantCulture).Trim().PadLeft(2, '0')}{separator.ToStringAttribute()}{date.Year.ToString(CultureInfo.InvariantCulture)}";
                break;

            case CrossEnum.DateFormat.Dmyh:
                dateProc = $"{date.Day.ToString(CultureInfo.InvariantCulture).Trim().PadLeft(2, '0')}{separator.ToStringAttribute()}{date.Month.ToString(CultureInfo.InvariantCulture).Trim().PadLeft(2, '0')}{separator.ToStringAttribute()}{date.Year.ToString(CultureInfo.InvariantCulture)} {date.TimeOfDay.ToString().Trim().Substring(0, 8)}";
                break;

            case CrossEnum.DateFormat.Mdy:
                dateProc = $"{date.Month.ToString(CultureInfo.InvariantCulture).Trim().PadLeft(2, '0')}{separator.ToStringAttribute()}{date.Day.ToString(CultureInfo.InvariantCulture).Trim().PadLeft(2, '0')}{separator.ToStringAttribute()}{date.Year.ToString(CultureInfo.InvariantCulture)}";
                break;

            case CrossEnum.DateFormat.Mdyh:
                dateProc = $"{date.Month.ToString(CultureInfo.InvariantCulture).Trim().PadLeft(2, '0')}{separator.ToStringAttribute()}{date.Day.ToString(CultureInfo.InvariantCulture).Trim().PadLeft(2, '0')}{separator.ToStringAttribute()}{date.Year.ToString(CultureInfo.InvariantCulture)} {date.TimeOfDay.ToString().Trim().Substring(0, 8)}";
                break;

            case CrossEnum.DateFormat.Ymd:
                dateProc = $"{date.Year.ToString(CultureInfo.InvariantCulture).Trim()}{separator.ToStringAttribute()}{date.Month.ToString(CultureInfo.InvariantCulture).Trim().PadLeft(2, '0')}{separator.ToStringAttribute()}{date.Day.ToString(CultureInfo.InvariantCulture)}";
                break;

            case CrossEnum.DateFormat.Ymdh:
                dateProc = $"{date.Year.ToString(CultureInfo.InvariantCulture).Trim()}{separator.ToStringAttribute()}{date.Month.ToString(CultureInfo.InvariantCulture).Trim().PadLeft(2, '0')}{separator.ToStringAttribute()}{date.Day.ToString(CultureInfo.InvariantCulture)} {date.TimeOfDay.ToString().Trim().Substring(0, 8)}";
                break;

            case CrossEnum.DateFormat.Hhmmss:
                dateProc = date.TimeOfDay.ToString().Trim()[..8];
                break;
        }
        return dateProc;
    }
    
    public static string GeneratePassword(int lon)
    {
        var password = string.Empty;
        string[] listLetter =
        [
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
            ".","$","*","&","-","+","#", "@", "/",
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
        ];
        var random = new Random();
        for (var i = 0; i < lon; i++)
        {
            var letter = random.Next(0, 100);
            var number = random.Next(0, 9);

            if (letter < listLetter.Length)
            {
                password += listLetter[letter];
            }
            else
            {
                password += number.ToString();
            }
        }
        return $"@Evi-{password}$";
    }
}
