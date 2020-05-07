using BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.DbContext.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(t => t.BookId);
            builder.Property(t => t.BookId)
                .UseIdentityColumn(1, 1);

            builder.Property(t => t.CategoryID)
                .IsRequired();
            builder.HasOne(t => t.Category)
                .WithMany(t => t.Books)
                .HasForeignKey(t => t.CategoryID)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(t => t.CartElements)
                .WithOne(t => t.Book)
                .HasForeignKey(t => t.BookID);

            builder.Property(t => t.Title)
                .IsRequired();

            builder.Property(t => t.PublishedDate)
                .IsRequired();

            builder.Property(t => t.Publisher)
                .IsRequired();

            builder.Property(t => t.PageCount)
                .IsRequired();

            builder.Property(t => t.Price)
                .IsRequired();

            builder.Property(t => t.Author)
                .IsRequired();

            builder.Property(t => t.InStock)
               .IsRequired();


        }
    }
}
