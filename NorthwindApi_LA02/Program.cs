using Microsoft.EntityFrameworkCore;
using NorthwindApi_LA02.Data;
using Microsoft.OpenApi.Models;
using NorthwindApi_LA02.Auth;
using NorthwindApi_LA02.Extension;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c => {

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Northwind Api Teszt", Version = "v1" });

    c.AddSecurityDefinition("APIKey", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "X-Api-Key",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "APIKey"
                }
            },
            new string[] { }

    }   });

});

ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<NorthwindContext>(options => options.UseNpgsql(
    configuration["ConnectionStrings:NorthwindConn"]));

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
    option.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;

}).AddApiKeySupport(option => { option.ToString(); });


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
