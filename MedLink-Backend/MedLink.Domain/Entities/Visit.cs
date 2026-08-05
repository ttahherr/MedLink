using System;

namespace MedLink.Domain.Entities;

public class Visit : BaseEntity
{
    public int AppointmentId { get; set; }
    public string? Diagnosis { get; set; }
    public string? ClinicalNotes { get; set; }

    public Appointment Appointment { get; set; }
    public Prescription Prescription { get; set; }
}
