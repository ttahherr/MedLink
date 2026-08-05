using MedLink.Domain.Enums;
namespace MedLink.Domain.Entities;


public class Patient : BaseEntity
{
    public int ApplicationUserId { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public BloodType? BloodType { get; set; }
    public string? EmergencyContactName { get; set; }
    public string? EmergencyContactPhone { get; set; }
    public MedicalRecord MedicalRecord { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
}
