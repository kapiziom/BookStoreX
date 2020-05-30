using BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.DbContext.Mapping
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(t => t.OrderId);
            builder.Property(t => t.OrderId)
                .UseIdentityColumn(1, 1);

            builder.HasOne(t => t.AppUser)
                .WithMany(t => t.Orders)
                .HasForeignKey(t => t.UserId);

            builder.HasMany(t => t.OrderDetails)
                .WithOne(t => t.Order)
                .HasForeignKey(t => t.OrderID);

            builder.Property(t => t.Email)
                .IsRequired();

            builder.Property(t => t.Phone)
                .IsRequired();

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

            builder.Property(t => t.Number)
                .IsRequired();

            builder.Property(t => t.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(t => t.OrderDate)
                .IsRequired();

            builder.Property(t => t.IsShipped)
                .IsRequired();
        }
    }
}
