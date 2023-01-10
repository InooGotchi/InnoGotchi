namespace InnoGotchi.Domain.Common;

public class PetBody
{
    public NoseTypeEnum Nose { get; set; }
    public EyesTypeEnum Eyes { get; set; }
    public MouthTypeEnum Mouth { get; set; }
    public BodyTypeEnum Body { get; set; }
    public virtual User User { get; set; }
}

public enum NoseTypeEnum
{
  Straight,
  Snub,
  Hawk
}

public enum EyesTypeEnum
{
  Narrow,
  Medium,
  Wide
}

public enum MouthTypeEnum
{
  Narrow,
  Medium,
  Wide
}

public enum BodyTypeEnum
{
  Slim,
  Medium,
  Fat
}