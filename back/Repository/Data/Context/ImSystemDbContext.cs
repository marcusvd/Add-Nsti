using Microsoft.EntityFrameworkCore;
using Domain.Entities.Shared;
using Repository.Data.RelationshipEntities;


using Domain.Entities.Customers;
using Domain.Entities.Companies;
using Microsoft.Extensions.Configuration;
using Domain.Entities.Profiles;


namespace Repository.Data.Context;

public class ImSystemDbContext : DbContext
{

    #region Shared
    public DbSet<Address> SD_Addresses { get; set; }
    public DbSet<Contact> SD_Contacts { get; set; }
    public DbSet<SocialNetwork> SD_socialnetworks { get; set; }
    #endregion

    #region Customers/Companies
    public DbSet<Customer> MN_Customers { get; set; }
    public DbSet<Company> MN_Companies { get; set; }
    #endregion

    #region AccountUser
    public DbSet<MyUser> AU_MyUsers { get; set; }
    public DbSet<Profile> AU_ProfileUsers { get; set; }

    #endregion

    private IConfiguration _configuration;
    public ImSystemDbContext(DbContextOptions<ImSystemDbContext> opt, IConfiguration Configuration) : base(opt)
    {
        _configuration = Configuration;

    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Shared
        builder.ApplyConfiguration(new AddressFluentApi());
        builder.ApplyConfiguration(new ContactFluentApi());
        // builder.ApplyConfiguration(new SocialNetworkFluentApi());


        //Customer - Companiy
        builder.ApplyConfiguration(new CustomerFluentApi());
        builder.ApplyConfiguration(new CompanyFluentApi());

        builder.Entity<MyUser>(x => 
        {
            x.HasKey(u => u.UserAccoutnId);
            x.Property(u => u.UserAccoutnId).ValueGeneratedNever();
        });

    }
}