using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



using Domain.Entities.System.Customers;
using Domain.Entities.System.BusinessesCompanies;
using Domain.Entities.System;


namespace Repository.Data.RelationshipEntities;

#region CompanyProfile
public class CompanyFluentApi : IEntityTypeConfiguration<CompanyProfile>
{
    public void Configure(EntityTypeBuilder<CompanyProfile> builder)
    {
        builder.HasKey(x => x.Id);


        // builder.HasMany<UserAccount>(x => x.UserAccounts).WithOne(x => x.Company)
        // .HasForeignKey(x => x.CompanyId).IsRequired(true);

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