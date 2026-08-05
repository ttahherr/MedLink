using MedLink.Domain.Entities;
using MedLink.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedLink.Infrastructure.Persistence.Configurations
{
    public class SpecializationConfiguration : BaseEntityConfiguration<Specialization>
    {
        public override void Configure(EntityTypeBuilder<Specialization> builder)
        {
            base.Configure(builder);

            builder.ToTable("Specialization");

            builder.Property(s => s.Name).IsRequired().HasMaxLength(150);
            builder.Property(s => s.Description).HasMaxLength(500);

            builder.HasIndex(s => s.Name).IsUnique().HasDatabaseName("UQ_Specialization_Name");


        }
    }
}
