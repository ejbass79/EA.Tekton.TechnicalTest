namespace EA.Tekton.TechnicalTest.Cross.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }
}
