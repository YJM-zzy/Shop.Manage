using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace Shop.Manage.EntityFramework.Core
{
    [AppDbContext("AppConnection", DbProvider.SqlServer)]
    public class DefaultDbContext : AppDbContext<DefaultDbContext>
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }
    }
}