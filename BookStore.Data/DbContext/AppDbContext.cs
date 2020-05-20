using BookStore.Data.DbContext.Configuration;
using BookStore.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookStore.Data.DbContext
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRoles, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CartElement> CartElements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AddressMap());
            builder.ApplyConfiguration(new BookMap());
            builder.ApplyConfiguration(new CartElementMap());
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new OrderMap());
            builder.ApplyConfiguration(new OrderDetailMap());
        }
    }
}
