using System.Data.Entity;
using IpRestriction.Logger.Domain;

namespace IPRestriction.Logger.Data
{
    public class Db : DbContext
    {
        public Db(string connString) : base(connString)
        {
        }

        public DbSet<RequestValidationExceptionLog> RequestValidationExceptionLogs { get; set; }
    }
}