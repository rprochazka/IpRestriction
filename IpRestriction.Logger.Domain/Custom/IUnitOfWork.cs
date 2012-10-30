namespace IpRestriction.Logger.Domain.Custom
{
    public interface IUnitOfWork
    {
        IRepository<RequestValidationExceptionLog> RequestValidationExceptionLogs { get; }        
        void Commit();
    }
}
