using System.Text;

using BGwalks.API.Data;
using BGwalks.API.Generators;
using BGwalks.API.Mapings;
using BGwalks.API.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BGwalks.API;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add WalkDataGenerator as a service
        builder.Services.AddSingleton<WalkDataGenerator>();



        // Register HttpClient
        builder.Services.AddHttpClient();


        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();  // Required for Swagger to work
        builder.Services.AddSwaggerGen();  // Registers the Swagger generator
        builder.Services.AddDbContext<BGWalksDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("BGWalksConnectionString"));
        });
        builder.Services.AddDbContext<BGWalksAuthDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("BGWalksAuthConnectionString"));
        });


        // Repository design pattern dependency injection
        builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
        builder.Services.AddScoped<IWalkRepository, SQLWalksRepository>();

        // AutoMapper depedency injection
        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

        // Authentication
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "jwt";
            options.DefaultChallengeScheme = "Jwt";
        }).AddJwtBearer("Jwt", options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;


            // new Token Validation Parameters
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["Jwt:Issuers"],

                ValidateAudience = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });




        // Building
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(); // Enable the Swagger UI
            app.UseSwaggerUI();  // Configures the UI
        }

        app.UseHttpsRedirection();


        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();


        // Resolve WalkDataGenerator and use it
        using (var scope = app.Services.CreateScope())
        {
            var walkDataGenerator = scope.ServiceProvider.GetRequiredService<WalkDataGenerator>();
            walkDataGenerator.GenerateWalkData(10);
        }

        app.Run();


    }
}