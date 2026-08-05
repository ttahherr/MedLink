using System;
using System.Collections.Generic;

namespace MedLink.Domain.Entities;

public class Prescription : BaseEntity
{
    public int VisitId { get; set; }
    public string? Notes { get; set; }

    public Visit Visit { get; set; }
    public ICollection<PrescriptionItem> PrescriptionItems { get; set; }
}
