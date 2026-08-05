using MedLink.Domain.Entities;
using MedLink.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedLink.Infrastructure.Persistence.Configurations
{
    public class VisitConfiguration : BaseEntityConfiguration<Visit>
    {
        public override void Configure(EntityTypeBuilder<Visit> builder)
        {
            base.Configure(builder);

            builder.ToTable("Visit");

            builder.Property(v => v.Diagnosis).HasMaxLength(1000);
            builder.Property(v => v.ClinicalNotes).HasColumnType("NVARCHAR(MAX)");

            builder.HasIndex(v => v.AppointmentId).IsUnique().HasDatabaseName("UQ_Visit_Appointment");

            builder.HasOne(v => v.Appointment)
                .WithOne(a => a.Visit)
                .HasForeignKey<Visit>(v => v.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
