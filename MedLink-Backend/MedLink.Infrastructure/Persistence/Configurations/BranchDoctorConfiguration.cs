using MedLink.Domain.Entities;
using MedLink.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedLink.Infrastructure.Persistence.Configurations
{
    public class BranchDoctorConfiguration : BaseEntityConfiguration<BranchDoctor>
    {
        public override void Configure(EntityTypeBuilder<BranchDoctor> builder)
        {
            base.Configure(builder);

            builder.ToTable("BranchDoctor");

            builder.HasIndex(bd => new { bd.BranchId, bd.DoctorId }).IsUnique().HasDatabaseName("UQ_BranchDoctor_Branch_Doctor");

            builder.HasOne(bd => bd.Branch)
                .WithMany(b => b.BranchDoctors)
                .HasForeignKey(bd => bd.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bd => bd.Doctor)
                .WithMany(d => d.BranchDoctors)
                .HasForeignKey(bd => bd.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            

        }
    }
}
