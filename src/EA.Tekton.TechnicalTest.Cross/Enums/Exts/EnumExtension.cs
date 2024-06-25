using System.Collections;

namespace EA.Tekton.TechnicalTest.Cross.Enums.Exts;

public static class EnumExtension
{
    public static string? ToStringAttribute(this Enum value)
    {
        var stringValues = new Hashtable();

        string? output = null;
        var type = value.GetType();

        if (stringValues.ContainsKey(value))
        {
            var stringValueAttribute = (EnumExtensionAttribute)stringValues[value]!;
            output = stringValueAttribute.Value;
        }
        else
        {
            var fi = type.GetField(value.ToString());
            if (fi is not null)
            {
                var attrs = (EnumExtensionAttribute[])fi.GetCustomAttributes(typeof(EnumExtensionAttribute), false);
                if (attrs.Length <= 0)
                {
                    return null;
                }

                stringValues.Add(value, attrs[0]);
                output = attrs[0].Value;
            }
        }

        return output;
    }
}
