using System;
using System.Collections.Generic;
using BookStore.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookStore.Data.DbContext;
using BookStore.Domain;
using BookStore.Data.Repository;
using BookStore.Middleware;
using FluentValidation;
using BookStore.Domain.Validators;
using System.Reflection;
using System.IO;
using AutoMapper;
using BookStore.Application.AutoMapper;
using BookStore.Domain.Interfaces;
using BookStore.Application.Services;
using BookStore.Configurations;

namespace BookStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
            services.AddControllers();

            //repositories, services, validators
            services.RepositoryServicesSetup();

            //DbSetup with Identity
            services.AddDbSetup(Configuration);

            //JwtAuth
            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());
            services.JWTsetup(key);
            

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ModelToViewModelMappingProfile());
                mc.AddProfile(new ViewModelToModelMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            
            //swagger
            services.AddSwaggerSetup();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, UserManager<AppUser> userManager, RoleManager<AppRoles> roleManager)
        {
            
            IdentityDataInit.SeedData(userManager, roleManager);

            app.UseRouting();
            app.UseCors("AllowAll");

            app.UseMiddleware<ExceptionsHandlingMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            //swagger
            app.UseSwaggerSetup();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
