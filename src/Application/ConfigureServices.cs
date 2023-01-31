using System.Reflection;
using InnoGotchi.Application.Common.Interfaces;
using InnoGotchi.Domain.Common;
using Microsoft.Extensions.DependencyInjection.Common.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddScoped<IPetService, PetService>();
        services.AddScoped<IFarmService, FarmService>();
        
        return services;
    }
}
