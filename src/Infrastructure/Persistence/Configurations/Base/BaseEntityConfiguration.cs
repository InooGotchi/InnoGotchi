using InnoGotchi.Domain.Common.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoGotchi.Infrastructure.Persistence.Configurations.Base;

public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Created).HasDefaultValue(DateTime.UtcNow).ValueGeneratedOnAdd();
    }
}

