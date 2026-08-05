using MedLink.Domain.Entities;
using MedLink.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedLink.Infrastructure.Persistence.Configurations
{
    public class AllergyConfiguration : BaseEntityConfiguration<Allergy>
    {
        public override void Configure(EntityTypeBuilder<Allergy> builder)
        {
            base.Configure(builder);

            builder.ToTable("Allergy");

            builder.Property(a => a.AllergenName).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Reaction).IsRequired().HasMaxLength(500);
            builder.Property(a => a.Severity).IsRequired().HasColumnType("TINYINT");
            builder.Property(a => a.Notes).HasColumnType("NVARCHAR(MAX)");

            builder.HasOne(a => a.MedicalRecord)
                .WithMany(m => m.Allergies)
                .HasForeignKey(a => a.MedicalRecordId)
                .OnDelete(DeleteBehavior.Cascade);  
            
            builder.ToTable(t => t.HasCheckConstraint("CK_Allergy_Severity", "Severity BETWEEN 0 AND 3"));
            

        }
    }
}
