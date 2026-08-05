using System;

namespace MedLink.Domain.Entities;

public class PrescriptionItem : BaseEntity
{
    public int PrescriptionId { get; set; }
    public string MedicationName { get; set; }
    public string Dosage { get; set; }
    public string Frequency { get; set; }
    public string Duration { get; set; }
    public string? Instructions { get; set; }

    public Prescription Prescription { get; set; }
}
