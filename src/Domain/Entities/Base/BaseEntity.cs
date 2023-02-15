namespace InnoGotchi.Domain.Common.Base;

public abstract class BaseEntity
{
    public Guid Id { get; init; }
    public DateTime Created { get; init; }
}
