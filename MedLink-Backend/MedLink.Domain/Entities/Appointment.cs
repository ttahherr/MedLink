using MedLink.Domain.Enums;

namespace MedLink.Domain.Entities;

public class Appointment : BaseEntity
{
    public int PatientId { get; set; }
    public int BranchDoctorId { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public Enums.AppointmentStatus Status { get; set; }
    public string? Notes { get; set; }

    public Patient Patient { get; set; }
    public BranchDoctor BranchDoctor { get; set; }
    public Visit Visit { get; set; }
}
