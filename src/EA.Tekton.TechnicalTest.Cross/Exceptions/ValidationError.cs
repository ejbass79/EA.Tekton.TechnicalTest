namespace EA.Tekton.TechnicalTest.Cross.Exceptions;

public sealed record ValidationError(string PropertyName, string ErrorMessage);
