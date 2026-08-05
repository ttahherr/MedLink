using MedLink.Domain.Entities;
using MedLink.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedLink.Infrastructure.Persistence.Configurations
{
    public class PrescriptionConfiguration : BaseEntityConfiguration<Prescription>
    {
        public override void Configure(EntityTypeBuilder<Prescription> builder)
        {
            base.Configure(builder);

            builder.ToTable("Prescription");

            builder.Property(p => p.Notes).HasColumnType("NVARCHAR(MAX)");

            builder.HasIndex(p => p.VisitId).IsUnique().HasDatabaseName("UQ_Prescription_Visit");

            builder.HasOne(p => p.Visit)
                .WithOne(v => v.Prescription)
                .HasForeignKey<Prescription>(p => p.VisitId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
