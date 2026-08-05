using MedLink.Domain.Entities;
using MedLink.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedLink.Infrastructure.Persistence.Configurations
{
    public class ChronicDiseaseConfiguration : BaseEntityConfiguration<ChronicDisease>
    {
        public override void Configure(EntityTypeBuilder<ChronicDisease> builder)
        {
            base.Configure(builder);

            builder.ToTable("ChronicDisease");

            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Notes).HasColumnType("NVARCHAR(MAX)");

            builder.HasOne(c => c.MedicalRecord)
                .WithMany(m => m.ChronicDiseases)
                .HasForeignKey(c => c.MedicalRecordId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
