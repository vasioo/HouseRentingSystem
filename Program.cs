using HouseRentingSystem.Data;
using HouseRentingSystem.Services.Agents;
using HouseRentingSystem.Services.Houses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<HouseRentingDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>
                (options => 
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit= false;
                    options.Password.RequireLowercase= false;
                    options.Password.RequireNonAlphanumeric= false;
                    options.Password.RequireUppercase= false;
                })
                .AddEntityFrameworkStores<HouseRentingDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IHouseService,HouseService>();
            builder.Services.AddTransient<IAgentService, AgentService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapDefaultControllerRoute();
            app.MapRazorPages();

            app.Run();
        }
    }
}