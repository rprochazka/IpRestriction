namespace IPRestriction.Handler.Configuration
{
    public interface IProviderSettings
    {
        string ClassName { get; }
        int AttemptThreshold { get; }
        int DisableAccessInSeconds { get; }
    }
}
