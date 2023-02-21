using InnoGotchi.Domain.Common.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.Infrastructure.Persistence.Configurations.Base;

public class BaseBodyPartConfiguration<TEntity> : BaseEntityConfiguration<TEntity> where TEntity : BodyPart
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name).HasMaxLength(255);
        builder.Property(p => p.Url).HasMaxLength(255);
    }
}