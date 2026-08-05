using MedLink.Domain.Entities;
using MedLink.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedLink.Infrastructure.Persistence.Configurations
{
    public class BranchConfiguration : BaseEntityConfiguration<Branch>
    {
        public override void Configure(EntityTypeBuilder<Branch> builder)
        {
            base.Configure(builder);

            builder.ToTable("Branch");

            builder.Property(b => b.Name).IsRequired().HasMaxLength(200);
            builder.Property(b => b.Email).IsRequired().HasMaxLength(254);
            builder.Property(b => b.PhoneNumber).IsRequired().HasMaxLength(11);

            builder.OwnsOne(b => b.Address, a =>
            {
                a.Property(p => p.Country).HasColumnName("Country").IsRequired().HasMaxLength(30);
                a.Property(p => p.City).HasColumnName("City").IsRequired().HasMaxLength(30);
                a.Property(p => p.Street).HasColumnName("Street").IsRequired().HasMaxLength(30);
            });

            

            builder.HasIndex(b => b.Email).IsUnique().HasDatabaseName("UQ_Branch_Email");
            builder.HasIndex(b => new { b.ClinicId, b.Name }).IsUnique().HasDatabaseName("UQ_Branch_Clinic_Name");

            builder.HasOne(b => b.Clinic)
                .WithMany(c => c.Branches)
                .HasForeignKey(b => b.ClinicId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
