using InnoGotchi.Domain.Common;
using InnoGotchi.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.Infrastructure.Persistence.Configurations;
public sealed class PetBodyConfiguration : BaseEntityConfiguration<PetBody>
{
    public override void Configure(EntityTypeBuilder<PetBody> builder)
    {
        base.Configure(builder);

        builder.HasOne(pb => pb.Pet).WithOne(p => p.Body).HasForeignKey<PetBody>(pb => pb.PetId);

        builder.HasOne(pb => pb.Eyes).WithMany(e => e.PetBodies).HasForeignKey(pb => pb.EyesId);

        builder.HasOne(pb => pb.Nose).WithMany(e => e.PetBodies).HasForeignKey(pb => pb.NoseId);

        builder.HasOne(pb => pb.Mouth).WithMany(e => e.PetBodies).HasForeignKey(pb => pb.MouthId);

        builder.HasOne(pb => pb.Body).WithMany(e => e.PetBodies).HasForeignKey(pb => pb.BodyId);

    }
}