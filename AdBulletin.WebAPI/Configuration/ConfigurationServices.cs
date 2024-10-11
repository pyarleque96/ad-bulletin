using AdBulletin.Domain.Contracts.Aggregates;
using AdBulletin.Domain.Data.Context;
using AdBulletin.Domain.Data.Repositories;
using AdBulletin.Domain.Managers;
using AdBulletin.Domain.Services.Email;
using AdBulletin.Domain.Services.Email.Provider.Gmail;
using AdBulletin.Domain.Services.Email.Provider;
using AdBulletin.Infrastructure.CrossCutting.AppSettings;
using Microsoft.EntityFrameworkCore;
using AdBulletin.WebAPI.Filters;
using AdBulletin.Domain.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AdBulletin.WebAPI.Configuration
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddConfigurationSection(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ExternalServicesSetting>(configuration.GetSection("ExternalServices"));

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

        public static IServiceCollection RegisterSchemeAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Tokens:Jwt:Issuer"],
                    ValidAudience = configuration["Tokens:Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Jwt:Key"]))
                };
            });

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.RegisterManagers();
            services.RegisterRepositories();
            services.RegisterExternalServices();
            services.RegisterExceptionHandlerServices();

            services.AddAutoMapper(c =>
            {
                c.AllowNullCollections = true;
            }, typeof(Program));

            return services;
        }

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


        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAdCategoryRepository, AdCategoryRepository>();
            services.AddScoped<IAdRepository, AdRepository>();

            return services;
        }

        private static IServiceCollection RegisterManagers(this IServiceCollection services)
        {
            services.AddScoped<IAdCategoryManager, AdCategoryManager>();
            services.AddScoped<IAdManager, AdManager>();

            return services;
        }
        
        private static IServiceCollection RegisterExternalServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailSender, GmailSender>();

            return services;
        }

        private static IServiceCollection RegisterExceptionHandlerServices(this IServiceCollection services)
        {
            services.AddScoped<ApiExceptionFilter>();

            return services;
        }
    }
}
