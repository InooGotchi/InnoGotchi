namespace InnoGotchi.Domain.Common.Base;

public abstract class BaseEntity
{
    public Guid Id { get; init; }
    public DateTime Created { get; init; } = DateTime.UtcNow;
    public Guid CreatedBy { get; set; }
}
