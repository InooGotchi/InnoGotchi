namespace Web.Configurations;

public sealed record CheckHostedServiceOptions()
{
    public int Interval { get; set; }
}
