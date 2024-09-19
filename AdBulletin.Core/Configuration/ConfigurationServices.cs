using AdBulletin.Core.Data;
using AdBulletin.Domain.Services.Email;
using AddBulletin.Domain.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using AdBulletin.Domain.Services.Email.Provider.Gmail;
using AdBulletin.Domain.Services.Email.Provider;
using AdBulletin.Infrastructure.CrossCutting.AppSettings;

namespace AdBulletin.Core.Configuration
{
    public static class ConfigurationServices
    {
        public static IServiceCollection RegisterContext(this IServiceCollection services, IConfiguration configuration)
        {
            //Register context for Product Schema
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("AdBulletinConnection"));
            });

            services.AddDistributedMemoryCache();

            return services;
        }

        public static IServiceCollection RegisterIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(opt => opt.SignIn.RequireConfirmedAccount = false)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            //services.RegisterManagers();
            //services.RegisterRepositories();

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailSender, GmailSender>();

            //services.AddSignalR();

            return services;
        }

        public static IServiceCollection AddConfigurationSection(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ExternalServicesSetting>(configuration.GetSection("ExternalServices"));

            return services;
        }
    }
}
