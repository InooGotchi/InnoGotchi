namespace InnoGotchi.Application.Common.Models.Base;

public abstract class BodyPartModel : BaseModel
{
    public string Name { get; init; }
    public string Url { get; init; }
    
    public IEnumerable<PetBodyModel> PetBodies { get; set; }
}