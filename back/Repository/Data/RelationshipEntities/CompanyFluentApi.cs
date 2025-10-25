using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


using Domain.Entities.System;
using Domain.Entities.System.Companies;
using Domain.Entities.System.Businesses;


namespace Repository.Data.RelationshipEntities;

#region CompanyProfile
public class CompanyFluentApi : IEntityTypeConfiguration<CompanyProfile>
{
    public void Configure(EntityTypeBuilder<CompanyProfile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.CNPJ).IsUnique(true);

    }
}
#endregion
#region CustomerCompany
public class CustomerCompanyFluentApi : IEntityTypeConfiguration<CustomerCompany>
{
    public void Configure(EntityTypeBuilder<CustomerCompany> builder)
    {
        //Many to maany
        builder.HasKey(uc => new { uc.CustomerId, uc.CompanyId });
        // builder.Ignore(x => x.Id);
    }
}

#endregion
// #region BusinessAuth
// public class BusinessAuthFluentApi : IEntityTypeConfiguration<BusinessAuth>
// {
//     public void Configure(EntityTypeBuilder<BusinessAuth> builder)
//     {
//         //Many to maany
//         builder.HasMany(x => x.Companies).WithOne(x => x.Business).HasForeignKey(fk => fk.BusinessId);
//         // builder.Ignore(x => x.Id);
//     }
// }

// #endregion
#region BusinessProfile
public class BusinessProfileFluentApi : IEntityTypeConfiguration<BusinessProfile>
{
    public void Configure(EntityTypeBuilder<BusinessProfile> builder)
    {
        //Many to maany
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Companies).WithOne(x => x.BusinessProfile).HasForeignKey(fk => fk.BusinessProfileId);

        //Many to maany
        builder.HasMany(x => x.UsersAccounts).WithOne(x => x.BusinessProfile).HasForeignKey(fk => fk.BusinessProfileId);

    }
}

#endregion