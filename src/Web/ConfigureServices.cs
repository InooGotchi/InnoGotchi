using System.Text;
using CleanArchitecture.Application.Common.Interfaces;
using InnoGotchi.Application.Common.Interfaces;
using InnoGotchi.Infrastructure.Persistence;
using InnoGotchi.Infrastructure.Services;
using InnoGotchi.Web.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Web;
using static InnoGotchi.Infrastructure.Services.TokenService;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHostedService<CheckHostedService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllers();

        // For Jwt authentication
        var jwtConfiguration = configuration.GetSection("Jwt");

        services.Configure<JwtTokenOptions>(jwtConfiguration);

        services.Configure<JwtBearerOptions>(options =>
        {
            var jwtOptions = jwtConfiguration.Get<JwtTokenOptions>();
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(jwtOptions.Expiration))
            };
        });

        // Dependency injection with key
        services.AddSingleton<ITokenService, TokenService>();

        // Configuration for token
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer();

        // Add Swagger services
        services.AddSwaggerGen(c =>
        {

            // To enable authorization using swagger (Jwt)
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer {token}\"",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
                {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

        });

        services.AddSwaggerDocument(config =>
        {
            config.PostProcess = document =>
            {
                document.Info.Version = "v1";
                document.Info.Title = "InnoGotchi API";
                document.Info.Description = "Virtual application to manage your virtual pets ";
                document.Info.TermsOfService = "None";
            };
        });

        return services;
    }
}
