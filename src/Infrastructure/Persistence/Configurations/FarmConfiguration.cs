using InnoGotchi.Domain.Common;
using InnoGotchi.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.Infrastructure.Persistence.Configurations;
public sealed class FarmConfiguration : BaseEntityConfiguration<Farm>
{
    public override void Configure(EntityTypeBuilder<Farm> builder)
    {
        base.Configure(builder);

        builder.Property(farm => farm.Name).HasMaxLength(255).IsRequired();
        builder.Property(farm => farm.Capacity).IsRequired();
        builder.Property(farm => farm.TotalPets).HasDefaultValue(0);
        builder.Property(farm => farm.AlivePets).HasDefaultValue(0);
        builder.Property(farm => farm.DeadPets).HasDefaultValue(0);


        builder.HasMany(f => f.Pets).WithOne(p => p.Farm).HasForeignKey(p => p.Id).OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(f => f.Players).WithMany(p => p.Farms);
    }
}
