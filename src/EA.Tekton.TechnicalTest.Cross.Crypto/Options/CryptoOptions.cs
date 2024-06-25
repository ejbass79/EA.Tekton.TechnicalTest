namespace EA.Tekton.TechnicalTest.Cross.Crypto.Options;

public class CryptoOptions
{
    public bool Enabled { get; set; } = false;
    public string PemFileName { get; set; } = null!;
    public string DataProtectionSecret { get; set; } = null!;
    public double DataProtectionTimeLifeHours { get; set; }
}
