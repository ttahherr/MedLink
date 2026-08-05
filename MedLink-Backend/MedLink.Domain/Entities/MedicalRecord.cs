namespace MedLink.Domain.Entities;

public class MedicalRecord : BaseEntity
{
    public int PatientId { get; set; }

    public Patient Patient { get; set; }
    public ICollection<Allergy> Allergies { get; set; }
    public ICollection<ChronicDisease> ChronicDiseases { get; set; }
    public ICollection<Visit> Visits { get; set; }
}
