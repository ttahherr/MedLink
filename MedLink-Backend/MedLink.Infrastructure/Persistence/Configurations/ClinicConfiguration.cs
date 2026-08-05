using MedLink.Domain.Entities;
using MedLink.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedLink.Infrastructure.Persistence.Configurations
{
    public class ClinicConfiguration : BaseEntityConfiguration<Clinic>
    {
        public override void Configure(EntityTypeBuilder<Clinic> builder)
        {
            base.Configure(builder);

            builder.ToTable("Clinic");

            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(254);
            builder.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(20);

            builder.OwnsOne(c => c.Address, a =>
            {
                a.Property(p => p.Country).HasColumnName("Country").IsRequired().HasMaxLength(100);
                a.Property(p => p.City).HasColumnName("City").IsRequired().HasMaxLength(100);
                a.Property(p => p.Street).HasColumnName("Street").IsRequired().HasMaxLength(200);
            });

            builder.HasIndex(c => c.Email).IsUnique().HasDatabaseName("UQ_Clinic_Email");
            

        }
    }
}
