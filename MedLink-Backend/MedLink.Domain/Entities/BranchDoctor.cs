using System;
using System.Collections.Generic;

namespace MedLink.Domain.Entities;

public class BranchDoctor : BaseEntity
{
    public int BranchId { get; set; }
    public int DoctorId { get; set; }

    public Branch Branch { get; set; }
    public Doctor Doctor { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
}
