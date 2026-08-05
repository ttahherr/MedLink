using MedLink.Domain.Entities;
using MedLink.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedLink.Infrastructure.Persistence.Configurations
{
    public class PrescriptionItemConfiguration : BaseEntityConfiguration<PrescriptionItem>
    {
        public override void Configure(EntityTypeBuilder<PrescriptionItem> builder)
        {
            base.Configure(builder);

            builder.ToTable("PrescriptionItem");

            builder.Property(p => p.MedicationName).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Dosage).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Frequency).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Duration).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Instructions).HasColumnType("NVARCHAR(MAX)");

            builder.HasOne(p => p.Prescription)
                .WithMany(p => p.PrescriptionItems)
                .HasForeignKey(p => p.PrescriptionId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
