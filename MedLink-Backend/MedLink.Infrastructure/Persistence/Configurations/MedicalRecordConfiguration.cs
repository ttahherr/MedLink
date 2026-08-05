using MedLink.Domain.Entities;
using MedLink.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedLink.Infrastructure.Persistence.Configurations
{
    public class MedicalRecordConfiguration : BaseEntityConfiguration<MedicalRecord>
    {
        public override void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            base.Configure(builder);

            builder.ToTable("MedicalRecord");

            builder.HasIndex(m => m.PatientId).IsUnique().HasDatabaseName("UQ_MedicalRecord_PatientId");

            builder.HasOne(m => m.Patient)
                .WithOne(p => p.MedicalRecord)
                .HasForeignKey<MedicalRecord>(m => m.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
            

        }
    }
}
