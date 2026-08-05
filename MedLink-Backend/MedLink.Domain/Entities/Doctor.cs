namespace MedLink.Domain.Entities;

public class Doctor : BaseEntity
{
    public int ClinicId { get; set; }
    public int SpecializationId { get; set; }
    public int ApplicationUserId { get; set; }
    public string LicenseNumber { get; set; }
    public byte YearsOfExperience { get; set; }
    public decimal ConsultationFee { get; set; }
    public string? Biography { get; set; }

    public Clinic Clinic { get; set; }
    public Specialization Specialization { get; set; }
    public ICollection<BranchDoctor> BranchDoctors { get; set; }
}
