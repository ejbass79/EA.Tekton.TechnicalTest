namespace EA.Tekton.TechnicalTest.Cross.Enums.Exts;

public class EnumExtensionAttribute : Attribute
{
    public EnumExtensionAttribute(string? value)
    {
        Value = value;
    }

    public string? Value { get; }
}
