using BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.DbContext.Configuration
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(t => t.OrderDetailId);
            builder.Property(t => t.OrderDetailId)
                .UseIdentityColumn(1, 1);

            builder.Property(t => t.NumberOfBooks)
                .IsRequired();

            builder.Property(t => t.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.HasOne(t => t.Order)
                .WithMany(t => t.OrderDetails)
                .HasForeignKey(t => t.OrderID);

            builder.HasOne(t => t.Book)
                .WithMany(t => t.OrderDetails)
                .HasForeignKey(t => t.BookID);
        }
    }
}
