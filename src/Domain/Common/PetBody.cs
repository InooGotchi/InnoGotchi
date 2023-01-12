namespace InnoGotchi.Domain.Common;

public class PetBody
{
    public NoseType Nose { get; set; }
    public EyesType Eyes { get; set; }
    public MouthType Mouth { get; set; }
    public BodyType Body { get; set; }
}

public enum NoseType
{
  Straight,
  Snub,
  Hawk
}

public enum EyesType
{
  Narrow,
  Medium,
  Wide
}

public enum MouthType
{
  Narrow,
  Medium,
  Wide
}

public enum BodyType
{
  Slim,
  Medium,
  Fat
}