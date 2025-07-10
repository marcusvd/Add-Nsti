
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Customers;

namespace Repository.Data.RelationshipEntities
{

    #region Customer
    public class CustomerFluentApi : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {}

    }
    #endregion

    
}