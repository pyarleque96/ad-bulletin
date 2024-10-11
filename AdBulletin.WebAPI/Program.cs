using AdBulletin.WebAPI.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
{
    //Register all context
    builder.Services.RegisterContext(configuration);

    //Register Entity Framework and Identity
    builder.Services.RegisterIdentity();

    //Register service for scheme authenticate
    builder.Services.RegisterSchemeAuthentication(configuration);

    //Add Configuration Options from appsetting.json
    builder.Services.AddConfigurationSection(configuration);

    //Register all services in the collection services
    builder.Services.RegisterServices();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
