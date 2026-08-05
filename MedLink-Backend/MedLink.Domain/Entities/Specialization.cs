using System;
using System.Collections.Generic;

namespace MedLink.Domain.Entities;

public class Specialization : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public ICollection<Doctor> Doctors { get; set; }
}
