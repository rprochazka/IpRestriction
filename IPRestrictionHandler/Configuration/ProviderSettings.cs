namespace IPRestriction.Handler.Configuration
{
    public class ProviderSettings : IProviderSettings
    {
        public string ClassName { get; set; }

        public int AttemptThreshold { get; set; }

        public int DisableAccessInSeconds { get; set; }
    }
}
