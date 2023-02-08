using InnoGotchi.Application.Common.Models.Base;
using InnoGotchi.Application.Common.Models.BodyPartsModels;

namespace InnoGotchi.Application.Common.Models;

public sealed class PetBodyModel : BaseModel
{
    public NoseModel Nose { get; set; }
    public EyesModel Eyes { get; set; }
    public MouthModel Mouth { get; set; }
    public BodyModel Body { get; set; }
}