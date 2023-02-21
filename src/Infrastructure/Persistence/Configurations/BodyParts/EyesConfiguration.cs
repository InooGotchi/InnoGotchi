using InnoGotchi.Domain.Common.BodyParts;
using InnoGotchi.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.Infrastructure.Persistence.Configurations.BodyParts;

public sealed class EyesConfiguration : BaseBodyPartConfiguration<Eyes>
{
    public override void Configure(EntityTypeBuilder<Eyes> builder)
    {
        base.Configure(builder);

        builder.HasMany(e => e.PetBodies).WithOne(pb => pb.Eyes);
    }
}