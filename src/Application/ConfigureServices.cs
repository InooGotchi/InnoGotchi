using System.Reflection;
using InnoGotchi.Application.Common.Interfaces;
using InnoGotchi.Application.Common.Services;

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
