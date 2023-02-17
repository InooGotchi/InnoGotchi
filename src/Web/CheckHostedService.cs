using InnoGotchi.Application.Common.Interfaces;

namespace Web;

public class CheckHostedService : BackgroundService
{
    private readonly IServiceScopeFactory _factory;
    private readonly ILogger<CheckHostedService> _logger;

    public CheckHostedService(
        ILogger<CheckHostedService> logger,
        IServiceScopeFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        PeriodicTimer timer = new(TimeSpan.FromSeconds(5));

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            using AsyncServiceScope scope = _factory.CreateAsyncScope();

            var petService = scope.ServiceProvider.GetService<IPetService>();
            if (petService is not null)
            {
                var alivePets = await petService.GetAliveAsync();
                foreach (var pet in alivePets)
                {
                    if (pet.NextDrinkDate < DateTime.UtcNow)
                        pet.ThirstEnum++;
                    if (pet.NextFeedDate < DateTime.UtcNow)
                        pet.HungerEnum++;
                }
                _logger.LogInformation("Pets checked!");
            }
        }
    }
}
