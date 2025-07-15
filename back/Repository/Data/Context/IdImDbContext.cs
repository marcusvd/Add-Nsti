using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Repository.Data.Context;

public class IdImDbContext : IdentityDbContext<MyUser, Role, int, IdentityUserClaim<int>,
UserRole,
IdentityUserLogin<int>,
IdentityRoleClaim<int>,
IdentityUserToken<int>>
{
    public IdImDbContext(DbContextOptions<IdImDbContext> opt) : base(opt)
    { }
    // private IConfiguration _configuration;
    // public IdImDbContext(DbContextOptions<IdImDbContext> opt, IConfiguration Configuration) : base(opt)
    // {
    //     _configuration = Configuration;
    // }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     string cxStr = _configuration.GetConnectionString("IdImDb");
    //     optionsBuilder.UseMySql(ServerVersion.AutoDetect(cxStr));
    //     // optionsBuilder.UseMySql(ServerVersion.AutoDetect(cxStr), opt => opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    // }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        builder.Entity<MyUser>(b =>
        {
            b.ToTable("Users");
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.MyUser)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        builder.Entity<Role>(b =>
        {
            b.ToTable("Roles");
            b.HasMany(e => e.UserRoles)
            .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        });

        builder.Entity<UserRole>(b =>
              {
                  b.ToTable("UserRoles");
                  b.HasKey(ur => new { ur.UserId, ur.RoleId });
              });

        // builder.Entity<MyUser>().
        // HasOne(u => u.Company)
        // .WithMany()
        // .HasForeignKey(u => u.CompanyId)
        // .OnDelete(DeleteBehavior.Restrict);




    }
}

