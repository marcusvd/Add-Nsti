
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Repository.Data.Context;
using Microsoft.EntityFrameworkCore;
using Authentication;

namespace Application.Services.Helpers.Extensions
{
    public static class ContextExtensionMethods
    {
        public static void AddContextIdImDb(this IServiceCollection services, IConfiguration Configuration)
        {
            string cxStr = Configuration.GetConnectionString("IdImDb");
            services.AddDbContext<IdImDbContext>(db =>
              db.UseMySql(cxStr, ServerVersion.AutoDetect(cxStr), migration =>
              migration.MigrationsAssembly("Authentication")));
        }
        public static void AddContextImSystemDb(this IServiceCollection services, IConfiguration Configuration)
        {
            string cxStr = Configuration.GetConnectionString("ImSystemDb");
            services.AddDbContext<ImSystemDbContext>(db =>
              db.UseMySql(cxStr, ServerVersion.AutoDetect(cxStr), migration =>
              migration.MigrationsAssembly("Repository")));
        }
    }
}