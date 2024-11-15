using Domain.Configurations;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories;
using Repositories;
using Services;
using Services.Common;
using Services.Implementation;
using Services.Implementation.Common;

namespace WebUI
{
    public class Program
    {
    
    
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DbContext,DataContext>(cfg =>
            {
                cfg.UseSqlServer(builder.Configuration.GetConnectionString("cString"), opt =>
                {
                    opt.MigrationsHistoryTable("MigrationHistory");
                });
            });
            builder.Services.Configure<EmailConfiguration>(cfg =>
            {
                builder.Configuration.GetSection(nameof(EmailConfiguration)).Bind(cfg);
            });
            builder.Services.Configure<CryptoServiceConfiguration>(cfg =>
            {
                builder.Configuration.GetSection(nameof(CryptoServiceConfiguration)).Bind(cfg);
            });
            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton<ICryptoService, CryptoService>();
            builder.Services.AddScoped<IContactPostService, ContactPostService>();
            builder.Services.AddScoped<IContactPostRepository, ContactPostRepository>();


            var app = builder.Build();
            app.UseStaticFiles();
            app.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Dashboard}/{action=index}/{id?}");
            app.MapControllerRoute(name: "default", pattern: "{controller=home}/{action=index}/{id?}");

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}