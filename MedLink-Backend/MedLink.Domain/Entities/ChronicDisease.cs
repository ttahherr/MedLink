using System;

namespace MedLink.Domain.Entities;

public class ChronicDisease : BaseEntity
{
    public int MedicalRecordId { get; set; }
    public string Name { get; set; }
    public string? Notes { get; set; }

    public MedicalRecord MedicalRecord { get; set; }
}
