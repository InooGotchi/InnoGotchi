using InnoGotchi.Domain.Common;
using InnoGotchi.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.Infrastructure.Persistence.Configurations;
public sealed class PlayerConfiguration : BaseEntityConfiguration<Player>
{
    public override void Configure(EntityTypeBuilder<Player> builder)
    {
        base.Configure(builder);

        builder.HasOne(p => p.Farm).WithOne(f => f.Owner).HasForeignKey<Farm>(f => f.Id).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.Farms).WithMany(p => p.Players);
    }
}