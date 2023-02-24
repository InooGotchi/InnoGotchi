using InnoGotchi.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using Web.Configurations;

namespace Web;

public partial class CheckHostedService : BackgroundService
{
    private readonly IServiceScopeFactory _factory;
    private readonly ILogger<CheckHostedService> _logger;
    private readonly CheckHostedServiceOptions _options;

    public CheckHostedService(
        IOptions<CheckHostedServiceOptions> options,
        ILogger<CheckHostedService> logger,
        IServiceScopeFactory factory)
    {
        _options = options.Value;
        _logger = logger;
        _factory = factory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        PeriodicTimer timer = new(TimeSpan.FromSeconds(_options.Interval));

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            using AsyncServiceScope scope = _factory.CreateAsyncScope();

            var petService = scope.ServiceProvider.GetService<IPetService>();
            if (petService is not null)
            {
                int petsUpdated = await petService.UpdateAlivePetsStatuses();
                _logger.LogInformation($"All Pets checked and {petsUpdated} updated!");
            }
        }
    }
}
