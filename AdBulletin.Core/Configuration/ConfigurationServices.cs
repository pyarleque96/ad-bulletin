using AdBulletin.Core.Handlers;
using AdBulletin.Core.Services;
using AdBulletin.Core.Services.Clients;
using AdBulletin.Domain.Data.Context;
using AdBulletin.Domain.Data.Entities;
using AdBulletin.Domain.Services.Email;
using AdBulletin.Domain.Services.Email.Provider;
using AdBulletin.Domain.Services.Email.Provider.Gmail;
using AdBulletin.Infrastructure.CrossCutting.AppSettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Refit;

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
            services.RegisterCoreServices();
            services.RegisterExternalServices();

            return services;
        }

        public static IServiceCollection RegisterRefitClient(this IServiceCollection services, IConfiguration configuration)
        {
            var apiURL = configuration.GetValue<string>("Hosts:WebAPI:Uri");

            services.AddRefitClient<IAdCategoryClientAPI>().ConfigureHttpClient(c => { c.Timeout = TimeSpan.FromSeconds(400); c.BaseAddress = new Uri(apiURL); }).AddHttpMessageHandler<JwtAuthHandler>();
            services.AddRefitClient<IAdClientAPI>().ConfigureHttpClient(c => { c.Timeout = TimeSpan.FromSeconds(400); c.BaseAddress = new Uri(apiURL); }).AddHttpMessageHandler<JwtAuthHandler>();

            return services;
        }

        public static IServiceCollection AddConfigurationSection(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ExternalServicesSetting>(configuration.GetSection("ExternalServices"));

            return services;
        }

        private static IServiceCollection RegisterCoreServices(this IServiceCollection services)
        {
            // API services
            services.AddScoped<AdCategoryService>();
            services.AddScoped<AdService>();

            // Auth services
            services.AddTransient<IAuthService, AuthService>();
            services.AddScoped<SignInManager<ApplicationUser>, CustomSignInManager>();

            // Handler services
            services.AddTransient<JwtAuthHandler>();

            // Razor components services
            services.AddScoped<LoadingService>();
            services.AddScoped<LayoutService>();

            return services;
        }

        private static IServiceCollection RegisterExternalServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailSender, GmailSender>();

            return services;
        }
    }
}
