using System;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace Repository.Data.Context.Auth;

public class IdImDbContext : IdentityDbContext<UserAccount, Role, int, IdentityUserClaim<int>, UserRole,
IdentityUserLogin<int>,
IdentityRoleClaim<int>,
IdentityUserToken<int>>
{

    public DbSet<CompanyUserAccount> CompaniesUsersAccounts { get; set; }
    public DbSet<CompanyAuth> CompaniesAuth { get; set; }


    public IdImDbContext(DbContextOptions<IdImDbContext> opt) : base(opt)
    { }
    protected override void OnModelCreating(ModelBuilder builder)
    {


        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => DateTime.SpecifyKind(v, DateTimeKind.Unspecified),
            v => DateTime.SpecifyKind(v, DateTimeKind.Unspecified)
        );

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(dateTimeConverter);
                }
            }
        }

        base.OnModelCreating(builder);

        builder.Entity<UserAccount>(b =>
        {
            b.ToTable("Users");
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.UserAccount)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            b.Property(x => x.Email).IsRequired();
        });

        //Many to maany
        builder.Entity<CompanyUserAccount>().HasKey(uc => new { uc.CompanyAuthId, uc.UserAccountId });

        builder.Entity<CompanyUserAccount>()
        .HasOne(uc => uc.CompanyAuth)
        .WithMany(uc => uc.CompanyUserAccounts)
        .HasForeignKey(uc => uc.CompanyAuthId);

        builder.Entity<CompanyUserAccount>()
        .HasOne(uc => uc.UserAccount)
        .WithMany(uc => uc.CompanyUserAccounts)
        .HasForeignKey(uc => uc.UserAccountId);

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
    }
}

