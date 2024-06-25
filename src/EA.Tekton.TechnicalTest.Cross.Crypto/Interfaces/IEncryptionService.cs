namespace EA.Tekton.TechnicalTest.Cross.Crypto.Interfaces;

public interface IEncryptionService
{
    string Protect(string plainText);

    string Unprotect(string cipherText);

    string? ProtectToTimeLimited(string plainText, double timeLifeHours = 0);

    (bool isSuccess, string message) UnprotectToTimeLimited(string cipherText);
}
