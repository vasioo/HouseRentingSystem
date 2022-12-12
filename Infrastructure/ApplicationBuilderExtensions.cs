using HouseRentingSystem.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace HouseRentingSystem.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedAdmin(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services=scopedServices.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync("Administrator"))
                {
                    return;
                }
                var role = new IdentityRole { Name = "Administrator" };

                await roleManager.CreateAsync(role);

                var admin = await userManager.FindByNameAsync("vasilorakov@gmail.com");
                await userManager.AddToRoleAsync(admin, role.Name);
            })
                .GetAwaiter()
                .GetResult();
            return app;
        }
    }
}
