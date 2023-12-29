using System;
using System.Collections.Generic;

namespace ClinicManagementWeb.Models;

public partial class Patient
{
    public string PhoneNumber { get; set; } = null!;

    public int Id { get; set; }

    public string PatientName { get; set; } = null!;

    public string PatientSurname { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
