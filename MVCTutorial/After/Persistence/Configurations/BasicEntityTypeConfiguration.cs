using Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public abstract class BasicEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
where TEntity : Entity
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        ConfigureEntity(builder);
    }

    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}