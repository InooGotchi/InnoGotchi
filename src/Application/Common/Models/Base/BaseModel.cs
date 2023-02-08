namespace InnoGotchi.Application.Common.Models.Base;

public abstract class BaseModel
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
}