using System.Text;

using Asp.Versioning;

using BGwalks.API.Data;
using BGwalks.API.Generators;
using BGwalks.API.Mapings;
using BGwalks.API.Middlewares;
using BGwalks.API.Repositories;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Serilog;

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


        // logger
        var logger = new LoggerConfiguration()
        // for DEV environment
        .WriteTo.Console()
        // for PROD environment
        .WriteTo.File("Logs/bgwalks.log", rollingInterval: RollingInterval.Infinite)
        .MinimumLevel.Information()
        .CreateLogger();

        // Clear out existing logging providers so Serilog can replace them.
        builder.Logging.ClearProviders();
        builder.Services.AddSerilog(logger);

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services.AddApiVersioning(options =>
        {
            // When no version is specified, assume version 1.0
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            // Report supported API versions in response headers
            options.ReportApiVersions = true;
            // Tell the API versioning system to read the version from the URL segment
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        // builder.Services.AddVersionedApiExplorer(options =>
        // {
        //     options.GroupNameFormat = "'v'VVV";
        //     options.SubstituteApiVersionInUrl = true;
        // });


        // Add Swagger
        // adding httpCTX accessor to allow access to HttpContext in services
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddEndpointsApiExplorer();  // Required for Swagger to work
        builder.Services.AddSwaggerGen(options =>
        {
            // 1. Define the Swagger document for API version "v1"
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BGWalks API",  // API Title
                Version = "v1"         // API Version
            });

            // 2. Create a new OpenAPI security scheme object for JWT Bearer tokens
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                // The name of the header that carries the token.
                Name = "Authorization",
                // Specify that the token will be passed in the header.
                In = ParameterLocation.Header,
                // Use the HTTP security scheme, which is appropriate for JWT tokens.
                Type = SecuritySchemeType.Http,
                // Set the scheme name to "bearer" (must be lowercase).
                Scheme = "bearer",
                // Optional: Inform that the bearer token format is JWT.
                BearerFormat = "JWT",
                // Provide a short description on how to use the header.
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {your_token_here}\"",
                // Reference this security scheme by the name "Bearer" so it can be reused.
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            // 3. Add the security definition to Swagger using the "Bearer" key.
            options.AddSecurityDefinition("Bearer", jwtSecurityScheme);

            // 4. Add a global security requirement so that all endpoints (or those marked with [Authorize])
            //    will use this security scheme by default.
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            // Reference the previously defined JWT Bearer scheme.
            jwtSecurityScheme,
            // Specify that no specific scopes are required (empty array).
            Array.Empty<string>()
        }
            });
        });

        // Database Context setup 
        builder.Services.AddDbContext<BGWalksDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("BGWalksConnectionString"));
        });
        builder.Services.AddDbContext<BGWalksAuthDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("BGWalksAuthConnectionString"));
        });


        // Identity User injection
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
           .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("BGWalks")
           .AddEntityFrameworkStores<BGWalksAuthDbContext>()
           .AddDefaultTokenProviders();
        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Password settings
            // options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            // options.Password.RequireUppercase = true;
            // options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;

            // User settings
            options.User.RequireUniqueEmail = true;

        });



        // Repository design pattern dependency injection
        builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
        builder.Services.AddScoped<IWalkRepository, SQLWalksRepository>();
        builder.Services.AddScoped<ITokenRespository, SQLTokenRepository>();
        builder.Services.AddScoped<IImageRepository, SQLImageRespository>();

        // AutoMapper depedency injection
        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

        // adding authentication with JWT Bearer
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Jwt";
            options.DefaultChallengeScheme = "Jwt";
        }).AddJwtBearer("Jwt", options =>
        {
            // Configure JWT Bearer to expect a token parameter in the Authorization header
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],

                ValidateAudience = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero

            };
            IdentityModelEventSource.ShowPII = true;

        });

        #region DEBUGGING
        // #DEBUGGING#
        // console log all the JWT configuration variables from builder.configuration to the console
        Console.WriteLine($"Jwt:Issuer: {builder.Configuration["Jwt:Issuer"]}");
        Console.WriteLine($"Jwt:Audience: {builder.Configuration["Jwt:Audience"]}");
        Console.WriteLine($"Jwt:Key: {builder.Configuration["Jwt:Key"]}");
        #endregion


        // Building
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(); // Enable the Swagger UI
            app.UseSwaggerUI();  // Configures the UI
        }

        // Global error handling middleware
        app.UseMiddleware<ExceptionHandlerMiddleware>();

        // Configure the HTTP request pipeline.
        app.UseHttpsRedirection();
        // using authentication and authorization middleware
        app.UseAuthentication();
        app.UseAuthorization();

        // Serve static files from the wwwroot folder
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
            RequestPath = "/Images"
            // http://localhost:1234/Images/
        });

        // enable controllers from this assembly
        app.MapControllers();


        // Resolve WalkDataGenerator and use it
        using (var scope = app.Services.CreateScope())
        {
            var walkDataGenerator = scope.ServiceProvider.GetRequiredService<WalkDataGenerator>();
            walkDataGenerator.GenerateWalkData(10);
        }

        // Run the web host
        app.Run();


    }
}