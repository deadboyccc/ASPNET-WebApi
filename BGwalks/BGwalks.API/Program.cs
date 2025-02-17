using BGwalks.API.Data;
using BGwalks.API.Mapings;
using BGwalks.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BGwalks.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            app.Run();

        }
    }
}
