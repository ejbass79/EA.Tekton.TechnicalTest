namespace EA.Tekton.TechnicalTest.Cross.Options;

public class IdentityOptions
{
    public bool RequiredDigit { get; set; }
    public int RequiredLength { get; set; }
    public bool RequireLowercase { get; set; }
    public int RequiredUniqueChars { get; set; }
    public bool RequireUppercase { get; set; }
    public int MaxFailedAttempts { get; set; }
    public int LockoutTimeSpanInDays { get; set; }
    public int IterationCount { get; set; }
    public bool RequireNonAlphanumeric { get; set; }
    public bool AllowedForNewUsers { get; set; }
    public bool RequireConfirmedEmail { get; set; }
}
