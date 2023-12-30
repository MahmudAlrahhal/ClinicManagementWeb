using ClinicManagementWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementWeb.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly MustafaClinic _db;
        public AppointmentsController(MustafaClinic db)
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
    }
}
