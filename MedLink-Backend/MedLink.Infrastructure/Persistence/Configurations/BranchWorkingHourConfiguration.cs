using MedLink.Domain.Entities;
using MedLink.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedLink.Infrastructure.Persistence.Configurations
{
    public class BranchWorkingHourConfiguration : BaseEntityConfiguration<BranchWorkingHour>
    {
        public override void Configure(EntityTypeBuilder<BranchWorkingHour> builder)
        {
            base.Configure(builder);

            builder.ToTable("BranchWorkingHour");

            builder.Property(b => b.DayOfWeek).IsRequired().HasColumnType("TINYINT");
            builder.Property(b => b.OpeningTime).IsRequired().HasColumnType("TIME(0)");
            builder.Property(b => b.ClosingTime).IsRequired().HasColumnType("TIME(0)");
            builder.Property(b => b.IsClosed).IsRequired().HasDefaultValue(false);

            builder.HasIndex(b => new { b.BranchId, b.DayOfWeek }).IsUnique().HasDatabaseName("UQ_BranchWorkingHour_Branch_Day");

            builder.HasOne(b => b.Branch)
                .WithMany(b => b.BranchWorkingHours)
                .HasForeignKey(b => b.BranchId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.ToTable(t => t.HasCheckConstraint("CK_BranchWorkingHour_DayOfWeek", "DayOfWeek BETWEEN 0 AND 6"));
            builder.ToTable(t => t.HasCheckConstraint("CK_BranchWorkingHour_TimeRange", "IsClosed = 1 OR ClosingTime > OpeningTime"));


        }
    }
}
