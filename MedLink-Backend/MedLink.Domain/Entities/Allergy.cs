using MedLink.Domain.Enums;

namespace MedLink.Domain.Entities;

public class Allergy : BaseEntity
{
    public int MedicalRecordId { get; set; }
    public string AllergenName { get; set; }
    public string Reaction { get; set; }
    public Severity Severity { get; set; }
    public string? Notes { get; set; }

    public MedicalRecord MedicalRecord { get; set; }
}
