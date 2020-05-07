using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookStore.Domain;

namespace BookStore.Data.DbContext.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(t => t.AddressId);
            builder.Property(t => t.AddressId)
                .UseIdentityColumn(1, 1);

            builder.HasOne(t => t.AppUser)
                .WithOne(t => t.Address);

            builder.Property(t => t.FirstName)
                .IsRequired();
            builder.Property(t => t.LastName)
                .IsRequired();
            builder.Property(t => t.Country)
                .IsRequired();
            builder.Property(t => t.City)
                .IsRequired();
            builder.Property(t => t.PostalCode)
                .IsRequired();
            builder.Property(t => t.Street)
                .IsRequired();
        }
    }
}
