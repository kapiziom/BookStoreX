using BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.DbContext.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(t => t.CategoryId);
            builder.Property(t => t.CategoryId)
                .UseIdentityColumn(1, 1);

            builder.Property(t => t.CategoryName)
                .IsRequired();

            builder.HasMany(t => t.Books)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryID);
        }
    }
}
