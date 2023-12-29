using System;
using System.Collections.Generic;

namespace ClinicManagementWeb.Models;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string DoctorName { get; set; } = null!;

    public string DoctorSurname { get; set; } = null!;

    public string? Specialization { get; set; }

    public string? ContactNumber { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
