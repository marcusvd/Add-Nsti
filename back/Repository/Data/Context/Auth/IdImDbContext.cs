using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


using Domain.Entities.Authentication;
using Domain.Entities.System.Businesses;
using Domain.Entities.System.Companies;


namespace Repository.Data.Context.Auth;

public class IdImDbContext : IdentityDbContext<UserAccount, Role, int, IdentityUserClaim<int>, UserRole,
IdentityUserLogin<int>,
IdentityRoleClaim<int>,
IdentityUserToken<int>>
{

    public DbSet<CompanyUserAccount> _AU_CompaniesUsersAccounts { get; set; }
    public DbSet<CompanyAuth> _AU_CompaniesAuth { get; set; }
    public DbSet<BusinessAuth> _AU_BusinessesAuth { get; set; }
    public DbSet<TimedAccessControl> _AC_TimedAccessControls { get; set; }


    public IdImDbContext(DbContextOptions<IdImDbContext> opt) : base(opt)
    { }
    protected override void OnModelCreating(ModelBuilder builder)
    {


        // var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
        //     v => DateTime.SpecifyKind(v, DateTimeKind.Unspecified),
        //     v => DateTime.SpecifyKind(v, DateTimeKind.Unspecified)
        // );

        // foreach (var entityType in builder.Model.GetEntityTypes())
        // {
        //     foreach (var property in entityType.GetProperties())
        //     {
        //         if (property.ClrType == typeof(DateTime))
        //         {
        //             property.SetValueConverter(dateTimeConverter);
        //         }
        //     }
        // }

        base.OnModelCreating(builder);




        RenameTablesToTable(builder);
        Identity(builder);

        builder.Entity<CompanyAuth>().HasIndex(x => x.CNPJ).IsUnique(true);

    }

    private void RenameTablesToTable(ModelBuilder builder)
    {
        builder.Entity<UserAccount>().ToTable("_Id_Users");
        builder.Entity<Role>().ToTable("_Id_Roles");
        builder.Entity<UserRole>().ToTable("_Id_UserRoles");

        builder.Entity<IdentityUserClaim<int>>().ToTable("_Id_UserClaims");
        builder.Entity<IdentityRoleClaim<int>>().ToTable("_Id_RoleClaims");
        builder.Entity<IdentityUserLogin<int>>().ToTable("_Id_UserLogins");
        builder.Entity<IdentityUserToken<int>>().ToTable("_Id_UserTokens");

    }

    private void Identity(ModelBuilder builder)
    {
        builder.Entity<UserAccount>(b =>
      {
          // b.ToTable("_Id_Users");
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
            b.HasMany(e => e.UserRoles)
            .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            b.HasData([
                         new Role { Id = 1, Name = "HOLDER", DisplayRole = "Acesso Total", NormalizedName = "HOLDER"},
                        new Role { Id = 2, Name = "SYSADMIN", DisplayRole = "Administrador", NormalizedName = "SYSADMIN" },
                        new Role { Id = 3, Name = "USERS", DisplayRole = "Usuário", NormalizedName = "USERS" }
                         ]);
        });

        builder.Entity<UserRole>(b =>
              {
                  b.HasKey(ur => new { ur.UserId, ur.RoleId });
              });
    }
}

