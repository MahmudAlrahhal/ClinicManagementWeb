using ClinicManagementWeb.Models;
using Microsoft.AspNetCore.Mvc;
using DataAccess;
using DataAccess.Data;

namespace ClinicManagementWeb.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly MustafaClinicDbContext _db;
        public AppointmentsController(MustafaClinicDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Appointment> objAppointments = _db.Appointments
            .Where(x => x.AppointmentDate == DateOnly.FromDateTime(DateTime.Now))
            .ToList();
            return View(objAppointments);
        }
        [HttpPost]
        public IActionResult Index1(DateTime date)
        {
            List<Appointment> objAppointments = _db.Appointments
            .Where(x => x.AppointmentDate == DateOnly.FromDateTime(date))
            .ToList();
            return View("Index",objAppointments);
        }
        [HttpPost]
        public IActionResult Select(Appointment appointment)
        {
            return View(appointment);
        }
        [HttpPost]
        public IActionResult Preserve(Appointment aptmt)
        {
            int procedure = _db.ExecuteAppointmentUpdateStoredProcedure(aptmt.AppointmentId, aptmt.AppointmentDate,
                aptmt.AppointmentTime, aptmt.TotalPrice, aptmt.PaidAmount, aptmt.RemainingAmount,
                aptmt.PatientId, aptmt.DoctorId, aptmt.Note, true);

            List<Appointment> objAppointments = _db.Appointments
            .Where(x => x.AppointmentDate == DateOnly.FromDateTime(DateTime.Now))
            .ToList();
            return View("Index", objAppointments);
        }
    }
}
