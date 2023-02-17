using InnoGotchi.Domain.Common;
using InnoGotchi.Domain.Enums;
using InnoGotchi.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.Infrastructure.Persistence.Configurations;

public sealed class PetConfiguration : BaseEntityConfiguration<Pet>
{
    public override void Configure(EntityTypeBuilder<Pet> builder)
    {
        base.Configure(builder);

        builder.Property(farm => farm.Name).HasMaxLength(255).IsRequired();
        builder.Property(farm => farm.Age).IsRequired();
        builder.Property(farm => farm.NextFeedDate).IsRequired();
        builder.Property(farm => farm.NextDrinkDate).IsRequired();
        builder.Property(farm => farm.HungerEnum).HasDefaultValue(HungerEnum.Normal);
        builder.Property(farm => farm.ThirstEnum).HasDefaultValue(ThirstEnum.Normal);
        builder.Property(farm => farm.Happiness).HasDefaultValue(0);


        builder.HasOne(p => p.Farm).WithMany(p => p.Pets);

        builder.HasOne(p => p.Body).WithOne(o => o.Pet).HasForeignKey<PetBody>(p => p.Id).OnDelete(DeleteBehavior.Restrict);
    }
}

