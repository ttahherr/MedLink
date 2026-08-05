using MedLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedLink.Infrastructure.Persistence.Configurations.Base
{
    public abstract class BaseEntityConfiguration<TEntity> 
        : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnType("DATETIME2(3)").HasDefaultValueSql("SYSUTCDATETIME()");
            builder.Property(x => x.UpdatedAt).HasColumnType("DATETIME2(3)");
        }
    }
}
