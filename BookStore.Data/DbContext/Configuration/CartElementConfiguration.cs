using BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.DbContext.Configuration
{
    public class CartElementConfiguration : IEntityTypeConfiguration<CartElement>
    {
        public void Configure(EntityTypeBuilder<CartElement> builder)
        {
            builder.HasKey(t => t.CartElementId);
            builder.Property(t => t.CartElementId)
                .UseIdentityColumn(1, 1);

            builder.HasOne(t => t.AppUser)
                .WithMany(t => t.CartElements)
                .HasForeignKey(t => t.UserId);

            builder.HasOne(t => t.Book)
                .WithMany(t => t.CartElements)
                .HasForeignKey(t => t.BookID);

            builder.Property(t => t.NumberOfBooks)
                .IsRequired();
            builder.Property(t => t.CreatedDate);

        }
    }
}
