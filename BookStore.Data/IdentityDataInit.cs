using BookStore.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public static class IdentityDataInit
    {
        public static void SeedData(UserManager<AppUser> userManager, RoleManager<AppRoles> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<AppUser> userManager)
        {
            if (userManager.FindByNameAsync("normaluser").Result == null)
            {
                AppUser user = new AppUser();
                user.UserName = "normaluser";
                user.Email = "us1@x.d";

                IdentityResult result = userManager.CreateAsync
                (user, "P@ssw0rd").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "NormalUser").Wait();
                }
            }

            if (userManager.FindByNameAsync("worker").Result == null)
            {
                AppUser user = new AppUser();
                user.UserName = "worker";
                user.Email = "us2@x.d";

                IdentityResult result = userManager.CreateAsync
                (user, "P@ssw0rd").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Worker").Wait();
                }
            }

            if (userManager.FindByNameAsync("admin").Result == null)
            {
                AppUser user = new AppUser();
                user.UserName = "admin";
                user.Email = "xd@xd.xd";

                IdentityResult result = userManager.CreateAsync
                (user, "P@ssw0rd").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<AppRoles> roleManager)
        {
            if (!roleManager.RoleExistsAsync("NormalUser").Result)
            {
                AppRoles role = new AppRoles();
                role.Name = "NormalUser";
                role.Description = "Perform normal operations.";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Worker").Result)
            {
                AppRoles role = new AppRoles();
                role.Name = "Worker";
                role.Description = "Crud books etc";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                AppRoles role = new AppRoles();
                role.Name = "Administrator";
                role.Description = "Perform all the operations.";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
