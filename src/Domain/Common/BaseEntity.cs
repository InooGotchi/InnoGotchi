namespace InnoGotchi.Domain.Common;
public abstract class BaseEntity
{
    public Guid Id
    {
        get;
        set;
    }
    public DateTime Created
    {
        get;
        set;
    }
}
