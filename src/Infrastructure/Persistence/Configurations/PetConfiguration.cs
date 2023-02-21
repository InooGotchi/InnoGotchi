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

        builder.Property(pet => pet.Name).HasMaxLength(255).IsRequired();
        builder.Property(pet => pet.Age).IsRequired();
        builder.Property(pet => pet.NextFeedDate).IsRequired();
        builder.Property(pet => pet.NextDrinkDate).IsRequired();
        builder.Property(pet => pet.HungerEnum).HasDefaultValue(HungerEnum.Normal);
        builder.Property(pet => pet.ThirstEnum).HasDefaultValue(ThirstEnum.Normal);
        builder.Property(pet => pet.Happiness).HasDefaultValue(0);


        builder.HasOne(p => p.Farm).WithMany(p => p.Pets);

        builder.HasOne(p => p.Body).WithOne(o => o.Pet).HasForeignKey<PetBody>(p => p.Id).OnDelete(DeleteBehavior.Restrict);
    }
}

