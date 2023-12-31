using System;
using System.Collections.Generic;

namespace ClinicManagementWeb.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }
    public bool IsPreserved {  get; set; }
    public string? Note { get; set; }

    public DateOnly? AppointmentDate { get; set; }

    public TimeOnly? AppointmentTime { get; set; }

    public string? TotalPrice { get; set; }

    public string? PaidAmount { get; set; }

    public string? RemainingAmount { get; set; }

    public string? PatientId { get; set; }

    public int? DoctorId { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }
}
