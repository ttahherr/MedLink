using MedLink.Domain.Entities;
using MedLink.Domain.Enums;
using MedLink.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedLink.Infrastructure.Persistence.Configurations
{
    public class AppointmentConfiguration : BaseEntityConfiguration<Appointment>
    {
        public override void Configure(EntityTypeBuilder<Appointment> builder)
        {
            base.Configure(builder);

            builder.ToTable("Appointment");

            builder.Property(a => a.AppointmentDateTime).IsRequired().HasColumnType("DATETIME2(0)");
            builder.Property(a => a.Status).IsRequired().HasColumnType("TINYINT").HasDefaultValue(AppointmentStatus.Scheduled);
            builder.Property(a => a.Notes).HasMaxLength(500);

            builder.HasIndex(a => new { a.BranchDoctorId, a.AppointmentDateTime }).IsUnique().HasDatabaseName("UQ_Appointment_Doctor_DateTime");

            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.BranchDoctor)
                .WithMany(bd => bd.Appointments)
                .HasForeignKey(a => a.BranchDoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(t => t.HasCheckConstraint("CK_Appointment_Status", "Status IN (1,2,3,4)"));


        }
    }
}
