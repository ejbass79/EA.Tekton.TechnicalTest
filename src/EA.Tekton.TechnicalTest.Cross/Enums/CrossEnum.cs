using EA.Tekton.TechnicalTest.Cross.Enums.Exts;

namespace EA.Tekton.TechnicalTest.Cross.Enums;

public class CrossEnum
{
    public enum DateFormat
    {
        Dmy,
        Dmyh,
        Mdy,
        Mdyh,
        Ymd,
        Ymdh,
        Hhmmss
    }

    public enum Separator
    {
        [EnumExtension("/")]
        Slash,

        [EnumExtension("-")]
        Hyphen,

        [EnumExtension(".")]
        Point,

        [EnumExtension("")]
        Empty
    }

    public enum Status
    {
        [EnumExtension("OK")]
        Ok = 1,

        [EnumExtension("ERROR")]
        Error = 0
    }

    public enum OrderByMethod
    {
        OrderBy,
        OrderByDescending
    }
}
