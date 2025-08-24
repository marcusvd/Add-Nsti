using Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Data.RelationshipEntities
{

    #region Shared
    public class AddressFluentApi : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
           
           
         }
    }

    public class ContactFluentApi : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
         
          
         }
    }
    // public class SocialNetworkFluentApi : IEntityTypeConfiguration<SocialNetwork>
    // {
    //     public void Configure(EntityTypeBuilder<SocialNetwork> builder)
    //     {
    //         builder.Ignore(x=> x.UserId);
    //         builder.Ignore(x=> x.User);
    //      }
    // }

    #endregion

}