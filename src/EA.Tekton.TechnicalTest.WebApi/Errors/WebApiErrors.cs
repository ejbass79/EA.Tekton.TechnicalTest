using EA.Tekton.TechnicalTest.Cross.Abstractions;

namespace EA.Tekton.TechnicalTest.WebApi.Errors;

public static class WebApiErrors
{
    public static Error NotFound = new("LoginRequest.File", "The private key PEM file was not found at the specified path.");
    public static Error FailedLoading = new("LoginRequest.FailedLoading", "An error occurred while loading the private key from the PEM file.");
    public static Error FailedDecrypting = new("LoginRequest.FailedDecrypting", "An unknown error occurred while decrypting the data.");
}
