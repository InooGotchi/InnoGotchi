using InnoGotchi.Domain.Common.BodyParts;
using InnoGotchi.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.Infrastructure.Persistence.Configurations.BodyParts;
public sealed class NoseConfiguration : BaseBodyPartConfiguration<Nose>
{
    public override void Configure(EntityTypeBuilder<Nose> builder)
    {
        base.Configure(builder);

        builder.HasMany(n => n.PetBodies).WithOne(pb => pb.Nose);
    }
}
