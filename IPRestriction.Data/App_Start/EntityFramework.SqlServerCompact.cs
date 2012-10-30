using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using IPRestriction.Data.App_Start;
using WebActivator;

[assembly: PreApplicationStartMethod(typeof (EntityFramework_SqlServerCompact), "Start")]

namespace IPRestriction.Data.App_Start
{
    public static class EntityFramework_SqlServerCompact
    {
        public static void Start()
        {
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
        }
    }
}