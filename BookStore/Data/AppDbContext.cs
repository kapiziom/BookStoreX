using BookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRoles, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasOne(a => a.Address)
                .WithOne(b => b.AppUser)
                .HasForeignKey<Address>(b => b.AppUserID);
            builder.Entity<OrderDetail>()
                .HasOne(a => a.Order)
                .WithMany(b => b.OrderDetails);
            builder.Entity<Categories>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Category);
            builder.Entity<Languages>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Language);
                
        }
    }
}
