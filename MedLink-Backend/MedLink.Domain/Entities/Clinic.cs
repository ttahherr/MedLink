using System;
using System.Collections.Generic;

namespace MedLink.Domain.Entities;

public class Clinic : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Address Address { get; set; }

    public ICollection<Branch> Branches { get; set; }
    public ICollection<Doctor> Doctors { get; set; }
}

public class Adress
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
}