namespace EA.Tekton.TechnicalTest.Cross.MemoryCache.Options
{
    public class CacheOptions
    {
        public bool Enabled { get; set; } = false;
        public int ExpirationTimeMinutes { get; set; }
    }
}
