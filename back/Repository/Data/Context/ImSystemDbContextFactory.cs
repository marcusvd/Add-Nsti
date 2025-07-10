
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Repository.Data.Context;

public class ImSystemDbContextFactory : IDesignTimeDbContextFactory<ImSystemDbContext>
{
    public ImSystemDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ImSystemDbContext>();
        
        // Substitua pela sua connection string real
        var connectionString = "server=localhost;userid=root;password=Nsti@2024$&;database=ImSystemDb";
        
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), 
            opt => opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));

        // Passa uma instância "dummy" de IConfiguration se quiser manter
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string> { 
                { "ConnectionStrings:ImSystemDb", connectionString }
            })
            .Build();

        return new ImSystemDbContext(optionsBuilder.Options, config);
    }
}