
using Microsoft.Extensions.DependencyInjection;
using Authentication.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;


namespace Authentication.Settings;

public static class IdImDbContextConfiguration
{
    //time before expires token for password reset.
      public static void AddContextIdImDb(this IServiceCollection services, IConfiguration Configuration)
        {
            string? cxStr = Configuration.GetConnectionString("IdImDb");
            services.AddDbContext<IdImDbContext>(db =>
              db.UseMySql(cxStr, ServerVersion.AutoDetect(cxStr), migration =>
              migration.MigrationsAssembly("Authentication")));
        }

}