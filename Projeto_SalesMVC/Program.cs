using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Projeto_SalesMVC.Data;
using System.Data.SqlClient;
using Projeto_SalesMVC.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Projeto_SalesMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //New option placed -> configuration
            ConfigurationManager configuration = builder.Configuration;
            
            builder.Services.AddDbContext<SalesMVCContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SalesMVCContext") ?? throw new InvalidOperationException("Connection string 'SalesMVCContext' not found.")));        

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<SeedingService>();
            builder.Services.AddScoped<SellerService>();
            builder.Services.AddScoped<DepartmentsService>();
            builder.Services.AddScoped<SalesRecordService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            else
            {
                // Obtenha uma instância do serviço SeedingService do provedor de serviços.
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var seedingService = services.GetRequiredService<SeedingService>();

                    try
                    {
                        seedingService.Seed();
                        Console.WriteLine("Seeding completed successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Seeding failed with error: {ex.Message}");
                    }
                }

            }

            var enUS = new CultureInfo("en-US");
            var localization_options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUS),
                SupportedCultures = new List<CultureInfo> { enUS },
                SupportedUICultures = new List<CultureInfo> { enUS }
            };

            app.UseRequestLocalization(localization_options);

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}