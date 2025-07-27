using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



using Domain.Entities.Customers;
using Domain.Entities.Companies;


namespace Repository.Data.RelationshipEntities
{

    #region Company
    public class CompanyFluentApi : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany<Customer>(x => x.Customers).WithOne(x => x.Company)
            .HasForeignKey(x => x.CompanyId).IsRequired(true);

            // builder.HasMany<UserAccount>(x => x.UserAccounts).WithOne(x => x.Company)
            // .HasForeignKey(x => x.CompanyId).IsRequired(true);

        }
    }

    #endregion


}