using Microsoft.EntityFrameworkCore;
using Domain.Entities.Shared;
using Repository.Data.RelationshipEntities;
using Domain.Entities.Main.Customers;
using Domain.Entities.Main.Companies;
using Microsoft.Extensions.Configuration;


namespace Repository.Data.Context;

public class IdImDbContext : DbContext
{
    private IConfiguration _configuration;
    public IdImDbContext(DbContextOptions<IdImDbContext> opt, IConfiguration Configuration) : base(opt)
    {
        _configuration = Configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string cxStr = _configuration.GetConnectionString("IdImDb");
        optionsBuilder.UseMySql(ServerVersion.AutoDetect(cxStr), opt => opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

    }
}

