using FluentValidation.AspNetCore;
using InnoGotchi.Application.Common.Interfaces;
using InnoGotchi.Infrastructure.Persistence;
using InnoGotchi.Web.Filters;
using InnoGotchi.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Web;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHostedService<CheckHostedService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllers(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        // Add Swagger services
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
