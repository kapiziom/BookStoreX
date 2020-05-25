using BookStore.Application.Services;
using BookStore.Data.Repository;
using BookStore.Domain;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Configurations
{
    public static class ServicesCollection
    {
        public static IServiceCollection RepositoryServicesSetup(
          this IServiceCollection services)
        {
            //repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //services
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAddressService, AddressService>();
            //validators
            services.AddScoped<IValidator<Address>, AddressValidator>();
            services.AddScoped<IValidator<Book>, BookValidator>();
            services.AddScoped<IValidator<CartElement>, CartElementValidator>();
            services.AddScoped<IValidator<Category>, CategoryValidator>();
            services.AddScoped<IValidator<Order>, OrderValidator>();

            return services;
        }
    }
}
