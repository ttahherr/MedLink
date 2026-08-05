using System;
using System.Collections.Generic;

namespace MedLink.Domain.Entities;

public class Branch : BaseEntity
{
    public int ClinicId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Address Address { get; set; }

    public Clinic Clinic { get; set; }
    public ICollection<BranchWorkingHour> BranchWorkingHours { get; set; }
    public ICollection<BranchDoctor> BranchDoctors { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
}

public class Address
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
}