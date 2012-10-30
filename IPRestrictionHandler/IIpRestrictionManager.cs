namespace IPRestriction.Handler
{
    public interface IIpRestrictionManager
    {
        void LogAttempt(string ipAddress, string page);
        bool IsAllowed(string ipAddress, string page);
    }
}