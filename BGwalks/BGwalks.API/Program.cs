using BGwalks.API.Data;
using BGwalks.API.Generators;
using BGwalks.API.Mapings;
using BGwalks.API.Models.Domain;
using BGwalks.API.Repositories;

using Microsoft.EntityFrameworkCore;

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


        // Repository design pattern dependency injection
        builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
        builder.Services.AddScoped<IWalkRepository, SQLWalksRepository>();

        // AutoMapper depedency injection
        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


        // Building
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(); // Enable the Swagger UI
            app.UseSwaggerUI();  // Configures the UI
        }

        app.UseHttpsRedirection();
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