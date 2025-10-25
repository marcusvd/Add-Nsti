using Microsoft.EntityFrameworkCore;
using Domain.Entities.Shared;
using Microsoft.Extensions.Configuration;


using Repository.Data.RelationshipEntities;
using Domain.Entities.System.Customers;
using Domain.Entities.System.Profiles;
using Domain.Entities.Authentication;
using Domain.Entities.System.Businesses;
using Domain.Entities.System.Companies;

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
    public DbSet<BusinessProfile> MN_businesses_profiles { get; set; }
    public DbSet<CompanyProfile> MN_Companies_profiles { get; set; }
    #endregion

    #region AccountUser
    public DbSet<UserProfile> PF_UserProfiles { get; set; }
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
        //builder.ApplyConfiguration(new SocialNetworkFluentApi());


        //Customer - Companiy
        builder.ApplyConfiguration(new CustomerFluentApi());
        builder.ApplyConfiguration(new CompanyFluentApi());
        builder.ApplyConfiguration(new CustomerCompanyFluentApi());
        builder.ApplyConfiguration(new BusinessProfileFluentApi());

        //builder.Entity<UserProfile>(x => 
        // {
        //     x.HasKey(u => u.UserAccoutnId);
        //     x.Property(u => u.UserAccoutnId).ValueGeneratedNever();
        // });

    }
}