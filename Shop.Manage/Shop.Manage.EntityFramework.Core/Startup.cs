using Furion;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Manage.EntityFramework.Core
{
    public class Startup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
                options.AddDbPool<DefaultDbContext>();
            }, "Shop.Manage.Database.Migrations");
        }
    }
}