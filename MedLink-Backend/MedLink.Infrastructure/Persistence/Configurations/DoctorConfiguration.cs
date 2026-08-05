using MedLink.Domain.Entities;
using MedLink.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedLink.Infrastructure.Persistence.Configurations
{
    public class DoctorConfiguration : BaseEntityConfiguration<Doctor>
    {
        public override void Configure(EntityTypeBuilder<Doctor> builder)
        {
            base.Configure(builder);

            builder.ToTable("Doctor");

            builder.Property(d => d.ApplicationUserId).IsRequired();
            builder.Property(d => d.LicenseNumber).IsRequired().HasMaxLength(50);
            builder.Property(d => d.YearsOfExperience).IsRequired().HasColumnType("TINYINT");
            builder.Property(d => d.ConsultationFee).IsRequired().HasColumnType("DECIMAL(10,2)");
            builder.Property(d => d.Biography).HasMaxLength(2000);

            builder.HasIndex(d => d.ApplicationUserId).IsUnique().HasDatabaseName("UQ_Doctor_ApplicationUserId");
            builder.HasIndex(d => d.LicenseNumber).IsUnique().HasDatabaseName("UQ_Doctor_LicenseNumber");

            builder.HasOne(d => d.Clinic)
                .WithMany(c => c.Doctors)
                .HasForeignKey(d => d.ClinicId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Specialization)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.ToTable(t => t.HasCheckConstraint("CK_Doctor_YearsOfExperience", "YearsOfExperience >= 0"));
            builder.ToTable(t => t.HasCheckConstraint("CK_Doctor_ConsultationFee", "ConsultationFee >= 0"));


        }
    }
}
