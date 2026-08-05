using MedLink.Domain.Entities;
using MedLink.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedLink.Infrastructure.Persistence.Configurations
{
    public class PatientConfiguration : BaseEntityConfiguration<Patient>
    {
        public override void Configure(EntityTypeBuilder<Patient> builder)
        {
            base.Configure(builder);

            builder.ToTable("Patient");

            builder.Property(p => p.ApplicationUserId).IsRequired();
            builder.Property(p => p.DateOfBirth).IsRequired().HasColumnType("DATE");
            builder.Property(p => p.Gender).IsRequired().HasColumnType("TINYINT");
            builder.Property(p => p.BloodType).HasColumnType("TINYINT");
            builder.Property(p => p.EmergencyContactName).HasMaxLength(15);
            builder.Property(p => p.EmergencyContactPhone).HasMaxLength(11);

            builder.HasIndex(p => p.ApplicationUserId).IsUnique().HasDatabaseName("UQ_Patient_ApplicationUserId");

            builder.ToTable(t => t.HasCheckConstraint("CK_Patient_DateOfBirth", "DateOfBirth <= CURRENT_DATE"));
            

        }
    }
}
