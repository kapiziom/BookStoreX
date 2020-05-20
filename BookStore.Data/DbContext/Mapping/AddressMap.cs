using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookStore.Domain;

namespace BookStore.Data.DbContext.Configuration
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(t => t.AddressId);
            builder.Property(t => t.AddressId)
                .UseIdentityColumn(1, 1);

            builder.HasOne(t => t.AppUser)
                .WithOne(t => t.Address)
                .HasForeignKey<AppUser>(t => t.AddressId);
        }
    }
}
