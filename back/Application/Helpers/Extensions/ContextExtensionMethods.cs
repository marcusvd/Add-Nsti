
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Repository.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Helpers.Extensions;
    public static class ContextExtensionMethods
    {
           public static void AddContextImSystemDb(this IServiceCollection services, IConfiguration Configuration)
        {
            string cxStr = Configuration.GetConnectionString("ImSystemDb");
            services.AddDbContext<ImSystemDbContext>(db =>
              db.UseMySql(cxStr, ServerVersion.AutoDetect(cxStr), migration =>
              migration.MigrationsAssembly("Repository")));
        }
    }