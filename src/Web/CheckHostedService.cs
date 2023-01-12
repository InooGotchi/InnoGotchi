namespace Web;

public class CheckHostedService : BackgroundService
{
    private readonly ILogger<CheckHostedService> _logger;

    public CheckHostedService(
        ILogger<CheckHostedService> logger)
    {
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        PeriodicTimer timer = new(TimeSpan.FromSeconds(5));

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
        }
    }
}
