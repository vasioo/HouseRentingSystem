using HouseRentingSystem.Controllers;
using HouseRentingSystem.Data;
using HouseRentingSystem.Data.Entities;
using HouseRentingSystem.Infrastructure;
using HouseRentingSystem.Services.Agents;
using HouseRentingSystem.Services.Houses;
using HouseRentingSystem.Services.Rents;
using HouseRentingSystem.Services.Statistics;
using HouseRentingSystem.Services.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                        
            // Add services to the container.
            var connectionString = builder.Configuration
                .GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException
                ("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<HouseRentingDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<User>
                (options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<HouseRentingDbContext>();
            
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            builder.Services.AddAutoMapper(
                typeof(IHouseService).Assembly,
                typeof(HomeController).Assembly);

            builder.Services.AddTransient<IHouseService, HouseService>();
            builder.Services.AddTransient<IAgentService, AgentService>();
            builder.Services.AddTransient<IStatisticsService, StatisticsService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IRentService, RentService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error/500");
                app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
                app.UseHsts();
            }
           
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.SeedAdmin();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"Areas",
                    pattern:"{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "House Details",
                    pattern: "/Houses/Details/{id}/{information}",
                    defaults: new { Controller = "Houses", Action = "Details" });

                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });

            app.MapDefaultControllerRoute();
            app.MapRazorPages();

            app.Run();
        }
    }
}