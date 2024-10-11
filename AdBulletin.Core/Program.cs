using AdBulletin.Core.Areas.Identity;
using AdBulletin.Core.Configuration;
using AdBulletin.Domain.Data.Entities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
{
    //Register all context
    builder.Services.RegisterContext(configuration);

    //Register Entity Framework and Identity
    builder.Services.RegisterIdentity();

    //Add Configuration Options from appsetting.json
    builder.Services.AddConfigurationSection(configuration);

    //Register all services in the collection services
    builder.Services.RegisterServices();

    //Register Refit services client
    builder.Services.RegisterRefitClient(configuration);

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();

    builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
