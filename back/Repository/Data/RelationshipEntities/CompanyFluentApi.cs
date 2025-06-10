using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


using Domain.Entities.Authentication;
using Domain.Entities.Main.Customers;
using Domain.Entities.Main.Companies;


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

            builder.HasMany<MyUser>(x => x.MyUsers).WithOne(x => x.Company)
            .HasForeignKey(x => x.CompanyId).IsRequired(true);

        }
    }

    #endregion


}