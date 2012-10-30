using System.Configuration;
using IPRestriction.Data;
using IpRestriction.Logger.Domain;
using IpRestriction.Logger.Domain.Custom;

namespace IPRestriction.Logger.Data.Custom
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private const string ConnectionStringName = "Logs";
        private readonly Db _context;
        private EFRepository<RequestValidationExceptionLog> _requestValidationExceptionLogs;

        public EFUnitOfWork()
        {
            string connectionString =
                ConfigurationManager
                    .ConnectionStrings[ConnectionStringName]
                    .ConnectionString;

            _context = new Db(ConnectionStringName);
        }

        #region IUnitOfWork Members

        public IRepository<RequestValidationExceptionLog> RequestValidationExceptionLogs
        {
            get
            {
                if (_requestValidationExceptionLogs == null)
                {
                    _requestValidationExceptionLogs = new EFRepository<RequestValidationExceptionLog>(_context);
                }
                return _requestValidationExceptionLogs;
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        #endregion
    }
}